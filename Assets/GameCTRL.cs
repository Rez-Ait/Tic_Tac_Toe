using System.Collections;
using System.Collections.Generic;
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

    private PlayerType statusPlayer;
    public PlayerType StatusPlayer { get { return statusPlayer; } }

    private Player firstPlayer;
    private Player secondPlayer;

    void Start()
    {
        statusPlayer = PlayerType.XPlayer;

        // Create New 2 Player
        firstPlayer = new Player(PlayerType.XPlayer, shapeXPlayer);
        secondPlayer = new Player(PlayerType.OPlayer, shapeOPlayer);

    }

    public Player SetClick()
    {

        switch (statusPlayer)
        {
            case PlayerType.XPlayer:
                statusPlayer = PlayerType.OPlayer;
                return firstPlayer;
            case PlayerType.OPlayer:
                statusPlayer = PlayerType.XPlayer;
                return secondPlayer;
        }

        return null;

    }

}


public class Player
{
    private PlayerType type;
    private Sprite myShape;

    public Player(PlayerType _type, Sprite _shape)
    {
        type = _type;
        myShape = _shape;
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