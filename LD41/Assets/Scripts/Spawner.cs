using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject entity;
    public float interval;//how much time in seconds between each spawn
    public bool limited = false;
    public int limit = 50;
    private float timer=1f;

	// Use this for initialization
	void Start () {
        timer = interval;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (!limited)
            {
                if (GameObject.FindGameObjectsWithTag("Food").Length < limit)
                {
                    Object.Instantiate(entity, transform.position, transform.rotation);
                }
            }
            timer = interval;
        }
	}
}
