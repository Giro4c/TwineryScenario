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
        [SerializeReference] private Scenario scenario;
        
        /// <summary>
        /// The current node of the scenario. Is an indicator of the progression in a scenario.
        /// </summary>
        [SerializeReference] private Node currentNode;
        
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
        
        // ------------------------------------------------
        //                    SETTERS
        // ------------------------------------------------

        public void SetScenario(Scenario scenario)
        {
            if (this.scenario != scenario)
            {
                this.scenario = scenario;
                // Get rid of player progress from the old scenario
                currentNode = null;
            }
        }
        
        public void InitDataAccess(IScenarioDialogDataAccess dataAccess)
        {
            scenarioDataAccess = dataAccess;
        }
        
        // ------------------------------------------------
        //                    METHODS
        // ------------------------------------------------
        
        private void Awake()
        {
            // In case the data access object is not initialized in the class, search the scene for a IScenarioDataAccess object
            if (scenarioDataAccess == null)
            {
                scenarioDataAccess = ObjectFinder.FindObjectInScene<IScenarioDataAccess>(SceneManager.GetActiveScene());
            }
        }

        public void InitScenario(string folder, string fileName)
        {
            // Retrieve Scenario
            SetScenario(scenarioDataAccess.GetScenario(folder, fileName));
        }
        
        public void LaunchScenario()
        {
            // Init player progress
            currentNode = scenario.startNode;
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
        }

        public bool HasReachedEnd()
        {
            return currentNode != null && (currentNode.links == null || currentNode.links.Length == 0);
        }
        
    }
}