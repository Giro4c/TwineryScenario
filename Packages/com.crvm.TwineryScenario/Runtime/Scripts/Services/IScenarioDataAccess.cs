using Core;

namespace Services
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
        /// <param name="emotions">The scriptable object that will contain the list of available emotions</param>
        /// <param name="persons">The scriptable object that will contain the list of characters/persons.</param>
        /// <returns>The scenario retrieved or created based on the data in the file.</returns>
        public Scenario GetScenario(string fileName, Emotions emotions, ref Persons persons);

    }
}