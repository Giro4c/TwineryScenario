using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class LinkReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-link-1");
            
            // Deserialize object read model
            LinkReadModel readModel = JsonUtility.FromJson<LinkReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.name == "Say Hello");
            Assert.IsTrue(readModel.link == "Branch 1");
            Assert.IsTrue(readModel.pid == "5");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testName = "Say Hello";
            string testLink = "Branch 1";
            string testPid = "5";
            
            // Construct read model
            LinkReadModel readModel = new LinkReadModel(testName, testLink, testPid);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.name == testName);
            Assert.IsTrue(readModel.link == testLink);
            Assert.IsTrue(readModel.pid == testPid);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testName = "Say Hello";
            string testLink = "Branch 1";
            string testPid = "5";
            LinkReadModel readModel1 = new LinkReadModel(testName, testLink, testPid);
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            LinkReadModel readModel2 = JsonUtility.FromJson<LinkReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.name == testName);
            Assert.IsTrue(readModel2.link == testLink);
            Assert.IsTrue(readModel2.pid == testPid);
            
        }
        
    }
}