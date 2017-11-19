using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour {

    public float Health { get; private set; }
    public float maxHealth;
    public UnityEvent deathEvent;

    private void Start()
    {
        Health = maxHealth;
    }

    public void Damage(float d)
    {
        Debug.Log("Damaged");
        Health -= d;
        if (Health <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        deathEvent.Invoke();
    }
}
