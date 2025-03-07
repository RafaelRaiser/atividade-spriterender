using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(float health)
    {
        healthText.text = "Health: " + health;
    }

    public void UpdateKillCount(int killCount)
    {
        killCountText.text = "Kills: " + killCount;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
