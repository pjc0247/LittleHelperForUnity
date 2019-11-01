using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LittleHelper
{
    public class LastPropertyData
    {
        public TextAnchor textAlign = TextAnchor.UpperLeft;
        public int fontAssetId;
        public int fontSize;
        public float lineSpacing;

        public ShadowData[] shadows;
        public OutlineData[] outlines;
    }

    public class ShadowData
    {
        public Color color;
        public Vector2 position;
    }
    public class OutlineData
    {
        public Color color;
        public Vector2 position;
    }
}