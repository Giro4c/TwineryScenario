using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class NodeReadModel<T> where T : PropsReadModel
    {
        public string pid;
        public string name;
        public PositionReadModel position;
        public string text;
        public LinkReadModel[] links;
        public T props;

        public NodeReadModel()
        {
            
        }

        public NodeReadModel(string pid, string name, PositionReadModel position, string text, LinkReadModel[] links, T props)
        {
            this.pid = pid;
            this.name = name;
            this.position = position;
            this.text = text;
            this.links = links;
            this.props = props;
        }
    }
}