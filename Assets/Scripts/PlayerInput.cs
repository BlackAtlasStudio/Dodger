using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
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
        inputDir.x = Input.GetAxis("Horizontal");
        inputDir.y = Input.GetAxis("Vertical");

        _controller.UpdateInput(inputDir);
    }
}
