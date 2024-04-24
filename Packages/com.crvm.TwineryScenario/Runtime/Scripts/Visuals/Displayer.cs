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
            while (m_GameObjects.Count > 0)
            {
                GameObject tmp = m_GameObjects[^1];
                m_GameObjects.Remove(tmp);
                Destroy(tmp);
                Debug.Log(m_GameObjects.Count);
            }
            /*foreach (GameObject obj in m_GameObjects)
            {
                m_GameObjects.Remove(obj);
                Destroy(obj);
            }*/
        }
        
    }
}