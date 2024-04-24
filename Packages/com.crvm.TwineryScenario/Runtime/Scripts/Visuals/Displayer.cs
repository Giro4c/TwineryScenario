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
            foreach (GameObject obj in m_GameObjects)
            {
                m_GameObjects.Remove(obj);
                Destroy(obj);
            }
        }
        
    }
}