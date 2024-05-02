using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class EmotionTest
    {
        [Test]
        public void EmotionTestInit()
        {
            // Instantiate test object
            Emotion emotionTest = ScriptableObject.CreateInstance<Emotion>();

            // Declare test values
            string testEmotionName = "Test Name";

            // Call Init
            emotionTest.Init(testEmotionName);

            // Assert values are changed
            Assert.Equals(emotionTest.emotionName, testEmotionName);

        }

        [Test]
        public void EmotionTestInstantiate()
        {
            // Instantiate test object
            Emotion emotionTest = ScriptableObject.CreateInstance<Emotion>();

            // Assert values are initialised (or not) with the correct values
            Assert.Equals(emotionTest.emotionName, "None");

        }

        [Test]
        public void EmotionTestCreate()
        {
            // Declare test values
            string testEmotionName = "Test Name";

            // Create test object
            Emotion emotionTest = Emotion.CreateEmotion(testEmotionName);

            // Assert Instance exists
            Assert.NotNull(emotionTest);

            // Assert values are initialised
            Assert.Equals(emotionTest.emotionName, testEmotionName);

        }

    }
}