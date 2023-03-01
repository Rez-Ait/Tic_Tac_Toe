using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour
{
    private GameCTRL gameCtrl;
    private Image myShape;
    private Player flagPlayer;
    private bool isSetPlayer = false;

    private void Start()
    {
        myShape = GetComponent<Image>();
    }

    public void click()
    {
        gameCtrl ??= GameCTRL.instance;

        if (!isSetPlayer)
        {
            isSetPlayer = true;
            flagPlayer = gameCtrl.SetClick();
            SetShape(flagPlayer.GetShape());
        }
    }

    private void SetShape(Sprite _Shape)
    {
        myShape.sprite = _Shape;
    }

}