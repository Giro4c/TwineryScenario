using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Visuals
{
    public abstract class OptionDisplayer : Displayer
    {

        public abstract void Create(string name, UnityAction doOnClick);

    }
}