using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon[] weapons;
    public Vector3 handPosition;//where to spawn weapon models
    public Camera viewCam;//used for spawning weapon models, they need to be children of the camera

    private int currentWeapon = 0;
    private int pastaAmmo = 0;

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
        if (Input.GetButtonDown("Weapon 1"))
        {
            if (currentWeapon != 0)
            {
                //spawn 1st weapon
                currentWeapon = 0;
            }
        }
        else
        if (Input.GetButtonDown("Weapon 2"))
        {
            if (currentWeapon != 1)
            {
                //spawn 2nd weapon
                currentWeapon = 1;
            }
        }
        else
        if (Input.GetButtonDown("Weapon 3"))
        {
            if (currentWeapon != 2)
            {
                //spawn 3rd weapon
                currentWeapon = 2;
            }
        }
    }
}