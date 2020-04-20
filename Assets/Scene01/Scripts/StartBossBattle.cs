using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public BattleController battleController;
    
    public GameObject arthur;

    public GameObject prefabBoss;

    public GameObject sword;

    public GameObject stone;

    public GameObject skull;
    
    public void StartBattle()
    {
        sword.SetActive(false);
        
        // TODO: Dialog about sword. Sword starts to damage Arthur etc.
        
        GameState.Instance.ArthurHasExcalibur = true;
        
        // TODO: Remove skull with fade out.
        
        skull.SetActive(false);
        
        // TODO: Wait a bit and remove stone with fade out and 
        
        stone.SetActive(false);
        
        var gameObjectBoss = Instantiate(prefabBoss);
        gameObjectBoss.transform.position = stone.transform.position;
        
        // TODO: Dialog with Arthur.

        battleController.StartBattle(new List<Enemy> { gameObjectBoss.GetComponent<Enemy>() });
    }
}
