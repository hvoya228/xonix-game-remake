using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InterfaceAndCanvas : MonoBehaviour
    {
        [SerializeField] Text _levelText;
        [SerializeField] Text _healthText;
        [SerializeField] Text _timerText;
        [SerializeField] Text _gameOverText;
        [SerializeField] RectTransform _restartButton;
        [SerializeField] RectTransform _pauseButton;

        private void SetLevelToText(int level)
        {
            _levelText.text = "Level: " + level.ToString();
        }

        private void SetHealthToText(int live)
        {
            _healthText.text = "Health: " + live.ToString();
        }

        private void SetTimerToText(int timer)
        {
            _timerText.text = timer.ToString();
        }

        private void SetGameOverToText()
        {
            if (PlayMode.gameOver.IsGameOver)
            {
                _gameOverText.gameObject.SetActive(true);
                _restartButton.gameObject.SetActive(true);
            }
        }

        public void OnPauseButton()
        {
            //pererobyty
            PlayMode.gameOver.Paused();
            if (PlayMode.gameOver.IsPaused)
                _pauseButton.GetChild(0).GetComponent<Text>().text = "Play";
            else
                _pauseButton.GetChild(0).GetComponent<Text>().text = "Pause";
        }

        public void OnRestartButton()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public void Start()
        {
            _gameOverText.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);
        }

        public void Update()
        {
            SetLevelToText(PlayMode.player.CurrentLevel);
            SetHealthToText(PlayMode.player.Health);
            SetHealthToText(PlayMode.player.Health);
            SetTimerToText(Convert.ToInt32(PlayMode.timer.Seconds));
            SetGameOverToText();
        }
    }
}