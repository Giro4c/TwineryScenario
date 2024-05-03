using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data.ReadModels
{
    public class GlobalPropsReadModelTest
    {
        [Test]
        public void ReadModelTestJsonDeserializeFull()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-props-global-1");
            
            // Deserialize object read model
            GlobalPropsReadModel readModel = JsonUtility.FromJson<GlobalPropsReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.type == "Global");
            Assert.IsTrue(readModel.emotion == "Neutral");
            Assert.IsTrue(readModel.speaker == "Arthur");
            Assert.NotNull(readModel.person);
            Assert.IsTrue(readModel.person.id == "0");
            Assert.IsTrue(readModel.person.name == "Arthur");
        }
        
        [Test]
        public void ReadModelTestJsonDeserializeBase()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-props-base-1");
            
            // Deserialize object read model
            GlobalPropsReadModel readModel = JsonUtility.FromJson<GlobalPropsReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.type == "Base");
            Assert.Null(readModel.emotion);
            Assert.Null(readModel.speaker);
            Assert.NotNull(readModel.person);
            Assert.Null(readModel.person.id);
            Assert.Null(readModel.person.name);
        }
        
        [Test]
        public void ReadModelTestJsonDeserializeBaseDialog()
        {
            // Get text asset
            TextAsset assetJson = Resources.Load<TextAsset>("Tests/JsonFiles/test-props-basedialog-1");
            
            // Deserialize object read model
            GlobalPropsReadModel readModel = JsonUtility.FromJson<GlobalPropsReadModel>(assetJson.text);

            // Verify object is initialized (not null)
            Assert.NotNull(readModel);
            
            // Assert values are correct
            Assert.IsTrue(readModel.type == "Base Dialog");
            Assert.IsTrue(readModel.emotion == "Neutral");
            Assert.IsTrue(readModel.speaker == "Arthur");
            Assert.NotNull(readModel.person);
            Assert.Null(readModel.person.id);
            Assert.Null(readModel.person.name);
        }
        
        [Test]
        public void ReadModelTestConstructorFull()
        {
            // Declare test values
            string testType = "Global";
            string testEmotion = "Neutral";
            string testSpeaker = "Arthur";
            PersonReadModel testPerson = new PersonReadModel("0", "Arthur");
            
            // Construct read model
            GlobalPropsReadModel readModel = new GlobalPropsReadModel(
                testType,
                testEmotion,
                testSpeaker,
                testPerson
            );
            
            // Assert object exists
            Assert.NotNull(readModel);

            // Assert values are correct
            Assert.IsTrue(readModel.type == testType);
            Assert.IsTrue(readModel.emotion == testEmotion);
            Assert.IsTrue(readModel.speaker == testSpeaker);
            Assert.IsTrue(readModel.person == testPerson);

        }
        
        [Test]
        public void ReadModelTestJsonSerialize()
        {
            // Create the initialized read model manually
            string testType = "Global";
            string testEmotion = "Neutral";
            string testSpeaker = "Arthur";
            PersonReadModel testPerson = new PersonReadModel("0", "Arthur");
            
            GlobalPropsReadModel readModel1 = new GlobalPropsReadModel(
                testType,
                testEmotion,
                testSpeaker,
                testPerson
            );
            
            // Serialize the object as json string
            string json = JsonUtility.ToJson(readModel1);
            
            // Deserialize the json string
            GlobalPropsReadModel readModel2 = JsonUtility.FromJson<GlobalPropsReadModel>(json);

            // Assert that all values are the same as before serialization
            Assert.IsTrue(readModel2.type == testType);
            Assert.IsTrue(readModel2.emotion == testEmotion);
            Assert.IsTrue(readModel2.speaker == testSpeaker);
            Assert.NotNull(readModel2.person);
            Assert.IsTrue(readModel2.person.id == testPerson.id);
            Assert.IsTrue(readModel2.person.name == testPerson.name);
            
        }
        
    }
}