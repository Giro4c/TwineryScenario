using System.Collections.Generic;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Core.ReadModels;
using TwineryScenario.Runtime.Scripts.Services;
using UnityEditor;
using UnityEngine;

namespace TwineryScenario.Editor.Scripts.ScenarioJSONParser
{
    public class ScenarioJSONParser : UnityEditor.Editor
    {

        private static PropsFactory factory = new PropsFactory();

        private class ScenarioAssets
        {
            public Scenario scenario;
            public string directoryScenario;
            
            public List<Node> nodes;
            public string directoryNodes;

            public List<BaseDialogProps> nodesProps;
            public string directoryProps;

            public List<Link> links;
            public string directoryLinks;

            public Persons persons;
            public string directoryPerson;
            public string directoryPersons;

            public Emotions emotions;
            public string directoryEmotion;
            public string directoryEmotions;


            public ScenarioAssets(string directoryScenario, string directoryNodes, string directoryProps, 
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
                
                scenario = null;
                nodes = new List<Node>();
                nodesProps = new List<BaseDialogProps>();
                links = new List<Link>();
                persons = Persons.CreatePersonsList();
                emotions = Emotions.CreateEmotionsList();
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
            ScenarioAssets assets = new ScenarioAssets(directoryScenario, directoryNodes, directoryProps, 
                directoryLinks, directoryPerson, directoryPersons, directoryEmotion, directoryEmotions);
            
            // Initialize the references in the props factory
            factory.personsList = assets.persons;
            factory.emotionsList = assets.emotions;
            
            // Convert the read model into a scenario
            Scenario scenario = ConvertReadModel(scenarioReadModel, assets);

            // Store All Assets
            StoreInAssetDatabase(assets);

            return scenario;
        }

        private static void StoreInAssetDatabase(ScenarioAssets assets)
        {
            string assetName = "";
            // Person
            foreach (Person person in assets.persons.persons)
            {
                assetName = person.name + "-" + person.id;
                StoreInAssetDatabase(person, assets.directoryPerson, assetName);
            }
            
            // Persons
            if (assets.persons.persons.Count != 0)
            {
                assetName = "PersonsList-" + assets.scenario.name;
                StoreInAssetDatabase(assets.persons, assets.directoryPersons, assetName);
            }
            
            // Emotion
            foreach (Emotion emotion in assets.emotions.emotions)
            {
                assetName = emotion.emotionName;
                StoreInAssetDatabase(emotion, assets.directoryEmotion, assetName);
            }
            
            // Emotions
            if (assets.emotions.emotions.Count != 0)
            {
                assetName = "EmotionsList-" + assets.scenario.name;
                StoreInAssetDatabase(assets.emotions, assets.directoryEmotions, assetName);
            }
            
            // NodeProps
            for (int i = 0; i < assets.nodesProps.Count; ++i)
            {
                assetName = assets.nodesProps[i].GetType().Name + "-" + assets.scenario.name + "-" + i;
                StoreInAssetDatabase(assets.nodesProps[i], assets.directoryProps, assetName);
            }
            
            // Link
            for (int i = 0; i < assets.links.Count; ++i)
            {
                assetName = "Link-" + assets.scenario.name + "-" + i;
                StoreInAssetDatabase(assets.links[i], assets.directoryLinks, assetName);
            }
            
            // ScenarioNode
            foreach (Node node in assets.nodes)
            {
                assetName = node.name + "-" + node.pid + "-" + assets.scenario.name;
                StoreInAssetDatabase(node, assets.directoryNodes, assetName);
            }
            
            // Scenario
            assetName = "Scenario-" + assets.scenario.name;
            StoreInAssetDatabase(assets.scenario, assets.directoryScenario, assetName);

        }

        public static void StoreInAssetDatabase(Object asset, string assetPath, string assetName)
        {
            AssetDatabase.CreateAsset(asset,assetPath + "/" + assetName +".asset");
        }

