using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class GlobalPropsReadModel : PropsReadModel
    {
        
        // Base Dialog
        public string emotion;
        public string speaker;
        
        // Other
        public PersonReadModel person;

        public GlobalPropsReadModel()
        {
            
        }

        public GlobalPropsReadModel(string type, string emotion, string speaker, PersonReadModel person) : base(type)
        {
            this.emotion = emotion;
            this.speaker = speaker;
            this.person = person;
        }
    }
}