using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [Header ("Bullet Settings")]
    public float moveSpeed;
    public Vector3 direction;

    private void Start()
    {
        direction.Normalize();
    }

    private void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
