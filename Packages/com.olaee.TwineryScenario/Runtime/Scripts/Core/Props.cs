using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class that represents additional elements in a scenario node. This class acts as the basis for any specific type
    /// of props that could possibly contain more data.
    /// </summary>
    [CreateAssetMenu(fileName = "BaseProps", menuName = "ScriptableObjects/Scenarios/Props/Base", order = 1)]
    public class Props : ScriptableObject
    {

        /// <summary>
        /// The type of props. Allow to determine which additional data must be stored and can be accessed through casting
        /// the object to another Props class.
        /// </summary>
        public string type = "Base";

        /// <summary>
        /// Initialize the props with the value of its type.
        /// </summary>
        /// <param name="type">The type of the props which determines the other data the props could contain</param>
        public void Init(string type)
        {
            this.type = type;
        }

        /// <summary>
        /// Create and initialize a props with the given type.
        /// </summary>
        /// <param name="type">The type of the props which determines the other data the props could contain</param>
        /// <returns>An instance of the Props ScriptableObject initialized with the type value passed in the parameters.</returns>
        public static Props CreateProps(string type)
        {
            Props props = CreateInstance<Props>();
            props.Init(type);
            return props;
        }

    }
}