using System;
using TMPro;
using UnityEngine;

namespace Visuals
{
    public abstract class SpeakerTextDisplayer : Displayer
    {

        public abstract void Create(string name, string text, string emotion);

    }
}