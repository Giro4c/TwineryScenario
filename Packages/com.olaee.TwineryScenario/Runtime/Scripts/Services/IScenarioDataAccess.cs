using TwineryScenario.Runtime.Scripts.Core;

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
        /// <param name="folder">The path to the folder containing the file with the data.</param>
        /// <param name="fileName">The name of the file containing the scenario data.</param>
        /// <returns>The scenario retrieved or created based on the data in the file.</returns>
        public Scenario GetScenario(string folder, string fileName);

    }
}