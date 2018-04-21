using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    [SerializeField] private float temperature = 20f;
    [SerializeField] private float boilingTemp = 100f;//temperature the pasta cooks
    [SerializeField] private float coolingTemp = 0.5f;//every so often, how much heat does the pot lose

    private float timer;
    private float maxTimer = 3f;//3 seconds

    // Use this for initialization
    void Start()
    {
        timer = maxTimer;
    }

    public void heatUp(float damage)
    {
        temperature += damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            temperature -= coolingTemp;
            timer = maxTimer;
        }
    }
}