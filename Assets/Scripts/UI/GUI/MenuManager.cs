using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.GUI
{
    public class MenuManager : MonoBehaviour
    {
        public void LoadBattle()
        {
            SceneManager.LoadScene("Battle", LoadSceneMode.Single);
        }


        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
