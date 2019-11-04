using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LittleHelper
{
    public class PasteHexColor
    {
        private static bool IsHexColor(string str)
        {
            if (str.Length == 6 || str.Length == 8)
                return false;

            // [TODO] A~F check
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];

                if (char.IsLetterOrDigit(c))
                    ; // PASS
                else
                    return false;
            }

            return true;
        }

        public static void OnKeyDown(Event e, bool isCtrlPressed)
        {
            if (isCtrlPressed && e.keyCode == KeyCode.V)
            {
                var buffer = GUIUtility.systemCopyBuffer;
                if (buffer.StartsWith("#") == false)
                    buffer = "#" + buffer;

                var comp = SelectionExt.GetUIComponentFromSelection();
                Color color;
                if (ColorUtility.TryParseHtmlString(buffer, out color))
                    comp.color = color;
            }
        }
    }
}
