using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class BaseDialogPropsReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserialize()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/fileSrc");
            
            // Deserialize object read model
            Object readModel = JsonUtility.FromJson<Object>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct


        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            
            
            // Construct read model
            Object readModel = new Object();
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct


        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            Object readModel1 = new Object();
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            Object readModel2 = JsonUtility.FromJson<Object>(json);

            // Assert that all values are the same as before serialization
            
            
        }
        
    }
}