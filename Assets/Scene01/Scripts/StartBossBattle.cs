using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public DialogController dialogController;
    
    public BattleController battleController;
    
    public GameObject arthur;

    public GameObject prefabBoss;

    public GameObject sword;

    public GameObject stone;

    public GameObject skull;
    
    public void StartBattle()
    {
        StartCoroutine(StartBattleCoroutine());
    }

    // Create smooth fade animation.
    private IEnumerator StartBattleCoroutine()
    {
        if (GameState.Instance.LancelotHealth > 0)
        {
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "We found it, Lancelot. Holy sword!");
        }
        else
        {
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "I found it. Holy sword!");
        }

        sword.SetActive(false);
        
        GameState.Instance.ArthurHasExcalibur = true;
        
        // TODO: Make silence pause here.

        if (GameState.Instance.LancelotHealth > 0)
        {
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "Ah!");
            yield return dialogController.Show(DialogLineAvatar.Lancelot, "Lancelot", "It drains your life. Drop it!");
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "No...");
        }
        else
        {
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "Ah!");
            yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "It drains my life.");            
        }

        skull.SetActive(false);

        yield return new WaitForSeconds(1.0f); 
        
        stone.SetActive(false);
        
        yield return new WaitForSeconds(1.0f);
        
        var gameObjectBoss = Instantiate(prefabBoss);
        gameObjectBoss.transform.position = stone.transform.position;
        
        yield return dialogController.Show(DialogLineAvatar.Ghost, "Ghost", "It will not make you happy. Drop it now.");
        yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "No. Die, monster!");

        battleController.StartBattle(new List<Enemy> { gameObjectBoss.GetComponent<Enemy>() }, false);
    }
}
