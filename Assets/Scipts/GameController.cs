using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Gold = 0;

    public int Lives = 3;

    public UIController UIController;
    public AudioController AudioController;
    public MapController MapController;

    public GameObject BallPrefab;
    public List<Ball> Balls = new List<Ball>();

    public Vector3 BallResetPosition;

    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    public GameObject BlockPrefab;
    float lastSpawn;
    float timeBetweenSpawn = 0.1f;

    public int Level = 1;

    public AudioClip BGM;


    public void Start()
    {
        AudioController = GetComponent<AudioController>();
        AudioController.PlayClipAsBGM(BGM);

        UIController.UpdateGoldText(Gold);
        UIController.UpdateLives(Lives);

        MapController.BuildRoom(0);

        PauseGame();
    }

    public void AddGold(int _value)
    {
        Gold += _value;

        UIController.UpdateGoldText(Gold);
    }

    public void SpawnNewBall()
    {
        GameObject ball = GameObject.Instantiate(BallPrefab, BallResetPosition, Quaternion.identity);
        Balls.Add(ball.GetComponent<Ball>());
    }

    public void BallLost(Ball _ball)
    {
        // reset position
        _ball.transform.position = BallResetPosition;

        Vector3 currentVelocity = _ball.Velocity;
        currentVelocity.y = Mathf.Abs(currentVelocity.y);
        _ball.Velocity = currentVelocity;

        Balls.Remove(_ball);
        //Destroy(_ball.gameObject);

        if (Balls.Count == 0)
        {
            // lose life
            Lives--;
            UIController.UpdateLives(Lives);
            if (Lives < 0)
            {
                Debug.Log("Game Over!");
                GameOver();
            }
        }
    }

    void GameOver()
    {
        UIController.ShowGameOver();
        isPlaying = false;
        PauseGame();
    }

    public void StartGame()
    {
        isPlaying = true;
        ResetGame();
        UnpauseGame();
    }

    void ResetGame()
    {
        Lives = 3;
        Gold = 0;
        UIController.UpdateGoldText(Gold);
        UIController.UpdateLives(Lives);
        UIController.HideStartGamePanel();
        UIController.HideGameOver();
        SpawnNewBall();
    }

    public void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0;

        if (isPlaying)
        {
            UIController.ShowPausePanel();
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        UIController.HidePausePanel();
    }

    public void QuitGame()
    {
        isPlaying = false;
        PauseGame();
        UIController.HideGameOver();
        UIController.HidePausePanel();
        UIController.ShowStartGamePanel();

        ClearBalls();
    }

    void ClearBalls()
    {
        for(int i = Balls.Count - 1; i >= 0; i--)
        {
            Destroy(Balls[i].gameObject);
        }

        Balls.Clear();
    }
}
