using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayMode : MonoBehaviour
    {
        private int _percents = 75;
        private static LandEnemies _landEnemies;
        private static InputChecker _inputChecker;

        public static PlayMode playMode;
        public static Timer timer;
        public static Player player;
        public static BackgroundImage backgroundImage;
        public static WaterEnemies waterEnemies;
        public static GameOver gameOver;

        public Texture2D texture;

        void Start()
        {
            playMode = this;
            LevelStart();
        }

        void Update()
        {
            timer.IncreasingTime();
            _inputChecker.InputControll();
        }

        public void LevelStart()
        {
            timer = new Timer();
            player = new Player();
            backgroundImage = new BackgroundImage();
            waterEnemies = new WaterEnemies();
            gameOver = new GameOver();
            _inputChecker = new InputChecker();
            timer.SetTimer();
            StartCoroutine(PlayModeOn());
        }

        private void Paint()
        {
            backgroundImage.Paint();
            player.Paint();
            waterEnemies.Paint();

            if(player.CurrentLevel >= 2)
                _landEnemies.Paint();

            gameOver.GameOverCheck();
            texture.Apply();
        }

        public static Color InitializeColor(int color)
        {
            switch (color)
            {
                case BackgroundImage.wayColor: return Color.black;
                case BackgroundImage.waterColor: return Color.gray;
                case BackgroundImage.landColor: return Color.cyan;
                case BackgroundImage.trackColor: return Color.magenta;
            }
            return Color.red;
        }

        public static Color[] InitializeColorArray(Color color, int size)
        {
            Color[] result = new Color[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = color;
            }

            return result;
        }

        public IEnumerator PlayModeOn()
        {
            while (!gameOver.IsGameOver)
            {
                if (gameOver.IsPaused)
                {
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    player.Move();
                    waterEnemies.Move();

                    if(player.CurrentLevel >=2)
                        _landEnemies.Move();

                    Paint();

                    yield return new WaitForSeconds(0.015f);

                    if (timer.TimeIsUp)
                    {
                        gameOver.IsGameOver = true;   
                    }

                    if (player.CurrentLevel >= 2)
                    {
                        if (player.IsDieBySelf || waterEnemies.IsHitPlayerOrWay() || _landEnemies.IsHitPlayer())
                        {
                            player.Damage();

                            if (player.Health > 0)
                            {
                                player.Reborn();
                                backgroundImage.CahngeWayToWater();
                                Paint();
                                yield return new WaitForSeconds(1f);
                            }
                        }
                    }
                    else
                    {
                        if (player.IsDieBySelf || waterEnemies.IsHitPlayerOrWay())
                        {
                            player.Damage();

                            if (player.Health > 0)
                            {
                                player.Reborn();
                                backgroundImage.CahngeWayToWater();
                                Paint();
                                yield return new WaitForSeconds(1f);
                            }
                        }
                    }

                    if (backgroundImage.FilledArea() >= _percents)
                    {
                        timer.SetTimer();
                        player.LevelUp();
                        backgroundImage.Reborn();
                        player.Reborn();
                        waterEnemies.AddWaterEnemy();

                        if (player.CurrentLevel == 2)
                            _landEnemies = new LandEnemies();

                        if (player.CurrentLevel >= 3)
                            _landEnemies.AddLandEnemy();

                        Paint();

                        yield return new WaitForSeconds(1f);
                    }
                }
            }
        }
    }
}
