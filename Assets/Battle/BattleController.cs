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
    
    public GameObject lamorakPrefab;
    
    public GameObject percyPrefab;
    
    private Random _random = new Random((uint)DateTime.Now.Millisecond);
    
    private List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _enemies = new List<GameObject>();
    
    public IEnumerator StartBattle(List<GameObject> enemies)
    {
        _enemies = enemies;
        
        cinematicManager.StartCinematic();
        
        yield return SpawnBrothers();
        
        cinematicManager.StopCinematic();
    }

    private IEnumerator SpawnBrothers()
    {
        var battleSpots = FindObjectsOfType<BattleSpot>().Where(battleSpot => !battleSpot.GetComponent<BattleSpot>().IsTouching).OrderBy(x => _random.NextInt()).ToList().GetRange(0, 2);

        GameObject gameObjectLamorak = Instantiate(lamorakPrefab);
        gameObjectLamorak.transform.position = arthur.transform.position;
        var tweenerLamorak = gameObjectLamorak.transform.DOMove(battleSpots[0].transform.position, Constants.SpeedRun)
            .SetSpeedBased();
        
        GameObject gameObjectPercy = Instantiate(percyPrefab);
        gameObjectPercy.transform.position = arthur.transform.position;
        var tweenerPercy = gameObjectPercy.transform.DOMove(battleSpots[1].transform.position, Constants.SpeedRun)
            .SetSpeedBased();

        yield return tweenerLamorak.WaitForCompletion();
        yield return tweenerPercy.WaitForCompletion();
        
        arthur.SetActive(false);
        
        GameObject gameObjectArthur = Instantiate(arthurPrefab);
        gameObjectArthur.transform.position = arthur.transform.position;
        
        _players.Add(gameObjectLamorak);
        _players.Add(gameObjectPercy);
        _players.Add(gameObjectArthur);
    }
}
