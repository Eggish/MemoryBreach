using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;

    private static float GameOverTimer = 0.0f;

    private static float GameOverDelay = 1.0f;

    void Update()
    {
        if (GameIsOver)
        {
            GameOverTimer -= Time.deltaTime;
            if (GameOverTimer < 0.0f)
            {
                RestartScene();
            }
        }
    }

    public static void GameOver()
    {
        GameIsOver = true;
        GameOverTimer = GameOverDelay;
    }

    public static void RestartScene()
    {
        GameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
