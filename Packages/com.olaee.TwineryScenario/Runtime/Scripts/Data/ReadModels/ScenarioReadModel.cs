using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class ScenarioReadModel
    {
        public string name;
        public string startnode;
        public string creator;
        public string creatorversion;
        public string ifid;
        public ScenarioNodeReadModel[] passages;
    }
}