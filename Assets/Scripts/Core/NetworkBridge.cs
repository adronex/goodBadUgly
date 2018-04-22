using UnityEngine;

namespace Core
{
    public class NetworkBridge : MonoBehaviour
    {
        private GameCore gameCore;

        private void Start()
        {

        }

        /*
         *  какой уровень загружается
         *  меняется состояние игры
         *  стреляет
         *  при получание 
         * 
         * 
         */

        public void LoadLevel(int levelId)
        {
        }

        public void Shoot()
        {
            Reload(01);
        }

        private void Reload(int hero) //0 - left, 1 - right
        {
        }

        //
        private void StartWaiting()
        {
        }

        private void StartCountdown()
        {
        }

        private void StartGame()
        {
        }

        private void GameOver()
        {
        }
        //
    }
}