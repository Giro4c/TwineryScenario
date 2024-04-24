using UnityEngine;
using UnityEngine.Events;
using Visuals;

namespace Services
{
    public class DisplayService : MonoBehaviour
    {

        public SpeakerTextDisplayer speakerTextDisplayer;
        public OptionDisplayer optionDisplayer;

        public void ShowSpeakerText(string name, string text)
        {
            speakerTextDisplayer.Create(name, text);
        }

        public void ShowOption(string name, UnityAction action)
        {
            optionDisplayer.Create(name, action);
        }

        public void ClearSpeakerTexts()
        {
            speakerTextDisplayer.Clear();
        }

        public void ClearOptions()
        {
            optionDisplayer.Clear();
        }

        public void ClearAll()
        {
            ClearSpeakerTexts();
            ClearOptions();
        }

    }
}