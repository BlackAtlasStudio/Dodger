using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Enemy Settings")]
    public float moveSpeed;
    public Vector3 moveDirection;
    public float knockback;

    private void Update()
    {
        Vector3 moveDelta = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(moveDelta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Bullet"))
        {
            Vector3 knockbackdelta = -moveDirection * knockback;
            transform.Translate(knockbackdelta);
            ObjectPool bulPool = ObjectPoolManager.Instance.GetPool(obj.tag);
            if (bulPool != null)
                bulPool.Destroy(obj);
        }
    }
}
