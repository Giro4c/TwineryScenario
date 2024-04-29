using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Core.ReadModels;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An interface determining the different interactions with a scenario data source.
    /// </summary>
    public interface IScenarioDataAccess
    {
        
        /// <summary>
        /// Retrieve a scenario in a file and fills the list of characters (persons) and emotions available.
        /// </summary>
        /// <param name="fileName">The name of the fila containing the scenario data</param>
        /// <returns>The scenario retrieved or created based on the data in the file.</returns>
        public Scenario GetScenario(string fileName);

        /// <summary>
        /// Retrieve the props factory in use which also contains any props type specific data such as
        /// Emotions or Persons that are related to Dialog Props.
        /// </summary>
        /// <returns></returns>
        public PropsFactory GetPropsFactory();

    }
}