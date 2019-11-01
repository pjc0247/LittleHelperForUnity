using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace LittleHelper
{
    public class Multiselect
    {
        public static void SelectAll<T>(Func<T, bool> pred)
            where T : Component
        {
            var objs = GameObject.FindObjectsOfType<T>();
            Selection.objects = 
                objs.Where(x => pred(x))
                .Select(x => x.gameObject)
                .ToArray();
        }

        #region UI.Text
        public static void SelectByTextFont()
        {
            Text text;
            if (TryGetComponent<Text>(out text))
                SelectAll<Text>(x => x.font == text.font);
        }
        public static void SelectByTextSize()
        {
            Text text;
            if (TryGetComponent<Text>(out text))
                SelectAll<Text>(x => x.fontSize == text.fontSize);
        }
        #endregion

        #region UI.Image
        public static void SelectByImageSprite()
        {
            Image image;
            if (TryGetComponent<Image>(out image))
                SelectAll<Image>(x => x.sprite == image.sprite);
        }
        public static void SelectByRawImageTexture()
        {
            RawImage image;
            if (TryGetComponent<RawImage>(out image))
                SelectAll<RawImage>(x => x.mainTexture == image.mainTexture);
        }
        #endregion

        private static bool TryGetComponent<T>(out T comp)
            where T : class
        {
            comp = null;
            if (Selection.objects.Length != 1)
                return false;

            try
            {
                var go = Selection.objects[0] as GameObject;
                comp = go.GetComponent<T>();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void OnKeydown(Event e, bool isAltPressed, bool isCtrlPressed)
        {
            if (isAltPressed)
            {
                var comp = SelectionExt.GetUIComponentFromSelection();

                if (comp == null) return;

                if (e.keyCode == KeyCode.Alpha1)
                {
                    if (comp is Text)
                        Multiselect.SelectByTextFont();
                    else if (comp is Image)
                        Multiselect.SelectByImageSprite();
                    else if (comp is RawImage)
                        Multiselect.SelectByRawImageTexture();
                }
                if (e.keyCode == KeyCode.Alpha2)
                {
                    if (comp is Text)
                        Multiselect.SelectByTextSize();
                }
            }
        }
    }
}
