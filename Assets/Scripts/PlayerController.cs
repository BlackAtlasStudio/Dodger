using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeedMax;
    public float moveAcceleration;
    [Range(0f, 5f)]
    public float moveDrag;
    public float fireCooldown;
    public float maxTurnAngle;

    [Header ("Energy Settings")]
    public float energyMeter;
    public float energyMeterMax;
    public float energyRegen;
    public float energyRegenDisabledBoost;
    public float bulletCost;
    public float dashCost;

    [Header("Player References")]
    public GameObject bulletPrefab;
    public GameObject playerModel;

    private bool meterDisabled = false;
    private ObjectPool bulletPool;
    private Vector2 inputDir;
    private Vector3 velocity = Vector3.zero;
    private float fireTimer;
    private bool canFire;

    private float turnAngle;

    private void Update()
    {
        //Creates a new movement delta based on the input
        Vector3 moveDelta = velocity;
        moveDelta.x += inputDir.x * moveAcceleration;
        moveDelta.z += inputDir.y * moveAcceleration;
        if (inputDir.magnitude <= 0.05f) {
            Vector3 drag = moveDelta.normalized * -moveDrag;
            if (drag.magnitude >= moveDelta.magnitude)
                moveDelta = Vector3.zero;
            else
                moveDelta += drag;
        }
        moveDelta = Vector3.ClampMagnitude(moveDelta, moveSpeedMax);
        velocity = moveDelta;
        moveDelta *= Time.deltaTime;

        Vector3 targetPos = transform.position + moveDelta;

        Vector3 camPos = Camera.main.transform.position;
        Vector3 minBounds = camPos + GameManager.ScreenBoundsMin;
        Vector3 maxBounds = camPos + GameManager.ScreenBoundsMax;

        if (!IsWithinBounds(targetPos, minBounds, maxBounds))
            moveDelta = ClampDelta(moveDelta, minBounds, maxBounds);

        moveDelta.y = 0f;

        transform.Translate(moveDelta);

        //Energy
        energyMeter += energyRegen * Time.deltaTime;
        if (meterDisabled) energyMeter += energyRegenDisabledBoost * Time.deltaTime;
        energyMeter = Mathf.Clamp(energyMeter, 0, energyMeterMax);
        if (energyMeter >= energyMeterMax - 0.01f)
            meterDisabled = false;

        //Fire Cooldown
        if (!canFire) {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireCooldown)
            {
                fireTimer = 0f;
                canFire = true;
            }
        }

        //Lerping Movement angle
        float targetAngle = inputDir.x >= 0.1f ? -maxTurnAngle : 0f;
        targetAngle += inputDir.x <= -0.1f ? maxTurnAngle : 0f;
        turnAngle = Mathf.LerpAngle(turnAngle, targetAngle, Time.deltaTime * 10f);
        playerModel.transform.rotation = Quaternion.AngleAxis(turnAngle, Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ShakeManager.Shake(ShakeManager.HugeIntensity, 0.5f);
        }
    }

    /// <summary>
    /// Clamps the delta movement to be within the bounds
    /// </summary>
    /// <returns>The delta.</returns>
    /// <param name="target">Delta.</param>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    private Vector3 ClampDelta(Vector3 delta, Vector3 min, Vector3 max)
    {
        Vector3 current = transform.position;
        Vector3 target = current + delta;

        //Min
        if (target.x < min.x)
            target.x = min.x;
        if (target.y < min.y)
            target.y = min.y;
        if (target.z < min.z)
            target.z = min.z;
        //Max
        if (target.x > max.x)
            target.x = max.x;
        if (target.y > max.y)
            target.y = max.y;
        if (target.z > max.z)
            target.z = max.z;

        //Return new delta from current to target
        return target - current;
    }

    /// <summary>
    /// Checks wether a target vector is within the given bounds
    /// </summary>
    /// <returns><c>true</c>, if within bounds, <c>false</c> otherwise.</returns>
    /// <param name="target">Target.</param>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    private bool IsWithinBounds(Vector3 target, Vector3 min, Vector3 max)
    {
        if (target.x >= min.x && target.x <= max.x)
            if (target.y >= min.y && target.y <= max.y)
                if (target.z >= min.z && target.z <= max.z)
                    return true;
        return false;
    }

    /// <summary>
    /// Updates the input vectors.
    /// </summary>
    /// <param name="dir">Direction.</param>
    public void Movement(Vector2 dir)
    {
        inputDir = dir;
    }

    /// <summary>
    /// Fires the main gun
    /// </summary>
    public void Fire()
    {
        if (!canFire) return;

        //Call object pool to create a new bullet prefab
        if (bulletPool == null) {
            bulletPool = ObjectPoolManager.Instance.AddPool("Bullet", bulletPrefab, 50);
        }

        if (energyMeter >= bulletCost && !meterDisabled)
        {
            energyMeter -= bulletCost;
            bulletPool.Instantiate(transform.position, Quaternion.identity);
        } else {
            meterDisabled = true;
        }

        canFire = false; 
    }
}