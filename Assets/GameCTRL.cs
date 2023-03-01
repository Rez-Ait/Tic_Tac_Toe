using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameCTRL : MonoBehaviour
{
    public static GameCTRL instance;

    void Awake()
    {
        instance = this;
    }

    [field: SerializeField]
    private Sprite shapeXPlayer;
    [field: SerializeField]
    private Sprite shapeOPlayer;

    private bool stateGamePlay;
    private PlayerType statusPlayer;
    public PlayerType StatusPlayer { get { return statusPlayer; } }

    private Player firstPlayer;
    private Player secondPlayer;

    void Start()
    {
        statusPlayer = PlayerType.XPlayer;
        stateGamePlay = true;
        // Create New 2 Player
        firstPlayer = new Player(PlayerType.XPlayer, shapeXPlayer);
        secondPlayer = new Player(PlayerType.OPlayer, shapeOPlayer);

    }

    public Player SetClick(int numberClick)
    {

        switch (statusPlayer)
        {
            case PlayerType.XPlayer:
                statusPlayer = PlayerType.OPlayer;
                firstPlayer.addNumber(numberClick);
                return firstPlayer;
            case PlayerType.OPlayer:
                statusPlayer = PlayerType.XPlayer;
                secondPlayer.addNumber(numberClick);
                return secondPlayer;
        }

        return null;

    }

}


public class Player
{
    private PlayerType type;
    private Sprite myShape;
    private int countClick;
    private List<int> numberClicks;

    private int minimumForWin = 3;
    public Player(PlayerType _type, Sprite _shape)
    {
        type = _type;
        myShape = _shape;
        countClick = 0;
        numberClicks = new List<int>();
    }

    public void addNumber(int number)
    {
        countClick++;

        numberClicks.Add(number);

        if (countClick >= minimumForWin)
            if (CheckNumbers())
            {
                Debug.Log("Win");
            }
    }

    private bool CheckNumbers()
    {
        foreach (int myNum in numberClicks)
        {
            if (myNum % 3 == 0)
            {
               return CalcRow(myNum);
            }
        }
        return false;
    }

    private bool CalcRow(int num)
    {
        int countTrue = 2;
        foreach (int myNum in numberClicks)
        {
            if (myNum + 1 == num || myNum + 2 == num)
            {
                countTrue--;
            }
        }

        if (countTrue == 0)
            return true;
        else
            return false;
    }

    public Sprite GetShape()
    {
        return myShape;
    }
}

public enum PlayerType
{
    XPlayer,
    OPlayer
}