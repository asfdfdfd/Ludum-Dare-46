using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInterfaceController : MonoBehaviour
{
    public BattleController battleController;

    public GameObject battleMenu;

    private void Update()
    {
        if (battleController.isBattleInProgress)
        {
            battleMenu.SetActive(true);
        }
        else
        {
            battleMenu.SetActive(false);
        }
    }
}
