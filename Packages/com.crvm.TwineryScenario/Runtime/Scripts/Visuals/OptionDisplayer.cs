using UnityEngine.Events;

namespace Visuals
{
    /// <summary>
    /// The abstract class that defines the base method to display a node option/link.
    /// </summary>
    public abstract class OptionDisplayer : Displayer
    {

        /// <summary>
        /// Create and add an option GameObject containing or not the name/content of the option and the action to do
        /// should the option be selected.
        /// </summary>
        /// <param name="name">The name/content of the option to be displayed.</param>
        /// <param name="doOnClick">The action to do if the option is selected.</param>
        public abstract void Create(string name, UnityAction doOnClick);

    }
}