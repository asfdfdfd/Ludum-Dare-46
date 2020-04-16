using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class BattleController : MonoBehaviour
{
    private List<Enemy> enemies;
    public List<Ally> allies;
    
    public List<NPCController> enemySpawnPoints;
    public List<NPCController> allySpawnPoints;
    
    [NonSerialized]
    public bool isPlayerTurn;
    
    private bool isAwaitingUserInput;

    private Random random = new Random((uint)DateTime.Now.Millisecond);
    
    private bool isBattleInProgress => IsAlliesAlive && IsEnemiesAlive;

    private bool IsAlliesAlive
    {
        get
        {
            int health = 0;
            allies.ForEach((ally) => health += ally.health);
            return health > 0;
        }
    }
    
    private bool IsEnemiesAlive
    {
        get
        {
            int health = 0;
            enemies.ForEach((enemy) => health += enemy.health);
            return health > 0;
        }
    }    
    
    void Start()
    {
        enemies = new List<Enemy>();
        enemies.Add(new Enemy());
        enemies.Add(new Enemy());
        
        allies = new List<Ally>();
        allies.Add(new Ally());
        allies.Add(new Ally());
        
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

        var playerIndex = random.NextInt(allySpawnPoints.Count);
            
        var player = allySpawnPoints[playerIndex];

        var enemyIndex = random.NextInt(enemySpawnPoints.Count);
        
        var enemy = enemySpawnPoints[enemyIndex];
        
        var positionOld = player.transform.position;

        var enemyAttackPanel = enemy.transform.Find("AttackPanel").gameObject;
        
        yield return player.transform.DOMove(enemyAttackPanel.transform.position, 0.3f).WaitForCompletion();
        
        yield return player.Attack();
        
        enemies[enemyIndex].health -= 10;
        
        yield return player.transform.DOMove(positionOld, 0.3f).WaitForCompletion();
        
        isPlayerTurn = false;
    }

    private IEnumerator EnemyTurn()
    {
        var enemyIndex = random.NextInt(enemySpawnPoints.Count);
        
        var enemy = enemySpawnPoints[enemyIndex];

        var playerIndex = random.NextInt(allySpawnPoints.Count);
            
        var player = allySpawnPoints[playerIndex];
        
        var positionOld = enemy.transform.position;
        
        var playerAttackPanel = player.transform.Find("AttackPanel").gameObject;
        
        yield return enemy.transform.DOMove(playerAttackPanel.transform.position, 0.3f).WaitForCompletion();
        
        yield return enemy.Attack();

        allies[playerIndex].health -= 1;
        
        yield return enemy.transform.DOMove(positionOld, 0.3f).WaitForCompletion();
        
        yield return null;
    }
}
