using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace LittleHelper
{
    public class SelectionExt
    {
        public static GameObject GetGameObjectFromSelection()
        {
            if (Selection.objects.Length != 1) return null;
            return Selection.objects[0] as GameObject;
        }

        public static Graphic GetUIComponentFromSelection()
        {
            if (Selection.objects.Length != 1) return null;

            try
            {
                var go = Selection.objects[0] as GameObject;
                return go.GetComponent<Graphic>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Component[] GetUIComponentsFromSelection()
        {
            if (Selection.objects.Length == 0) 
                return new Component[] { };

            try
            {
                return Selection.objects
                    .OfType<GameObject>()
                    .Select(x => x.GetComponent<Graphic>())
                    .Where(x => x != null)
                    .ToArray();
            }
            catch (Exception e)
            {
                return new Component[] { };
            }
        }
    }
}