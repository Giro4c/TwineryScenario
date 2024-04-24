using System.Collections.Generic;
using UnityEngine;

namespace Visuals
{
    public abstract class Displayer : MonoBehaviour
    {

        public GameObject container;
        public GameObject prefab;
        private List<GameObject> m_GameObjects = new List<GameObject>();

        public void AddGameObject(GameObject obj)
        {
            m_GameObjects.Add(obj);
            obj.transform.SetParent(container.transform);
        }

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