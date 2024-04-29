using System;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class representing a node/possibility/event in a scenario
    /// </summary>
    [CreateAssetMenu(fileName = "Node", menuName = "ScriptableObjects/Scenarios/Node", order = 1)]
    public class Node : ScriptableObject
    {

        /// <summary>
        /// The identifier of the node within a scenario
        /// </summary>
        public int pid;
        
        /// <summary>
        /// The name of the node
        /// </summary>
        public string name;
        
        /// <summary>
        /// The position of the node on a 2D plane should the scenario be visualized
        /// </summary>
        public Position position;
        
        /// <summary>
        /// The text content of the node. Can be a "spoken" sentence, a notification, a description, etc...
        /// </summary>
        public string text;
        
        /// <summary>
        /// The list of links pointing to all the nodes accessible through this one
        /// </summary>
        public Link[] links;
        
        /// <summary>
        /// The additional data that describes the characteristics of this scenario node
        /// </summary>
        public Props props;
        
        /// <summary>
        /// A class representing an object's position on a two-axis plane.
        /// </summary>
        [Serializable]
        public class Position
        {
            public int x;
            public int y;

            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            
        }

        /// <summary>
        /// Initialize the Node with the values passed in the parameters
        /// </summary>
        /// <param name="pid">The identifier of the node within a scenario</param>
        /// <param name="name">The name of the node</param>
        /// <param name="x">The position in the first axis of the plane on which the node would be visualized</param>
        /// <param name="y">The position in the second axis of the plane on which the node would be visualized</param>
        /// <param name="text">The text content of the node</param>
        /// <param name="links">The list of links pointing to all the nodes accessible through this one</param>
        /// <param name="props">The additional data that describes the characteristics of this scenario node</param>
        public void Init(int pid, string name, int x, int y, string text, Link[] links, Props props)
        {
            this.pid = pid;
            this.name = name;
            this.position = new Position(x, y);
            this.text = text;
            this.links = links;
            this.props = props;
        }

        /// <summary>
        /// Creates a ScriptableObject instance of a Node and initializes it with the values passed in the parameters
        /// </summary>
        /// <param name="pid">The identifier of the node within a scenario</param>
        /// <param name="name">The name of the node</param>
        /// <param name="x">The position in the first axis of the plane on which the node would be visualized</param>
        /// <param name="y">The position in the second axis of the plane on which the node would be visualized</param>
        /// <param name="text">The text content of the node</param>
        /// <param name="links">The list of links pointing to all the nodes accessible through this one</param>
        /// <param name="props">The additional data that describes the characteristics of this scenario node</param>
        /// <returns>A new Node instance initialized with the values in the parameters</returns>
        public static Node CreateNode(int pid, string name, int x, int y, string text, Link[] links,
            Props props)
        {
            Node node = ScriptableObject.CreateInstance<Node>();
            node.Init(pid, name, x, y, text, links, props);
            return node;
        }
        
        /// <summary>
        /// Finds a scenario node in an array based on its pid
        /// </summary>
        /// <param name="pid">The pid of the searched node</param>
        /// <param name="nodes">The array in which the node is searched</param>
        /// <returns>
        /// If it is in the array, returns the Node whose pid corresponds to the value given in the parameters.</br>
        /// If not, then returns null.
        /// </returns>
        public static Node FindInArray(int pid, Node[] nodes)
        {
            foreach (Node node in nodes)
            {
                if (node.pid == pid) return node;
            }
            return null;
        }
        
    }
}