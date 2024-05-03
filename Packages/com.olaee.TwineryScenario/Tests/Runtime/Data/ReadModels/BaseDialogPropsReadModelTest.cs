using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
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
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-props-basedialog-1");
            
            // Deserialize object read model
            BaseDialogPropsReadModel readModel = JsonUtility.FromJson<BaseDialogPropsReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.type == "Base Dialog");
            Assert.IsTrue(readModel.emotion == "Neutral");
            Assert.IsTrue(readModel.speaker == "Arthur");

        }
        
        [Test]
        public void ReadModelTestConstructor()
        {
            // Declare test values
            string testType = "Base Dialog";
            string testEmotion = "Neutral";
            string testSpeaker = "Arthur";
            
            // Construct read model
            BaseDialogPropsReadModel readModel = new BaseDialogPropsReadModel(testType, testEmotion, testSpeaker);
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.type == testType);
            Assert.IsTrue(readModel.emotion == testEmotion);
            Assert.IsTrue(readModel.speaker == testSpeaker);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testType = "Base Dialog";
            string testEmotion = "Neutral";
            string testSpeaker = "Arthur";
            BaseDialogPropsReadModel readModel1 = new BaseDialogPropsReadModel(testType, testEmotion, testSpeaker);
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            BaseDialogPropsReadModel readModel2 = JsonUtility.FromJson<BaseDialogPropsReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.type == testType);
            Assert.IsTrue(readModel2.emotion == testEmotion);
            Assert.IsTrue(readModel2.speaker == testSpeaker);
        }
        
    }
}