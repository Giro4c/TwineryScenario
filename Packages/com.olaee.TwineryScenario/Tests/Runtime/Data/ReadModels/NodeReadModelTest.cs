using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class NodeReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserializeFull()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-node-1");
            
            // Deserialize object read model
            NodeReadModel<PropsReadModel> readModel = JsonUtility.FromJson<NodeReadModel<PropsReadModel>>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.pid == "1");
            Assert.IsTrue(readModel.name == "Start");
            Assert.IsTrue(readModel.text == "Hello world !");
            Assert.NotNull(readModel.position);
            Assert.IsTrue(readModel.position.x == "2");
            Assert.IsTrue(readModel.position.y == "1");
            Assert.NotNull(readModel.props);
            Assert.IsTrue(readModel.props.type == "Base");
            Assert.NotNull(readModel.links);
            Assert.IsTrue(readModel.links.Length == 2);

        }
        
        [Test]
        public void ReadModelTestJsonDeserializeNoProps()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-node-2");
            
            // Deserialize object read model
            NodeReadModel<PropsReadModel> readModel = JsonUtility.FromJson<NodeReadModel<PropsReadModel>>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.pid == "1");
            Assert.IsTrue(readModel.name == "Start");
            Assert.IsTrue(readModel.text == "Hello world !");
            Assert.NotNull(readModel.position);
            Assert.IsTrue(readModel.position.x == "2");
            Assert.IsTrue(readModel.position.y == "1");
            Assert.NotNull(readModel.props);
            Assert.IsTrue(readModel.props.type == "Base");
            Assert.NotNull(readModel.links);
            Assert.IsTrue(readModel.links.Length == 2);

        }
        
        [Test]
        public void ReadModelTestJsonDeserializeNoLinks()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-node-3");
            
            // Deserialize object read model
            NodeReadModel<PropsReadModel> readModel = JsonUtility.FromJson<NodeReadModel<PropsReadModel>>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.pid == "1");
            Assert.IsTrue(readModel.name == "Start");
            Assert.IsTrue(readModel.text == "Hello world !");
            Assert.NotNull(readModel.position);
            Assert.IsTrue(readModel.position.x == "2");
            Assert.IsTrue(readModel.position.y == "1");
            Assert.NotNull(readModel.props);
            Assert.IsTrue(readModel.props.type == "Base");
            Assert.Null(readModel.links);
            // Assert.IsTrue(readModel.links.Length == 0);

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testPid = "1";
            string testName = "Start";
            string testText = "Hello world !";
            PositionReadModel testPosition = new PositionReadModel("2", "1");
            PropsReadModel testProps = new PropsReadModel("Base");
            LinkReadModel[] testLinks = new[]
            {
                new LinkReadModel("Say Hello", "Branch 1", "2"),
                new LinkReadModel("Say Hi", "Branch 2", "3")
            };
            
            // Construct read model
            NodeReadModel<PropsReadModel> readModel = new NodeReadModel<PropsReadModel>(
                testPid, testName, testPosition, testText, testLinks, testProps);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.pid == testPid);
            Assert.IsTrue(readModel.name == testName);
            Assert.IsTrue(readModel.text == testText);
            Assert.IsTrue(readModel.position == testPosition);
            Assert.IsTrue(readModel.props == testProps);
            Assert.IsTrue(readModel.links == testLinks);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testPid = "1";
            string testName = "Start";
            string testText = "Hello world !";
            PositionReadModel testPosition = new PositionReadModel("2", "1");
            PropsReadModel testProps = new PropsReadModel("Base");
            LinkReadModel[] testLinks = new[]
            {
                new LinkReadModel("Say Hello", "Branch 1", "2"),
                new LinkReadModel("Say Hi", "Branch 2", "3")
            };
            NodeReadModel<PropsReadModel> readModel1 = new NodeReadModel<PropsReadModel>(
                testPid, testName, testPosition, testText, testLinks, testProps);

            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            NodeReadModel<PropsReadModel> readModel2 = JsonUtility.FromJson<NodeReadModel<PropsReadModel>>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.pid == testPid);
            Assert.IsTrue(readModel2.name == testName);
            Assert.IsTrue(readModel2.text == testText);
            Assert.NotNull(readModel2.position);
            Assert.IsTrue(readModel2.position.x == testPosition.x);
            Assert.IsTrue(readModel2.position.y == testPosition.y);
            Assert.NotNull(readModel2.props);
            Assert.IsTrue(readModel2.props.type == testProps.type);
            Assert.NotNull(readModel2.links);
            Assert.IsTrue(readModel2.links.Length == testLinks.Length);
            
        }
        
    }
}