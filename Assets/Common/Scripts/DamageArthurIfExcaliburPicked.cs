using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageArthurIfExcaliburPicked : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DamageArthurWithExcaliburCoroutine());
    }

    private IEnumerator DamageArthurWithExcaliburCoroutine()
    {
        while (true)
        {
            if (GameState.Instance.ArthurHasExcalibur) {
                GameState.Instance.DamageArthur(1);
                if (GameState.Instance.ArthurHealth == 0)
                {
                    GameState.Instance.ending = Ending.C;
                    SceneManager.LoadScene("End");
                }
                yield return new WaitForSeconds(1.0f);
            }
            
            yield return null;
        }
    }
}
