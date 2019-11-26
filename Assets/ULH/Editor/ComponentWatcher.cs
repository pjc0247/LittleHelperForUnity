using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;

namespace LittleHelper {
    public class ComponentWatcher : MonoBehaviour
    {
        public static LastPropertyData lastPropertyData = new LastPropertyData();

        private static FeatureSet features => LittleHelperConfig.activeConfig.featureSet;

        private static int lastComponentCount;
        private static GameObject lastSelectedGO;

        private static bool shouldReleaseLockAfterDrop = false;

        private static bool isCtrlPressed = false;
        private static bool isAltPressed = false;
        private static Font initialFont;
        private static Text[] batchTexts;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var json = EditorPrefs.GetString("_little_helper", "");
            if (json != "")
                lastPropertyData = JsonUtility.FromJson<LastPropertyData>(json);

            Selection.selectionChanged += OnSelectionChanged;
            EditorApplication.update += OnUpdate;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            Undo.postprocessModifications += OnPostProcessModifications;

            SceneView.duringSceneGui += view =>
            {
                var e = Event.current;
                
                if (features.UI_HUD)
                    EditorHUD.OnSceneGUI();

                if (e == null) return;
                if (e.type != EventType.KeyDown) return;

                OnKeyDown(e);
            };

            System.Reflection.FieldInfo info = typeof(EditorApplication).GetField("globalEventHandler", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            EditorApplication.CallbackFunction value = (EditorApplication.CallbackFunction)info.GetValue(null);
            value += EditorGlobalKeyPress;
            info.SetValue(null, value);
        }
        static void EditorGlobalKeyPress()
        {
            var e = Event.current;

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftControl)
            {
                isCtrlPressed = true;
            }
            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftControl)
            {
                initialFont = null;
                batchTexts = null;

                isCtrlPressed = false;
            }

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftAlt)
                isAltPressed = true;
            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftAlt)
                isAltPressed = false;

            if (e.type == EventType.KeyDown)
            {
                PasteHexColor.OnKeyDown(e, isCtrlPressed);
                Multiselect.OnKeydown(e, isAltPressed, isCtrlPressed);
                ObjectHotkey.OnKeyDown(e, isCtrlPressed);
            }
        }

        private static void OnUpdate()
        {
            if (features.KeepSelectionOnInspector)
            {
                if (Selection.activeObject == null && lastSelectedGO != null)
                    Selection.objects = new Object[] { lastSelectedGO };
            }
            if (features.RestoreInspectorOnDrag)
            {
                if (DragAndDrop.paths.Length > 0 &&
                    !(Selection.activeObject is GameObject) && lastSelectedGO != null)
                {
                    var prevLocked = ActiveEditorTracker.sharedTracker.isLocked;

                    Selection.objects = new Object[] { lastSelectedGO };
                    ActiveEditorTracker.sharedTracker.isLocked = true;
                    ActiveEditorTracker.sharedTracker.ForceRebuild();

                    if (prevLocked == false)
                        shouldReleaseLockAfterDrop = true;
                }
                if (DragAndDrop.paths.Length == 0 &&
                    shouldReleaseLockAfterDrop)
                {
                    ActiveEditorTracker.sharedTracker.isLocked = false;
                    ActiveEditorTracker.sharedTracker.ForceRebuild();

                    shouldReleaseLockAfterDrop = false;
                }
            }
        }
        private static void OnSelectionChanged()
        {
            if (Selection.objects.Length == 0)
                return;

            var go = Selection.objects[0] as GameObject;
            if (go == null) return;

            lastComponentCount = go.GetComponents<Component>().Length;
            lastSelectedGO = go;
        }
        private static void OnHierarchyChanged()
        {
            if (Selection.objects.Length == 0)
                return;

            var go = Selection.objects[0] as GameObject;
            var comps = go.GetComponents<Component>();

            if (comps.Length == 0 ||
                (go == lastSelectedGO && comps.Length == lastComponentCount))
                return;

            var lastComp = comps.Last();
            if (lastComp is Text text)
            {
                if (features.UI_CopyTextData)
                {
                    text.alignment = lastPropertyData.textAlign;
                    if (lastPropertyData.fontSize > 0)
                        text.fontSize = lastPropertyData.fontSize;
                    if (lastPropertyData.lineSpacing > 0)
                        text.lineSpacing = lastPropertyData.lineSpacing;
                    text.horizontalOverflow = lastPropertyData.horizontal;
                    text.verticalOverflow = lastPropertyData.vertical;
                    text.color = lastPropertyData.color;

                    var lastFont = AssetDatabase.LoadAssetAtPath<Font>(AssetDatabase.GetAssetPath(lastPropertyData.fontAssetId));
                    if (lastFont != null)
                        text.font = lastFont;
                }

                if (features.UI_CopyTextOutlines)
                {
                    foreach (var outline in lastPropertyData.outlines)
                    {
                        var o = go.AddComponent<Outline>();
                        o.effectColor = outline.color;
                        o.effectDistance = outline.position;
                    }
                }
                if (features.UI_CopyTextShadows)
                {
                    foreach (var outline in lastPropertyData.shadows)
                    {
                        var o = go.AddComponent<Shadow>();
                        o.effectColor = outline.color;
                        o.effectDistance = outline.position;
                    }
                }

                if (features.UI_DisableRaycastForText)
                    text.raycastTarget = false;
            }
            else if (lastComp is AudioListener)
            {
                if (features.RemoveAudioListener)
                    DestroyImmediate(lastComp);
            }
            else if (lastComp is Button btnComp)
            {
                UnityEditor.Events.UnityEventTools.AddPersistentListener(btnComp.onClick);
            }
        }

        private static void OnKeyDown(Event e)
        {
            if (Selection.objects.Length == 0) return;
            var go = Selection.objects[0] as GameObject;
            if (go == null) return;
            var rt = go.GetComponent<RectTransform>();
            if (rt == null) return;

            if (e.keyCode == KeyCode.Space)
            {
                var collider = go.GetComponent<Collider>();
                if (collider != null)
                {
                    // RaycastAll??
                    var hits = Physics.RaycastAll(go.transform.position, go.transform.up * -1);
                    if (hits.Length > 0)
                    {
                        var yOffset = hits[0].collider.bounds.center.y + hits[0].collider.bounds.extents.y;
                        var pos = go.transform.position;
                        pos.y = yOffset + collider.bounds.extents.y;
                        go.transform.position = pos;
                    }
                }
            }
            if (features.UI_MoveByArrows)
            {
                if (e.keyCode == KeyCode.UpArrow && e.control)
                    rt.anchoredPosition += new Vector2(0, 1);
                if (e.keyCode == KeyCode.DownArrow && e.control)
                    rt.anchoredPosition += new Vector2(0, -1);
                if (e.keyCode == KeyCode.LeftArrow && e.control)
                    rt.anchoredPosition += new Vector2(-1, 0);
                if (e.keyCode == KeyCode.RightArrow && e.control)
                    rt.anchoredPosition += new Vector2(1, 0);
            }
        }

        private static UndoPropertyModification[] OnPostProcessModifications(UndoPropertyModification[] propertyModifications)
        {
            foreach (var m in propertyModifications)
            {
                if (m.currentValue.target is Text textTarget)
                {
                    if (features.UI_CopyTextData)
                    {
                        if (m.currentValue.propertyPath == "m_FontData.m_FontSize")
                            lastPropertyData.fontSize = textTarget.fontSize;
                        else if (m.currentValue.propertyPath == "m_FontData.m_Font")
                            lastPropertyData.fontAssetId = textTarget.font.GetInstanceID();
                        else if (m.currentValue.propertyPath == "m_FontData.m_LineSpacing")
                            lastPropertyData.lineSpacing = textTarget.lineSpacing;
                        else if (m.currentValue.propertyPath == "m_FontData.m_Alignment")
                            lastPropertyData.textAlign = textTarget.alignment;
                        else if (m.currentValue.propertyPath == "m_FontData.m_HorizontalOverflow")
                            lastPropertyData.horizontal = textTarget.horizontalOverflow;
                        else if (m.currentValue.propertyPath == "m_FontData.m_VerticalOverflow")
                            lastPropertyData.vertical = textTarget.verticalOverflow;
                        else if (m.currentValue.propertyPath.StartsWith("m_Color."))
                            lastPropertyData.color = textTarget.color;
                    }

                    if (features.UI_BatchModifyForText)
                    {
                        if (isCtrlPressed)
                        {
                            if (m.currentValue.propertyPath == "m_FontData.m_Font")
                            {
                                var texts = GameObject.FindObjectsOfType<Text>();

                                if (initialFont == null)
                                    initialFont = (Font)m.previousValue.objectReference;
                                if (batchTexts == null)
                                    batchTexts = texts.Where(x => x.font == initialFont).ToArray();

                                foreach (var text in batchTexts)
                                {
                                    text.font = textTarget.font;
                                    text.FontTextureChanged();

                                    if (text.resizeTextForBestFit == false)
                                        text.Resize();
                                }
                            }
                        }
                    }

                    if (features.UI_AutoFitSizeText && textTarget.resizeTextForBestFit == false)
                        textTarget.Resize();
                }
                else if (m.currentValue.target is Outline outlineTarget)
                {
                    var outlines = outlineTarget.GetComponents<Outline>();
                    lastPropertyData.outlines = outlines.Select(x => new OutlineData
                    {
                        color = x.effectColor,
                        position = x.effectDistance
                    }).ToArray();
                }
                else if (m.currentValue.target is Shadow shadowTarget)
                {
                    var shadows = shadowTarget.GetComponents<Shadow>();
                    lastPropertyData.shadows = shadows.Select(x => new ShadowData
                    {
                        color = x.effectColor,
                        position = x.effectDistance
                    }).ToArray();
                }
                else if (m.currentValue.target is RectTransform rtTarget)
                {
                    if (m.currentValue.propertyPath == "m_LocalPosition.z")
                    {
                        var pos = rtTarget.localPosition;
                        pos.z = 0;
                        rtTarget.localPosition = pos;
                    }
                }
                else if (m.currentValue.target is Image imageTarget)
                {
                    if (m.currentValue.propertyPath == "m_Sprite")
                    {
                        if (features.UI_AutoFitSizeImage)
                        {
                            imageTarget.SetNativeSize();
                        }
                    }
                }
                else if (m.currentValue.target is RawImage rawImageTarget)
                {
                    if (m.currentValue.propertyPath == "m_Texture")
                    {
                        if (features.UI_AutoFitSizeImage)
                        {
                            rawImageTarget.SetNativeSize();
                        }
                    }
                }

                Debug.Log(m.currentValue.propertyPath);
            }

            var json = JsonUtility.ToJson(lastPropertyData);
            EditorPrefs.SetString("_little_helper", json);

            return propertyModifications;
        }
    }
}
