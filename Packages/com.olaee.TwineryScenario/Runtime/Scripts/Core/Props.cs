using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class that represents all additional elements in a scenario node.
    /// </summary>
    [CreateAssetMenu(fileName = "BaseProps", menuName = "ScriptableObjects/Scenarios/Props/Base", order = 1)]
    public class Props : ScriptableObject
    {

        public string type;

        public void Init(string type)
        {
            this.type = type;
        }

        public static Props CreateProps(string type)
        {
            Props props = CreateInstance<Props>();
            props.Init(type);
            return props;
        }

    }
}