using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattleBoss : MonoBehaviour
{
    public BattleController battleController;
    
    public GameObject arthur;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == arthur.GetComponent<Collider2D>())
        {
            battleController.StartBattle(new List<Enemy> { gameObject.GetComponent<Enemy>() });
        }
    }
}
