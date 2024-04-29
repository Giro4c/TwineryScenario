using System;

namespace TwineryScenario.Runtime.Scripts.Core.ReadModels
{
    [Serializable]
    public class ScenarioReadModel<T> where T : PropsReadModel
    {
        public string name;
        public string startnode;
        public string creator;
        public string creatorversion;
        public string ifid;
        public NodeReadModel<T>[] passages;
    }
}