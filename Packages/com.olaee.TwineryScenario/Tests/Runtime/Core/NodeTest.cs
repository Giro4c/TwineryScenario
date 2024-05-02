using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.TestTools;


namespace TwineryScenario.Runtime.Tests.Core
{
    public class NodeTest
    {
        [Test]
        public void NodeTestInit()
        {
            // Instantiate test object
            Node nodeTest = ScriptableObject.CreateInstance<Node>();

            // Declare test values
            // Pid
            int testPid = 1;
            // Name
            string testName = "Test Name";
            // Position (X, Y)
            int testXPos = 1;
            int testYPos = 1;
            // Text
            string testText = "Test Text";
            // Links
            Link testLink =
                Resources.Load<Link>("Tests/ScriptableObjects/Constants/LinkTest_1");
            Link[] testLinks = new[] { testLink };
            // Props
            Props testProps =
                Resources.Load<Props>("Tests/ScriptableObjects/Constants/BasePropsTest_1");

            // Call Init
            nodeTest.Init(testPid, testName, testXPos, testYPos, testText, testLinks, testProps);

            // Assert values are changed
            Assert.IsTrue(nodeTest.pid == testPid);
            Assert.IsTrue(nodeTest.name == testName);
            Assert.NotNull(nodeTest.position);
            Assert.IsTrue(nodeTest.position.x == testXPos);
            Assert.IsTrue(nodeTest.position.y == testYPos);
            Assert.IsTrue(nodeTest.text == testText);
            Assert.IsTrue(nodeTest.links.Length == 1);
            Assert.Contains(testLink, testLinks);
            Assert.IsTrue(nodeTest.links == testLinks);
            Assert.IsTrue(nodeTest.props == testProps);

        }

        [Test]
        public void NodeTestInstantiate()
        {
            // Instantiate test object
            Node nodeTest = ScriptableObject.CreateInstance<Node>();

            // Assert values are initialised (or not) with the correct values
            Assert.IsTrue(nodeTest.pid == 0);
            Assert.IsTrue(nodeTest.name == "None");
            Assert.NotNull(nodeTest.position);
            Assert.IsTrue(nodeTest.position.x == 0);
            Assert.IsTrue(nodeTest.position.y == 0);
            Assert.IsEmpty(nodeTest.text);
            Assert.Null(nodeTest.links);
            Assert.Null(nodeTest.props);

        }

        [Test]
        public void NodeTestCreate()
        {
            // Declare test values
            // Pid
            int testPid = 1;
            // Name
            string testName = "Test Name";
            // Position (X, Y)
            int testXPos = 1;
            int testYPos = 1;
            // Text
            string testText = "Test Text";
            // Links
            Link testLink =
                Resources.Load<Link>("Tests/ScriptableObjects/Constants/LinkTest_1");
            Link[] testLinks = new[] { testLink };
            // Props
            Props testProps =
                Resources.Load<Props>("Tests/ScriptableObjects/Constants/BasePropsTest_1");

            // Create test object
            Node nodeTest = Node.CreateNode(testPid, testName, testXPos, testYPos, testText, testLinks, testProps);

            // Assert Instance exists
            Assert.NotNull(nodeTest);

            // Assert values are initialised
            Assert.IsTrue(nodeTest.pid == testPid);
            Assert.IsTrue(nodeTest.name == testName);
            Assert.NotNull(nodeTest.position);
            Assert.IsTrue(nodeTest.position.x == testXPos);
            Assert.IsTrue(nodeTest.position.y == testYPos);
            Assert.IsTrue(nodeTest.text == testText);
            Assert.IsTrue(nodeTest.links.Length == 1);
            Assert.Contains(testLink, testLinks);
            Assert.IsTrue(nodeTest.links == testLinks);
            Assert.IsTrue(nodeTest.props == testProps);

        }

        [Test]
        public void NodeTestFindInArray()
        {
            Node testNode1 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_1");
            Node testNode2 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_2");
            Node testNode3 =
                Resources.Load<Node>("Tests/ScriptableObjects/Constants/NodeTest_3");
            Node[] testNodes = new[] { testNode1, testNode2 };

            // Assert values in array
            Assert.IsTrue(testNode1 == Node.FindInArray(testNode1.pid, testNodes));
            Assert.IsTrue(testNode2 == Node.FindInArray(testNode2.pid, testNodes));

            // Assert value not in array
            Assert.Null(Node.FindInArray(testNode3.pid, testNodes));

        }

    }
}
