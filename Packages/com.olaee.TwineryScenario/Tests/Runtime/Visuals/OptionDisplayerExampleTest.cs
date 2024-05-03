using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using TwineryScenario.Runtime.Scripts.Visuals;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace TwineryScenario.Runtime.Tests.Visuals
{
    public class OptionDisplayerExampleTest
    {
        [Test]
        public void TestCreate()
        {
            // Declare prefab used
            GameObject prefab = Resources.Load<GameObject>("Tests/Prefabs/OptionLink-Test");
            Assert.NotNull(prefab);
            
            // Create the component and gameObjects
            GameObject obj = new GameObject();
            OptionDisplayerExample displayer = obj.AddComponent<OptionDisplayerExample>();
            GameObject container = new GameObject();
            
            // Initialize Displayer
            displayer.SetPrefab(prefab);
            displayer.SetContainer(container);
            
            // Declare test values for method parameters
            string testName = "Option Name";
            string testMsg = "Test Action";
            Exception testException = new Exception(testMsg);
            UnityAction testAction = new UnityAction(() => TestActionCatchable(testException));
            
            // Call method: Create(string name, string text, string emotion)
            displayer.Create(testName, testAction);
            
            // Assertions
            Assert.IsTrue(displayer.GetCountGameObjects() == 1);
            GameObject lastDisplay = displayer.GetLatestDisplay();
            Assert.NotNull(lastDisplay);
                // Assert that the action is registered in the onClick listener and works using exceptions to detect when it's called
            Assert.IsTrue(lastDisplay.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text == "Option Name");
            try
            {
                lastDisplay.GetComponent<Button>().onClick.Invoke();
                Assert.Fail("Click event do not call UnityAction method given.");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e == testException);
            }
            
        }

        private void TestActionCatchable(Exception exception)
        {
            throw exception;
        }

    }
}