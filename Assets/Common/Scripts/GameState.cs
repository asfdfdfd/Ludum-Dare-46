using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private const int ArthurMaxHealth = 100;
    private const int LancelotMaxHealth = 100;
    
    public static readonly GameState Instance = new GameState();

    private int _arthurHealth;
    private int _lancelotHealth;

    public int ArthurHealth => _arthurHealth;
    public int LancelotHealth => _lancelotHealth;

    public readonly int ArthurDamage = 40;
    public readonly int LancelotHeal = 40;

    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        _arthurHealth = ArthurMaxHealth;
        _lancelotHealth = LancelotMaxHealth;
    }

    public void DamageArthur(int damage)
    {
        _arthurHealth -= damage;

        if (_arthurHealth < 0)
        {
            _arthurHealth = 0;
        }
    }
    
    public void HealArthur(int heal)
    {
        _arthurHealth += heal;

        if (_arthurHealth > ArthurMaxHealth)
        {
            _arthurHealth = ArthurMaxHealth;
        }
    }    

    public void DamageLancelot(int damage)
    {
        _lancelotHealth -= damage;
        
        if (_lancelotHealth < 0)
        {
            _lancelotHealth = 0;
        }
    }
    
    public void HealLancelot(int heal)
    {
        _lancelotHealth += heal;

        if (_lancelotHealth > LancelotMaxHealth)
        {
            _lancelotHealth = LancelotMaxHealth;
        }
    }      
}
