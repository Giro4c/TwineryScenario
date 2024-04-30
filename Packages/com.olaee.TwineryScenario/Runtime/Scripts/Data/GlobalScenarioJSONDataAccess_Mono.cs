using System;
using System.Collections.Generic;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using TwineryScenario.Runtime.Scripts.Services;
using UnityEngine;

namespace TwineryScenario.Runtime.Scripts.Data
{
    /// <summary>
    /// A MonoBehavior class that allows to access and process scenario data in files based on the JSON data format.
    /// The class uses a simple class (no MonoBehavior or ScriptableObject) as an intermediate for methods implementation.
    /// Meaning that all implementations of IScenarioGlobalDataAccess in this class are calls of the implemented methods
    /// in the simple class.
    /// </summary>
    public class GlobalScenarioJSONDataAccess_Mono : MonoBehaviour, IScenarioGlobalDataAccess
    {
        
        /// <summary>
        /// The real global data access for scenario parsing. Contains the implementation for all methods in IScenarioGlobalDataAccess.
        /// </summary>
        private readonly GlobalScenarioJSONDataAccess m_DataAccess = new GlobalScenarioJSONDataAccess();


        public Scenario GetScenario(string folder, string fileName)
        {
            return m_DataAccess.GetScenario(folder, fileName);
        }

        public Emotions GetEmotions()
        {
            return m_DataAccess.GetEmotions();
        }

        public void SetEmotionsReference(Emotions emotions)
        {
            m_DataAccess.SetEmotionsReference(emotions);
        }

        public Persons GetPersons()
        {
            return m_DataAccess.GetPersons();
        }

        public void SetPersonsReferences(Persons persons)
        {
            m_DataAccess.SetPersonsReferences(persons);
        }
    }
    
}