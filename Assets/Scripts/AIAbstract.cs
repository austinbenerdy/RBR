using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AIAbstract : MonoBehaviour
{
    
    protected int baseSpeed = 4;
    protected float speedModifier = 1.1f;
    
    protected int baseHealth = 100;
    protected float healthModifier = 1.0f;
    protected int currentHealth;

    protected int baseSightRange = 9;
    protected float sightRangeModifier = 1.0f;

    protected Vector3 momentum = new Vector3();

    protected AIAbstract target;
    public AIAbstract[] population;
    
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject self;

    protected bool isDead = false;

    protected float baseCycleTime = 0.1f;
    protected float cycleTimeModifier = 1.0f;
    protected float currentCycleTime;

    protected void Init()
    {
        RestoreHealth();
        ResetCycleTime();
    }

    protected float GetSpeed()
    {
        return baseSpeed * speedModifier;
    }

    protected float getSightRange()
    {
        return baseSightRange * sightRangeModifier;
    }

    protected void ResetCycleTime()
    {
        currentCycleTime = baseCycleTime * cycleTimeModifier;
    }

    protected void RestoreHealth(int percentToRestore = 100)
    {
        int newHealthValue = currentHealth + (int) Math.Round(baseHealth * healthModifier) * (percentToRestore / 100);
        currentHealth = (newHealthValue <= Math.Round(baseHealth * healthModifier))
            ? newHealthValue
            : (int) Math.Round(baseHealth * healthModifier);
        
    }

    public void Move(Vector3 direction)
    {
        Vector3 currentPosition = transform.position;
        Vector3 normalizedDirection = new Vector3(direction.x, direction.y, 0).normalized;

        transform.position = currentPosition + (normalizedDirection * GetSpeed() * Time.deltaTime);
        momentum = (normalizedDirection * GetSpeed() * Time.deltaTime);
    }

    public void ModifySpeed(int percentIncrease)
    {
        float floatMod = percentIncrease / 100f;
        speedModifier += floatMod;
    }

    public void TakeDamage(int damage = 0)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            EndLife();
        }
    }

    protected virtual void EndLife()
    {
        isDead = true;
    }

    protected void findTarget()
    {
        AIAbstract closestTarget = null;
        float minDistance = getSightRange();
        
        foreach (AIAbstract npc in population)
        {
            float distance = Vector3.Distance(transform.position, npc.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = npc;
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
        else
        {
            target = null;
        }
    }

    public bool isAlive()
    {
        return !isDead;
    }

    public void Attack()
    {
        target.TakeDamage(25);
    }
}