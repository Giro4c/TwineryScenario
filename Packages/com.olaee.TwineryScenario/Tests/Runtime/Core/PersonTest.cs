using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class PersonTest
    {
        [Test]
        public void PersonTestInit()
        {
            // Instantiate test object
            Person personTest = ScriptableObject.CreateInstance<Person>();

            // Declare test values
            int testId = 1;
            string testName = "Elodie";

            // Call Init
            personTest.Init(testId, testName);

            // Assert values are changed
            Assert.IsTrue(personTest.id == testId);
            Assert.IsTrue(personTest.name == testName);

        }

        [Test]
        public void PersonTestInstantiate()
        {
            // Instantiate test object
            Person personTest = ScriptableObject.CreateInstance<Person>();

            // Assert values are initialised (or not) with the correct values
            Assert.IsTrue(personTest.id == 0);
            Assert.IsTrue(personTest.name == "None");
        }

        [Test]
        public void PersonTestCreate()
        {
            // Declare test values
            int testId = 1;
            string testName = "Elodie";

            // Create test object
            Person personTest = Person.CreatePerson(testId, testName);

            // Assert Instance exists
            Assert.NotNull(personTest);

            // Assert values are initialised
            Assert.IsTrue(personTest.id == testId);
            Assert.IsTrue(personTest.name == testName);

        }

    }
}