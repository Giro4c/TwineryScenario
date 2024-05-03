using System;
using TMPro;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Visuals
{
    /// <summary>
    /// An example implementation of a speak bubble displayer.
    /// </summary>
    public class SpeakerTextDisplayerExample : SpeakerTextDisplayer
    {

        /// <summary>
        /// The name of the GameObject whose component will be changed based on the speaker's name.
        /// </summary>
        [SerializeField] private string speakerNameContainer = "Name";
        
        /// <summary>
        /// The name of the GameObject whose component will be changed based on what the speaker said.
        /// </summary>
        [SerializeField] private string textContainer = "Text";

        public override void Create(string name, string text, string emotion)
        {
            // Instantiate the prefab
            GameObject obj = Instantiate(prefab);
            
            // Get All the text components
            TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            int indexSpeakerName = -1;
            int indexText = -1;

            // Verifies for each text mesh found if it is supposed to be the speaker's name, the dialog content or something else.
            for (int index = 0; index < texts.Length; ++index)
            {
                string objName = texts[index].gameObject.name;
                // The object's name match the name of the speaker name container
                if (objName == speakerNameContainer)
                {
                    indexSpeakerName = index;
                }
                // The object's name match the name of the speaker name container
                else if (objName == textContainer)
                {
                    indexText = index;
                }
                
            }

            // Verifies if the structure is correct and the speaker name and dialog content texts have been found
            if (indexSpeakerName == -1 || indexText == -1)
            {
                throw new Exception("Invalid prefab structure");
            }
            
            // Change the main text : Speaker name
            texts[indexSpeakerName].text = name + " (" + emotion + ")";
            // Change the secondary text : Speaker text
            texts[indexText].text = text;
            
            // Add the gameObject to the container
            AddGameObject(obj);
        }

    }
}