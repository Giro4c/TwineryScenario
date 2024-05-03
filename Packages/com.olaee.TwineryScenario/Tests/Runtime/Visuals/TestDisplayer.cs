using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Visuals;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Visuals
{
    public class TestDisplayerTest
    {
        [Test]
        public void TestAddGameObject()
        {
            // Initialize a GameObject to add the Displayer component and test it
            GameObject obj = new GameObject();
            
            // Create the displayer and initializes it
            Displayer displayer = obj.AddComponent<Displayer>();
            GameObject container = new GameObject();
            displayer.SetContainer(container);
            GameObject prefab = new GameObject();
            displayer.SetPrefab(prefab);
            
            // Call method: AddGameObject(GameObject obj)
            GameObject newObject = Object.Instantiate(prefab);
            displayer.AddGameObject(newObject);
            
            // Assert that the object was added to the container and the private list of childs
            Assert.NotNull(newObject.transform.parent);
            Assert.IsTrue(newObject.transform.parent == container.transform);
            Assert.IsTrue(displayer.GetCountGameObjects() == 1);
            Assert.IsTrue(newObject == displayer.GetLatestDisplay());

        }

        // Must be in play mode because Clear uses Destroy and not DestroyImmediate
        [UnityTest]
        public IEnumerator TestClear()
        {
            // Initialize a GameObject to add the Displayer component and test it
            GameObject obj = new GameObject();
            
            // Create the displayer and initializes it
            Displayer displayer = obj.AddComponent<Displayer>();
            GameObject container = new GameObject();
            displayer.SetContainer(container);
            GameObject prefab = new GameObject();
            displayer.SetPrefab(prefab);
            
            // Add an object to the displayer's container that will be cleared later
            GameObject newObject = Object.Instantiate(prefab);
            displayer.AddGameObject(newObject);
            
            // Call method: Clear()
            displayer.Clear();
            // Skip a frame to wait for update and destruction of the objects
            yield return null;
            
            // Assert that list is emptied and object destroyed
            Assert.IsTrue(displayer.GetCountGameObjects() == 0);
            Assert.Null(displayer.GetLatestDisplay());
            Assert.Null(newObject);
            
        }
        
    }
}