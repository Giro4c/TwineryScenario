using System;
using System.Collections.Generic;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using TwineryScenario.Runtime.Scripts.Services;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Data
{
    /// <summary>
    /// A class that allows to access and process scenario data in files based on the JSON data format.
    /// </summary>
    public class GlobalScenarioJSONDataAccess : IScenarioGlobalDataAccess
    {

        private readonly PropsFactory m_Factory = new PropsFactory();

        
        public Scenario GetScenario(string folder, string fileName)
        {
            // Get String JSON content from file
            string path = GetCorrectPath(folder, fileName);
            TextAsset scenarioTextAsset = Resources.Load<TextAsset>(path);

            if (scenarioTextAsset == null) throw new Exception("File not found at path : " + path);

            // Clear lists in props factory
            m_Factory.Clear();
            
            // Create a Scenario read model from the JSON string. Will later be converted into a Scenario object
            ScenarioReadModel<GlobalPropsReadModel> scenarioReadModel = JsonUtility.FromJson<ScenarioReadModel<GlobalPropsReadModel>>(scenarioTextAsset.text);
            
            return ConvertReadModel(scenarioReadModel);
        }

        /// <summary>
        /// Verify that location variables are correct and change the initial path in case of wrong value
        /// </summary>
        /// <param name="folder">The path to the folder containing the file with the data.</param>
        /// <param name="fileName">The name of the file containing the scenario data.</param>
        /// <returns>The formatted path to the scenario file</returns>
        public static string GetCorrectPath(string folder, string fileName)
        {
            string path = "";
            // Verify that root folder name is not empty
            if (!String.IsNullOrEmpty(folder))
            {
                path += folder + "/";
            }
            // Add file name to the path
            path += fileName;
            
            return path;
        }

        /// <summary>
        /// Convert an array of scenario nodes read models into an array of scenario nodes though the links within the
        /// nodes do not yet point toward other nodes.
        /// </summary>
        /// <param name="nodesReadModels">The array of nodes read models used for the conversion</param>
        /// <returns>An array of scenario nodes whose links don't point to other nodes yet.</returns>
        public Node[] ConvertToNodesNoLinks(NodeReadModel<GlobalPropsReadModel>[] nodesReadModels)
        {
            List<Node> nodes = new List<Node>();
            foreach (NodeReadModel<GlobalPropsReadModel> nodeReadModel in nodesReadModels)
            {
                Props props = ConvertToProps(nodeReadModel.props);
                
                // Create the links list with no pointed nodes references
                Link[] links = ConvertToLinksNoNodes(nodeReadModel.links);
                
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

        /// <summary>
        /// Convert a props read model into a props.
        /// </summary>
        /// <param name="propsReadModel">The props read model used for the conversion</param>
        /// <returns>A props that contains additional data of a scenario node.</returns>
        public Props ConvertToProps(GlobalPropsReadModel propsReadModel)
        {
            return m_Factory.ConvertReadModel(propsReadModel);
        }

        /// <summary>
        /// Converts an array of link read models into an array of links though the links do not yet point to any reference of a node.
        /// </summary>
        /// <param name="linksReadModels">The array of link read model used for the conversion.</param>
        /// <returns>An array of links that don't point to any node reference yet.</returns>
        public Link[] ConvertToLinksNoNodes(LinkReadModel[] linksReadModels)
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

        /// <summary>
        /// Fill the node pointers of all the links in an array using an array of nodes that contains all the available node references.
        /// </summary>
        /// <param name="links">The array containing the links whose pointed nodes will be attributed.</param>
        /// <param name="nodes">The array of nodes available for references.</param>
        public void FillLinks(ref Link[] links, Node[] nodes)
        {
            if (links == null) return;
            for (int index = 0; index < links.Length; ++index)
            {
                links[index].node = Node.FindInArray(links[index].pidNode, nodes);
            }
        }
        
        /// <summary>
        /// For each node in the given array, fill the links' node pointers using said array as the list of reference nodes.
        /// </summary>
        /// <param name="nodes">The array of nodes whose links must be completed. Also used as a reference to complete said links.</param>
        public void FillLinks(ref Node[] nodes)
        {
            if (nodes == null) return;
            foreach (Node node in nodes)
            {
                FillLinks(ref node.links, nodes);
            }
        }
        
        /// <summary>
        /// Convert an array of scenario nodes read models into an array of scenario nodes with complete links.
        /// </summary>
        /// <param name="nodesReadModels">The array of nodes read models used for the conversion</param>
        /// <returns>An array of scenario nodes whose links are complte.</returns>
        public Node[] ConvertToNodes(NodeReadModel<GlobalPropsReadModel>[] nodesReadModels)
        {
            // Initialized with no reference to other nodes in the links
            Node[] nodes = ConvertToNodesNoLinks(nodesReadModels);
            // Fill the links with the associated reference to a ScenarioNode
            FillLinks(ref nodes);

            return nodes;
        }

        /// <summary>
        /// Converts a scenario read model into a scenario and fills accordingly the list of persons and emotions available in this scenario.
        /// </summary>
        /// <param name="readModel">The scenario read model used for the conversion.</param>
        /// <returns>An initialized instance of a scenario created through this conversion.</returns>
        public Scenario ConvertReadModel(ScenarioReadModel<GlobalPropsReadModel> readModel)
        {
            // Scenario Nodes : passages
            Node[] nodes = ConvertToNodes(readModel.passages);
            
            // Find the starting node
            Node startNode = Node.FindInArray(int.Parse(readModel.startnode), nodes);
            
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

        public Emotions GetEmotions()
        {
            return m_Factory.emotionsList;
        }

        public void SetEmotionsReference(Emotions emotions)
        {
            m_Factory.emotionsList = emotions;
        }

        public Persons GetPersons()
        {
            return m_Factory.personsList;
        }

        public void SetPersonsReferences(Persons persons)
        {
            m_Factory.personsList = persons;
        }

        public PropsFactory GetPropsFactory()
        {
            return m_Factory;
        }
        
    }
    
}