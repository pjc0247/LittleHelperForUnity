using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace LittleHelper
{
    public class LittleHelperConfig : ScriptableObject
    {
        public static LittleHelperConfig activeConfig;

        public FeatureSet featureSet;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var cfg = AssetDatabase.LoadAssetAtPath<LittleHelperConfig>("Assets/LittleHelperConfig.asset");

            if (cfg == null)
            {
                cfg = CreateInstance<LittleHelperConfig>();
                AssetDatabase.CreateAsset(cfg, "Assets/LittleHelperConfig.asset");
            }

            activeConfig = cfg;
        }
#endif
    }
}