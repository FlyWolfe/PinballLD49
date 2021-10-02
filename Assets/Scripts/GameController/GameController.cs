using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void OnWinGame();
    public delegate void OnResetGame();


    public Transform levelBottom;
    public Transform levelTop;

    public static GameController Instance;
    private Player player;
    private Vector3 playerStartPosition;
    private float timer;
    private bool isGameRunning = false;


    public OnWinGame OnGameWon { get; set; }
    public OnResetGame OnGameReset { get; set; }

    public float LevelProgress
    { 
        get 
        {
            if(player == null)
            {
                return 0f;
            }

            float distanceToTop = Vector3.Distance(levelBottom.position, levelTop.position);
            float distanceToPlayer = Vector3.Distance(levelBottom.position, player.transform.position);
            return distanceToPlayer / distanceToTop;
        } 
    }

    public float Timer
    {
        get
        {
            return timer;
        }
    }



    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerStartPosition = player.transform.position;

        ResetGame();
    }


    private void Update()
    {
        if (isGameRunning)
        {
            timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPlayer();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void WinGame() {
        // TODO: Victory logic
        // Placeholder functionality
        isGameRunning = false;
        OnGameWon.Invoke();
    }

    public void ResetGame()
    {
        isGameRunning = true;
        timer = 0f;
        OnGameReset.Invoke();
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        player.transform.position = playerStartPosition;
        player.ResetRigidbody();
    }
    

}
