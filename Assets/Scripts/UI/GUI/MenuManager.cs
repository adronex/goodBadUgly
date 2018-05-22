using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.GUI
{
    public class MenuManager : MonoBehaviour
    {
        #region Public methods
        public void LoadBattle()
        {
            SceneManager.LoadScene(Helps.BattleScene, LoadSceneMode.Single);
        }


        public void LoadMenu()
        {
            SceneManager.LoadScene(Helps.MenuScene, LoadSceneMode.Single);
        }
        #endregion
    }
}
