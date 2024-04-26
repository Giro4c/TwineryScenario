namespace Visuals
{
    /// <summary>
    /// The abstract class that defines the base method to display a speak bubble.
    /// </summary>
    public abstract class SpeakerTextDisplayer : Displayer
    {

        /// <summary>
        /// Create and add a speak bubble GameObject containing or not the name of the speaker, what the speaker said
        /// and the emotion the speaker feels.
        /// </summary>
        /// <param name="name">The name of the speaker.</param>
        /// <param name="text">What the speaker says.</param>
        /// <param name="emotion">The emotion the speaker relays.</param>
        public abstract void Create(string name, string text, string emotion);

    }
}