using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private int maxMoves = 50;
    private int maxMovesStart;
    public Checkpoint startCheckpoint;

    #region SingleTon

    private static GameLoop instance;

    public static GameLoop Instance => instance;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    private void Start()
    {
        maxMovesStart = maxMoves;
    }

    public int MaxMoves
    {
        get
        {
            if (maxMoves <= 0)
                GameOver();

            return maxMoves;
        }
        set => maxMoves = value;
    }


    public Checkpoint CurrentCheckpoint { get; set; }

    public void GameOver()
    {
        GameManager.Instance.player.transform.position = startCheckpoint.transform.position;
        GameManager.Instance.player.GetComponent<BallMovement>().Body.velocity = Vector2.zero;
        maxMoves = maxMovesStart;
    }
}
