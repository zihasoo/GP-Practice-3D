using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverScene;
    public Text HPText;
    public Text ScoreText;
    public Player player;

    public static UIManager instance;

    private int monsterKillCount = 0;
    private int monster2KillCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int monsterType)
    {
        if (monsterType == 1)
        {
            monsterKillCount++;
        }
        else if (monsterType == 2)
        {
            monster2KillCount++;
        }
    }

    public void UpdateHP(int currentHP, int MaxHP)
    {
        HPText.text = $"HP: {currentHP}/{MaxHP}";
    }

    public void HandleGameOver()
    {
        Time.timeScale = 0f;
        ScoreText.text = $"Monster1 Killed: {monsterKillCount}\n" +
            $"Monster2 Killed: {monster2KillCount}\n" +
            $"Total Score: {monsterKillCount * 5 + monster2KillCount * 12}";
        GameOverScene.SetActive(true);
    }

    public void HandleRestartButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
