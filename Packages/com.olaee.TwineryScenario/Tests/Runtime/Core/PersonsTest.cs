using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class PersonsTest
    {
        [Test]
        public void PersonsTestInit()
        {
            // Instantiate test object
            Persons personsTest = ScriptableObject.CreateInstance<Persons>();

            // Declare test values
            Person testPerson1 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");
            Person testPerson2 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_2");
            List<Person> testPersonsList = new List<Person>(new[] { testPerson1, testPerson2 });

            // Call Init
            personsTest.Init(testPersonsList);

            // Assert values are changed
            Assert.NotNull(personsTest.persons);
            Assert.Equals(personsTest.persons.Count, 2);
            Assert.Contains(testPerson1, personsTest.persons);
            Assert.Contains(testPerson2, personsTest.persons);

        }

        [Test]
        public void PersonsTestInstantiate()
        {
            // Instantiate test object
            Persons personsTest = ScriptableObject.CreateInstance<Persons>();

            // Assert values are initialised (or not) with the correct values
            Assert.Null(personsTest.persons);

        }

        [Test]
        public void PersonsTestCreateWithList()
        {
            // Declare test values
            Person testPerson1 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");
            Person testPerson2 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_2");
            List<Person> testPersonsList = new List<Person>(new[] { testPerson1, testPerson2 });

            // Create test object
            Persons personsTest = Persons.CreatePersonsList(testPersonsList);

            // Assert Instance exists
            Assert.NotNull(personsTest);

            // Assert values are initialised
            Assert.NotNull(personsTest.persons);
            Assert.Equals(personsTest.persons.Count, 2);
            Assert.Contains(testPerson1, personsTest.persons);
            Assert.Contains(testPerson2, personsTest.persons);

        }

        [Test]
        public void PersonsTestCreateEmpty()
        {
            // Create test object
            Persons personsTest = Persons.CreatePersonsList();

            // Assert Instance exists
            Assert.NotNull(personsTest);

            // Assert values are initialised
            Assert.NotNull(personsTest.persons);
            Assert.Equals(personsTest.persons.Count, 0);

        }

        [Test]
        public void PersonsTestGetPersonByID()
        {
            // Declare test values
            Person testPerson1 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");
            Person testPerson2 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_2");
            Person testPerson3 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_3");
            List<Person> testPersonsList = new List<Person>(new[] { testPerson1, testPerson2 });

            // Create test object
            Persons personsTest = Persons.CreatePersonsList(testPersonsList);

            // Assert values are found
            Assert.Equals(personsTest.GetPerson(testPerson1.id), testPerson1);
            Assert.Equals(personsTest.GetPerson(testPerson2.id), testPerson2);

            // Assert value is not found
            Assert.Null(personsTest.GetPerson(testPerson3.id));

        }

        [Test]
        public void PersonsTestGetPersonByName()
        {
            // Declare test values
            Person testPerson1 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1");
            Person testPerson2 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_2");
            Person testPerson3 =
                Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_3");
            List<Person> testPersonsList = new List<Person>(new[] { testPerson1, testPerson2 });

            // Create test object
            Persons personsTest = Persons.CreatePersonsList(testPersonsList);

            // Assert values are found
            Assert.Equals(personsTest.GetPerson(testPerson1.name), testPerson1);
            Assert.Equals(personsTest.GetPerson(testPerson2.name), testPerson2);

            // Assert value is not found
            Assert.Null(personsTest.GetPerson(testPerson3.name));

        }
    }
}