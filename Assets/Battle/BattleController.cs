using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public NPCController enemy;
    public NPCController player;

    public GameObject enemyAttackPanel;
    public GameObject playerAttackPanel;
    
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

        var positionOld= player.transform.position;
        
        yield return player.transform.DOMove(enemyAttackPanel.transform.position, 0.3f).WaitForCompletion();
        
        yield return player.Attack();
        
        enemyHitPoints -= 10;
        
        yield return player.transform.DOMove(positionOld, 0.3f).WaitForCompletion();
        
        isPlayerTurn = false;
    }

    private IEnumerator EnemyTurn()
    {
        var positionOld= enemy.transform.position;
        
        yield return enemy.transform.DOMove(playerAttackPanel.transform.position, 0.3f).WaitForCompletion();
        
        yield return enemy.Attack();
        
        playerHitPoints -= 1;
        
        yield return enemy.transform.DOMove(positionOld, 0.3f).WaitForCompletion();
        
        yield return null;
    }
}
