using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed;
    [Range(-5f, 5f)]
    public float edgeBuffer;

    private ObjectPool bulletPool;

    private Vector2 inputDir;

    private Vector3 screenBoundsMin;
    private Vector3 screenBoundsMax;

    private Vector3 camOffset;

    private void Start()
    {
        //Calculates the perspective screen min and max world points intersecting with the playing plane
        camOffset = Camera.main.transform.position;

        Ray minRay = Camera.main.ScreenPointToRay(Vector3.zero);
        Ray maxRay = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        RaycastHit hit;

        if (Physics.Raycast(minRay, out hit, LayerMask.GetMask("PlayField")))
            screenBoundsMin = hit.point;
        else
            screenBoundsMin = new Vector3(-10f, 0f, -5f);

        if (Physics.Raycast(maxRay, out hit, LayerMask.GetMask("PlayField")))
            screenBoundsMax = hit.point;
        else
            screenBoundsMax = new Vector3(10f, 0f, 5f);

        screenBoundsMin -= camOffset;
        screenBoundsMax -= camOffset;

        screenBoundsMin.y = 0f;
        screenBoundsMax.y = 0f;

        Vector3 edgeBufferVec = new Vector3(edgeBuffer, 0f, edgeBuffer);
        screenBoundsMin += edgeBufferVec;
        screenBoundsMax -= edgeBufferVec;
    }

    private void Update()
    {
        //Creates a new movement delta based on the input
        Vector3 moveDelta = Vector3.zero;
        moveDelta.x = inputDir.x * moveSpeed;
        moveDelta.z = inputDir.y * moveSpeed;
        moveDelta *= Time.deltaTime;

        Vector3 targetPos = transform.position + moveDelta;

        Vector3 camPos = Camera.main.transform.position;
        Vector3 minBounds = camPos + screenBoundsMin;
        Vector3 maxBounds = camPos + screenBoundsMax;

        if (!IsWithinBounds(targetPos, minBounds, maxBounds))
            moveDelta = ClampDelta(moveDelta, minBounds, maxBounds);

        moveDelta.y = 0f;

        transform.Translate(moveDelta);
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
        //Call object pool to create a new bullet prefab
    }
}