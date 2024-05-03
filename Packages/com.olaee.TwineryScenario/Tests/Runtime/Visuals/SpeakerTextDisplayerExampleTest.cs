using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using TwineryScenario.Runtime.Scripts.Visuals;
using UnityEngine;
using UnityEngine.TestTools;
using Object = System.Object;

namespace TwineryScenario.Runtime.Tests.Visuals
{
    public class SpeakerTextDisplayerExampleTest
    {
        [Test]
        public void TestCreate()
        {
            // Declare prefab used
            GameObject prefab = Resources.Load<GameObject>("Tests/Prefabs/SpeakBubble-Test");
            Assert.NotNull(prefab);
            
            // Create the component and gameObjects
            GameObject obj = new GameObject();
            SpeakerTextDisplayerExample displayer = obj.AddComponent<SpeakerTextDisplayerExample>();
            GameObject container = new GameObject();
            
            // Initialize Displayer
            displayer.SetPrefab(prefab);
            displayer.SetContainer(container);
            
            // Declare test values for method parameters
            string testName = "Arthur";
            string testText = "Hello";
            string testEmotion = "Happy";
            
            // Call method: Create(string name, string text, string emotion)
            displayer.Create(testName, testText, testEmotion);
            
            // Assertions
            Assert.IsTrue(displayer.GetCountGameObjects() == 1);
            GameObject lastDisplay = displayer.GetLatestDisplay();
            Assert.NotNull(lastDisplay);
            Assert.IsTrue(lastDisplay.GetComponentsInChildren<TextMeshProUGUI>()[0].text == "Arthur (Happy)");
            Assert.IsTrue(lastDisplay.GetComponentsInChildren<TextMeshProUGUI>()[1].text == "Hello");

        }
        
    }
}