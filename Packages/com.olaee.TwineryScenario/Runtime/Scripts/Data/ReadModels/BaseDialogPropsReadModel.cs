using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class BaseDialogPropsReadModel : PropsReadModel
    {
        public string emotion;
        // public PersonReadModel speaker;
        public string speaker;
    }
}