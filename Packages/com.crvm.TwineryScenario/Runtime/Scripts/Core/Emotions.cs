using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// A class storing a list of Emotion scriptable objects
    /// </summary>
    [CreateAssetMenu(fileName = "EmotionsList", menuName = "ScriptableObjects/Scenarios/EmotionsList", order = 1)]
    public class Emotions : ScriptableObject
    {

        /// <summary>
        /// The stored list of emotions
        /// </summary>
        public List<Emotion> emotions;
        
        /// <summary>
        /// Find an emotion in the stored list based on its name
        /// </summary>
        /// <param name="emotionName">The name of the searched emotion</param>
        /// <returns>Returns the found emotion within the list. If it's not in the list then returns null.</returns>
        public Emotion GetEmotion(string emotionName)
        {
            foreach (Emotion emotion in emotions)
            {
                if (emotion.emotionName == emotionName) return emotion;
            }

            return null;
        }

        /// <summary>
        /// Initialize the list container with an already existing list passed in the parameters
        /// </summary>
        /// <param name="emotions">The new stored list of emotions</param>
        public void Init(List<Emotion> emotions)
        {
            this.emotions = emotions;
        }

        /// <summary>
        /// Creates and initialize a Emotions scriptable object with a list of emotions passed in the parameters
        /// </summary>
        /// <param name="emotions">The new stored list of emotions</param>
        /// <returns>The created and initialized Emotions ScriptableObject</returns>
        public static Emotions CreateEmotionsList(List<Emotion> emotions)
        {
            Emotions emotionsList = ScriptableObject.CreateInstance<Emotions>();
            emotionsList.Init(emotions);
            return emotionsList;
        }
        
        /// <summary>
        /// Creates and initialize a Emotions scriptable object with an empty list of emotions
        /// </summary>
        /// <returns>The created and initialized Emotions ScriptableObject with an empty list</returns>
        public static Emotions CreateEmotionsList()
        {
            return CreateEmotionsList(new List<Emotion>());
        }
        
    }
}