using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shooting : MonoBehaviour {
    // Use this for initialization
    private RaycastHit hit;
    public int testPls = 5;

    private long lastShotTime;
    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
	void Start () {
        watch.Start();
        Debug.Log("Shooting Script Loaded");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            FireShot();
        }
	}

    void FireShot()
    {  
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            if(hit.collider.gameObject.tag == "Food")
            {
                GameObject obj = hit.collider.gameObject;
                lastShotTime = watch.ElapsedMilliseconds;
                if (lastShotTime + gameObject.GetComponent<Weapon>().TimeBetweenShot >= watch.ElapsedMilliseconds)
                {
                    obj.GetComponent<Entity>().takeDmg(gameObject.GetComponent<Weapon>().WeaponDamage);
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        }
    }
}
