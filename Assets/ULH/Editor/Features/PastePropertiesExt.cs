using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class PastePropertiesExt
    {
        private static UnityEngine.Object srcObject;

        [MenuItem("CONTEXT/Object/Copy Properties", priority = 5000)]
        public static void CopyProperties(MenuCommand command)
        {
            srcObject = command.context;
        }
        [MenuItem("CONTEXT/Object/Paste Properties", priority = 5000)]
        public static void PasteProperties(MenuCommand command)
        {
            if (srcObject == null) return;
            if (command.context == srcObject) return;

            var sobj = new SerializedObject(srcObject);
            var dstObj = new SerializedObject(command.context);

            var it = sobj.GetIterator();
            while (it.Next(true))
            {
                dstObj.CopyFromSerializedProperty(it);
            }
            dstObj.ApplyModifiedProperties();
        }
    }
}