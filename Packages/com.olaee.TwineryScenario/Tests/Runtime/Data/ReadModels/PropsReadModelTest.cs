using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class PropsReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-props-base-1");
            
            // Deserialize object read model
            PropsReadModel readModel = JsonUtility.FromJson<PropsReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.NotNull(readModel.type);
            Assert.IsTrue(readModel.type == "Base");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testType = "Base Test";
            
            // Construct read model
            PropsReadModel readModel = new PropsReadModel(testType);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.NotNull(readModel.type);
            Assert.IsTrue(readModel.type == testType);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testType = "Base Test";
            PropsReadModel readModel1 = new PropsReadModel(testType);
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            PropsReadModel readModel2 = JsonUtility.FromJson<PropsReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.NotNull(readModel2.type);
            Assert.IsTrue(readModel2.type == testType);
            
        }
        
    }
}