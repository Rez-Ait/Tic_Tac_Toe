using System;
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

    [field: SerializeField]
    private ShowWinner showWinner;

    void Start()
    {
        statusPlayer = PlayerType.XPlayer;
        stateGamePlay = true;
        // Create New 2 Player
        firstPlayer = new Player(PlayerType.XPlayer, shapeXPlayer);
        secondPlayer = new Player(PlayerType.OPlayer, shapeOPlayer);
    }

    List<Btn> btns;

    public void ResetGamePlay()
    {
        statusPlayer = PlayerType.XPlayer;
        stateGamePlay = true;

        // Cash All Btn To btns
        if (btns == null)
        {
            btns = new List<Btn>();
            btns.AddRange(Transform.FindObjectsOfType<Btn>());
        }

        // Reset All Btns
        foreach (Btn myBtn in btns)
            myBtn.Res();

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

    public void Win(Player player)
    {
        showWinner.init(player);
    }

}


public class Player
{
    private PlayerType type;
    private Sprite myShape;
    private int countClick;
    private List<int> numberClicks;
    private int minimumForWin = 3;

    public PlayerType Type { get { return type; } }
    public Sprite Shape { get { return myShape; } }
    public int CountClick { get { return countClick; } }
    public List<int> NumberClicks { get { return numberClicks; } }




    public Player(PlayerType _type, Sprite _shape)
    {
        type = _type;
        myShape = _shape;
        countClick = 0;
        numberClicks = new List<int>();
    }
    private GameCTRL gameCTRL;
    public void addNumber(int number)
    {
        countClick++;

        numberClicks.Add(number);

        if (countClick >= minimumForWin)
            if (CheckNumbers())
            {
                gameCTRL ??= GameCTRL.instance;
                gameCTRL.Win(this);
            }
    }

    public char GetType()
    {
        string str = type.ToString();
        return str[0];
    }

    private bool CheckNumbers()
    {
        foreach (int myNum in numberClicks)
        {
            if (myNum % 3 == 1)
                if (Calc(myNum, 1))
                    return true;
            if (myNum == 3)
                if (Calc(myNum, 2))
                    return true;
            if (myNum <= 3)
                if (Calc(myNum, 3))
                    return true;
            if (myNum == 1)
                if (Calc(myNum, 4))
                    return true;
        }
        return false;
    }

    private int tmpForCulc = 4;


    private bool Calc(int num, int countPlus)
    {
        int countTrue = 2;
        foreach (int numCheck in numberClicks)
            if (num + countPlus == numCheck || num + countPlus * 2 == numCheck)
            {
                countTrue--;
                if (countTrue == 0)
                    return true;
            }

        return false;
    }
    //private bool CalcDiagonalToRight(int num)
    //{
    //    int countTrue = 2;
    //    tmpForCulc = 2;
    //    foreach (int myNum in numberClicks)
    //        if (num + tmpForCulc == myNum || num + tmpForCulc * 2 == myNum)
    //        {
    //            countTrue--;
    //            if (countTrue == 0)
    //                return true;
    //        }

    //    return false;
    //}

    //private bool CalcDiagonalToLeft(int num)
    //{
    //    int countTrue = 2;
    //    tmpForCulc = 4;

    //    foreach (int myNum in numberClicks)
    //        if (num + tmpForCulc == myNum || num + tmpForCulc * 2 == myNum)
    //        {
    //            countTrue--;
    //            if (countTrue == 0)
    //                return true;
    //        }

    //    return false;
    //}

    //private bool CalcColumn(int num)
    //{
    //    int countTrue = 2;
    //    tmpForCulc = 3;
    //    foreach (int myNum in numberClicks)
    //        if (num + tmpForCulc == myNum || num + tmpForCulc * 2 == myNum)
    //        {
    //            countTrue--;
    //            if (countTrue == 0)
    //                return true;
    //        }

    //    return false;
    //}

    //private bool CalcRow(int num)
    //{
    //    int countTrue = 2;
    //    tmpForCulc = 1;

    //    foreach (int myNum in numberClicks)
    //        if (num + tmpForCulc == myNum || num + tmpForCulc * 2 == myNum)
    //        {
    //            countTrue--;
    //            if (countTrue == 0)
    //                return true;
    //        }

    //    return false;
    //}

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