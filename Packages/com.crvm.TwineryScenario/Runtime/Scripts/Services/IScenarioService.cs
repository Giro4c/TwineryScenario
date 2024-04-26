using Core;

namespace Services
{
    public interface IScenarioService
    {

        public void InitScenario(string fileName);
        public void LaunchScenario();
        public void GoToNode(int pidNode);
        public void GoToNode(ScenarioNode newCurrentNode);
        public bool HasReachedEnd();
        
        // Getters
        public Scenario GetScenario();
        public ScenarioNode GetCurrentNode();
        public NodeProps GetPropsState();
        public Persons GetPersonList();
        public Emotions GetEmotionList();
        
        // Setters
        // public void SetScenario(Scenario scenario);

    }
}