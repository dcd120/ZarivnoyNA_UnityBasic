using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EI2
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button menuQuit;

        [SerializeField] private string m_menuName = "GameMenu";

        private void Awake()
        {
            menuButton.onClick.AddListener(OpenMenu);
        }

        private void OpenMenu()
        {

        }
    }

}