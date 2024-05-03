using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class PropsReadModel
    {
        public string type;

        public PropsReadModel()
        {
            type = "Base";
        }

        public PropsReadModel(string type)
        {
            this.type = type;
        }
    }
}