﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    int speed;
    private int health;
    private static int maxHealth;

    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public static int MaxHealth
    {
        get
        {
            return maxHealth;
        }

    }

    public void takeDmg(int ammount)
    {
        health = -ammount;
        if(health <= 0)
        {
            //play death animation
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        maxHealth = 100;
        speed = Random.Range(5, 100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
