using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data
{
    public class GlobalScenarioJSONDataAccessTest
    {
        [Test]
        public void TestGetCorrectPathEmptyFolderString()
        {
            // Declare test values
            string folder = "";
            string fileName = "Filename";
            string expected = "Filename";

            // Get result
            string result = GlobalScenarioJSONDataAccess.GetCorrectPath(folder, fileName);

            // Assert path is correct
            Assert.IsTrue(result == expected);
        }
        
        [Test]
        public void TestGetCorrectPathNotEmptyFolderString()
        {
            // Declare test values
            string folder = "Folder";
            string fileName = "Filename";
            string expected = "Folder/Filename";

            // Get result
            string result = GlobalScenarioJSONDataAccess.GetCorrectPath(folder, fileName);

            // Assert path is correct
            Assert.IsTrue(result == expected);
        }

        [Test]
        public void TestGetScenario()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Declare test values
            string folder = "Tests/JsonFiles";
            string fileName = "test-scenario-1";
            dataAccess.SetEmotionsReference(Resources.Load<Emotions>("Tests/ScriptableObjects/Change/EmotionsListTest_1"));
            dataAccess.SetPersonsReferences(Resources.Load<Persons>("Tests/ScriptableObjects/Change/PersonsListTest_1"));

            // Call method
            Scenario scenario = dataAccess.GetScenario(folder, fileName);
            
            // Assert values
                // Scenario characteristics
            Assert.IsTrue(scenario.name == "Scenario Test");
            Assert.IsTrue(scenario.creator == "Olaee");
            Assert.IsTrue(scenario.ifid == "aaaa-aaaa");
            Assert.NotNull(scenario.passages);
            Assert.IsTrue(scenario.passages.Length == 2);
            Assert.IsTrue(scenario.passages[0] == scenario.startNode);
                // Nodes characteristics
                    // 1
            Assert.IsTrue(scenario.passages[0].name == "Start");
            Assert.IsTrue(scenario.passages[0].pid == 1);
            Assert.NotNull(scenario.passages[0].links);
            Assert.IsTrue(scenario.passages[0].links.Length == 1);
            Assert.NotNull(scenario.passages[0].props);
            Assert.IsTrue(scenario.passages[0].text == "Hello world.");
            Assert.IsTrue(scenario.passages[0].position.x == 1);
            Assert.IsTrue(scenario.passages[0].position.y == 2);
                    // 2
            Assert.IsTrue(scenario.passages[1].name == "End");
            Assert.IsTrue(scenario.passages[1].text == "Fin");
            Assert.IsTrue(scenario.passages[1].pid == 2);
            Assert.NotNull(scenario.passages[1].props);
            Assert.IsTrue(scenario.passages[1].position.x == 3);
            Assert.IsTrue(scenario.passages[1].position.y == 4);
            Assert.IsTrue(scenario.passages[1].links.Length == 0);
            
                // Props
            Assert.IsTrue(scenario.passages[0].props.type == "Base Dialog");
            BaseDialogProps baseDialogProps = scenario.passages[0].props as BaseDialogProps;
            Assert.NotNull(baseDialogProps);
            Assert.IsTrue(baseDialogProps.emotion.emotionName == "Happy");
            Assert.IsTrue(baseDialogProps.speaker.id == 0);
            Assert.IsTrue(baseDialogProps.speaker.name == "Arthur");

            
                // Links
            Assert.IsTrue(scenario.passages[0].links[0].name == "Ending");
            Assert.IsTrue(scenario.passages[0].links[0].pidNode == 2);
            Assert.IsTrue(scenario.passages[0].links[0].node == scenario.passages[1]);
            
            // Persons and Emotions
            Assert.IsTrue(dataAccess.GetPersons().persons.Count == 1);
            Assert.IsTrue(dataAccess.GetEmotions().emotions.Count == 1);
            
            
        }
        
        [Test]
        public void TestConvertToNodes()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Declare test values
            // Links
            LinkReadModel[] linkReadModels = new[]
            {
                new LinkReadModel("Test link 1", "Node 1", "1"),
            };
            // Props
            GlobalPropsReadModel propsReadModel = new GlobalPropsReadModel();
            propsReadModel.type = "Base";
            // Position
            PositionReadModel positionReadModel = new PositionReadModel("0", "0");
            // Nodes Read Model
            NodeReadModel<GlobalPropsReadModel> nodeReadModel = new NodeReadModel<GlobalPropsReadModel>("1", "Node 1",
                positionReadModel, "Text 1", linkReadModels, propsReadModel);
            NodeReadModel<GlobalPropsReadModel>[] nodeReadModels = new[] { nodeReadModel };
            
            // Call method: ConvertToNodes(NodeReadModel<GlobalPropsReadModel>[] nodesReadModels)
            Node[] nodes = dataAccess.ConvertToNodes(nodeReadModels);
            
            // Assert values
            Assert.IsTrue(nodes.Length == nodeReadModels.Length);
            Assert.IsTrue(nodes.Length == 1);
            Assert.IsTrue(nodes[0].pid == 1);
            Assert.IsTrue(nodes[0].name == "Node 1");
            Assert.IsTrue(nodes[0].text == "Text 1");
            Assert.IsTrue(nodes[0].props.type == nodeReadModel.props.type);
            Assert.IsTrue(nodes[0].links.Length == linkReadModels.Length);
            Assert.IsTrue(nodes[0].links.Length == 1);
            for (int i = 0; i < linkReadModels.Length; ++i)
            {
                Assert.IsTrue(nodes[0].links[i].name == linkReadModels[i].name);
                Assert.IsTrue(nodes[0].links[i].pidNode == int.Parse(linkReadModels[i].pid));
                Assert.NotNull(nodes[0].links[i].node);
                Assert.IsTrue(nodes[0].links[i].node.pid == nodes[0].links[i].pidNode);
            }
        }

        [Test]
        public void TestFillLinksInAllNodes()
        {
            // Initialize test values
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Initialize test values
                // Create lists
            Node[] nodes = new[]
            {
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 1"),
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 2"),
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 3")
            };
                // Remove all references to a node in links
            foreach (Node node in nodes)
            {
                foreach (Link link in node.links)
                {
                    link.node = null;
                }
            }
            
            // Call method: FillLinks(ref Node[] nodes)
            dataAccess.FillLinks(ref nodes);
            
            // Assert links are complete and point to the right node
            foreach (Node node in nodes)
            {
                foreach (Link link in node.links)
                {
                    Assert.NotNull(link.node);
                    Assert.IsTrue(link.node.pid == link.pidNode);
                }
            }

        }
        
        [Test]
        public void TestFillLinksInArrayBasedOnNodesArray()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Initialize test values
                // Create lists
            Link[] links = new[]
            {
                Resources.Load<Link>("Tests/ScriptableObjects/Change/Link 1"),
                Resources.Load<Link>("Tests/ScriptableObjects/Change/Link 2"),
            };
            Node[] nodes = new[]
            {
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 1"),
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 2"),
                Resources.Load<Node>("Tests/ScriptableObjects/Change/Node 3")
            };
                // Remove all references to a node in links
            foreach (Link link in links)
            {
                link.node = null;
            }
            
            // Call method: FillLinks(ref Link[] links, Node[] nodes)
            dataAccess.FillLinks(ref links, nodes);
            
            // Assert links are complete and point to the right node
            foreach (Link link in links)
            {
                Assert.NotNull(link.node);
                Assert.IsTrue(link.node.pid == link.pidNode);
            }

        }
        
        [Test]
        public void TestConvertToLinksNoNodes()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Declare test values
            LinkReadModel[] readModels = new[]
            {
                new LinkReadModel("Test link 1", "Node 1", "1"),
                new LinkReadModel("Test link 2", "Node 2", "2")
            };

            // Call method: ConvertToLinksNoNodes(LinkReadModel[] linksReadModels)
            Link[] links = dataAccess.ConvertToLinksNoNodes(readModels);
            
            // Assert values match
            Assert.IsTrue(readModels.Length == links.Length);
            for (int i = 0; i < readModels.Length; ++i)
            {
                Assert.IsTrue(links[i].name == readModels[i].name);
                Assert.IsTrue(links[i].pidNode == int.Parse(readModels[i].pid));
                Assert.Null(links[i].node);
            }
            
            // Assert that parse int was correct
            Assert.IsTrue(links[0].pidNode == 1);
            Assert.IsTrue(links[1].pidNode == 2);

        }
        
        [Test]
        public void TestConvertToNodesNoLinks()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Declare test values
                // Links
            LinkReadModel[] linkReadModels = new[]
            {
                new LinkReadModel("Test link 1", "Node 1", "1"),
                new LinkReadModel("Test link 2", "Node 2", "2")
            };
                // Props
            GlobalPropsReadModel propsReadModel = new GlobalPropsReadModel();
            propsReadModel.type = "Base";
                // Position
            PositionReadModel positionReadModel = new PositionReadModel("0", "0");
                // Nodes Read Model
            NodeReadModel<GlobalPropsReadModel> nodeReadModel = new NodeReadModel<GlobalPropsReadModel>("1", "Node 1",
                positionReadModel, "Text 1", linkReadModels, propsReadModel);
            NodeReadModel<GlobalPropsReadModel>[] nodeReadModels = new[] { nodeReadModel };
            
            // Call method: ConvertToNodesNoLinks(NodeReadModel<GlobalPropsReadModel>[] nodesReadModels)
            Node[] nodes = dataAccess.ConvertToNodesNoLinks(nodeReadModels);
            
            // Assert values
            Assert.IsTrue(nodes.Length == nodeReadModels.Length);
            Assert.IsTrue(nodes.Length == 1);
            Assert.IsTrue(nodes[0].pid == 1);
            Assert.IsTrue(nodes[0].name == "Node 1");
            Assert.IsTrue(nodes[0].text == "Text 1");
            Assert.IsTrue(nodes[0].props.type == nodeReadModel.props.type);
            Assert.IsTrue(nodes[0].links.Length == linkReadModels.Length);
            Assert.IsTrue(nodes[0].links.Length == 2);
            for (int i = 0; i < linkReadModels.Length; ++i)
            {
                Assert.IsTrue(nodes[0].links[i].name == linkReadModels[i].name);
                Assert.IsTrue(nodes[0].links[i].pidNode == int.Parse(linkReadModels[i].pid));
                Assert.Null(nodes[0].links[i].node);
            }

        }
        
        [Test]
        public void TestConvertReadModelScenario()
        {
            GlobalScenarioJSONDataAccess dataAccess = new GlobalScenarioJSONDataAccess();

            // Declare test values
            NodeReadModel<GlobalPropsReadModel>[] nodeReadModels = new[]
            {
                new NodeReadModel<GlobalPropsReadModel>("1", "Node 1",
                    new PositionReadModel("0", "0"), "Text 1", null, null),
                new NodeReadModel<GlobalPropsReadModel>("2", "Node 2",
                    new PositionReadModel("0", "0"), "Text 2", null, null)
            };
            ScenarioReadModel<GlobalPropsReadModel> readModel = new ScenarioReadModel<GlobalPropsReadModel>(
                "Test Scenario", "1", "Olaee", "1.2.1", "a-a-a", nodeReadModels);

            // Call method: ConvertReadModel(ScenarioReadModel<GlobalPropsReadModel> readModel)
            Scenario scenario = dataAccess.ConvertReadModel(readModel);
            
            // Assert values
            Assert.IsTrue(scenario.name == readModel.name);
            Assert.IsTrue(scenario.creator == readModel.creator);
            Assert.IsTrue(scenario.ifid == readModel.ifid);
            Assert.NotNull(scenario.passages);
            Assert.IsTrue(scenario.passages.Length == nodeReadModels.Length);
            Assert.IsTrue(scenario.startNode == scenario.passages[0]);

        }
        
    }
}