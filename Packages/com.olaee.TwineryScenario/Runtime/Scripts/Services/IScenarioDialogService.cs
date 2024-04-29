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
        // public void SetScenario(Scenario scenario);

    }
}