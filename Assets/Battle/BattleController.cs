using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    private int enemyHitPoints = 100;
    public int playerHitPoints = 100;

    [NonSerialized]
    public bool isPlayerTurn;
    
    private bool isAwaitingUserInput;

    private bool isBattleInProgress
    {
        get { return playerHitPoints > 0 && enemyHitPoints > 0; }
    }
    
    void Start()
    {
        StartCoroutine(BattleCoroutine());
    }
    
    public void OnAttackPressed()
    {
        isAwaitingUserInput = false;
    }

    private IEnumerator BattleCoroutine()
    {
        while (isBattleInProgress)
        {
            yield return PlayerTurn();
            yield return EnemyTurn();
        }
    }
    
    private IEnumerator PlayerTurn()
    {
        isPlayerTurn = true;
        
        isAwaitingUserInput = true;
        
        while (isAwaitingUserInput)
        {
            yield return null;
        }

        enemyHitPoints -= 10;
        
        isPlayerTurn = false;
    }

    private IEnumerator EnemyTurn()
    {
        playerHitPoints -= 1;
        
        yield return null;
    }
}
