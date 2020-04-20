using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameState.Instance.ending = Ending.G;
        SceneManager.LoadScene("End");
    }
}
