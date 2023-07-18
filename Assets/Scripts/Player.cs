using UnityEngine;

namespace Assets.Scripts
{
    class Player
    {
        private int _health;
        private int _currentLevel;
        private int _x;
        private int _y;
        private int _direct;
        private bool _isWater;
        private bool _isDieBySelf;

        public const int upDirect = 38;
        public const int downDirect = 40;
        public const int rightDirect = 39;
        public const int leftDirect = 37;


        public Player()
        {
            _health = 3;
            _currentLevel = 1;

            Reborn();
        }

        public int Health { get { return _health; } }
        public int CurrentLevel { get { return _currentLevel; } }
        public bool IsDieBySelf { get { return _isDieBySelf; } }

        public int GetX() { return _x; }
        public int GetY() { return _y; }
        public void Damage() { _health--; }
        public void LevelUp() { _currentLevel++; }
        public void ChangeDirection(int direct) { _direct = direct; }

        public void Reborn()
        {
            _x = BackgroundImage.gameWindowWidth / 2;
            _y = 0;
            _direct = 0;

            _isWater = false;
        }
        public void Move()
        {
            if (_direct == upDirect) { _y--; }
            if (_direct == downDirect) { _y++; }
            if (_direct == rightDirect) { _x++; }
            if (_direct == leftDirect) { _x--; }

            BordersCheck();

            _isDieBySelf = PlayMode.backgroundImage.GetPixelColor(_x, _y) == BackgroundImage.trackColor;

            if (PlayMode.backgroundImage.GetPixelColor(_x, _y) == BackgroundImage.landColor && _isWater)
            {
                _direct = 0;
                _isWater = false;
                PlayMode.backgroundImage.Fill();
            }

            if (PlayMode.backgroundImage.GetPixelColor(_x, _y) == BackgroundImage.waterColor)
            {
                _isWater = true;
                PlayMode.backgroundImage.SetPixelColor(_x, _y, BackgroundImage.trackColor);
            }
        }

        private void BordersCheck()
        {
            if (_x > BackgroundImage.gameWindowWidth - 1) { _x = BackgroundImage.gameWindowWidth - 1; }
            if (_y > BackgroundImage.gameWindowHeight - 1) { _y = BackgroundImage.gameWindowHeight - 1; }

            if (_x < 0) { _x = 0; }
            if (_y < 0) { _y = 0; }
        }

        public void Paint()
        {
            PlayMode.playMode.texture.SetPixels(
                _x * BackgroundImage.pixelSize,
                _y * BackgroundImage.pixelSize,
                BackgroundImage.pixelSize,
                BackgroundImage.pixelSize,
                PlayMode.InitializeColorArray((PlayMode.backgroundImage.GetPixelColor(_x, _y) == BackgroundImage.landColor) ? PlayMode.InitializeColor(BackgroundImage.trackColor) : Color.white,
                BackgroundImage.pixelSize * BackgroundImage.pixelSize));

            PlayMode.playMode.texture.SetPixels(
                _x * BackgroundImage.pixelSize + 3,
                _y * BackgroundImage.pixelSize + 3,
                BackgroundImage.pixelSize - 6,
                BackgroundImage.pixelSize - 6,
                PlayMode.InitializeColorArray((PlayMode.backgroundImage.GetPixelColor(_x, _y) == BackgroundImage.landColor) ? Color.white : PlayMode.InitializeColor(BackgroundImage.trackColor),
                (BackgroundImage.pixelSize - 6) * (BackgroundImage.pixelSize - 6)));
        }

    }
}