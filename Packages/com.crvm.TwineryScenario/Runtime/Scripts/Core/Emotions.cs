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

            return null;
        }

        public void Init(List<Emotion> emotions)
        {
            this.emotions = emotions;
        }

        public static Emotions CreateEmotionsList(List<Emotion> emotions)
        {
            Emotions emotionsList = ScriptableObject.CreateInstance<Emotions>();
            emotionsList.Init(emotions);
            return emotionsList;
        }
        
        public static Emotions CreateEmotionsList()
        {
            return CreateEmotionsList(new List<Emotion>());
        }
        
    }
}