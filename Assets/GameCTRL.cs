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
            if (myNum % 3 == 1)
                return CalcRow(myNum);
            if (myNum <= 3)
            {

            }
        }
        return false;
    }

    private bool CalcDiagonalToRight(int num)
    {
        int countTrue = 2;

        foreach (int myNum in numberClicks)
            if (num + 4 == myNum || num + 8 == myNum)
            {
                countTrue--;
                if (countTrue == 0)
                    return true;
            }

        return false;
    }

    private bool CalcColumn(int num)
    {
        int countTrue = 2;

        foreach (int myNum in numberClicks)
            if (num + 3 == myNum || num + 6 == myNum)
            {
                countTrue--;
                if (countTrue == 0)
                    return true;
            }

        return false;
    }

    private bool CalcRow(int num)
    {
        int countTrue = 2;

        foreach (int myNum in numberClicks)
            if (num + 1 == myNum || num + 2 == myNum)
            {
                countTrue--;
                if (countTrue == 0)
                    return true;
            }

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