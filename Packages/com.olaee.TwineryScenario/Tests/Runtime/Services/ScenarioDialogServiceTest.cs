using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data;
using TwineryScenario.Runtime.Scripts.Services;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Services
{
    public class ScenarioDialogServiceTest
    {
        [Test]
        public void TestSetScenarioSame()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            
            // Initialize test scenario and service state
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            Scenario scenario2 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
                // Init the current node to the scenario's start node
            service.SetScenario(scenario1);
            service.LaunchScenario();
            
            // Call scenario setter
            service.SetScenario(scenario2);

            // Assert that scenario are the same and current node did not change
            Assert.IsTrue(service.GetScenario() == scenario2);
            Assert.IsTrue(service.GetScenario() == scenario1);
            Assert.NotNull(service.GetCurrentNode());

        }
        
        [Test]
        public void TestSetScenarioDifferent()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            
            // Initialize test scenario and service state
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            Scenario scenario2 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_2");
            // Init the current node to the scenario's start node
            service.SetScenario(scenario1);
            service.LaunchScenario();
            
            // Call scenario setter
            service.SetScenario(scenario2);

            // Assert that scenario are the same and current node did not change
            Assert.IsTrue(service.GetScenario() == scenario2);
            Assert.IsFalse(service.GetScenario() == scenario1);
            Assert.Null(service.GetCurrentNode());
        }

        [Test]
        public void TestInitScenario()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            service.InitDataAccess(new GlobalScenarioJSONDataAccess());
            service.SetEmotions(Resources.Load<Emotions>("Tests/ScriptableObjects/Change/EmotionsListTest_1"));
            service.SetPersons(Resources.Load<Persons>("Tests/ScriptableObjects/Change/PersonsListTest_1"));
            
            // Declare test values
            string testFolder = "Tests/JsonFiles";
            string testFileName = "test-scenario-1";
            
            // Call Init Scenario
            service.InitScenario(testFolder, testFileName);
            
            // Assert that scenario is set and is the right one
            Assert.NotNull(service.GetScenario());
            Assert.IsTrue(service.GetScenario().name == "Scenario Test");
            Assert.IsTrue(service.GetScenario().ifid == "aaaa-aaaa");
            Assert.IsTrue(service.GetEmotionList().emotions.Count == 1);
            Assert.IsTrue(service.GetPersonList().persons.Count == 1);
        }

        [Test]
        public void TestLaunchScenario()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            // Init service
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            service.SetScenario(scenario1);
            
            // Call method: LaunchScenario()
            service.LaunchScenario();
            
            // Assert values are correct
            Assert.NotNull(service.GetCurrentNode());
            Assert.IsTrue(service.GetCurrentNode() == scenario1.startNode);
            
        }
        
        [Test]
        public void TestGoToNode()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            
            // Declare test node
            Node node = Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");
            
            // Call method: GoToNode(Node newCurrentNode)
            service.GoToNode(node);
            
            // Assert values are correct
            Assert.NotNull(service.GetCurrentNode());
            Assert.IsTrue(service.GetCurrentNode() == node);
        }
        
        [Test]
        public void TestGoToNodeByPidFound()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            // Init service
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            service.SetScenario(scenario1);
            
            // Call method: GoToNode(int pidNode)
            service.GoToNode(2);
            
            // Assert values are correct
            Assert.NotNull(service.GetCurrentNode());
            Assert.IsTrue(service.GetCurrentNode().pid == 2);

        }
        
        [Test]
        public void TestGoToNodeByPidNotFound()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            // Init service
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            service.SetScenario(scenario1);
            service.GoToNode(scenario1.startNode);
            
            // Call method: GoToNode(int pidNode)
            service.GoToNode(3);
            
            // Assert values have not changed
            Assert.NotNull(service.GetCurrentNode());
            Assert.IsTrue(service.GetCurrentNode() == scenario1.startNode);

        }
        
        
        [Test]
        public void TestHasReachedEndTrue()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            
            // Init service
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            service.SetScenario(scenario1);
            
            // Go to a node with no links
            service.GoToNode(scenario1.passages[1]);
            
            // Assert HasReachedEnd() returns true
            Assert.IsTrue(service.HasReachedEnd());
        }
        
        [Test]
        public void TestHasReachedEndFalse()
        {
            // Create service
            GameObject obj = new GameObject();
            ScenarioDialogService service = obj.AddComponent<ScenarioDialogService>();
            
            // Init service
            Scenario scenario1 = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            service.SetScenario(scenario1);
            
            // Go to a node with links
            service.GoToNode(scenario1.passages[0]);
            
            // Assert HasReachedEnd() returns false
            Assert.IsFalse(service.HasReachedEnd());
        }
    }
}