using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using TwineryScenario.Runtime.Scripts.Utilities;
using TwineryScenario.Runtime.Scripts.Visuals;

namespace TwineryScenario.Runtime.Scripts.Control
{
    /// <summary>
    /// An example of a scenario controller with basic actions
    /// </summary>
    public class ScenarioController : MonoBehaviour
    {

        /// <summary>
        /// An object implementing the interface IScenarioService that contains all use cases for a scenario.
        /// </summary>
        public IScenarioService scenarioService;
        
        /// <summary>
        /// The Displayer related to the display of speak bubbles
        /// </summary>
        public SpeakerTextDisplayer speakerTextDisplayer;
        
        /// <summary>
        /// The Displayer related to the display of the options available in a node
        /// </summary>
        public OptionDisplayer optionDisplayer;

        /// <summary>
        /// The name of the file containing the json of a scenario
        /// </summary>
        public string scenarioFileName = "twinery-example";
        
        /// <summary>
        /// The name of the player
        /// </summary>
        public string playerName = "Player";

        private void Start()
        {
            scenarioService = ObjectFinder.FindObjectInScene<IScenarioService>(SceneManager.GetActiveScene());
        }

        private void Awake()
        {
            if (scenarioService == null)
            {
                scenarioService = ObjectFinder.FindObjectInScene<IScenarioService>(SceneManager.GetActiveScene());
            }
        }

        /// <summary>
        /// Trigger the initialisation of the player progress in the current scenario of the service and display
        /// the current node : text content and options
        /// </summary>
        public void LaunchScenario()
        {
            // Trigger the initialisation of the player progress in the scenario
            scenarioService.LaunchScenario();
            // Display the current scenario node : start node
            ShowCurrentNode();
        }

        /// <summary>
        /// Trigger the creation of a new scenario in the service based on the name of the file that contains the scenario data
        /// </summary>
        /// <param name="fileName">The name of the file that contains the data of the new scenario</param>
        public void NewScenario(string fileName)
        {
            scenarioService.InitScenario(fileName);
        }

        /// <summary>
        /// Trigger the creation of a new scenario in the service based on the name of the file that contains the scenario data and launches it.
        /// </summary>
        /// <param name="fileName"></param>
        public void CreateAndLaunchScenario(string fileName)
        {
            NewScenario(fileName);
            LaunchScenario();
        }

        /// <summary>
        /// Verifies if the service already has a scenario.
        /// If it does, launches the current scenario.
        /// If not, create a new scenario using the file name in the controllers attributes and launches it.
        /// </summary>
        public void GeneralLaunchScenarioAction()
        {
            // No scriptable object Scenario already in the service
            if (scenarioService.GetScenario() == null)
            {
                Debug.Log("No scenario, starting research");
                CreateAndLaunchScenario(scenarioFileName);
            }
            // Start or Restart the current scenario
            else
            {
                LaunchScenario();
            }
            
        }
        
        /// <summary>
        /// Show all possible choices available for the current scenario node.
        /// </summary>
        public void ShowOptions()
        {
            // Clear the options from before
            ClearOptions();
            
            // For each option, show the option with its name and the action to do if its selected
            foreach (Link link in scenarioService.GetCurrentNode().links)
            {
                optionDisplayer.Create(link.name, () => TriggerLink(link.name, link.node));
            }
        }

        /// <summary>
        /// Show the text and the options of the current scenario node.
        /// </summary>
        public void ShowCurrentNode()
        {
            // Show the response to the previous speak bubble
            string response = ProcessText(scenarioService.GetCurrentNode().text);
            speakerTextDisplayer.Create(scenarioService.GetPropsState().speaker.name, response, scenarioService.GetPropsState().emotion.emotionName);
            
            // Show the new options
            ShowOptions();
        }

        /// <summary>
        /// The method that should be called when a link is activated. It displays the option selected as a speak bubble
        /// from the player then navigates to the target scenario node of the link before triggering its display at last.
        /// </summary>
        /// <param name="linkName">The name of the link triggered / option selected.</param>
        /// <param name="targetNode">The target node of the link. Will become the new current scenario node.</param>
        public void TriggerLink(string linkName, ScenarioNode targetNode)
        {
            // First show the name of the selected link as a speak bubble to keep track of the conversation
            Debug.Log("Displayed player choice");
            speakerTextDisplayer.Create(playerName, linkName, null);
            
            // Change the current node to the one the link points to
            Debug.Log("Change to Node : " + targetNode.name);
            scenarioService.GoToNode(targetNode);
            Debug.Log("Display new Node.");
            ShowCurrentNode();
        }
        
        /// <summary>
        /// Process the a text to remove unwanted text elements that were used to define the props and links.
        /// For that, uses the separator in the text : "\n--\n".
        /// </summary>
        /// <param name="rawText">The text to be parsed to remove the unwanted elements</param>
        /// <returns>A string without the props and links declaration</returns>
        private string ProcessText(string rawText)
        {
            return rawText.Split("\n--\n")[0];
        }

        /// <summary>
        /// Remove all speak bubbles connected to the current speak bubble displayer
        /// </summary>
        public void ClearSpeakBubbles()
        {
            speakerTextDisplayer.Clear();
        }
        
        /// <summary>
        /// Remove all options connected to the current option displayer
        /// </summary>
        public void ClearOptions()
        {
            optionDisplayer.Clear();
        }

        /// <summary>
        /// Remove all speak bubbles and options connected to the current speak bubble and option displayers
        /// </summary>
        public void ClearAll()
        {
            ClearSpeakBubbles();
            ClearOptions();
        }
        
        /// <summary>
        /// A general function the remove the visual elements connected to the displayers depending on the option value :
        /// - 0 : Clear All
        /// - 1 : Clear Speak Bubbles
        /// - 2 : Clear Options
        /// </summary>
        /// <param name="option"></param>
        public void Clear(int option)
        {
            // Clear All
            if (option == 0)
            {
                ClearAll();
            }
            // Clear Speak Bubbles
            else if (option == 1)
            {
                ClearSpeakBubbles();
            }
            // Clear Options
            else if (option == 2)
            {
                ClearOptions();
            }
        }

    }
}