using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class BaseDialogTest
    {
        [Test]
        public void BaseDialogTestInit()
        {
            // Instantiate test object
            BaseDialogProps propsTest = ScriptableObject.CreateInstance<BaseDialogProps>();

            // Declare test values
            Emotion testEmotion =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1");
            Person testPerson =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");

            // Call Init
            propsTest.Init(testEmotion, testPerson);

            // Assert values are changed
            Assert.Equals(propsTest.emotion, testEmotion);
            Assert.Equals(propsTest.speaker, testPerson);

        }

        [Test]
        public void BaseDialogTestInstantiate()
        {
            // Instantiate test object
            BaseDialogProps propsTest = ScriptableObject.CreateInstance<BaseDialogProps>();

            // Assert values are initialised (or not) with the correct values
            Assert.Null(propsTest.emotion);
            Assert.Null(propsTest.speaker);

        }

        [Test]
        public void BaseDialogTestCreate()
        {
            // Declare test values
            Emotion testEmotion =
                Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1");
            Person testPerson =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");

            // Create test object
            BaseDialogProps propsTest = BaseDialogProps.CreateBaseDialogProps(testEmotion, testPerson);

            // Assert Instance exists
            Assert.NotNull(propsTest);

            // Assert values are initialised
            Assert.Equals(propsTest.emotion, testEmotion);
            Assert.Equals(propsTest.speaker, testPerson);

        }

    }
}