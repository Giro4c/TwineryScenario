using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Visuals
{
    /// <summary>
    /// An example implementation of an option displayer.
    /// </summary>
    public class OptionDisplayerExample : OptionDisplayer
    {

        public override void Create(string name, UnityAction doOnClick)
        {
            // Instantiate the prefab
            GameObject obj = Instantiate(prefab);
            // Get the button
            Button btn = obj.GetComponent<Button>();
            
            // Change the text : Option name
            btn.GetComponentInChildren<TextMeshProUGUI>().text = name;
            // Add an event listener for click event
            btn.onClick.AddListener(doOnClick);
            
            // Add the gameObject to the container
            AddGameObject(obj.gameObject);
        }

    }
}