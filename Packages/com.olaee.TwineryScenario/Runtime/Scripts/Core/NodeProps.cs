using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class that represents all additional elements in a dialog based scenario which includes the person currently
    /// speaking and the emotion the person is displaying.
    /// </summary>
    [CreateAssetMenu(fileName = "NodeProps", menuName = "ScriptableObjects/Scenarios/NodeProps", order = 1)]
    public class NodeProps : ScriptableObject
    {

        /// <summary>
        /// The emotion the speaker is displaying
        /// </summary>
        public Emotion emotion;
        
        /// <summary>
        /// The person that is currently communicating
        /// </summary>
        public Person speaker;

        /// <summary>
        /// Initialize the node props with all the necessary infos for a dialog-based scenario node
        /// </summary>
        /// <param name="emotion">The emotion the speaker is displaying</param>
        /// <param name="speaker">The person that is currently communicating</param>
        public void Init(Emotion emotion, Person speaker)
        {
            this.emotion = emotion;
            this.speaker = speaker;
        }

        /// <summary>
        /// Creates a ScriptableObject instance of a NodeProps and initializes it with the values in the parameters
        /// </summary>
        /// <param name="emotion">The emotion the speaker is displaying</param>
        /// <param name="speaker">The person that is currently communicating</param>
        /// <returns></returns>
        public static NodeProps CreateNodeProps(Emotion emotion, Person speaker)
        {
            NodeProps nodeProps = ScriptableObject.CreateInstance<NodeProps>();
            nodeProps.Init(emotion, speaker);
            
            return nodeProps;
        }

    }
}