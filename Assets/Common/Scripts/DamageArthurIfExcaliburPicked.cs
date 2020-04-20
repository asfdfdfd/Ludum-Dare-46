using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameState.Instance.DamageArthur(1);
            
            yield return new WaitForSeconds(1.0f);
            yield return null;
        }
    }
}
