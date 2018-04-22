using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingWater : MonoBehaviour {
    private Pot pot;
	// Use this for initialization
	void Start () {
        pot = FindObjectOfType<Pot>();
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Pasta"))
        {
            pot.PastaAdded++;
            Destroy(collision.gameObject);
        }
    }

}
