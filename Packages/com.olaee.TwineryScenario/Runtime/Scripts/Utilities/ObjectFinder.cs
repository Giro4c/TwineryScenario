using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwineryScenario.Runtime.Scripts.Utilities
{
    /// <summary>
    /// A class to find objects in a scene
    /// </summary>
    public class ObjectFinder
    {

        /// <summary>
        /// Find all objects of type T in a scene. Can be used to find objects implementing interfaces.
        /// </summary>
        /// <param name="scene">The scene containing the objects</param>
        /// <typeparam name="T">The type of the searched objects</typeparam>
        /// <returns>An array of objects of type T</returns>
        public static T[] FindObjectsInScene<T>(Scene scene)
        {
            // Initialize the array as empty
            T[] objects = Array.Empty<T>();
            
            // Get all root gameObjects of the scene
            List<GameObject> rootObjectsInScene = new List<GameObject>();
            scene.GetRootGameObjects(rootObjectsInScene);
            
            // Look for all classes that implements the interface T in the scene
            foreach (GameObject o in rootObjectsInScene)
            {
                T[] added = o.GetComponentsInChildren<T>();
                objects = objects.Concat(added).ToArray();
            }

            return objects;
        }

        /// <summary>
        /// Find the first object of type T in a scene. Can be used to find an object implementing an interface.
        /// </summary>
        /// <param name="scene">The scene containing the object</param>
        /// <typeparam name="T">The type of the searched object</typeparam>
        /// <returns>An object of type T, if it's not found then returns the default value for the type T</returns>
        public static T FindObjectInScene<T>(Scene scene)
        {
            // Initialize the array as empty
            T obj = default(T);
            
            // Get all root gameObjects of the scene
            List<GameObject> rootObjectsInScene = new List<GameObject>();
            scene.GetRootGameObjects(rootObjectsInScene);
            
            // Look for all classes that implements the interface T in the scene
            foreach (GameObject o in rootObjectsInScene)
            {
                obj = o.GetComponentInChildren<T>();
                if (obj != null) return obj;
            }

            return obj;
        }

    }
}