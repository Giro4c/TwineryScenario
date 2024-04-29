using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Core
{
    /// <summary>
    /// A class representing a person in a scenario with its ID and its name
    /// </summary>
    [CreateAssetMenu(fileName = "Person", menuName = "ScriptableObjects/Scenarios/Person", order = 1)]
    public class Person : ScriptableObject
    {
        /// <summary>
        /// The identifier that allows to recognize a person within a scenario
        /// </summary>
        public int id;
        
        /// <summary>
        /// The full name of the person
        /// </summary>
        public string name;

        /// <summary>
        /// Initialize the person's characteristics with the values in the parameters
        /// </summary
        /// <param name="id">The identifier that allows to recognize a person within a scenario</param>
        /// <param name="name">The full name of the person</param>
        public void Init(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Creates a ScriptableObject instance of a Person and initializes it the values in the parameters
        /// </summary>
        /// <param name="id">The identifier that allows to recognize a person within a scenario</param>
        /// <param name="name">The full name of the person</param>
        /// <returns>A new instance of Person whose characteristics match those in the parameters</returns>
        public static Person CreatePerson(int id, string name)
        {
            Person person = ScriptableObject.CreateInstance<Person>();
            person.Init(id, name);
            return person;
        }
        
    }
}