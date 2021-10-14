using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameController : MonoBehaviour, IDisposable
    {
        public PlayerType PlayerType = PlayerType.Ball;
        private CameraController _cameraController;
        private InputController _inputController;
        private ListExecuteObject _interactiveObject;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;
        private Condition _condition;
        private int _countBonuses;
        private Reference _reference;

        private void Awake()
        {
            _interactiveObject = new ListExecuteObject();

            _reference = new Reference();

            PlayerBase player = null;
            if(PlayerType == PlayerType.Ball)
            {
                player = _reference.PlayerBall;
            }

            _cameraController = new CameraController(_reference.PlayerBall.transform, _reference.MainCamera.transform);
            _interactiveObject.AddExecuteObject(_cameraController);

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                _inputController = new InputController(_reference.PlayerBall);
                _interactiveObject.AddExecuteObject(_inputController);
            }

            _condition = new Condition(_reference.Condition);
            _displayEndGame = new DisplayEndGame(_reference.EndGame);
            _displayBonuses = new DisplayBonuses(_reference.Bonus);
            foreach(var o in _interactiveObject)
            {
                if(o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange += CaughtPlayer;
                    badBonus.OnCaughtPlayerChange += _displayEndGame.GameOver;
                }

                if(o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange += AddBonus;
                }
            }

            _reference.Restart.onClick.AddListener(RestartGame);
            _reference.Restart.gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }

        private void CaughtPlayer(string value, Color args)
        {
            _reference.Restart.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        private void AddBonus(int value)
        {
            _countBonuses += value;
            _displayBonuses.Display(_countBonuses);
        }

        private void Update()
        {
            _condition.Display();

            for (var i = 0; i < _interactiveObject.Length; i++)
            {
                var interactiveObject = _interactiveObject[i];

                if(interactiveObject == null)
                {
                    continue;
                }
                interactiveObject.Execute();
            }
        }

        public void Dispose()
        {
            foreach(var o in _interactiveObject)
            {
                if(o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange -= CaughtPlayer;
                    badBonus.OnCaughtPlayerChange -= _displayEndGame.GameOver;
                }

                if(o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange -= AddBonus;
                }
            }
        }
    }
}