using System.Collections.Generic;

namespace Assets.Scripts
{
    class LandEnemy
    {
        private int _x;
        private int _y;
        private int _directX;
        private int _directY;

        public LandEnemy()
        {
            _x = 1;
            _y = 1;
            _directX = 1;
            _directY = -1;
        }

        private void ChangeDirection()
        {
            if (PlayMode.backgroundImage.GetPixelColor(_x + _directX, _y) == BackgroundImage.waterColor)
            {
                _directX = -_directX;
            }

            if (PlayMode.backgroundImage.GetPixelColor(_x, _y + _directY) == BackgroundImage.waterColor)
            {
                _directY = -_directY;
            }
        }

        public void Move()
        {
            ChangeDirection();
            _x += _directX;
            _y += _directY;
        }

        public bool IsHitPlayer()
        {
            ChangeDirection();

            if (_x + _directX == PlayMode.player.GetX() && _y + _directY == PlayMode.player.GetY())
            {
                return true;
            }

            return false;
        }

        public void Paint()
        {
            PlayMode.playMode.texture.SetPixels(
                _x * BackgroundImage.pixelSize,
                _y * BackgroundImage.pixelSize,
                BackgroundImage.pixelSize,
                BackgroundImage.pixelSize,
                PlayMode.InitializeColorArray(PlayMode.InitializeColor(BackgroundImage.waterColor),
                BackgroundImage.pixelSize * BackgroundImage.pixelSize));

            PlayMode.playMode.texture.SetPixels(_x * BackgroundImage.pixelSize + 2,
                _y * BackgroundImage.pixelSize + 2, BackgroundImage.pixelSize - 4,
                BackgroundImage.pixelSize - 4, PlayMode.InitializeColorArray(PlayMode.InitializeColor(BackgroundImage.landColor),
                (BackgroundImage.pixelSize - 4) * (BackgroundImage.pixelSize - 4)));
        }
    }

    class LandEnemies
    {
        private List<LandEnemy> _landEnemies = new List<LandEnemy>();

        public LandEnemies()
        {
            AddLandEnemy();
        }

        public void AddLandEnemy()
        {
            _landEnemies.Add(new LandEnemy());
        }

        public void Move()
        {
            foreach (var landEnemy in _landEnemies)
            {
                landEnemy.Move();
            }
        }

        public bool IsHitPlayer()
        {
            foreach (var landEnemy in _landEnemies)
            {
                if (landEnemy.IsHitPlayer())
                {
                    return true;
                }
            }

            return false;
        }

        public void Paint()
        {
            foreach (var landEnemy in _landEnemies)
            {
                landEnemy.Paint();
            }
        }
    }
}
