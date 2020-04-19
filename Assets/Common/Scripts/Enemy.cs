using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public int damage;

    public void Damage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
