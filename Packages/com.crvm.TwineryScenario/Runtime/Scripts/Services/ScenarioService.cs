using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Services
{
    public class ScenarioService : MonoBehaviour
    {

        public IScenarioDataAccess scenarioDataAccess;
        public Scenario scenario;
        public NodeProps propsState;
        public ScenarioNode currentNode;
        public Emotions emotions;
        public Persons persons;

        private void Awake()
        {
            if (scenarioDataAccess == null)
            {
                scenarioDataAccess = ObjectFinder.FindObjectInScene<IScenarioDataAccess>(SceneManager.GetActiveScene());
            }
            Debug.Log(scenarioDataAccess);
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
                propsState = ScriptableObject.CreateInstance<NodeProps>();
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
            if (currentNode.props.emotion != Emotion.None)
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