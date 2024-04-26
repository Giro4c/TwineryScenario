using TwineryScenario.Runtime.Scripts.Core;

namespace TwineryScenario.Runtime.Scripts.Services
{
    /// <summary>
    /// An interface that defines the uses case for managing a scenario
    /// </summary>
    public interface IScenarioService
    {

        /// <summary>
        /// Initialize the managed scenario (nodes, links, name, etc...) with the content of the file whose name is given
        /// </summary>
        /// <param name="fileName">The name of the file that contains the data of a scenario. Warning : Do not give the file extension (.txt, .json, etc...)</param>
        public void InitScenario(string fileName);
        
        /// <summary>
        /// Start the current scenario and initialize the player progress for this scenario
        /// </summary>
        public void LaunchScenario();
        
        /// <summary>
        /// Change the current node to the scenario node in the scenario passage list whose PID match with the one given. 
        /// </summary>
        /// <param name="pidNode">The PID of the new current node present in the current scenario.</param>
        public void GoToNode(int pidNode);
        
        /// <summary>
        /// Change the current node to the scenario node given and update the player progress.
        /// </summary>
        /// <param name="newCurrentNode">The new current scenario node.</param>
        public void GoToNode(ScenarioNode newCurrentNode);
        
        /// <summary>
        /// Checks if the player has reached the end of the scenario. An end is reached when the current node doesn't have any link/option available.
        /// </summary>
        /// <returns>True if an end is reached, False if not.</returns>
        public bool HasReachedEnd();
        
        // Getters ---------------------------
        
        /// <summary>
        /// Retrieve the current scenario.
        /// </summary>
        /// <returns>The current scenario.</returns>
        public Scenario GetScenario();
        
        /// <summary>
        /// Retrieve the current scenario node based on player progress.
        /// </summary>
        /// <returns>The current scenario node</returns>
        public ScenarioNode GetCurrentNode();
        
        /// <summary>
        /// Retrieve the current state of the props based on the player progress and the current node
        /// </summary>
        /// <returns>The current state of the props</returns>
        public NodeProps GetPropsState();
        
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
        
        // Setters
        // public void SetScenario(Scenario scenario);

    }
}