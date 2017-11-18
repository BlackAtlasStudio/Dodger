using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent(typeof(PlayerController)) as PlayerController;
        if (_controller == null) _controller = gameObject.AddComponent(typeof(PlayerController)) as PlayerController;
    }

    private void Update()
    {
        Vector2 inputDir = Vector2.zero;
        inputDir.x = Input.GetKey(KeyCode.A) ? -1f : 0f;
        inputDir.x += Input.GetKey(KeyCode.D) ? 1f : 0f;
        inputDir.y = Input.GetKey(KeyCode.S) ? -1f : 0f;
        inputDir.y += Input.GetKey(KeyCode.W) ? 1f : 0f;

        if (inputDir.magnitude <= 0.05f) {
            inputDir.x = Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f;
            inputDir.x += Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;
            inputDir.y = Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;
            inputDir.y += Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
        }

        _controller.Movement(inputDir);

        if (Input.GetKey(KeyCode.Space)) {
            _controller.Fire();
        }
    }
}
