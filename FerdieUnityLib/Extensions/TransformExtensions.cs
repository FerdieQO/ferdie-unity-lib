using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Gets the first or default child with the provided name recursively
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="name">The name.</param>
        /// <returns>Transform.</returns>
        public static Transform FindChildRecursive(this Transform transform, string name)
        {
            Transform result = null;
            if (!string.IsNullOrEmpty(name))
            {
                result = transform.GetComponentsInChildren<Transform>().Where(x => x.name == name).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Gets the children with the provided name recursively.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="name">The name.</param>
        /// <returns>Transform[].</returns>
        public static Transform[] FindChildrenRecursive(this Transform transform, string name)
        {
            Transform[] results = null;
            if (!string.IsNullOrEmpty(name))
            {
                List<Transform> resultsList = transform.GetComponentsInChildren<Transform>().ToList();
                List<Transform> bla = new List<Transform>();
                if (resultsList != null)
                {
                    foreach (Transform t in resultsList)
                    {
                        if (t.name == name)
                        {
                            bla.Add(t);
                        }
                    }
                }

                results = bla.ToArray();
            }
            return results;
        }

        /// <summary>
        /// Gets the component in the first of default child with the provided name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transform">The transform.</param>
        /// <param name="childName">Name of the child.</param>
        /// <returns>T.</returns>
        public static T GetComponentInFindChild<T>(this Transform transform, string childName) where T : Component
        {
            T result = null;
            Transform child = FindChildRecursive(transform, childName);
            if (child != null)
            {
                result = child.gameObject.GetSafeComponent<T>();
            }
            return result;
        }

        /// <summary>
        /// Gets the components in the children with the provided name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transform">The transform.</param>
        /// <param name="childName">Name of the child.</param>
        /// <returns>T[].</returns>
        public static T[] GetComponentsInFindChildren<T>(this Transform transform, string childName) where T : Component
        {
            T[] results = null;
            Transform[] children = FindChildrenRecursive(transform, childName);
            if (children != null && children.Any())
            {
                List<T> resultList = new List<T>();
                foreach (Transform child in children)
                {
                    if (child.gameObject.HasComponent<T>())
                        resultList.Add(child.gameObject.GetSafeComponent<T>());
                }
                results = resultList.ToArray();
            }
            return results;
        }

        /// <summary>
        /// Destroys all children of the Transform.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public static void DestroyChildren(this Transform transform)
        {
            if (transform.childCount > 0)
            {
                Transform[] children = transform.GetComponentsInChildren<Transform>();

            }
        }

        /// <summary>
        /// Destroys all children in the specified transform
        /// </summary>
        /// <param name="transform">The transform.</param>
        public static void DestroyAllChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }
            children.ForEach(child => GameObject.Destroy(child.gameObject));
        }

        /// <summary>
        /// Determines whether the specified transform has children.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns><c>true</c> if the specified transform has children; otherwise, <c>false</c>.</returns>
        public static bool HasChildren(this Transform transform)
        {
            return transform.childCount > 0;
        }

        /// <summary>
        /// Moves the child down one step in hierarchy.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public static void MoveChildDownOneStepInHierarchy(this Transform transform)
        {
            if (transform.parent != null && transform.GetSiblingIndex() == (transform.parent.childCount - 1))
            {
                transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            }
        }

        /// <summary>
        /// Moves the child up one step in hierarchy.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public static void MoveChildUpOneStepInHierarchy(this Transform transform)
        {
            if (transform.GetSiblingIndex() > 0)
            {
                transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
            }
        }
    }
}
