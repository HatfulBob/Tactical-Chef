using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {

    List<Entity> initiative,entities;
	// Use this for initialization
	void Start () {
        initiative = new List<Entity>();
	}
	void StartCombat()
    {
        initiative.AddRange(GameObject.FindObjectsOfType<Entity>());
        List<Entity> list = (List<Entity>)initiative.OrderByDescending(x => x.Speed);// for 3, 2, 1
        initiative = list; 

    }

    // Update is called once per frame
    void Update () {

    }
}
