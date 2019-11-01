using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LittleHelper
{
    [Serializable]
    public class LastPropertyData
    {
        public TextAnchor textAlign = TextAnchor.UpperLeft;
        public int fontAssetId;
        public int fontSize = 14;
        public float lineSpacing = 1.0f;

        public Color color = Color.black;
        public HorizontalWrapMode horizontal = HorizontalWrapMode.Wrap;
        public VerticalWrapMode vertical = VerticalWrapMode.Truncate;

        public ShadowData[] shadows;
        public OutlineData[] outlines;
    }

    [Serializable]
    public class ShadowData
    {
        public Color color;
        public Vector2 position;
    }
    [Serializable]
    public class OutlineData
    {
        public Color color;
        public Vector2 position;
    }
}