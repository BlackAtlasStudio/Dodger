using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public float Health { get; private set; }
    public float maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }

    public void Damage(float d)
    {
        Health -= d;
        if (Health <= 0f)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("You died");
    }
}
