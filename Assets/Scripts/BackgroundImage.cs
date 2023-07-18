using UnityEngine;

namespace Assets.Scripts
{
    class BackgroundImage
    {
        private int[,] _gameWindow = new int[gameWindowWidth, gameWindowHeight];
        private float _filledWater;
        private const int _waterSquare = (gameWindowWidth - 16) * (gameWindowHeight - 16);

        public const int pixelSize = 10;
        public const int gameWindowWidth = 64;
        public const int gameWindowHeight = 43;
        public const int wayColor = 1;
        public const int landColor = 0x00a8a8;
        public const int waterColor = 0;
        public const int trackColor = 0x901290;

        public BackgroundImage()
        {
            Reborn();
        }

        public int FilledArea()
        {
            return (int)Mathf.Round(100f - _filledWater / _waterSquare * 100);
        }

        public void Reborn()
        {
            for (int y = 0; y < gameWindowHeight; y++)
                for (int x = 0; x < gameWindowWidth; x++)
                    _gameWindow[x, y] = (x < 2 || x > gameWindowWidth - 3 || y < 2 || y > gameWindowHeight - 3)
                        ? landColor
                        : waterColor;
            _filledWater = _waterSquare;
        }

        public void SetPixelColor(int x, int y, int color) 
        {
            _gameWindow[x, y] = color;
        }

        public int GetPixelColor(int x, int y)
        {
            if (x < 0 || y < 0 || x > gameWindowWidth - 1 || y > gameWindowHeight - 1)
            {
                return waterColor;
            }

            return _gameWindow[x, y];
        }

        public void CahngeWayToWater()
        {
            for (int i = 0; i < gameWindowHeight; i++)
            {
                for (int j = 0; j < gameWindowWidth; j++)
                {
                    if (_gameWindow[j, i] == trackColor)
                    {
                        _gameWindow[j, i] = waterColor;
                    }
                }
            }
        }

        public void Fill()
        {
            _filledWater = 0;

            foreach (var waterEnemy in PlayMode.waterEnemies.GetWaterEnemies)
            {
                FillWay(waterEnemy.PositionX, waterEnemy.PositionY);
            }

            for (int i = 0; i < gameWindowHeight; i++)
            {
                for (int j = 0; j < gameWindowWidth; j++)
                {
                    if (_gameWindow[j, i] == trackColor || _gameWindow[j, i] == waterColor)
                    {
                        _gameWindow[j, i] = landColor;
                    }

                    if (_gameWindow[j, i] == wayColor)
                    {
                        _gameWindow[j, i] = waterColor;
                        _filledWater++;
                    }
                }
            }
        }

        private void FillWay(int x, int y)
        {
            if (_gameWindow[x, y] > waterColor)
            {
                return;
            }

            _gameWindow[x, y] = wayColor;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    FillWay(x + i, y + j);
                }
            }
        }

        public void Paint()
        {
            for (int i = 0; i < gameWindowHeight; i++)
            {
                for (int j = 0; j < gameWindowWidth; j++)
                {
                    PlayMode.playMode.texture.SetPixels(
                        j * pixelSize,
                        i * pixelSize,
                        pixelSize,
                        pixelSize,
                        PlayMode.InitializeColorArray(PlayMode.InitializeColor(_gameWindow[j, i]), pixelSize * pixelSize));
                }
            }
        }
    }
}
