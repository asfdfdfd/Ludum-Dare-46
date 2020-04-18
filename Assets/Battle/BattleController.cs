using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class BattleController : MonoBehaviour
{
    public CinematicManager cinematicManager;
    
    public GameObject arthur;

    public GameObject arthurPrefab;
    public GameObject lancelotPrefab;
    
    private Random _random = new Random((uint)DateTime.Now.Millisecond);
    
    private List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();

    private bool _isBattleInProgress = false;

    public bool isBattleInProgress => _isBattleInProgress;
    
    public IEnumerator StartBattle(List<GameObject> enemies)
    {
        _isBattleInProgress = true;
        
        _enemies = enemies;
        
        cinematicManager.StartCinematic();
        
        yield return SpawnBrothers();

        _enemies[0].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        
        cinematicManager.StopCinematic();
    }

    private IEnumerator SpawnBrothers()
    {
        var battleSpot = FindObjectsOfType<BattleSpot>().Where(x => !x.GetComponent<BattleSpot>().IsTouching).OrderBy(x => _random.NextInt()).First();
        
        GameObject gameObjectLancelot = Instantiate(lancelotPrefab);
        gameObjectLancelot.transform.position = arthur.transform.position;
        var tweenerLancelot = gameObjectLancelot.transform.DOMove(battleSpot.transform.position, Constants.SpeedRun)
            .SetSpeedBased();
        
        yield return tweenerLancelot.WaitForCompletion();
        
        arthur.SetActive(false);
        
        GameObject gameObjectArthur = Instantiate(arthurPrefab);
        gameObjectArthur.transform.position = arthur.transform.position;
        
        _players.Add(gameObjectLancelot);
        _players.Add(gameObjectArthur);
    }
}
