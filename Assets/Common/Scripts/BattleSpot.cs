using UnityEngine;

public class BattleSpot : MonoBehaviour
{
    private int _objectsTouched = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _objectsTouched++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _objectsTouched--;
    }

    public bool IsTouching => _objectsTouched > 0;
}
