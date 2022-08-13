using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EI2
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Button mainMenu;
        [SerializeField] private Button Level_0;
        [SerializeField] private Button Level_1;
        [SerializeField] private Button menuQuit;

        [SerializeField] private string gameScene0 = "Level_0";
        [SerializeField] private string gameScene1 = "Level_1";
        [SerializeField] private string m_menuName = "GameMenu";

        private void Awake()
        {
            if (mainMenu != null) mainMenu.onClick.AddListener(Menu);
            if (Level_0 != null) Level_0.onClick.AddListener(Load0);
            if (Level_1 != null) Level_1.onClick.AddListener(Load1);
            if (menuQuit != null) menuQuit.onClick.AddListener(Quit);
        }

        private void Menu()
        {
            SceneManager.LoadScene(m_menuName);
        }
        private void Load0()
        {
            SceneManager.LoadScene(gameScene0);
        }

        private void Load1()
        {
            SceneManager.LoadScene(gameScene1);
        }
        private void Quit()
        {
            Application.Quit();
        }


    }

}