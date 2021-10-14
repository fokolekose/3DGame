using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class Condition
    {
        private Text _condition;

        public Condition(GameObject condition)
        {
            _condition = condition.GetComponentInChildren<Text>();
            _condition.text = String.Empty;
        }

        public void Display()
        {
            _condition.text = $"7 бонусов = победа!";
        }
    }
}
