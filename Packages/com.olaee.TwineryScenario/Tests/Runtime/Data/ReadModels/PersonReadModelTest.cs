using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class PersonReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-person-1");
            
            // Deserialize object read model
            PersonReadModel readModel = JsonUtility.FromJson<PersonReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.id == "0");
            Assert.IsTrue(readModel.name == "Arthur");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testId = "0";
            string testName = "Arthur";
            
            // Construct read model
            PersonReadModel readModel = new PersonReadModel(testId, testName);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.id == testId);
            Assert.IsTrue(readModel.name == testName);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testId = "0";
            string testName = "Arthur";
            PersonReadModel readModel1 = new PersonReadModel(testId, testName);
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            PersonReadModel readModel2 = JsonUtility.FromJson<PersonReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.id == testId);
            Assert.IsTrue(readModel2.name == testName);
            
        }
        
    }
}