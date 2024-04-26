using UnityEngine;

namespace Core
{
    /// <summary>
    /// A class representing an emotion with its name
    /// </summary>
    [CreateAssetMenu(fileName = "Emotion", menuName = "ScriptableObjects/Scenarios/Emotion", order = 1)]
    public class Emotion : ScriptableObject
    {

        /// <summary>
        /// The name of the emotion
        /// </summary>
        public string emotionName;

        /// <summary>
        /// Initialize the emotion with its name passed in the parameters
        /// </summary>
        /// <param name="emotionName">The name of the emotion</param>
        public void Init(string emotionName)
        {
            this.emotionName = emotionName;
        }

        /// <summary>
        /// Create and initialize the emotion with its name passed in the parameters
        /// </summary>
        /// <param name="emotionName">The name of the emotion</param>
        /// <returns>A new Emotion with its name initialized</returns>
        public static Emotion CreateEmotion(string emotionName)
        {
            Emotion emotion = ScriptableObject.CreateInstance<Emotion>();
            emotion.Init(emotionName);
            return emotion;
        }
        
    }
}