using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace LittleHelper
{
    public class MoveComponent
    {
        [MenuItem("CONTEXT/Object/Move to Top", priority = 5000)]
        public static void MoveToTop(MenuCommand command)
        {
            while (true)
            {
                if (ComponentUtility.MoveComponentUp(command.context as Component) == false)
                    break;
            }
        }
        [MenuItem("CONTEXT/Object/Move to Bottom", priority = 5000)]
        public static void MoveToBottom(MenuCommand command)
        {
            while (true)
            {
                if (ComponentUtility.MoveComponentDown(command.context as Component) == false)
                    break;
            }
        }
    }
}
