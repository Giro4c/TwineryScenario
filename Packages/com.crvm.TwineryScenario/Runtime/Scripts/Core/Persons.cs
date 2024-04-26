using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// A class storing a list of Person scriptable objects
    /// </summary>
    [CreateAssetMenu(fileName = "PersonsList", menuName = "ScriptableObjects/Scenarios/PersonsList", order = 1)]
    public class Persons : ScriptableObject
    {

        /// <summary>
        /// The stored list of persons
        /// </summary>
        public List<Person> persons;
        
        /// <summary>
        /// Find a person in the stored list based on its id
        /// </summary>
        /// <param name="id">The id of the searched person</param>
        /// <returns>Returns the found person within the list. If it's not in the list then returns null.</returns>
        public Person GetPerson(int id)
        {
            foreach (Person person in persons)
            {
                if (person.id == id) return person;
            }

            return null;
        }
        
        /// <summary>
        /// Initialize the list container with an already existing list passed in the parameters
        /// </summary>
        /// <param name="persons">The new stored list of persons</param>
        public void Init(List<Person> persons)
        {
            this.persons = persons;
        }

        /// <summary>
        /// Creates and initialize a Persons scriptable object with a list of persons passed in the parameters
        /// </summary>
        /// <param name="persons">The new stored list of persons</param>
        /// <returns>The created and initialized Persons ScriptableObject</returns>
        public static Persons CreatePersonsList(List<Person> persons)
        {
            Persons emotionsList = ScriptableObject.CreateInstance<Persons>();
            emotionsList.Init(persons);
            return emotionsList;
        }
        
        /// <summary>
        /// Creates and initialize a Persons scriptable object with an empty list of persons
        /// </summary>
        /// <returns>The created and initialized Persons ScriptableObject with an empty list</returns>
        public static Persons CreatePersonsList()
        {
            return CreatePersonsList(new List<Person>());
        }
        
    }
}