using System;
using Core;

namespace Services
{
    public interface IScenarioDataAccess
    {

        // public Scenario GetScenario();
        public Scenario GetScenario(string fileName, Emotions emotions, ref Persons persons);
        // public Tuple<Scenario, Person[]> GetScenarioAndPersons();

    }
}