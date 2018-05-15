using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Helpers
{
    /// <summary>
    /// Static class containing helper functions regarding application
    /// layer functionality
    /// </summary>
    public static class AppHelpers
    {
        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        /// <summary>
        /// Gets the device mac.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetDeviceMAC()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }

        /// <summary>
        /// Gets the battery level.
        /// </summary>
        /// <returns>System.Single.</returns>
        public static float GetBatteryLevel()
        {
            return SystemInfo.batteryLevel;
        }
    }
}
