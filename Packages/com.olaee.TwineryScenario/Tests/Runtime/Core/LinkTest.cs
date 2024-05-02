using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Core
{
    public class LinkTest
    {
        [Test]
        public void LinkTestInit()
        {
            // Instantiate test object
            Link linkTest = ScriptableObject.CreateInstance<Link>();

            // Declare test values
            string testName = "Test Name";
            int testPidNode = 1;
            Node testNode =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");

            // Call Init
            linkTest.Init(testName, testPidNode, testNode);

            // Assert values are changed
            Assert.IsTrue(linkTest.name == testName);
            Assert.IsTrue(linkTest.pidNode == testPidNode);
            Assert.IsTrue(linkTest.node == testNode);

        }

        [Test]
        public void LinkTestInstantiate()
        {
            // Instantiate test object
            Link linkTest = ScriptableObject.CreateInstance<Link>();

            // Assert values are initialised (or not) with the correct values
            Assert.IsTrue(linkTest.name == "None");
            Assert.IsTrue(linkTest.pidNode == 0);
            Assert.Null(linkTest.node);
        }

        [Test]
        public void LinkTestCreate()
        {
            // Declare test values
            string testName = "Test Name";
            int testPidNode = 1;
            Node testNode =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");

            // Create test object
            Link linkTest = Link.CreateLink(testName, testPidNode, testNode);

            // Assert Instance exists
            Assert.NotNull(linkTest);

            // Assert values are initialised
            Assert.IsTrue(linkTest.name == testName);
            Assert.IsTrue(linkTest.pidNode == testPidNode);
            Assert.IsTrue(linkTest.node == testNode);

        }
    }
}