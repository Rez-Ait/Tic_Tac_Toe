using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowWinner : MonoBehaviour
{

    private GameCTRL gameCTRL;
    [field: SerializeField]
    private TextMeshProUGUI txtDescription;

    public void init(Player winner)
    {
        this.gameObject.SetActive(true);
        txtDescription.SetText($"The Winner Of This Game Player {winner.GetType()} \nCount Click: {winner.CountClick}");
        Debug.Log(winner.ToString());
    }

    public void clickRes()
    {
        gameCTRL ??= GameCTRL.instance;
        gameCTRL.ResetGamePlay();
        this.gameObject.SetActive(false);
    }


}
