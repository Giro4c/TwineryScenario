using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TwineryScenario.Runtime.Scripts.Visuals
{
    /// <summary>
    /// An class that defines the basic behavior for any GameObject spawning related displays.
    /// </summary>
    public class Displayer : MonoBehaviour
    {

        /// <summary>
        /// The container of the displayed elements
        /// </summary>
        [SerializeField] protected GameObject container;
        
        /// <summary>
        /// The GameObject prefab that defines the base structure and visual of the display
        /// </summary>
        [SerializeReference] protected GameObject prefab;
        
        /// <summary>
        /// The list of GameObjects contained in the container that were added using this script
        /// </summary>
        protected readonly List<GameObject> m_GameObjects = new List<GameObject>();

        
        /// <summary>
        /// Adds a GameObject in the container and also adds it to the list to keep track of the visual elements.
        /// </summary>
        /// <param name="obj">The GameObject to be added in the container and the list.</param>
        public void AddGameObject(GameObject obj)
        {
            m_GameObjects.Add(obj);
            obj.transform.SetParent(container.transform);
        }

        /// <summary>
        /// Remove and destroy all GameObjects that were present in the list of present objects.
        /// </summary>
        public void Clear()
        {
            while (m_GameObjects.Count > 0)
            {
                // Retrieve the object to keep the reference for the different method calls
                GameObject tmp = m_GameObjects[^1];
                // remove the object from the list
                m_GameObjects.Remove(tmp);
                // Destroy the object to remove it from the scene
                Destroy(tmp);
            }
            
        }

        public GameObject GetContainer()
        {
            return container;
        }

        public void SetContainer(GameObject container)
        {
            this.container = container;
        }

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public int GetCountGameObjects()
        {
            return m_GameObjects.Count;
        }

        public GameObject GetLatestDisplay()
        {
            if (m_GameObjects.Count == 0) return null;
            return m_GameObjects[^1];
        }

    }
}