        private static Node[] ConvertReadModels(NodeReadModel<GlobalPropsReadModel>[] nodesReadModels, ScenarioAssets assets)
        {
            List<Node> nodes = new List<Node>();
            foreach (NodeReadModel<GlobalPropsReadModel> nodeReadModel in nodesReadModels)
            {
                GlobalPropsReadModel tmpProps = nodeReadModel.props;
                
                // Verify that the person in the props exists.
                    // Search By ID
                // Person person = assets.persons.GetPerson(int.Parse(tmpProps.speaker.id));
                    // Search by name
                // Person person = assets.persons.GetPerson(tmpProps.speaker.name);
                Person person = assets.persons.GetPerson(tmpProps.speaker);
                
                // If it does not exist in the list, creates a new person and adds it to the list
                if (!person)
                {
                    // person = Person.CreatePerson(int.Parse(tmpProps.speaker.id), tmpProps.speaker.name);
                    person = Person.CreatePerson(assets.persons.persons.Count, tmpProps.speaker);
                    // Store person in assets to save
                    assets.persons.persons.Add(person);
                }
                
                // Verify that the emotion in the props exists.
                Emotion emotion = assets.emotions.GetEmotion(tmpProps.emotion);
                // If it does not exist in the list, creates a new emotion and adds it to the list
                if (!emotion)
                {
                    emotion = Emotion.CreateEmotion(tmpProps.emotion);
                    // Store emotion in assets to save
                    assets.emotions.emotions.Add(emotion);
                }
                
                // Create the node props
                BaseDialogProps props = BaseDialogProps.CreateNodeProps(emotion, 
                    person);
                // Store node props in assets to save
                assets.nodesProps.Add(props);
                
                // Create the links list with no pointed nodes references
                Link[] links = ConvertReadModel(nodeReadModel.links, assets);
                
                // Create the node
                Node node = Node.CreateNode(
                    int.Parse(nodeReadModel.pid),
                    nodeReadModel.name,
                    int.Parse(nodeReadModel.position.x),
                    int.Parse(nodeReadModel.position.y),
                    nodeReadModel.text,
                    links,
                    props
                );
                
                // Add the new scenario node to the list
                nodes.Add(node);
            }

            return nodes.ToArray();
        }

        private static Link[] ConvertReadModel(LinkReadModel[] linksReadModels, ScenarioAssets assets)
        {
            List<Link> links = new List<Link>();
            if (linksReadModels != null)
            {
                foreach (LinkReadModel linkReadModel in linksReadModels)
                {
                    // Create the link
                    Link link = Link.CreateLink(
                        linkReadModel.name,
                        int.Parse(linkReadModel.pid),
                        null
                    );
                
                    // Add the new link to the list
                    links.Add(link);
                }
            }
            
            return links.ToArray();
        }

        private static void FillLinks(ref Link[] links, Node[] nodes)
        {
            if (links == null) return;
            for (int index = 0; index < links.Length; ++index)
            {
                links[index].node = Node.FindInArray(links[index].pidNode, nodes);
            }
        }
        
        private static void FillLinks(ref Node[] nodes, ScenarioAssets assets)
        {
            if (nodes == null) return;
            foreach (Node node in nodes)
            {
                FillLinks(ref node.links, nodes);
                // Store complete links (with nodes) in assets to save
                assets.links.AddRange(node.links);
            }
        }

        private static Scenario ConvertReadModel(ScenarioReadModel<GlobalPropsReadModel> readModel, ScenarioAssets assets)
        {
            // Scenario Nodes : passages
            // Initialized with no reference to other nodes in the links
            Node[] nodes = ConvertReadModels(readModel.passages, assets);
            // Fill the links with the associated reference to a ScenarioNode
            FillLinks(ref nodes, assets);
            
            // Find the starting node
            Node startNode = Node.FindInArray(int.Parse(readModel.startnode), nodes);
            
            // Store nodes in assets to save
            assets.nodes = new List<Node>(nodes);
            
            // Create the scenario
            Scenario scenario = Scenario.CreateScenario(
                readModel.name, 
                startNode, 
                readModel.creator, 
                readModel.ifid, 
                nodes
            );
            
            // Store scenario in assets to save
            assets.scenario = scenario;
            
            return scenario;
        }

    }
}