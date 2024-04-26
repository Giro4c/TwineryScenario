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
    public class ScenarioFileDataAccess : MonoBehaviour, IScenarioDataAccess
    {

        /// <summary>
        /// The relative path of the folder containing the files. The root of this path is the Resources folder.
        /// </summary>
        public string filePath = "";

        private void DebugAll()
        {
            DebugJSONScenario();
            DebugJSONScenarioNode();
            DebugJSONLink();
            DebugJSONPerson();
            DebugJSONNodeProps();
        }
        private void DebugJSONScenario()
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
        private void DebugJSONScenarioNode()
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
        private void DebugJSONPerson()
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
        private void DebugJSONNodeProps()
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
        private void DebugJSONLink()
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

        /// <summary>
        /// Convert an array of scenario nodes read models into an array of scenario nodes though the links within the
        /// nodes do not yet point toward other nodes.
        /// </summary>
        /// <param name="nodesReadModels">The array of nodes read models used for the conversion</param>
        /// <param name="emotions">The list of available emotions</param>
        /// <param name="persons">The list of the persons who spoke in at least on of the nodes in the list.</param>
        /// <returns>An array of scenario nodes whose links don't point to other nodes yet.</returns>
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

        /// <summary>
        /// Converts an array of link read models into an array of links though the links do not yet point to any reference of a node.
        /// </summary>
        /// <param name="linksReadModels">The array of link read model used for the conversion.</param>
        /// <returns>An array of links that don't point to any node reference yet.</returns>
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

        /// <summary>
        /// Fill the node pointers of all the links in an array using an array of nodes that contains all the available node references.
        /// </summary>
        /// <param name="links">The array containing the links whose pointed nodes will be attributed.</param>
        /// <param name="nodes">The array of nodes available for references.</param>
        private void FillLinks(ref Link[] links, ScenarioNode[] nodes)
        {
            if (links == null) return;
            for (int index = 0; index < links.Length; ++index)
            {
                links[index].node = ScenarioNode.FindInArray(links[index].pidNode, nodes);
            }
        }
        
        /// <summary>
        /// For each node in the given array, fill the links' node pointers using said array as the list of reference nodes.
        /// </summary>
        /// <param name="nodes">The array of nodes whose links must be completed. Also used as a reference to complete said links.</param>
        private void FillLinks(ref ScenarioNode[] nodes)
        {
            if (nodes == null) return;
            foreach (ScenarioNode node in nodes)
            {
                FillLinks(ref node.links, nodes);
            }
        }

        /// <summary>
        /// Converts a scenario read model into a scenario and fills accordingly the list of persons and emotions available in this scenario.
        /// </summary>
        /// <param name="readModel">The scenario read model used for the conversion.</param>
        /// <param name="emotions">The list of available emotions to be filled.</param>
        /// <param name="persons">The list of persons to be filled.</param>
        /// <returns>An initialized instance of a scenario created through this conversion.</returns>
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