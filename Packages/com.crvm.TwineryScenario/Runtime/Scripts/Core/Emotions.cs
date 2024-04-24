using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EmotionsList", menuName = "ScriptableObjects/Scenarios/EmotionsList", order = 1)]
    public class Emotions : ScriptableObject
    {

        public List<Emotion> emotions;
        
        public Emotion GetEmotion(string emotionName)
        {
            foreach (Emotion emotion in emotions)
            {
                if (emotion.emotionName == emotionName) return emotion;
            }

            return Emotion.None;
        }
        
    }
}