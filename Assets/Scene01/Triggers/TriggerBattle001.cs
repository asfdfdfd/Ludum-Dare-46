using System.Collections;
using UnityEngine;

public class TriggerBattle001 : MonoBehaviour
{
    public BattleController battleController;
    
    public GameObject arthur;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == arthur.GetComponent<Collider2D>())
        {
            StartCoroutine(StartBattle());
        }
    }

    private IEnumerator StartBattle()
    {
        yield return battleController.StartBattle();
    }
}
