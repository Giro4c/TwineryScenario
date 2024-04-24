using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class ScenarioService : MonoBehaviour
    {

        public IScenarioDataAccess scenarioDataAccess;
        public Scenario scenario;
        public NodeProps propsState;
        public ScenarioNode currentNode;
        public DisplayService displayService;
        public Emotions emotions;
        public Persons persons;
        public string playerName = "Player";

        private void Awake()
        {
            if (scenarioDataAccess == null)
            {
                IScenarioDataAccess[] array = Array.Empty<IScenarioDataAccess>();
            
                // Get all root gameObjects of the active scene
                List<GameObject> rootObjectsInScene = new List<GameObject>();
                Scene scene = SceneManager.GetActiveScene();
                scene.GetRootGameObjects(rootObjectsInScene);
            
                // Look for all IdentifiableRestrictable classes in the scene
                foreach (GameObject o in rootObjectsInScene)
                {
                    IScenarioDataAccess[] added = o.GetComponentsInChildren<IScenarioDataAccess>();
                    array = array.Concat(added).ToArray();
                }

                if (array.Length > 0)
                {
                    scenarioDataAccess = array[0];
                }
                
            }
        }

        public void LaunchScenario(string name)
        {
            if (scenario == null)
            {
                RetrieveNewScenario(name);
            }

            currentNode = scenario.startNode;
            propsState = NodeProps.CreateNodeProps(currentNode.props.emotion, currentNode.props.speaker);

            ShowCurrentNode();
            
        }
        
        public void RetrieveNewScenario(string name)
        {
            persons.persons.Clear();
            scenario = scenarioDataAccess.GetScenario(name, emotions, ref persons);
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
            Debug.Log(currentNode.name);
            // Change the emotion only if it is precised
            if (currentNode.props.emotion != Emotion.None)
            {
                propsState.emotion = currentNode.props.emotion;
            }
            Debug.Log(propsState.emotion);
            // Always change the speaker to match the current in the new Node
            propsState.speaker = currentNode.props.speaker;
            Debug.Log(propsState.speaker);
        }

        public void TriggerLink(string linkName, ScenarioNode targetNode)
        {
            // First show the name of the selected link as a speak bubble to keep track of the conversation
            Debug.Log("Displayed player choice");
            displayService.ShowSpeakerText(playerName, linkName);
            // Change the current node to the one the link points to
            Debug.Log("Change to Node : " + targetNode.name);
            GoToNode(targetNode);
            Debug.Log("Display new Node.");
            ShowCurrentNode();
        }

        public void ShowCurrentNode()
        {
            // Show the response to the previous speak bubble
            string response = ProcessText(currentNode.text);
            displayService.ShowSpeakerText(propsState.speaker.name, response);
            
            // Show the new options
            ShowOptions();
        }

        private string ProcessText(string rawText)
        {
            return rawText.Split("\n--\n")[0];
        }

        public void ShowOptions()
        {
            // Clear the options from before
            displayService.ClearOptions();
            
            // For each option, show the option with its name and the action to do if its selected
            foreach (Link link in currentNode.links)
            {
                displayService.ShowOption(link.name, () => TriggerLink(link.name, link.node));
            }
            
        }

        public void Clear(int option)
        {
            // Clear All
            if (option == 0)
            {
                displayService.ClearAll();
            }
            // Clear Speak Bubbles
            else if (option == 1)
            {
                displayService.ClearSpeakerTexts();
            }
            // Clear Options
            else if (option == 2)
            {
                displayService.ClearOptions();
            }
        }
        
    }
}