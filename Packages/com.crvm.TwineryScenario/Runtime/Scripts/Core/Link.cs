using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class that represents a link pointing to a scenario node. Most commonly, it is used as a representation of the different choices/actions
    /// available at a point / node in the scenario.
    /// </summary>
    [CreateAssetMenu(fileName = "Link", menuName = "ScriptableObjects/Scenarios/Link", order = 1)]
    public class Link : ScriptableObject
    {

        /// <summary>
        /// The name of the link. Used for display, cannot be used independently for identification of a link.
        /// </summary>
        public string name;

        /// <summary>
        /// The pid of the node the link points to
        /// </summary>
        public int pidNode;
        
        /// <summary>
        /// The node the link points to
        /// </summary>
        public ScenarioNode node;

        /// <summary>
        /// Initialize a link with the name and pointed-at node passed in the parameters
        /// </summary>
        /// <param name="name">The display name for the link</param>
        /// <param name="pidNode">The pid of the node the link points to</param>
        /// <param name="node">The node the link points to</param>
        public void Init(string name, int pidNode, ScenarioNode node)
        {
            this.name = name;
            this.pidNode = pidNode;
            this.node = node;
        }

        /// <summary>
        /// Create a ScriptableObject instance of a Link and initializes it with the name and the node passed in the parameters
        /// </summary>
        /// <param name="name">The display name for the link</param>
        /// <param name="pidNode">The pid of the node the link points to</param>
        /// <param name="node">The node the link points to</param>
        /// <returns>A new Link instance initialized with the values in the parameters</returns>
        public static Link CreateLink(string name, int pidNode, ScenarioNode node)
        {
            Link link = ScriptableObject.CreateInstance<Link>();
            link.Init(name, pidNode, node);
            
            return link;
        }

    }
}