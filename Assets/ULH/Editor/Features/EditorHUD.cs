using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class EditorHUD
    {
        public static void OnSceneGUI()
        {
            DrawOutlines();

            var ui = SelectionExt.GetUIComponentFromSelection();
            if (ui == null) return;

            var rt = ui.GetComponent<RectTransform>();
            EditorDrawString.DrawString($"{rt.rect.width.ToString("0.0")} x {rt.rect.height.ToString("0.0")}");
        }

        private static void DrawOutlines()
        {
            var uis = SelectionExt.GetUIComponentsFromSelection();
            for (int i=0;i<uis.Length-1;i++)
            {
                var u = uis[i];
                var u2 = uis[i + 1];
                var rt = u.GetComponent<RectTransform>();
                var rt2 = u2.GetComponent<RectTransform>();

                var alignedX =
                    (u2.transform.position.x >= u.transform.position.x - rt.rect.width / 2 &&
                    u2.transform.position.x <= u.transform.position.x + rt.rect.width / 2) ||
                    (u.transform.position.x >= u2.transform.position.x - rt2.rect.width / 2 &&
                    u.transform.position.x <= u2.transform.position.x + rt2.rect.width / 2);
                var alignedY =
                    (u2.transform.position.y >= u.transform.position.y - rt.rect.height / 2 &&
                    u2.transform.position.y <= u.transform.position.y + rt.rect.height / 2) ||
                    (u.transform.position.y >= u2.transform.position.y - rt2.rect.height / 2 &&
                    u.transform.position.y <= u2.transform.position.y + rt2.rect.height / 2);

                Handles.color = Color.red;
                if (alignedX == false && alignedY)
                {
                    var dist = 0.0f;

                    if (u.transform.position.x < u2.transform.position.x)
                    {
                        dist = Mathf.Abs(
                            (rt.anchoredPosition.x + rt.rect.width / 2) - 
                            (rt2.anchoredPosition.x - rt2.rect.width / 2));

                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position + new Vector3(rt.rect.width / 2, -5),
                            u.transform.position + new Vector3(rt.rect.width / 2, 5));
                        Handles.DrawAAPolyLine(
                            5,
                            u2.transform.position - new Vector3(rt2.rect.width / 2, -5),
                            u2.transform.position - new Vector3(rt2.rect.width / 2, 5));
                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position + new Vector3(rt.rect.width / 2, 0),
                            u2.transform.position - new Vector3(rt2.rect.width / 2, 0));

                        EditorDrawString.DrawString(dist.ToString("0.0"),
                            (u.transform.position + new Vector3(rt.rect.width / 2, 0)) +
                            new Vector3(((u2.transform.position.x - rt2.rect.width/2) - (u.transform.position.x + rt.rect.width/2)), 0) / 2,
                            Color.red);
                    }
                    else
                    {
                        dist = Mathf.Abs(
                            (rt.anchoredPosition.x - rt.rect.width / 2) -
                            (rt2.anchoredPosition.x + rt2.rect.width / 2));

                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position - new Vector3(rt.rect.width / 2, -5),
                            u.transform.position - new Vector3(rt.rect.width / 2, 5));
                        Handles.DrawAAPolyLine(
                            5,
                            u2.transform.position + new Vector3(rt2.rect.width / 2, -5),
                            u2.transform.position + new Vector3(rt2.rect.width / 2, 5));
                        Handles.DrawLine(
                            u.transform.position - new Vector3(rt.rect.width / 2, 0),
                            u2.transform.position + new Vector3(rt2.rect.width / 2, 0));

                        EditorDrawString.DrawString(dist.ToString("0.0"),
                            (u.transform.position - new Vector3(rt.rect.width / 2, 0)) -
                            new Vector3(((u.transform.position.x - rt.rect.width / 2) - (u2.transform.position.x + rt2.rect.width / 2)), 0) / 2,
                            Color.red);
                    }
                }
                if (alignedX && alignedY == false)
                {
                    var dist = 0.0f;

                    if (u.transform.position.y < u2.transform.position.y)
                    {
                        dist = Mathf.Abs(
                            (rt.anchoredPosition.y + rt.rect.height / 2) -
                            (rt2.anchoredPosition.y - rt2.rect.height / 2));

                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position + new Vector3(-5, rt.rect.height / 2),
                            u.transform.position + new Vector3(5, rt.rect.height / 2));
                        Handles.DrawAAPolyLine(
                            5,
                            u2.transform.position - new Vector3(-5, rt2.rect.height / 2),
                            u2.transform.position - new Vector3(5, rt2.rect.height / 2));
                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position + new Vector3(0, rt.rect.height / 2),
                            u2.transform.position - new Vector3(0, rt2.rect.height / 2));

                        EditorDrawString.DrawString(dist.ToString("0.0"),
                            u.transform.position + (u2.transform.position - u.transform.position) / 2,
                            Color.red);
                    }
                    else
                    {
                        dist = Mathf.Abs(
                            (rt.anchoredPosition.y - rt.rect.height / 2) -
                            (rt2.anchoredPosition.y + rt2.rect.height / 2));

                        Handles.DrawAAPolyLine(
                            5,
                            u.transform.position - new Vector3(-5, rt.rect.height / 2),
                            u.transform.position - new Vector3(5, rt.rect.height / 2));
                        Handles.DrawAAPolyLine(
                            5,
                            u2.transform.position + new Vector3(-5, rt2.rect.height / 2),
                            u2.transform.position + new Vector3(5, rt2.rect.height / 2));
                        Handles.DrawLine(
                            u.transform.position - new Vector3(0, rt.rect.height / 2),
                            u2.transform.position + new Vector3(0, rt2.rect.height / 2));
                    }

                    EditorDrawString.DrawString(dist.ToString("0.0"),
                        u.transform.position + (u2.transform.position - u.transform.position) / 2,
                        Color.red);
                }
                if (alignedX && alignedY)
                {
                    //Debug.Log("CC");
                }
            }
        }
    }
}
