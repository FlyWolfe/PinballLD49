using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform levelBottom;
    public Transform levelTop;

    public static GameController Instance;
    private Player player;
    private Vector3 playerStartPosition;


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

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerStartPosition = player.transform.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
    }



    private void ResetPlayer()
    {
        player.transform.position = playerStartPosition;
        player.ResetRigidbody();
    }
}
