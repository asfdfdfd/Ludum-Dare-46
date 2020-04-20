using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public const int ArthurMaxHealth = 100;
    public const int LancelotMaxHealth = 100;
    
    public static readonly GameState Instance = new GameState();

    private bool _hasApple = false;
    
    private int _arthurHealth;
    private int _lancelotHealth;

    public bool HasApple => _hasApple;

    public int ArthurHealth => _arthurHealth;
    public int LancelotHealth => _lancelotHealth;

    public readonly int ArthurDamage = 40;
    public readonly int LancelotHeal = 40;

    public bool ArthurHasExcalibur = false;

    public Ending ending = Ending.N;
    
    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        ArthurHasExcalibur = false;
        _hasApple = false;
        _arthurHealth = ArthurMaxHealth;
        _lancelotHealth = LancelotMaxHealth;
        ending = Ending.N;
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

    public void KillLancelot()
    {
        _lancelotHealth = 0;
    }

    public void PickUpApple()
    {
        _hasApple = true;
    }

    public void FullHealArthur()
    {
        _arthurHealth = ArthurMaxHealth;

        _hasApple = false;
    }
}
