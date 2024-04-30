using TwineryScenario.Runtime.Scripts.Core;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An interface determining the different interactions with a dialog-based scenario data source.
    /// </summary>
    public interface IScenarioDialogDataAccess : IScenarioDataAccess
    {

        /// <summary>
        /// Retrieve the Emotions scriptable object referenced by the class and used for conversions
        /// </summary>
        /// <returns>The reference to the Emotions scriptable object used during a scenario parsing</returns>
        public Emotions GetEmotions();

        /// <summary>
        /// Set the reference to the list of emotions used for scenario parsing.
        /// </summary>
        /// <param name="emotions">The new reference for the list of emotions.</param>
        public void SetEmotionsReference(Emotions emotions);
        
        /// <summary>
        /// Retrieve the Persons scriptable object referenced by the class and used for conversions
        /// </summary>
        /// <returns>The reference to the Persons scriptable object used during a scenario parsing</returns>
        public Persons GetPersons();

        /// <summary>
        /// Set the reference to the list of persons used for scenario parsing.
        /// </summary>
        /// <param name="persons">The new reference for the list of persons.</param>
        public void SetPersonsReferences(Persons persons);

    }
}