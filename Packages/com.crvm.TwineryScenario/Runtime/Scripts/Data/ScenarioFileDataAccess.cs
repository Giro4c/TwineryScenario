using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Data.ReadModels;
using Services;
using UnityEngine;

namespace Data
{
    public class ScenarioFileDataAccess : MonoBehaviour, IScenarioDataAccess
    {

        public string filePath = "";

        public void DebugAll()
        {
            DebugJSONScenario();
            DebugJSONScenarioNode();
            DebugJSONLink();
            DebugJSONPerson();
            DebugJSONNodeProps();
        }
        
        public void DebugJSONScenario()
        {
            Debug.Log("Debug Scenario -------------------");
            // Get String JSON content from file
            TextAsset textAsset = Resources.Load<TextAsset>("twinery-example");
            
            // Create a Scenario read model from the JSON string. Will later be converted into a Scenario object
            ScenarioReadModel readModel = JsonUtility.FromJson<ScenarioReadModel>(textAsset.text);
            Debug.Log(readModel);
            Debug.Log("Name : " + readModel.name);
            Debug.Log("Start Node : " + readModel.startnode);
            Debug.Log("Passages : " + readModel.passages);
            Debug.Log("Creator : " + readModel.creator);
            Debug.Log("IFID : " + readModel.ifid);
            Debug.Log("Creator Version : " + readModel.creatorversion);
        }
        public void DebugJSONScenarioNode()
        {
            Debug.Log("Debug ScenarioNode -------------------");
            // Get String JSON content from file
            TextAsset textAsset = Resources.Load<TextAsset>("node-example");
            
            // Create a ScenarioNode read model from the JSON string.
            ScenarioNodeReadModel readModel = JsonUtility.FromJson<ScenarioNodeReadModel>(textAsset.text);
            Debug.Log(readModel);
            Debug.Log("PID : " + readModel.pid);
            Debug.Log("Name : " + readModel.name);
            Debug.Log("Text : " + readModel.text);
            Debug.Log("Position : " + readModel.position);
            Debug.Log("Position X : " + readModel.position.x);
            Debug.Log("Position Y : " + readModel.position.y);
            Debug.Log("Props : " + readModel.props);
            Debug.Log("Props Emotion : " + readModel.props.emotion);
            Debug.Log("Props Speaker : " + readModel.props.speaker);
            Debug.Log("Links : " + readModel.links);
            Debug.Log("Links Length : " + readModel.links.Length);
        }
        public void DebugJSONPerson()
        {
            Debug.Log("Debug Person -------------------");
            // Get String JSON content from file
            TextAsset textAsset = Resources.Load<TextAsset>("person-example");
            
            // Create a Person read model from the JSON string.
            PersonReadModel readModel = JsonUtility.FromJson<PersonReadModel>(textAsset.text);
            Debug.Log(readModel);
            Debug.Log("ID : " + readModel.id);
            Debug.Log("Name : " + readModel.name);
        }
        public void DebugJSONNodeProps()
        {
            Debug.Log("Debug NodeProps -------------------");
            // Get String JSON content from file
            TextAsset textAsset = Resources.Load<TextAsset>("props-example");
            
            // Create a NodeProps read model from the JSON string.
            NodePropsReadModel readModel = JsonUtility.FromJson<NodePropsReadModel>(textAsset.text);
            Debug.Log(readModel);
            Debug.Log("Emotion : " + readModel.emotion);
            Debug.Log("Speaker : " + readModel.speaker);
            Debug.Log("Speaker ID : " + readModel.speaker.id);
            Debug.Log("Speaker Name : " + readModel.speaker.name);
        }
        public void DebugJSONLink()
        {
            Debug.Log("Debug Link -------------------");
            // Get String JSON content from file
            TextAsset textAsset = Resources.Load<TextAsset>("link-example");
            
            // Create a Link read model from the JSON string.
            LinkReadModel readModel = JsonUtility.FromJson<LinkReadModel>(textAsset.text);
            Debug.Log(readModel);
            Debug.Log("Name : " + readModel.name);
            Debug.Log("Node Name : " + readModel.link);
            Debug.Log("Node PID : " + readModel.pid);
        }
        
        public Scenario GetScenario(string fileName, Emotions emotions, ref Persons persons)
        {
            // Get String JSON content from file
            TextAsset scenarioTextAsset = Resources.Load<TextAsset>(filePath + fileName);

            // Create a Scenario read model from the JSON string. Will later be converted into a Scenario object
            ScenarioReadModel scenarioReadModel = JsonUtility.FromJson<ScenarioReadModel>(scenarioTextAsset.text);
            
            return ConvertReadModel(scenarioReadModel, emotions, ref persons);
        }

        private ScenarioNode[] ConvertToNodesNoLinks(ScenarioNodeReadModel[] nodesReadModels, Emotions emotions, ref Persons persons)
        {
            List<ScenarioNode> nodes = new List<ScenarioNode>();
            foreach (ScenarioNodeReadModel nodeReadModel in nodesReadModels)
            {
                NodePropsReadModel tmpProps = nodeReadModel.props;
                
                // Verify that the person in the props exists.
                    // Search By ID
                Person person = persons.GetPerson(int.Parse(tmpProps.speaker.id));
                    // Search by name
                // Person person = persons.GetPerson(tmpProps.speaker.name);

                    // If it does not exist in the list, creates a new person and adds it to the list
                if (!person)
                {
                    person = Person.CreatePerson(int.Parse(tmpProps.speaker.id), tmpProps.speaker.name);
                    persons.persons.Add(person);
                }
                
                // Create the node props
                NodeProps props = NodeProps.CreateNodeProps(emotions.GetEmotion(tmpProps.emotion), 
                    person);
                
                // Create the links list with no pointed nodes references
                Link[] links = ConvertToLinksNoNodes(nodeReadModel.links);
                
                // Create the node
                ScenarioNode node = ScenarioNode.CreateScenarioNode(
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

        private Link[] ConvertToLinksNoNodes(LinkReadModel[] linksReadModels)
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


        private void FillLinks(ref Link[] links, ScenarioNode[] nodes)
        {
            if (links == null) return;
            for (int index = 0; index < links.Length; ++index)
            {
                links[index].node = ScenarioNode.FindInArray(links[index].pidNode, nodes);
            }
        }
        
        private void FillLinks(ref ScenarioNode[] nodes)
        {
            if (nodes == null) return;
            foreach (ScenarioNode node in nodes)
            {
                FillLinks(ref node.links, nodes);
            }
        }

        public Scenario ConvertReadModel(ScenarioReadModel readModel, Emotions emotions, ref Persons persons)
        {
            // Scenario Nodes : passages
            // Initialized with no reference to other nodes in the links
            ScenarioNode[] nodes = ConvertToNodesNoLinks(readModel.passages, emotions, ref persons);
            // Fill the links with the associated reference to a ScenarioNode
            FillLinks(ref nodes);
            
            // Find the starting node
            ScenarioNode startNode = ScenarioNode.FindInArray(int.Parse(readModel.startnode), nodes);
            
            // Create the scenario
            Scenario scenario = Scenario.CreateScenario(
                readModel.name, 
                startNode, 
                readModel.creator, 
                readModel.ifid, 
                nodes
            );
            return scenario;
        }
        
    }
    
}