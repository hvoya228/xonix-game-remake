using UnityEngine;

namespace Assets.Scripts
{
    public class InputChecker
    {
        private Vector3 _touchPosition;
        private float _swipeX = 50.0f;
        private float _swipeY = 100.0f;

        public void InputControll()
        {
            #if UNITY_EDITOR
            if (!PlayMode.gameOver.IsGameOver && !PlayMode.gameOver.IsPaused)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    PlayMode.player.ChangeDirection(Player.downDirect);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    PlayMode.player.ChangeDirection(Player.upDirect);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    PlayMode.player.ChangeDirection(Player.leftDirect);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    PlayMode.player.ChangeDirection(Player.rightDirect);
                }
            }
            #endif

            #if UNITY_ANDROID
            if (!PlayMode.gameOver.IsPaused && !PlayMode.gameOver.IsGameOver)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _touchPosition = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    var deltaSwipe = _touchPosition - Input.mousePosition;
                    if (Mathf.Abs(deltaSwipe.x) > _swipeX)
                    {
                        PlayMode.player.ChangeDirection((deltaSwipe.x < 0) ? Player.rightDirect : Player.leftDirect);
                    }
                    else if (Mathf.Abs(deltaSwipe.y) > _swipeY)
                    {
                        PlayMode.player.ChangeDirection((deltaSwipe.y < 0) ? Player.downDirect : Player.upDirect);
                    }
                }
            }
            #endif
        }
    }
}