using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace LittleHelper
{
    public static class UITextUtility
    {
        public static void Resize(this Text _this)
        {
            var rt = _this.GetComponent<RectTransform>();
            var setting = _this.GetGenerationSettings(rt.sizeDelta);
            TextGenerator generator = new TextGenerator();
            var w = generator.GetPreferredWidth(_this.text, setting);
            setting.generationExtents = new Vector2(w, rt.sizeDelta.y);
            var h = generator.GetPreferredHeight(_this.text, setting);
            rt.sizeDelta = new Vector2(w, h);
        }
    }
}