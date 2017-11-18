using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Vector3 moveDirection;
    public float knockback;
    public float flashDuration;

    [Header("Enemy References")]
    public Material flashMat;

    private bool flash;
    private float flashTimer;
    private Renderer rend;
    private Material defaultMat;

    private void Start()
    {
        moveDirection.Normalize();
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            defaultMat = rend.material;
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDelta = moveDirection * EnemyManager.MoveSpeed * Time.deltaTime;
        transform.Translate(moveDelta);

        //Material flashing
        if (flash && flashTimer <= 0.01f) {
            rend.material = flashMat;
        }
        if (flash)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= flashDuration) {
                flash = false;
                flashTimer = 0f;
                rend.material = defaultMat;
            }
        }
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
            flash = true;
        }
    }
}
