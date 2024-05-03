using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class BaseDialogPropsReadModel : PropsReadModel
    {
        public string emotion;
        public string speaker;

        public BaseDialogPropsReadModel()
        {
            
        }

        public BaseDialogPropsReadModel(string type, string emotion, string speaker) : base(type)
        {
            this.emotion = emotion;
            this.speaker = speaker;
        }
    }
}