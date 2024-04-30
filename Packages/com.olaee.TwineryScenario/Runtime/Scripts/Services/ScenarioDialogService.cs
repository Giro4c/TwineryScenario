using TwineryScenario.Runtime.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TwineryScenario.Runtime.Scripts.Utilities;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An example of Scenario Service that implements the necessary use cases for managing a scenario.
    /// </summary>
    public class ScenarioDialogService : MonoBehaviour, IScenarioDialogService
    {

        /// <summary>
        /// The object that allows to retrieve the data of a Scenario
        /// </summary>
        private IScenarioDialogDataAccess scenarioDataAccess;
        
        /// <summary>
        /// The current scenario that is managed
        /// </summary>
        [SerializeReference] private Scenario scenario;
        
        /// <summary>
        /// The current node of the scenario. Is an indicator of the progression in a scenario.
        /// </summary>
        [SerializeReference] private Node currentNode;
        
        /// <summary>
        /// The list of available emotions for the current scenario
        /// </summary>
        [SerializeReference] private Emotions emotions;
        
        /// <summary>
        /// The list of persons that intervene in the scenario
        /// </summary>
        [SerializeReference] private Persons persons;

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
                scenarioDataAccess = ObjectFinder.FindObjectInScene<IScenarioDialogDataAccess>(SceneManager.GetActiveScene());
            }
        }

        public void InitScenario(string folder, string fileName)
        {
            // Update References to Lists
            scenarioDataAccess.SetEmotionsReference(emotions);
            scenarioDataAccess.SetPersonsReferences(persons);
            
            // Retrieve Scenario
            scenario = scenarioDataAccess.GetScenario(folder, fileName);
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
            return currentNode.links == null || currentNode.links.Length == 0;
        }

        
    }
}