using System;
using SpaceInvaders.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvaders.View
{
    public class ScoreView: MonoBehaviour
    {
        [SerializeField] private Text _scoreLabel;

        [Inject] private IGameNotifications _gameNotifications;
        [Inject] private IGameStateProvider _gameStateProviders;

        public void OnEnable()
        {
            _gameNotifications.ScoreChanged += ScoreChanged;
        }
        
        public void OnDisable()
        {
            _gameNotifications.ScoreChanged -= ScoreChanged;
        }

        private void ScoreChanged(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}