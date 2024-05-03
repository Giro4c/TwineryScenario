using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
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

        public ScenarioReadModel()
        {
            
        }

        public ScenarioReadModel(string name, string startnode, string creator, string creatorversion, string ifid, NodeReadModel<T>[] passages)
        {
            this.name = name;
            this.startnode = startnode;
            this.creator = creator;
            this.creatorversion = creatorversion;
            this.ifid = ifid;
            this.passages = passages;
        }
    }
}