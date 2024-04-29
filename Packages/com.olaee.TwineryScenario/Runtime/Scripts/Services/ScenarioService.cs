using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TwineryScenario.Runtime.Scripts.Utilities;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An example of Scenario Service that implements the necessary use cases for managing a scenario.
    /// </summary>
    public class ScenarioService : MonoBehaviour, IScenarioService
    {

        /// <summary>
        /// The object that allows to retrieve the data of a Scenario
        /// </summary>
        public IScenarioDataAccess scenarioDataAccess;
        
        /// <summary>
        /// The current scenario that is managed
        /// </summary>
        public Scenario scenario;
        
        /// <summary>
        /// A NodeProps used to keep track of the progress in the scenario and the value of persistent variables
        /// such as the current emotion for example
        /// </summary>
        public BaseDialogProps propsState;
        
        /// <summary>
        /// The current node of the scenario. Is an indicator of the progression in a scenario.
        /// </summary>
        public ScenarioNode currentNode;
        
        /// <summary>
        /// The list of available emotions for the current scenario
        /// </summary>
        public Emotions emotions;
        
        /// <summary>
        /// The list of persons that intervene in the scenario
        /// </summary>
        public Persons persons;

        // ------------------------------------------------
        //                    GETTERS
        // ------------------------------------------------
        
        public Scenario GetScenario()
        {
            return scenario;
        }

        public ScenarioNode GetCurrentNode()
        {
            return currentNode;
        }

        public BaseDialogProps GetPropsState()
        {
            return propsState;
        }

        public Persons GetPersonList()
        {
            return persons;
        }

        public Emotions GetEmotionList()
        {
            return emotions;
        }
        
        
        private void Awake()
        {
            // In case the data access object is not initialized in the class, search the scene for a IScenarioDataAccess object
            if (scenarioDataAccess == null)
            {
                scenarioDataAccess = ObjectFinder.FindObjectInScene<IScenarioDataAccess>(SceneManager.GetActiveScene());
            }
        }

        public void InitScenario(string fileName)
        {
            persons.persons.Clear();
            scenario = scenarioDataAccess.GetScenario(fileName, emotions, ref persons);
        }
        
        public void LaunchScenario()
        {
            // Init player progress
            currentNode = scenario.startNode;

            // Create a NodeProps to keep track of the current state if it doesn't exist
            if (propsState == null)
            {
                propsState = ScriptableObject.CreateInstance<BaseDialogProps>();
            }
            // Initialize the props state marker
            propsState.Init(currentNode.props.emotion, currentNode.props.speaker);
        }

        public void GoToNode(int pidNode)
        {
            ScenarioNode newCurrentNode = ScenarioNode.FindInArray(pidNode, scenario.passages);
            if (newCurrentNode == null) return;
            GoToNode(newCurrentNode);
        }

        public void GoToNode(ScenarioNode newCurrentNode)
        {
            // Update the current scenario node
            currentNode = newCurrentNode;
            
            // Change the emotion only if it is precised
            if (currentNode.props.emotion != null)
            {
                propsState.emotion = currentNode.props.emotion;
            }
            
            // Always change the speaker to match the current in the new Node
            propsState.speaker = currentNode.props.speaker;
        }

        public bool HasReachedEnd()
        {
            return currentNode.links == null || currentNode.links.Length == 0;
        }

        
    }
}