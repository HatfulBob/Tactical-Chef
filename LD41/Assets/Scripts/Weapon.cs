using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private int weaponDamage = 20;
    private float timeBetweenShot = 0.1f;
    public bool animating = false;
    private Animator fire;

    public int WeaponDamage
    {
        get
        {
            return weaponDamage;
        }
    }

    public float TimeBetweenShot
    {
        get
        {
            return timeBetweenShot;
        }
    }

	// Use this for initialization
	void Start () {
        fire = GetComponent<Animator>();
        fire.enabled = false;
	}

    // Update is called once per frame
    void Update() {
	}
}
