using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LittleHelper
{
    [Serializable]
    public struct FeatureSet
    {
        [Header("Inspector")]
        public bool KeepSelectionOnInspector;
        public bool RestoreInspectorOnDrag;

        [Header("UGUI")]
        public bool UI_HUD;

        public bool UI_CopyTextData;
        public bool UI_CopyTextOutlines;
        public bool UI_CopyTextShadows;

        public bool UI_AutoFitSizeText;
        public bool UI_DisableRaycastForText;
        public bool UI_BatchModifyForText;

        public bool UI_KeepZPosition0;
        public bool UI_MoveByArrows;

        public bool UI_AutoFitSizeImage;

        [Header("Components")]
        public bool RemoveAudioListener;
    }
}