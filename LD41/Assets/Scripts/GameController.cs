using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text objectiveText;
    public Text temperatureText;
    public string[] objectives;
    public Pot pot;//for getting count of pasta/tomatos/onions
    public int pastaNeeded = 20, tomatosNeeded = 10, onionsNeeded = 5;
    public GameObject player;
    public Camera victoryCamera;
    public GameObject pasta;

    private string secondObjective;//Save the second objective cause you're gonna have to keep changing it (for the pasta counter)
    private string fourthObjective;//Save the fourth objective cause you're gonna have to keep changing it (for the tomato/onion counters)
    private int currentObjective = 0;

    //pasta boiling timer
    private float timer;
    private float maxTimer = 20;//20 seconds required for boiling. multiply this value by 60 to make it take minutes
    
    public int CurrentObjective
    {
        get
        {
            return currentObjective;
        }

    }

    // Use this for initialization
    void Start()
    {
        secondObjective = objectives[1];
        fourthObjective = objectives[4];
        timer = maxTimer;

    }


    // Update is called once per frame
    void Update()
    {
        switch (currentObjective)
        {
            case 0:
                if (pot.Temperature > pot.BoilingTemp)
                    currentObjective++;
                break;
            case 1:
                objectiveText.text = secondObjective + " (" + pot.PastaAdded + "/" + pastaNeeded + ")";
                if (pot.PastaAdded >= pastaNeeded)
                {
                    currentObjective++;
                }
                break;
            case 2:
                if (timer < 0)
                {
                    currentObjective++;
                }
                else
                {
                    if (pot.Temperature >= pot.BoilingTemp)
                        timer -= Time.deltaTime;
                }
                break;
            case 3:
                if (pot.Broken)
                    currentObjective++;
                break;
            case 4:
                objectiveText.text = fourthObjective + " (" + pot.TomatosAdded + "/" + tomatosNeeded + "," + pot.OnionsAdded + "/" + onionsNeeded + ")";
                if (pot.TomatosAdded >= tomatosNeeded && pot.OnionsAdded >= onionsNeeded)
                    currentObjective++;
                break;
            case 5:
                if (pot.KnockedOver)
                    currentObjective++;
                break;

        }
        if (pot.KnockedOver || currentObjective > objectives.Length)
        {
            //the game is over
            victoryCamera.gameObject.SetActive(true);
            player.SetActive(false);
            pasta.SetActive(true);
            var ui = FindObjectOfType<Canvas>().GetComponentsInChildren<Text>();
            for(int i = 1;i < ui.Length; i++)
            {
                ui[i].text = "";
            }
            ui[0].enabled = true;
            GetComponent<AudioSource>().Play();
            enabled = false;

        }

        if (currentObjective != 1 && currentObjective != 4)
        {
            objectiveText.text = objectives[currentObjective];
        }

        temperatureText.text = pot.Temperature + "°C";
        if (pot.Temperature < 100)
        {
            temperatureText.color = Color.red;
        }
        else temperatureText.color = Color.green;
    }
}