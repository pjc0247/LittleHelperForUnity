using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class ObjectHotkey
    {
        public static void OnKeyDown(Event e, bool isCtrlPressed)
        {
            if (isCtrlPressed == false) return;

            var setup = ObjectHotkeySetup.setup;
            if (setup == null) return;

            if (e.keyCode == KeyCode.Alpha1 && setup.key1 != null)
                Selection.objects = new Object[] { setup.key1 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key2 != null)
                Selection.objects = new Object[] { setup.key2 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key3 != null)
                Selection.objects = new Object[] { setup.key3 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key4 != null)
                Selection.objects = new Object[] { setup.key4 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key5 != null)
                Selection.objects = new Object[] { setup.key5 };

            // ctrl + 6 and over is reserved by unity
            /*
            if (e.keyCode == KeyCode.Alpha1 && setup.key7 != null)
                Selection.objects = new Object[] { setup.key7 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key8 != null)
                Selection.objects = new Object[] { setup.key8 };
            if (e.keyCode == KeyCode.Alpha1 && setup.key9 != null)
                Selection.objects = new Object[] { setup.key9 };
                */
        }
    }
}