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
    }
}