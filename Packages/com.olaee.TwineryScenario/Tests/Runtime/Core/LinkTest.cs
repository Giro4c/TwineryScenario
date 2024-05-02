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
            Assert.Equals(linkTest.name, testName);
            Assert.Equals(linkTest.pidNode, testPidNode);
            Assert.Equals(linkTest.node, testNode);

        }

        [Test]
        public void LinkTestInstantiate()
        {
            // Instantiate test object
            Link linkTest = ScriptableObject.CreateInstance<Link>();

            // Assert values are initialised (or not) with the correct values
            Assert.Equals(linkTest.name, "None");
            Assert.Equals(linkTest.pidNode, 0);
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
            Assert.Equals(linkTest.name, testName);
            Assert.Equals(linkTest.pidNode, testPidNode);
            Assert.Equals(linkTest.node, testNode);

        }
    }
}