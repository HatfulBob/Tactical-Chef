
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;
using System;

public class Player : MonoBehaviour
{
    public Weapon[] weapons;
    public Vector3 handPosition;//where to spawn weapon models
    public Camera viewCam;//used for spawning weapon models, they need to be children of the camera
    public Text ammoCounter;
    public GameObject projectile;//the pasta javelin
    public GameObject pastaSpawn;//where the pasta flies out of
    public GameObject fire;//what the flamwerffer werfs
    public GameObject fireSpawn;//the nozzle of the flamwerfer
    public GameObject bullet;
    public GameObject bulletSpawn;

    private GameController gc;
    private int currentWeapon = 0;
    [SerializeField] private int pastaAmmo = 0;


    public int CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

    }

    // Use this for initialization
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        //spawn 1st weapon
        weapons[0].gameObject.SetActive(true);
        weapons[1].gameObject.SetActive(false);
        weapons[2].gameObject.SetActive(false);
        currentWeapon = 0;
        ammoCounter.text = "";
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            switch (currentWeapon)
            {
                case 0: weapons[0].GetComponent<Animation>().Play();
                    weapons[0].GetComponent<AudioSource>().Play();
                    FireShot(); break;
                case 2: TossPasta(); break;
            }
        }
        if (Input.GetButton("Fire1") && currentWeapon == 1)
        {
            //werf flames
            if(!weapons[1].GetComponent<AudioSource>().isPlaying)
            weapons[1].GetComponent<AudioSource>().Play();
            FireFlame();
        } else
        {
            weapons[1].GetComponent<AudioSource>().Stop();
        }


        if (Input.GetButtonDown("Weapon 1"))
        {
            if (currentWeapon != 0)
            {
                //spawn 1st weapon
                weapons[0].gameObject.SetActive(true);
                weapons[1].gameObject.SetActive(false);
                weapons[2].gameObject.SetActive(false);
                currentWeapon = 0;
                ammoCounter.text = "";
            }
        }
        else
        if (Input.GetButtonDown("Weapon 2"))
        {
            if (currentWeapon != 1)
            {
                //spawn 2nd weapon
                weapons[0].gameObject.SetActive(false);
                weapons[1].gameObject.SetActive(true);
                weapons[2].gameObject.SetActive(false);
                currentWeapon = 1;
                ammoCounter.text = "";
            }
        }
        else
        if (Input.GetButtonDown("Weapon 3"))
        {
            if (pastaAmmo > 0)
            {
                if (currentWeapon != 2)
                {
                    //spawn 3rd weapon
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(false);
                    weapons[2].gameObject.SetActive(true);
                    currentWeapon = 2;
                }
            }
        }

        if (currentWeapon == 2)
        {
            if (pastaAmmo > 0)
            {
                ammoCounter.text = pastaAmmo + "/50";
            }
            else
            {
                //change to first weapon

                //spawn 1st weapon
                weapons[0].gameObject.SetActive(true);
                weapons[1].gameObject.SetActive(false);
                weapons[2].gameObject.SetActive(false);
                currentWeapon = 0;
                ammoCounter.text = "";
            }
        }
    }

    void TossPasta()
    {
        var pasta = (GameObject)Instantiate(
            projectile, pastaSpawn.transform.position, pastaSpawn.transform.rotation);

        // Add velocity to the bullet
        pasta.GetComponent<Rigidbody>().velocity = pasta.transform.up * 90;

        // Destroy the bullet after 4 seconds
        //Destroy(pasta, 4.0f);
        pastaAmmo--;
    }

    void FireShot()
    {
        var shot = (GameObject)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        // Add velocity to the bullet
        shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * 90;

        // Destroy the bullet after 2 seconds
        Destroy(shot, 2.0f);
    }

    void FireFlame()
    {
        // Create the Bullet from the Bullet Prefab
        var flame = (GameObject)Instantiate(
            fire, fireSpawn.transform.position, fireSpawn.transform.rotation);

        // Add velocity to the bullet
        flame.GetComponent<Rigidbody>().velocity = flame.transform.forward * 25;

        Destroy(flame, .4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Handle") && gc.CurrentObjective == 5)
        {
            FindObjectOfType<Pot>().KnockOver();
        }

        if (collision.gameObject.name.Contains("Squash"))
        {
            if (collision.gameObject.name.Contains("Tomato"))
            {
                FindObjectOfType<Pot>().TomatosAdded++;
                Destroy(collision.gameObject);
            }
            else
            {
                FindObjectOfType<Pot>().OnionsAdded++;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Handle") && gc.CurrentObjective == 5)
        {
            FindObjectOfType<Pot>().KnockOver();
        }

        if (other.gameObject.name.Contains("Squash"))
        {
            if (other.gameObject.name.Contains("Tomato"))
            {
                FindObjectOfType<Pot>().TomatosAdded++;
                Destroy(other.gameObject);
            }
            else
            {
                FindObjectOfType<Pot>().OnionsAdded++;
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.name.Contains("KILL"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        if (other.gameObject.name.Contains("Pick"))
        {
            pastaAmmo = 50;

            //spawn 3rd weapon
            weapons[0].gameObject.SetActive(false);
            weapons[1].gameObject.SetActive(false);
            weapons[2].gameObject.SetActive(true);
            currentWeapon = 2;
            ammoCounter.text = pastaAmmo + "/50";
        }

    }
}