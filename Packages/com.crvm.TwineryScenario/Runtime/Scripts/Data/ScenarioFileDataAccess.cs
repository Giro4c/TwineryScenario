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
        
        
        public Scenario GetScenario(string fileName, Emotions emotions, ref Persons persons)
        {
            // Get String JSON content from file
            TextAsset scenarioTextAsset = Resources.Load<TextAsset>(filePath + fileName);
            // Create a Scenario read model from the JSON string. Will later be converted into a Scenario object
            ScenarioReadModel scenarioReadModel = JsonUtility.FromJson<ScenarioReadModel>(scenarioTextAsset.text);

            // Scenario Nodes : passages
                // Initialized with no reference to other nodes in the links
            ScenarioNode[] nodes = ConvertToNodesNoLinks(scenarioReadModel.passages, emotions, ref persons);
                // Fill the links with the associated reference to a ScenarioNode
            FillLinks(ref nodes);
            
            // Find the starting node
            ScenarioNode startNode = ScenarioNode.FindInArray(int.Parse(scenarioReadModel.startnode), nodes);
            
            // Create the scenario
            Scenario scenario = Scenario.CreateScenario(
                scenarioReadModel.name, 
                startNode, 
                scenarioReadModel.creator, 
                scenarioReadModel.ifid, 
                nodes
            );
            return scenario;
        }

        private ScenarioNode[] ConvertToNodesNoLinks(ScenarioNodeReadModel[] nodesReadModels, Emotions emotions, ref Persons persons)
        {
            List<ScenarioNode> nodes = new List<ScenarioNode>();
            foreach (ScenarioNodeReadModel nodeReadModel in nodesReadModels)
            {
                
                NodePropsReadModel tmpProps = nodeReadModel.props;
                
                // Verify that the person in the props exists.
                Person person = persons.GetPerson(int.Parse(tmpProps.speaker.id));
                    // If it does not exist in th list, creates a new person and adds it to the list
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
            foreach (LinkReadModel linkReadModel in linksReadModels)
            {
                // Create the link
                Link link = Link.CreateLink(
                    linkReadModel.name,
                    int.Parse(linkReadModel.link),
                    null
                );
                
                // Add the new link to the list
                links.Add(link);
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
        
    }
    
}