using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Use this for initialization
    private RaycastHit hit;
    private Player player;
    private GameController controller;

    private long lastShotTime;
    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        watch.Start();
        controller = GameObject.FindObjectOfType<GameController>();
        Debug.Log("Shooting Script Loaded");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FireShot()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "Food")
            {
                GameObject obj = hit.collider.gameObject;
                DamageObject(obj);
            }
            else if (hit.collider.gameObject.GetComponent<Pot>() != null)
            {
                //you shot the pot
                GameObject obj = hit.collider.gameObject;
                if (player.CurrentWeapon == 1)
                {
                    //with the flamethrower
                    obj.GetComponent<Pot>().heatUp(gameObject.GetComponent<Weapon>().WeaponDamage);
                }
                else if (controller.CurrentObjective == 3)
                {

                    DamageObject(obj);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
        }
    }

    private void DamageObject(GameObject obj)
    {
        lastShotTime = watch.ElapsedMilliseconds;
        if (lastShotTime + gameObject.GetComponent<Weapon>().TimeBetweenShot >= watch.ElapsedMilliseconds)
        {
            obj.GetComponent<Entity>().takeDmg(true);
        }
    }
}
