namespace Assets.Scripts
{
    class GameOver
    {
        private bool _gameOver;
        private bool _pause;

        public bool IsGameOver { get { return _gameOver; } set { _gameOver = value; } }
        public bool IsPaused { get { return _pause; } }

        public void GameOverCheck()
        {
            if (PlayMode.player.Health == 0)
            {
                _gameOver = true;
            }
        }

        public void Paused()
        {
            _pause = !_pause;
        }
    }
}
