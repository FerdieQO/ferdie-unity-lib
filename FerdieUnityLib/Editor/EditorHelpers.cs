using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FerdieUnityLib.Editor
{
    public static class EditorHelpers
    {
        public static void Collapse(GameObject gameObject, bool collapse)
        {
            if (gameObject.transform.childCount == 0) return;

            EditorWindow hierarchy = GetFocusedWindow("Hierarchy");
            if (hierarchy != null)
            {
                SelectObjectInHierarchy(gameObject);
                Event keyEvent = new Event { keyCode = collapse ? KeyCode.RightArrow : KeyCode.LeftArrow, type = EventType.keyDown };
                hierarchy.SendEvent(keyEvent);
            }
        }

        public static void SelectObjectInHierarchy(UnityEngine.Object obj)
        {
            Selection.activeObject = obj;
        }

        public static void DeselectObjectInHierarchy()
        {
            Selection.activeObject = null;
        }

        public static void FocusOnWindow(string window)
        {
            EditorApplication.ExecuteMenuItem("Window/" + window);
        }

        public static EditorWindow GetFocusedWindow(string window)
        {
            FocusOnWindow(window);
            return EditorWindow.focusedWindow;
        }
    }
}
