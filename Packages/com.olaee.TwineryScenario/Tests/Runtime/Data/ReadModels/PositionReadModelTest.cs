using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class PositionReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-position-1");
            
            // Deserialize object read model
            PositionReadModel readModel = JsonUtility.FromJson<PositionReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.x == "1");
            Assert.IsTrue(readModel.y == "2");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testX = "1";
            string testY = "2";
            
            // Construct read model
            PositionReadModel readModel = new PositionReadModel(testX, testY);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.x == testX);
            Assert.IsTrue(readModel.y == testY);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testX = "1";
            string testY = "2";
            PositionReadModel readModel1 = new PositionReadModel(testX, testY);
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            PositionReadModel readModel2 = JsonUtility.FromJson<PositionReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.x == testX);
            Assert.IsTrue(readModel2.y == testY);
            
        }
        
    }
}