using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class ScenarioTest
    {
        [Test]
        public void ScenarioTestInit()
        {
            // Instantiate test object
            Scenario scenarioTest = ScriptableObject.CreateInstance<Scenario>();

            // Declare test values
            // Name
            string testName = "Test Name";
            // Nodes
            Node testNode1 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");
            Node testNode2 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_2");
            Node[] testNodes = new[] { testNode1, testNode2 };
            // Creator
            string testCreator = "Test Creator";
            // Ifid
            string testIfid = "Test IFID";

            // Call Init
            scenarioTest.Init(testName, testNode1, testCreator, testIfid, testNodes);

            // Assert values are changed
            Assert.Equals(scenarioTest.name, testName);
            Assert.Equals(scenarioTest.startNode, testNode1);
            Assert.Equals(scenarioTest.creator, testCreator);
            Assert.Equals(scenarioTest.ifid, testIfid);
            Assert.Equals(scenarioTest.passages, testNodes);
            Assert.Equals(scenarioTest.passages.Length, 2);
            Assert.Contains(testNode1, scenarioTest.passages);
            Assert.Contains(testNode2, scenarioTest.passages);

        }

        [Test]
        public void ScenarioTestInstantiate()
        {
            // Instantiate test object
            Scenario scenarioTest = ScriptableObject.CreateInstance<Scenario>();

            // Assert values are initialised (or not) with the correct values
            Assert.Equals(scenarioTest.name, "None");
            Assert.Null(scenarioTest.startNode);
            Assert.Null(scenarioTest.creator);
            Assert.Null(scenarioTest.ifid);
            Assert.Null(scenarioTest.passages);

        }

        [Test]
        public void ScenarioTestCreate()
        {
            // Declare test values
            // Name
            string testName = "Test Name";
            // Nodes
            Node testNode1 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");
            Node testNode2 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_2");
            Node[] testNodes = new[] { testNode1, testNode2 };
            // Creator
            string testCreator = "Test Creator";
            // Ifid
            string testIfid = "Test IFID";

            // Create test object
            Scenario scenarioTest = Scenario.CreateScenario(testName, testNode1, testCreator, testIfid, testNodes);

            // Assert Instance exists
            Assert.NotNull(scenarioTest);

            // Assert values are initialised
            Assert.Equals(scenarioTest.name, testName);
            Assert.Equals(scenarioTest.startNode, testNode1);
            Assert.Equals(scenarioTest.creator, testCreator);
            Assert.Equals(scenarioTest.ifid, testIfid);
            Assert.Equals(scenarioTest.passages, testNodes);
            Assert.Equals(scenarioTest.passages.Length, 2);
            Assert.Contains(testNode1, scenarioTest.passages);
            Assert.Contains(testNode2, scenarioTest.passages);

        }
    }
}