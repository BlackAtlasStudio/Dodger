using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [Header("Energy Slider HUD")]
    public Gradient energyGradient;
    public Vector3 energySliderOffset;

    [Header("Health Slider HUD")]
    public Gradient healthGradient;

    [Header("HUD References")]
    public Slider energyMeter;
    public Image energyMeterImage;
    public Slider healthMeter;
    public Image healthMeterImage;

    private PlayerController player;
    private HealthController playerHealth;
    private float smoothing = 1f;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>() as PlayerController;
        if (player == null) {
            Debug.Log("Could not find player controller!");
        }

        GameObject p = GameObject.Find("Player");
        if (p == null) {
            Debug.Log("Could not find player");
            return;
        }
        playerHealth = p.GetComponent(typeof(HealthController)) as HealthController;
        Debug.Assert(playerHealth != null);

        energyMeter.minValue = 0;
        energyMeter.maxValue = player.energyMeter;

        healthMeter.minValue = 0;
        healthMeter.maxValue = playerHealth.Health;
    }

    private void Update()
    {
        float curVal = player.energyMeter;
        energyMeter.value = Mathf.Lerp(energyMeter.value, curVal, Time.deltaTime * 5f);
        energyMeterImage.color = energyGradient.Evaluate(energyMeter.normalizedValue);
        energyMeter.transform.position = player.transform.position + energySliderOffset;

        float curHealth = playerHealth.Health;
        healthMeter.value = Mathf.Lerp(healthMeter.value, curHealth, Time.deltaTime * 4f);
        healthMeterImage.color = healthGradient.Evaluate(healthMeter.normalizedValue);
    }
}
