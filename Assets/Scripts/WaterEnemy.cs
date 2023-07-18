using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class WaterEnemy
    {
        private int _x;
        private int _y;
        private int _directX;
        private int _directY;

        public WaterEnemy()
        {
            do
            {
                _x = Random.Range(0, BackgroundImage.gameWindowWidth);
                _y = Random.Range(0, BackgroundImage.gameWindowHeight);
            }
            while (PlayMode.backgroundImage.GetPixelColor(_x, _y) > BackgroundImage.waterColor);

            DirectionChange();
        }

        public int PositionX { get { return _x; } }
        public int PositionY { get { return _y; } }

        private void DirectionChange()
        {
            _directX = Random.Range(0, 1) == 0 ? 1 : -1;
            _directY = Random.Range(0, 1) == 0 ? 1 : -1;
        }

        private void LandCollision()
        {
            if (PlayMode.backgroundImage.GetPixelColor(_x + _directX, _y) == BackgroundImage.landColor)
            {
                _directX = -_directX;
            }

            if (PlayMode.backgroundImage.GetPixelColor(_x, _y + _directY) == BackgroundImage.landColor)
            {
                _directY = -_directY;
            }
        }

        public void Move()
        {
            LandCollision();
            _x += _directX;
            _y += _directY;
        }

        public bool IsHitPlayerOrWay()
        {
            LandCollision();

            if (PlayMode.backgroundImage.GetPixelColor(_x + _directX, _y + _directY) == BackgroundImage.trackColor)
            {
                return true;
            }

            if (_x + _directX == PlayMode.player.GetX() && _y + _directY == PlayMode.player.GetY())
            {
                return true;
            }

            return false;
        }

        public void Paint()
        {
            PlayMode.playMode.texture.SetPixels(_x * BackgroundImage.pixelSize, _y * BackgroundImage.pixelSize, BackgroundImage.pixelSize, BackgroundImage.pixelSize, PlayMode.InitializeColorArray(Color.white, BackgroundImage.pixelSize * BackgroundImage.pixelSize));
            PlayMode.playMode.texture.SetPixels(_x * BackgroundImage.pixelSize + 2, _y * BackgroundImage.pixelSize + 2, BackgroundImage.pixelSize - 4, BackgroundImage.pixelSize - 4, PlayMode.InitializeColorArray(PlayMode.InitializeColor(BackgroundImage.landColor), (BackgroundImage.pixelSize - 4) * (BackgroundImage.pixelSize - 4)));
        }
    }

    class WaterEnemies
    {
        private List<WaterEnemy> _waterEnemies = new List<WaterEnemy>();

        public WaterEnemies()
        {
            AddWaterEnemy();
        }

        public List<WaterEnemy> GetWaterEnemies { get { return _waterEnemies; } }

        public void Move()
        {
            foreach (var waterEnemy in _waterEnemies)
            {
                waterEnemy.Move();
            }
        }

        public void AddWaterEnemy()
        {
            _waterEnemies.Add(new WaterEnemy());
        }

        public bool IsHitPlayerOrWay()
        {
            foreach (var waterEnemy in _waterEnemies)
            {
                if (waterEnemy.IsHitPlayerOrWay())
                {
                    return true;
                }
            }

            return false;
        }

        public void Paint()
        {
            foreach (var waterEnemy in _waterEnemies)
            {
                waterEnemy.Paint();
            }
        }

    }
}