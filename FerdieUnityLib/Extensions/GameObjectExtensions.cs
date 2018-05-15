using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Safe way of getting a GameObjects Component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>T.</returns>
        public static T GetSafeComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type " +
                    typeof(T) + " but found none", obj);
            }

            return component;
        }

        /// <summary>
        /// Returns a boolean indicating whether the given
        /// gameobject has a certain Component
        /// </summary>
        /// <typeparam name="T">The component to be checked</typeparam>
        /// <param name="obj">The gameObject</param>
        /// <returns><c>true</c> if the specified object has component; otherwise, <c>false</c>.</returns>
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        /// <summary>
        /// Returns a boolean indicating whether the GameObject
        /// has a component with the name provided via componentName
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="componentName">Name of the component.</param>
        /// <returns><c>true</c> if the specified component name has component; otherwise, <c>false</c>.</returns>
        public static bool HasComponent(this GameObject obj, string componentName)
        {
            return obj.GetComponent(componentName) != null;
        }

        /// <summary>
        /// Gets the first child with provided tag.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>Transform.</returns>
        public static Transform GetChildWithTag(this Transform obj, string tag)
        {
            Transform result = null;

            if (!string.IsNullOrEmpty(tag))
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    if (obj.transform.GetChild(i).tag == tag)
                    {
                        result = obj.transform.GetChild(i);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Destroys all children of the specified gameobject
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void DestroyAllChildren(this GameObject obj)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in obj.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => GameObject.DestroyImmediate(child));
        }

        /// <summary>
        /// Gets the game object methods.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.String[]&gt;.</returns>
        public static Dictionary<string, string[]> GetGameObjectMethods(this GameObject obj)
        {
            Component[] components = obj.GetComponents<Component>();
            Dictionary<string, string[]> gameObjectMethods = new Dictionary<string, string[]>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != null)
                {
                    string[] methods = components[i].GetType()
                        .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                        .Where(x => x.DeclaringType == components[i].GetType())
                        .Select(x => x.Name)
                        .ToArray();
                    if (!gameObjectMethods.ContainsKey(components[i].GetType().ToString()))
                    {
                        gameObjectMethods.Add(components[i].GetType().ToString(), methods);
                    }
                }
            }
            return gameObjectMethods;
        }

        /// <summary>
        /// Gets the game object methods.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="ignoreMethods">The ignore methods.</param>
        /// <returns>Dictionary&lt;System.String, System.String[]&gt;.</returns>
        public static Dictionary<string, string[]> GetGameObjectMethods(this GameObject obj, string[] ignoreMethods)
        {
            Component[] components = obj.GetComponents<Component>();
            Dictionary<string, string[]> gameObjectMethods = new Dictionary<string, string[]>();

            for (int i = 0; i < components.Length; i++)
            {
                string[] methods = components[i].GetType()
                .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(x => x.DeclaringType == components[i].GetType())
                .Where(x => !ignoreMethods.Any(n => n == x.Name))
                .Select(x => x.Name)
                .ToArray();
                gameObjectMethods.Add(components[i].name, methods);
            }
            return gameObjectMethods;
        }

        /// <summary>
        /// Gets the property from component.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string GetPropertyFromComponent(this GameObject obj, string path)
        {
            string result = string.Empty;
            string componentName = path.Substring(0, path.IndexOf("/"));
            string variableName = path.Substring((path.IndexOf("/") + 1));
            if (!string.IsNullOrEmpty(componentName) && !string.IsNullOrEmpty(variableName))
            {
                Component component = obj.GetComponent(componentName);
                if (component != null)
                {
                    System.Type componentType = component.GetType();
                    PropertyInfo propertyInfo = componentType.GetProperty(variableName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (propertyInfo != null)
                    {
                        MethodInfo propertyGetMethod = propertyInfo.GetGetMethod();
                        result = propertyGetMethod.Invoke(component, null).ToString();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the component methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>System.String[].</returns>
        private static string[] GetComponentMethods<T>()
        {
            return typeof(T)
                .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(x => x.DeclaringType == typeof(T))
                .Where(x => x.GetParameters().Length == 0)
                .Select(x => x.Name)
                .ToArray();
        }

        /// <summary>
        /// Gets the component methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ignoreMethods">The ignore methods.</param>
        /// <returns>System.String[].</returns>
        private static string[] GetComponentMethods<T>(string[] ignoreMethods)
        {
            return typeof(T)
                .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(x => x.DeclaringType == typeof(T))
                .Where(x => x.GetParameters().Length == 0)
                .Where(x => !ignoreMethods.Any(n => n == x.Name))
                .Select(x => x.Name)
                .ToArray();
        }

        /// <summary>
        /// Invokes the in component.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="componentName">Name of the component.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        public static void InvokeInComponent(this GameObject obj, string componentName, string methodName, object[] parameters)
        {
            if (!string.IsNullOrEmpty(componentName) && !string.IsNullOrEmpty(methodName) && obj.HasComponent(componentName))
            {
                Component objectComponent = obj.GetComponent(componentName);
                if (objectComponent != null)
                {
                    MethodInfo methodInfo = objectComponent.GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                            .Where(x => x.DeclaringType == objectComponent.GetType())
                            .Where(x => x.Name == methodName)
                            .FirstOrDefault();
                    if (methodInfo != null && obj != null && objectComponent != null)
                    {
                        methodInfo.Invoke(objectComponent, parameters);
                    }
                }
            }
        }
    }
}
