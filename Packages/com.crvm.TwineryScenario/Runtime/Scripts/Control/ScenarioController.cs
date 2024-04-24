using Services;
using UnityEngine;

namespace Control
{
    public class ScenarioController : MonoBehaviour
    {

        public ScenarioService scenarioService;
        public DisplayService displayService;

        public void LaunchScenario(string filePath)
        {
            scenarioService.LaunchScenario(filePath);
        }

        public void ClearSpeakBubbles()
        {
            displayService.ClearSpeakerTexts();
        }

        public void ClearAll()
        {
            displayService.ClearAll();
        }

    }
}