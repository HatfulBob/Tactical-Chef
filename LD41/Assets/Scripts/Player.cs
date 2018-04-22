using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Weapon[] weapons;
    public Vector3 handPosition;//where to spawn weapon models
    public Camera viewCam;//used for spawning weapon models, they need to be children of the camera
    public Text ammoCounter;
    public GameObject projectile;//the pasta javelin
    public GameObject pastaSpawn;//where the pasta flies out of
    private int currentWeapon = 2;
    [SerializeField]  private int pastaAmmo = 0;

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

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (currentWeapon) {
                case 0:
                case 1: GetComponent<Shooting>().FireShot(); break;
                case 2: TossPasta();break;
            }
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
                    ammoCounter.text = pastaAmmo+"/50";
                }
            }
        }

        if (currentWeapon == 2)
        {
            if (pastaAmmo > 0)
            {
                ammoCounter.text = pastaAmmo+"/50";
            } else
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
        // Create the Bullet from the Bullet Prefab
        var pasta = (GameObject)Instantiate(
            projectile, pastaSpawn.transform.position, pastaSpawn.transform.rotation);

        // Add velocity to the bullet
        pasta.GetComponent<Rigidbody>().velocity = pasta.transform.forward * 2;

        // Destroy the bullet after 4 seconds
        //Destroy(pasta, 4.0f);
        pastaAmmo--;
    }
}