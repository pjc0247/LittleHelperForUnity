using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class ObjectHotkeySetup : MonoBehaviour
    {
        public static ObjectHotkeySetup setup;

        public GameObject key1;
        public GameObject key2;
        public GameObject key3;
        public GameObject key4;
        public GameObject key5;
        public GameObject key6;
        public GameObject key7;
        public GameObject key8;
        public GameObject key9;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            setup = GameObject.FindObjectOfType<ObjectHotkeySetup>();
            if (setup == null)
            {
                var go = new GameObject("HOTKEY__littlehelper");
                setup = go.AddComponent<ObjectHotkeySetup>();
            }
        } 
#endif
    }
}