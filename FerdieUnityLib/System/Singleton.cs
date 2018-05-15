using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.FerdieSystem
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but isn't there");
                    }
                }

                return instance;
            }
        }
    }
}
