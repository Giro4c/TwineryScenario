using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class PropsTest
    {

        [Test]
        public void PropsTestInit()
        {
            // Instantiate test object
            Props propsTest = ScriptableObject.CreateInstance<Props>();

            // Declare test values
            string testType = "Hello";

            // Call Init
            propsTest.Init(testType);

            // Assert values are changed
            Assert.Equals(propsTest.type, testType);

        }

        [Test]
        public void PropsTestInstantiate()
        {
            // Instantiate test object
            Props propsTest = ScriptableObject.CreateInstance<Props>();

            // Assert values are initialised (or not) with the correct values
            Assert.Equals(propsTest.type, "Base");
        }

        [Test]
        public void PropsTestCreate()
        {
            // Declare test values
            string testType = "Hello";

            // Create test object
            Props propsTest = Props.CreateProps(testType);

            // Assert Instance exists
            Assert.NotNull(propsTest);

            // Assert values are initialised
            Assert.Equals(propsTest.type, testType);

        }

    }
}