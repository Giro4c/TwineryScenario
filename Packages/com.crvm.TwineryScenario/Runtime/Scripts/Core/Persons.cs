using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "PersonsList", menuName = "ScriptableObjects/Scenarios/PersonsList", order = 1)]
    public class Persons : ScriptableObject
    {

        public List<Person> persons;
        
        public Person GetPerson(int id)
        {
            foreach (Person person in persons)
            {
                if (person.id == id) return person;
            }

            return null;
        }
        
    }
}