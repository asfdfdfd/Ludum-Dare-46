using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleInterfaceController : MonoBehaviour
{
    public BattleController battleController;
    
    public TMP_Text textHitPoints;
    
    public GameObject buttonAttack;

    void Awake()
    {
        buttonAttack.SetActive(false);
    }
    
    void Update()
    {
        //textHitPoints.text = battleController.playerHitPoints.ToString();
        buttonAttack.SetActive(battleController.isPlayerTurn);
    }
}
