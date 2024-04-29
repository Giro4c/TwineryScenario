using System;

namespace TwineryScenario.Runtime.Scripts.Core.ReadModels
{
    [Serializable]
    public class BaseDialogPropsReadModel : PropsReadModel
    {
        public string emotion;
        // public PersonReadModel speaker;
        public string speaker;
    }
}