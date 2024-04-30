using System.Collections.Generic;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEditor;
using UnityEngine;

namespace TwineryScenario.Editor.Scripts.ScenarioJSONParser
{
    public class ScenarioJSONParser : UnityEditor.Editor
    {

        private static GlobalScenarioJSONDataAccess _dataAccess = new GlobalScenarioJSONDataAccess();

        private class ScenarioDirectories
        {
            public string directoryScenario;
            public string directoryNodes;
            public string directoryProps;
            public string directoryLinks;
            public string directoryPerson;
            public string directoryPersons;
            public string directoryEmotion;
            public string directoryEmotions;


            public ScenarioDirectories(string directoryScenario, string directoryNodes, string directoryProps, 
                string directoryLinks, string directoryPerson, string directoryPersons, 
                string directoryEmotion, string directoryEmotions)
            {
                this.directoryScenario = directoryScenario;
                this.directoryNodes = directoryNodes;
                this.directoryProps = directoryProps;
                this.directoryLinks = directoryLinks;
                this.directoryPerson = directoryPerson;
                this.directoryPersons = directoryPersons;
                this.directoryEmotion = directoryEmotion;
                this.directoryEmotions = directoryEmotions;
            }
            
        }
        
        public static Scenario ParseScenario(TextAsset scenarioTextAsset, string directoryScenario, string directoryNodes, 
            string directoryProps, string directoryLinks, string directoryPerson, string directoryPersons, 
            string directoryEmotion, string directoryEmotions)
        {
            // Create a Scenario read model from the JSON string. Will later be converted into a Scenario object
            ScenarioReadModel<GlobalPropsReadModel> scenarioReadModel = JsonUtility.FromJson<ScenarioReadModel<GlobalPropsReadModel>>(scenarioTextAsset.text);
            
            // Create the empty lists that contain all the different scriptable object.
            // Allow to avoid recreating scriptable objects that have the same information
            ScenarioDirectories directories = new ScenarioDirectories(directoryScenario, directoryNodes, directoryProps, 
                directoryLinks, directoryPerson, directoryPersons, directoryEmotion, directoryEmotions);
            
            // Initialize the references in the props factory
            _dataAccess.SetEmotionsReference(Emotions.CreateEmotionsList());
            _dataAccess.SetPersonsReferences(Persons.CreatePersonsList());
            
            // Convert the read model into a scenario
            Scenario scenario = _dataAccess.ConvertReadModel(scenarioReadModel);

            // Store All Assets
            StoreInAssetDatabase(scenario, directories);

            return scenario;
        }

        private static void StoreInAssetDatabase(Scenario scenario, ScenarioDirectories directories)
        {
            string assetName = "";
            // Person
            foreach (Person person in _dataAccess.GetPersons().persons)
            {
                assetName = person.name + "-" + person.id;
                StoreInAssetDatabase(person, directories.directoryPerson, assetName);
            }
            
            // Persons
            if (_dataAccess.GetPersons().persons.Count != 0)
            {
                assetName = "PersonsList-" + scenario.name;
                StoreInAssetDatabase(_dataAccess.GetPersons(), directories.directoryPersons, assetName);
            }
            
            // Emotion
            foreach (Emotion emotion in _dataAccess.GetEmotions().emotions)
            {
                assetName = emotion.emotionName;
                StoreInAssetDatabase(emotion, directories.directoryEmotion, assetName);
            }
            
            // Emotions
            if (_dataAccess.GetEmotions().emotions.Count != 0)
            {
                assetName = "EmotionsList-" + scenario.name;
                StoreInAssetDatabase(_dataAccess.GetEmotions(), directories.directoryEmotions, assetName);
            }
            
            // Initialize list for all props and links since they must be numbered to be identifiable
            List<Props> propsList = new List<Props>();
            List<Link> linksList = new List<Link>();
            
            // Node
            foreach (Node node in scenario.passages)
            {
                // Add to props and links lists
                propsList.Add(node.props);
                linksList.AddRange(node.links);
                
                // Store node
                assetName = node.name + "-" + node.pid + "-" + scenario.name;
                StoreInAssetDatabase(node, directories.directoryNodes, assetName);
            }
            
            // NodeProps
            for (int i = 0; i < propsList.Count; ++i)
            {
                assetName = propsList[i].GetType().Name + "-" + scenario.name + "-" + i;
                StoreInAssetDatabase(propsList[i], directories.directoryProps, assetName);
            }
            
            // Link
            for (int i = 0; i < linksList.Count; ++i)
            {
                assetName = "Link-" + scenario.name + "-" + i;
                StoreInAssetDatabase(linksList[i], directories.directoryLinks, assetName);
            }
            
            
            
            // Scenario
            assetName = "Scenario-" + scenario.name;
            StoreInAssetDatabase(scenario, directories.directoryScenario, assetName);

        }

        public static void StoreInAssetDatabase(Object asset, string assetPath, string assetName)
        {
            string path = "";
            if (assetPath != "")
            {
                path += assetPath + "/";
            }
            path += assetName + ".asset";
            
            AssetDatabase.CreateAsset(asset, path);
        }

    }
}