using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class DisplayEndGame
    {
        private Text _finishGameLabel;
        private GameObject _finishLabel;

        public DisplayEndGame(GameObject endGame)
        {
            _finishGameLabel = endGame.GetComponentInChildren<Text>();
            _finishGameLabel.text = String.Empty;

            _finishLabel = endGame;
        }

        public void GameOver(string name, Color color)
        {
            _finishLabel.SetActive(true);
            _finishGameLabel.text = $"Вы проиграли. Вас убил {name} {color} цвета";
        }
    }
}
