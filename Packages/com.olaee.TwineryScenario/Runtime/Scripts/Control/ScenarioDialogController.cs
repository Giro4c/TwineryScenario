using System;
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
    public class ScenarioDialogController : MonoBehaviour
    {

        /// <summary>
        /// An object implementing the interface IScenarioService that contains all use cases for a scenario.
        /// </summary>
        private IScenarioDialogService scenarioService;
        
        /// <summary>
        /// The Displayer related to the display of speak bubbles
        /// </summary>
        [SerializeField] private SpeakerTextDisplayer speakerTextDisplayer;
        
        /// <summary>
        /// The Displayer related to the display of the options available in a node
        /// </summary>
        [SerializeField] private OptionDisplayer optionDisplayer;

        /// <summary>
        /// The relative path of the folder containing the files. The root of this path is the Resources folder.
        /// </summary>
        [SerializeField] private string scenarioFolder = "";
        
        /// <summary>
        /// The name of the file containing the json of a scenario
        /// </summary>
        [SerializeField] private string scenarioFileName = "twinery-example2";
        
        /// <summary>
        /// The name of the player
        /// </summary>
        [SerializeField] private string playerName = "Player";

        private void Start()
        {
            scenarioService = ObjectFinder.FindObjectInScene<IScenarioDialogService>(SceneManager.GetActiveScene());
        }

        private void Awake()
        {
            if (scenarioService == null)
            {
                scenarioService = ObjectFinder.FindObjectInScene<IScenarioDialogService>(SceneManager.GetActiveScene());
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
        /// <param name="folder">The path to the folder containing the file with scenario data.</param>
        /// <param name="fileName">The name of the file that contains the data of the new scenario</param>
        public void NewScenario(string folder, string fileName)
        {
            scenarioService.InitScenario(folder, fileName);
        }

        /// <summary>
        /// Trigger the creation of a new scenario in the service based on the name of the file that contains the scenario data and launches it.
        /// </summary>
        /// <param name="folder">The path to the folder containing the file with scenario data.</param>
        /// <param name="fileName">The name of the file that contains the data of the new scenario</param>
        public void CreateAndLaunchScenario(string folder, string fileName)
        {
            NewScenario(folder, fileName);
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
                CreateAndLaunchScenario(scenarioFolder, scenarioFileName);
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
            BaseDialogProps props = scenarioService.GetCurrentNode().props as BaseDialogProps;
                // Verify that current props type match casting type
            if (props == null) throw new Exception("Incorrect props type : " + scenarioService.GetCurrentNode().props.GetType().Name + " not related to " + nameof(BaseDialogProps));
            speakerTextDisplayer.Create(props.speaker.name, response, props.emotion.emotionName);
            
            // Show the new options
            ShowOptions();
        }

        /// <summary>
        /// The method that should be called when a link is activated. It displays the option selected as a speak bubble
        /// from the player then navigates to the target scenario node of the link before triggering its display at last.
        /// </summary>
        /// <param name="linkName">The name of the link triggered / option selected.</param>
        /// <param name="targetNode">The target node of the link. Will become the new current scenario node.</param>
        public void TriggerLink(string linkName, Node targetNode)
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
        /// - 2 : Clear Options.
        /// </summary>
        /// <param name="option">Indicates which clear method to call.</param>
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

        /// <summary>
        /// Set the service that regroups all possible operations on a scenario
        /// </summary>
        /// <param name="service">The new service for the management of a scenario</param>
        public void SetScenarioService(IScenarioDialogService service)
        {
            scenarioService = service;
        }

        public void ChangeOptionDisplayer(OptionDisplayer optionDisplayer)
        {
            this.optionDisplayer = optionDisplayer;
        }

        public void ChangeSpeakBubbleDisplayer(SpeakerTextDisplayer speakerTextDisplayer)
        {
            this.speakerTextDisplayer = speakerTextDisplayer;
        }

        public void SetFolderSource(string folder)
        {
            scenarioFolder = folder;
        }

        public void SetFileSource(string file)
        {
            scenarioFileName = file;
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }

    }
}