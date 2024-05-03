using TwineryScenario.Runtime.Scripts.Core;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An interface that defines the uses case for managing a scenario
    /// </summary>
    public interface IScenarioDialogService : IScenarioService
    {
        
        // Getters ---------------------------
        
        /// <summary>
        /// Retrieve the list of persons in current scenario
        /// </summary>
        /// <returns>The list of persons in the scenario</returns>
        public Persons GetPersonList();
        
        /// <summary>
        /// Retrieve the list of available emotions in the current scenario
        /// </summary>
        /// <returns>The list of available emotions in the scenario.</returns>
        public Emotions GetEmotionList();
        
        // Setters ----------------------------
        
        /// <summary>
        /// Set the list of emotions available in the current scenario
        /// </summary>
        /// <param name="emotions">A ScriptableObject list of emotions</param>
        public void SetEmotions(Emotions emotions);

        /// <summary>
        /// Set the list of persons available in the current scenario
        /// </summary>
        /// <param name="persons">A ScriptableObject list of persons</param>
        public void SetPersons(Persons persons);

    }
}