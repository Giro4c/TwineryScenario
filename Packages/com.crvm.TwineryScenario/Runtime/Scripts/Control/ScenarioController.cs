using System;
using Core;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using Visuals;

namespace Control
{
    public class ScenarioController : MonoBehaviour
    {

        public IScenarioService scenarioService;
        
        public SpeakerTextDisplayer speakerTextDisplayer;
        public OptionDisplayer optionDisplayer;

        public string scenarioFileName = "twinery-example";
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

        public void LaunchScenario()
        {
            // Trigger the initialisation of the player progress in the scenario
            scenarioService.LaunchScenario();
            // Display the current scenario node : start node
            ShowCurrentNode();
        }

        public void NewScenario(string fileName)
        {
            scenarioService.InitScenario(fileName);
        }

        public void CreateAndLaunchScenario(string fileName)
        {
            NewScenario(fileName);
            LaunchScenario();
        }

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

        public void ShowCurrentNode()
        {
            // Show the response to the previous speak bubble
            string response = ProcessText(scenarioService.GetCurrentNode().text);
            speakerTextDisplayer.Create(scenarioService.GetPropsState().speaker.name, response, scenarioService.GetPropsState().emotion.emotionName);
            
            // Show the new options
            ShowOptions();
        }

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
        
        private string ProcessText(string rawText)
        {
            return rawText.Split("\n--\n")[0];
        }

        public void ClearSpeakBubbles()
        {
            speakerTextDisplayer.Clear();
        }
        
        public void ClearOptions()
        {
            optionDisplayer.Clear();
        }

        public void ClearAll()
        {
            ClearSpeakBubbles();
            ClearOptions();
        }
        
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