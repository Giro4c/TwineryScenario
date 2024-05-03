using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class ScenarioReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get String JSON content from file
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-scenario-1");
            
            // Deserialize object read model
            ScenarioReadModel<GlobalPropsReadModel> readModel = 
                JsonUtility.FromJson<ScenarioReadModel<GlobalPropsReadModel>>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.name == "Scenario Test");
            Assert.IsTrue(readModel.startnode == "1");
            Assert.NotNull(readModel.passages);
            Assert.IsTrue(readModel.passages.Length == 2);
            Assert.IsTrue(readModel.creator == "Olaee");
            Assert.IsTrue(readModel.ifid == "aaaa-aaaa");
            Assert.IsTrue(readModel.creatorversion == "1.2.1");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testName = "Test name";
            string testStartnode = "1";
            string testCreator = "Creator test";
            string testIfid = "ifid";
            string testCreatorversion = "1.0";
            NodeReadModel<GlobalPropsReadModel>[] testNodes = new[]
            {
                new NodeReadModel<GlobalPropsReadModel>("1", "Node name", new PositionReadModel(),
                    "Node text", null, new GlobalPropsReadModel("Base Dialog", "Happy",
                        "Bastien", null))
            };
            
            // Construct read model
            ScenarioReadModel<GlobalPropsReadModel> readModel = new ScenarioReadModel<GlobalPropsReadModel>(
                testName,
                testStartnode,
                testCreator,
                testCreatorversion,
                testIfid,
                testNodes
            );
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.name == testName);
            Assert.IsTrue(readModel.startnode == testStartnode);
            Assert.NotNull(readModel.passages);
            Assert.IsTrue(readModel.passages.Length == 1);
            Assert.IsTrue(readModel.passages[0].pid == testNodes[0].pid);
            Assert.IsTrue(readModel.creator == testCreator);
            Assert.IsTrue(readModel.ifid == testIfid);
            Assert.IsTrue(readModel.creatorversion == testCreatorversion);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testName = "Test name";
            string testStartnode = "1";
            string testCreator = "Creator test";
            string testIfid = "ifid";
            string testCreatorversion = "1.0";
            NodeReadModel<GlobalPropsReadModel>[] testNodes = new[]
            {
                new NodeReadModel<GlobalPropsReadModel>("1", "Node name", new PositionReadModel("1", "1"),
                    "Node text", null, new GlobalPropsReadModel("Base Dialog", "Happy",
                        "Bastien", null))
            };
            ScenarioReadModel<GlobalPropsReadModel> readModel1 = new ScenarioReadModel<GlobalPropsReadModel>(
                testName,
                testStartnode,
                testCreator,
                testCreatorversion,
                testIfid,
                testNodes
            );
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            Debug.Log(json);
            
            // Deserialize the json string
            ScenarioReadModel<GlobalPropsReadModel> readModel2 = 
                JsonUtility.FromJson<ScenarioReadModel<GlobalPropsReadModel>>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.name == testName);
            Assert.IsTrue(readModel2.startnode == testStartnode);
            Assert.NotNull(readModel2.passages);
            Assert.IsTrue(readModel2.passages.Length == 1);
            Assert.IsTrue(readModel2.passages[0].pid == "1");
            Assert.IsTrue(readModel2.creator == testCreator);
            Assert.IsTrue(readModel2.ifid == testIfid);
            Assert.IsTrue(readModel2.creatorversion == testCreatorversion);
            
        }
        
    }
}