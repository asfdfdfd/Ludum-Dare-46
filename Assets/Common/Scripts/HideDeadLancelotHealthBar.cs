using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDeadLancelotHealthBar : MonoBehaviour
{
    void Update()
    {
        if (GameState.Instance.LancelotHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
