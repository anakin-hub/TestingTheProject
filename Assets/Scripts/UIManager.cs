using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] GameObject startMenuUI;
    [SerializeField] GameObject GameOverUI;

    [SerializeField] TextMeshProUGUI gameOverScoreUI;
    [SerializeField] TextMeshProUGUI gameOverHighScoreUI;

    GameManager gm;

    private void Start()
    {
        scoreUI.transform.gameObject.SetActive(false);
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }

    public void ActivateGameOverUI()
    {
        GameOverUI.SetActive(true);
        scoreUI.transform.gameObject.SetActive(false);

        gameOverScoreUI.text = "Score: " + gm.PrettyScore();
        gameOverHighScoreUI.text = "Highscore: " + gm.PrettyHighscore();
    }

    public void PlayButton()
    {
        scoreUI.transform.gameObject.SetActive(true);
        gm.StartGame();
    }
}
