using System;
using TMPro;
using UnityEngine;

namespace Visuals
{
    public class SpeakerTextDisplayer : Displayer
    {

        public string speakerNameContainer = "Name";
        public string textContainer = "Text";
        

        public void Create(string name, string text)
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
                Debug.Log(objName);
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
            Debug.Log("Index Speaker Name : " + indexSpeakerName);
            Debug.Log("Index Text Content : " + indexText);

            // Verifies if the structure is correct and the speaker name and dialog content texts have been found
            if (indexSpeakerName == -1 || indexText == -1)
            {
                throw new Exception("Invalid prefab structure");
            }
            
            // Change the main text : Speaker name
            texts[indexSpeakerName].text = name;
            // Change the secondary text : Speaker text
            texts[indexText].text = text;
            
            // Add the gameObject to the container
            AddGameObject(obj);
        }

    }
}