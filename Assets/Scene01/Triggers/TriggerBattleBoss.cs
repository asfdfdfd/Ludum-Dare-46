using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattleBoss : MonoBehaviour
{
    public BattleController battleController;
    
    public GameObject arthur;

    public Enemy enemy;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == arthur.GetComponent<Collider2D>())
        {
            Destroy(gameObject);
        
            GameState.Instance.ArthurHasExcalibur = true;
            
            battleController.StartBattle(new List<Enemy> { enemy });
        }
    }
}
