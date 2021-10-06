using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class DisplayBonuses
    {
        private Text _bonuses;

        public DisplayBonuses(GameObject bonus)
        {
            _bonuses = bonus.GetComponentInChildren<Text>();
            _bonuses.text = String.Empty;
        }

        public void Display(int value)
        {
            _bonuses.text = $"Вы набрали {value}";
        }
    }
}
