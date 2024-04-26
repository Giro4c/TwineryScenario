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
        
        public Person GetPerson(string name)
        {
            foreach (Person person in persons)
            {
                if (person.name == name) return person;
            }

            return null;
        }
        
        public void Init(List<Person> persons)
        {
            this.persons = persons;
        }

        public static Persons CreatePersonsList(List<Person> persons)
        {
            Persons emotionsList = ScriptableObject.CreateInstance<Persons>();
            emotionsList.Init(persons);
            return emotionsList;
        }
        
        public static Persons CreatePersonsList()
        {
            return CreatePersonsList(new List<Person>());
        }
        
    }
}