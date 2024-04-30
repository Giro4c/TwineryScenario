using System.Collections.Generic;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Visuals
{
    /// <summary>
    /// An abstract class that defines the basic behavior for any scenario-element related displays.
    /// </summary>
    public abstract class Displayer : MonoBehaviour
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
        protected void AddGameObject(GameObject obj)
        {
            m_GameObjects.Add(obj);
            obj.transform.SetParent(container.transform);
        }

        /// <summary>
        /// Remove and destroy all GameObjects that were present in the list of present objects.
        /// </summary>
        public void Clear()
        {
            // int i = 0;
            while (m_GameObjects.Count > 0)
            {
                // Retrieve the object to keep the reference for the different method calls
                GameObject tmp = m_GameObjects[^1];
                // remove the object from the list
                m_GameObjects.Remove(tmp);
                // Destroy the object to remove it from the scene
                Destroy(tmp);
                
                // Debug.Log(m_GameObjects.Count);
                // ++i;
                // if (i > 5) break;
            }
            
        }
        
    }
}