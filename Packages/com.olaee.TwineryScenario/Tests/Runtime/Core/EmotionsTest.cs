using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class EmotionsTest
    {
        [Test]
        public void EmotionsTestInit()
        {
            // Instantiate test object
            Emotions emotionsTest = ScriptableObject.CreateInstance<Emotions>();

            // Declare test values
            Emotion testEmotion1 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1");
            Emotion testEmotion2 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_2");
            List<Emotion> testEmotionsList = new List<Emotion>(new[] { testEmotion1, testEmotion2 });

            // Call Init
            emotionsTest.Init(testEmotionsList);

            // Assert values are changed
            Assert.NotNull(emotionsTest.emotions);
            Assert.Equals(emotionsTest.emotions.Count, 2);
            Assert.Contains(testEmotion1, emotionsTest.emotions);
            Assert.Contains(testEmotion2, emotionsTest.emotions);

        }

        [Test]
        public void EmotionsTestInstantiate()
        {
            // Instantiate test object
            Emotions emotionsTest = ScriptableObject.CreateInstance<Emotions>();

            // Assert values are initialised (or not) with the correct values
            Assert.Null(emotionsTest.emotions);

        }

        [Test]
        public void EmotionsTestCreateWithList()
        {
            // Declare test values
            Emotion testEmotion1 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1");
            Emotion testEmotion2 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_2");
            List<Emotion> testEmotionsList = new List<Emotion>(new[] { testEmotion1, testEmotion2 });

            // Create test object
            Emotions emotionsTest = Emotions.CreateEmotionsList(testEmotionsList);

            // Assert Instance exists
            Assert.NotNull(emotionsTest);

            // Assert values are initialised
            Assert.NotNull(emotionsTest.emotions);
            Assert.Equals(emotionsTest.emotions.Count, 2);
            Assert.Contains(testEmotion1, emotionsTest.emotions);
            Assert.Contains(testEmotion2, emotionsTest.emotions);

        }

        [Test]
        public void EmotionsTestCreateEmpty()
        {
            // Create test object
            Emotions emotionsTest = Emotions.CreateEmotionsList();

            // Assert Instance exists
            Assert.NotNull(emotionsTest);

            // Assert values are initialised
            Assert.NotNull(emotionsTest.emotions);
            Assert.Equals(emotionsTest.emotions.Count, 0);

        }

        [Test]
        public void EmotionsTestGetEmotion()
        {
            // Declare test values
            Emotion testEmotion1 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1");
            Emotion testEmotion2 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_2");
            Emotion testEmotion3 =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_3");
            List<Emotion> testEmotionsList = new List<Emotion>(new[] { testEmotion1, testEmotion2 });

            // Create test object
            Emotions emotionsTest = Emotions.CreateEmotionsList(testEmotionsList);

            // Assert values are found
            Assert.Equals(emotionsTest.GetEmotion(testEmotion1.emotionName), testEmotion1);
            Assert.Equals(emotionsTest.GetEmotion(testEmotion2.emotionName), testEmotion2);

            // Assert value is not found
            Assert.Null(emotionsTest.GetEmotion(testEmotion3.emotionName));

        }

    }
}