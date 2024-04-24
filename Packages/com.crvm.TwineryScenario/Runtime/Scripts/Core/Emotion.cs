using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Emotion", menuName = "ScriptableObjects/Scenarios/Emotion", order = 1)]
    public class Emotion : ScriptableObject
    {

        public string emotionName;

        public void Init(string emotionName)
        {
            this.emotionName = emotionName;
        }

        public static Emotion CreateEmotion(string emotionName)
        {
            Emotion emotion = ScriptableObject.CreateInstance<Emotion>();
            emotion.Init(emotionName);
            return emotion;
        }

        public static readonly Emotion None = null;
        
        
    }
}