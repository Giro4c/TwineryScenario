using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TwineryScenario.Runtime.Scripts.Utilities;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An example of Scenario Service that implements the necessary use cases for managing a scenario.
    /// </summary>
    public class ScenarioBaseService : MonoBehaviour, IScenarioService
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
        /// The current node of the scenario. Is an indicator of the progression in a scenario.
        /// </summary>
        public Node currentNode;
        
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

        public Node GetCurrentNode()
        {
            return currentNode;
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
            // Update References to Lists
            scenarioDataAccess.GetPropsFactory().emotionsList = emotions;
            scenarioDataAccess.GetPropsFactory().personsList = persons;
            
            // Retrieve Scenario
            scenario = scenarioDataAccess.GetScenario(fileName);
        }
        
        public void LaunchScenario()
        {
            // Init player progress
            currentNode = scenario.startNode;
            
            // // Create a NodeProps to keep track of the current state if it doesn't exist
            // if (propsState == null)
            // {
            //     propsState = ScriptableObject.CreateInstance<BaseDialogProps>();
            // }
            //
            // // Get Casted Node Props of current Node and set current prop state
            // propsState = currentNode.props as BaseDialogProps;
            
        }

        public void GoToNode(int pidNode)
        {
            Node newCurrentNode = Node.FindInArray(pidNode, scenario.passages);
            if (newCurrentNode == null) return;
            GoToNode(newCurrentNode);
        }

        public void GoToNode(Node newCurrentNode)
        {
            // Update the current scenario node
            currentNode = newCurrentNode;
            
            // Get Casted Node Props of current Node and set current prop state
            // propsState = currentNode.props as BaseDialogProps;
            
        }

        public bool HasReachedEnd()
        {
            return currentNode.links == null || currentNode.links.Length == 0;
        }

        
    }
}