using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class representing a scenario with a name, a creator and a list of nodes representing events/choices in the scenario.
    /// </summary>
    [CreateAssetMenu(fileName = "Scenario", menuName = "ScriptableObjects/Scenarios/Scenario", order = 1)]
    public class Scenario : ScriptableObject
    {

        /// <summary>
        /// The name of the scenario
        /// </summary>
        public string name = "None";
        
        /// <summary>
        /// The node at which the scenario starts.
        /// </summary>
        public Node startNode;
        
        /// <summary>
        /// The name of the scenario's creator.
        /// </summary>
        public string creator;
        
        /// <summary>
        /// The id of the scenario in twinery
        /// </summary>
        public string ifid;
        
        /// <summary>
        /// The list of nodes representing all the possibilities and "events" in the scenario.
        /// </summary>
        public Node[] passages;

        /// <summary>
        /// Initialize the scenario with the parameters
        /// </summary>
        /// <param name="name">The name of the scenario</param>
        /// <param name="startNode">The node at which the scenario starts</param>
        /// <param name="creator">The name of the scenario's creator</param>
        /// <param name="ifid">The id of the scenario in twinery</param>
        /// <param name="passages">The list of nodes representing all the possibilities and "events" in the scenario</param>
        public void Init(string name, Node startNode, string creator, string ifid, Node[] passages)
        {
            this.name = name;
            this.startNode = startNode;
            this.creator = creator;
            this.ifid = ifid;
            this.passages = passages;
        }

        /// <summary>
        /// Creates a new ScriptableObject instance of a Scenario and initializes it with the values in the parameters
        /// </summary>
        /// <param name="name">The name of the scenario</param>
        /// <param name="startNode">The node at which the scenario starts</param>
        /// <param name="creator">The name of the scenario's creator</param>
        /// <param name="ifid">The id of the scenario in twinery</param>
        /// <param name="passages">The list of nodes representing all the possibilities and "events" in the scenario</param>
        /// <returns>A new Scenario instance initialized with the values in the parameters</returns>
        public static Scenario CreateScenario(string name, Node startNode, string creator, string ifid,
            Node[] passages)
        {
            Scenario scenario = ScriptableObject.CreateInstance<Scenario>();
            scenario.Init(name, startNode, creator, ifid, passages);
            return scenario;
        }

    }
}
