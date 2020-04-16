using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleInterfaceController : MonoBehaviour
{
    public BattleController battleController;
    
    public List<TMP_Text> textHitPointsList;
    
    public GameObject buttonAttack;

    void Awake()
    {
        buttonAttack.SetActive(false);
    }
    
    void Update()
    {
        for (int i = 0; i < battleController.allies.Count; i++)
        {
            textHitPointsList[i].text = battleController.allies[i].health.ToString();            
        }
        
        buttonAttack.SetActive(battleController.isPlayerTurn);
    }
}
