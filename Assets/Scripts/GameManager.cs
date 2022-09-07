using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public StateMachine stateMachine;
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public GameObject mainMenuCanvas;
    public float releaseBallTime;

    public int pointsToWin = 5;
    public KeyCode pauseKey = KeyCode.P;
    public float timeToStart = 3f;

    public List<Player> players;
    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (var player in players) player.scored = CheckScore;
        stateMachine.Menu();
        // Prepare();
    }

    public void Prepare()
    {
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        foreach (var player in players) player.Reset();
        Reset();
    }

    public void Reset()
    {
        CancelInvoke();
        ball.CancelInvoke();
        ResetBall();
    }

    public void Play() {
        foreach (var player in players) player.CanMove = true;
        Invoke(nameof(ReleaseBall), releaseBallTime);
    }

    public void CheckScore()
    {
        bool win = false;
        foreach (var player in players)
        {
            int points = player.GetPlayerPoints();
            if (points >= pointsToWin)
            {
                GameObject valign = gameOverCanvas.transform.Find("VerticalAlign").gameObject;
                var label = valign.transform.Find("lblPlayerWin").gameObject.GetComponent<TextMeshProUGUI>();
                label.text = player.screenName + " wins!";
                stateMachine.GameOver();
                win = true;
                break;
            }
        }
        if(!win) stateMachine.ResetPosition();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void ResetBall() {
        ball.CanMove = false;
        ball.ResetBall();
        Invoke(nameof(ReleaseBall), releaseBallTime);
    }

    public void ReleaseBall()
    {
        ball.CanMove = true;
    }

    public void Pause(bool paused) {
        players.ForEach(player => player.CanMove = !paused);
        ball.CanMove = !paused;
    }

    public void Menu() {
        // Reseta a bola e paraliza a mesma para não disparar o Goal novamente voltando ao gameover
        ball.ResetBall(); 
        ball.CanMove = false;
        ShowGameOver(false);
        ShowMainMenu(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void Stop() {
        Pause(true);
    }

    public void ShowGameOver(bool show) {
        gameOverCanvas.SetActive(show);
    }

    public void ShowMainMenu(bool show) {
        mainMenuCanvas.SetActive(show);
    }

}