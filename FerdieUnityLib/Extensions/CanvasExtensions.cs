using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Extensions
{
    public static class CanvasExtensions
    {
        /// <summary>
        /// Converts a Vector3 worldPosition to a Vector2 CanvasPosition
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="worldPosition">The world position.</param>
        /// <param name="camera">The camera.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 WorldToCanvas(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }

            Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            return new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
        }
    }
}
