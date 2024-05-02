using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class ScriptableObjectTest
    {
        [Test]
        public void TestInit()
        {
            // Instantiate test object
            ScriptableObject Test = ScriptableObject.CreateInstance<ScriptableObject>();

            // Declare test values


            // Call Init


            // Assert values are changed


        }

        [Test]
        public void TestInstantiate()
        {
            // Instantiate test object
            ScriptableObject Test = ScriptableObject.CreateInstance<ScriptableObject>();

            // Assert values are initialised (or not) with the correct values

        }

        [Test]
        public void TestCreate()
        {
            // Declare test values


            // Create test object
            ScriptableObject Test = null;

            // Assert Instance exists
            Assert.NotNull(Test);

            // Assert values are initialised


        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}