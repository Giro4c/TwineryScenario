using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class PersonReadModel
    {
        public string id;
        public string name;

        public PersonReadModel()
        {
            
        }

        public PersonReadModel(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}