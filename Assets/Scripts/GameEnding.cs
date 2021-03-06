using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 1f;
    [SerializeField] public float displayImageDuration = 10f;
    [SerializeField] public GameObject player;
    [SerializeField] public CanvasGroup exitBackgroundImageCanvasGroup;

    private bool m_IsPlayerAtExit = false;
    private float m_Timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        m_Timer += Time.deltaTime;

        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            //Application.Quit();
            SceneManager.LoadScene(0);
        }
    }
}
