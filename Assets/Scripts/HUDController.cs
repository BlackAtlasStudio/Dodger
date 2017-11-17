using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [Header("Energy Slider HUD")]
    public Gradient energyGradient;
    public Vector3 energySliderOffset;

    [Header("HUD References")]
    public Slider energyMeter;
    public Image energyMeterImage;

    private PlayerController _player;
    private float smoothing = 1f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>() as PlayerController;
        if (_player == null) {
            Debug.Log("Could not find player controller!");
        }

        energyMeter.minValue = 0;
        energyMeter.maxValue = _player.energyMeter;
    }

    private void Update()
    {
        float curVal = _player.energyMeter;
        energyMeter.value = Mathf.Lerp(energyMeter.value, curVal, smoothing);

        energyMeterImage.color = energyGradient.Evaluate(energyMeter.normalizedValue);

        energyMeter.transform.position = _player.transform.position + energySliderOffset;
    }
}
