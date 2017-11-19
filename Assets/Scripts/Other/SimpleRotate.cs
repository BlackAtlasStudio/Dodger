using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

    public bool rotationActive;
    public float rotationSpeed;

	private void Update()
    {
        if (rotationActive) transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
