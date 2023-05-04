using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;

    [SerializeField]
    private int _totalHealth = 100;
    public int MaxHealth = 100;

    public int TotalHealth
    {
        get
        {
            return _totalHealth;
        }
        set
        {
            _totalHealth = value;
            if (_totalHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isImmune = false;

    private float hitTimer = 0;
    public float immuneTimer = 0.20f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if(isImmune)
        {
            if(hitTimer > immuneTimer)
            {
                //remove Immunity
                isImmune = false;
                hitTimer = 0;
            }

            hitTimer += Time.deltaTime;
        }

    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isImmune)
        {
            TotalHealth -= damage;
            isImmune = true;

            animator.SetTrigger(AnimationStrings.hit);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            PlayerEvents.playerTookDamage.Invoke(gameObject, damage);

            return true;
        }
        
        //Unable to hit.
        return false;
    }

    public bool Heal(int healthRestored)
    {
        if(IsAlive && TotalHealth < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - TotalHealth, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            TotalHealth += actualHeal;
            PlayerEvents.playerRestoredHealth(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
