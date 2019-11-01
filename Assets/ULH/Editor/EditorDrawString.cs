using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class EditorDrawString
    {
        public static void DrawString(string text, Color? color = null, bool showTexture = true, int offsetY = 0)
        {
            Handles.BeginGUI();

            var restoreColor = GUI.color;
            var viewSize = SceneView.currentDrawingSceneView.position;

            if (color.HasValue) GUI.color = color.Value;
            var view = SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(
                view.camera.transform.position + view.camera.transform.forward);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                Handles.EndGUI();
                return;
            }
            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 50;
            Vector2 size = style.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(40, viewSize.height - 100 - offsetY, size.x, size.y), text, style);
            GUI.color = restoreColor;

            /*
            if (showTexture)
            {
                if (cssTex == null)
                    OnEnable();
                GUI.DrawTexture(
                    new Rect(40, viewSize.height - 220, 100, 100), cssTex,
                    ScaleMode.StretchToFill, true, 1.0f, new Color(1, 1, 1, alpha), 0, 0);
            }
            */

            Handles.EndGUI();
        }
        public static void DrawString(string text, Vector3 position, Color? color = null, bool showTexture = true, int offsetY = 0)
        {
            Handles.BeginGUI();

            var restoreColor = GUI.color;
            var viewSize = SceneView.currentDrawingSceneView.position;

            if (color.HasValue) GUI.color = color.Value;
            var view = SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(
                position + view.camera.transform.forward);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                Handles.EndGUI();
                return;
            }
            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 30;
            Vector2 size = style.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - size.x/2, viewSize.height - screenPos.y - offsetY - size.y/2, size.x, size.y), text, style);
            GUI.color = restoreColor;

            /*
            if (showTexture)
            {
                if (cssTex == null)
                    OnEnable();
                GUI.DrawTexture(
                    new Rect(40, viewSize.height - 220, 100, 100), cssTex,
                    ScaleMode.StretchToFill, true, 1.0f, new Color(1, 1, 1, alpha), 0, 0);
            }
            */

            Handles.EndGUI();
        }

        public static void DrawRect(Rect rect, Color? color = null, bool showTexture = true, int offsetY = 0)
        {
            Handles.BeginGUI();

            var restoreColor = GUI.color;
            var viewSize = SceneView.currentDrawingSceneView.position;

            if (color.HasValue) GUI.color = color.Value;
            var view = SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(
                new Vector3(rect.position.x, rect.position.y) + view.camera.transform.forward);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                Handles.EndGUI();
                return;
            }

            EditorGUI.DrawRect(rect, color.Value);

            GUI.color = restoreColor;

            /*
            if (showTexture)
            {
                if (cssTex == null)
                    OnEnable();
                GUI.DrawTexture(
                    new Rect(40, viewSize.height - 220, 100, 100), cssTex,
                    ScaleMode.StretchToFill, true, 1.0f, new Color(1, 1, 1, alpha), 0, 0);
            }
            */

            Handles.EndGUI();
        }

        public static void DrawLine(Vector3 a, Vector3 b, Color? color = null, bool showTexture = true, int offsetY = 0)
        {
            Handles.BeginGUI();

            var restoreColor = GUI.color;
            var viewSize = SceneView.currentDrawingSceneView.position;

            if (color.HasValue) GUI.color = color.Value;
            var view = SceneView.currentDrawingSceneView;
            Vector3 screenPos1 = view.camera.WorldToScreenPoint(
                a + view.camera.transform.forward);
            Vector3 screenPos2 = view.camera.WorldToScreenPoint(
                b + view.camera.transform.forward);

            //Debug.DrawLine()

            GUI.color = restoreColor;

            /*
            if (showTexture)
            {
                if (cssTex == null)
                    OnEnable();
                GUI.DrawTexture(
                    new Rect(40, viewSize.height - 220, 100, 100), cssTex,
                    ScaleMode.StretchToFill, true, 1.0f, new Color(1, 1, 1, alpha), 0, 0);
            }
            */

            Handles.EndGUI();
        }
    }
}
