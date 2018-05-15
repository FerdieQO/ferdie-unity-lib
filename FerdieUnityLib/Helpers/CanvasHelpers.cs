using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Helpers
{
    public static class CanvasHelpers
    {
        /// <summary>
        /// Converts a Vector3 WorldPosition to its Vector2 Canvas position.
        /// </summary>
        /// <param name="worldPosition">The world position.</param>
        /// <param name="camera">The camera.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 WorldToCanvas(Canvas canvas, Vector3 worldPosition, UnityEngine.Camera camera = null)
        {
            Vector2 result = Vector2.zero;

            if (canvas != null)
            {
                if (camera == null)
                {
                    camera = UnityEngine.Camera.main;
                }

                Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                result = new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                    (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

            }

            return result;
        }
    }
}
