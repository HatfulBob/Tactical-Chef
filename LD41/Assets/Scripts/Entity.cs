using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
    [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    public GameObject carcass;//what the food drops

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody m_Rigidbody;
    private Player player;
    private GameController gc;

    int speed;
    private int health;
    private static int maxHealth = 100;

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


    // Use this for initialization
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        health = maxHealth;
        speed = Random.Range(5, 100);
        m_Rigidbody = GetComponent<Rigidbody>();
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        var direction = player.transform.position - transform.position;
        Move(direction);
    }


    public void takeDmg(bool leaveCorpse)
    {
        if (leaveCorpse&&gc.CurrentObjective==4)
        {
            Instantiate(carcass,transform.position,Quaternion.identity);
        }
            //play death animation
            Destroy(gameObject);
    }

    public void Move(Vector3 moveDirection)
    {
        // If using torque to rotate the ball...
        if (m_UseTorque)
        {
            // ... add torque around the axis defined by the move direction.
            m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
        }
        else
        {
            // Otherwise add force in the move direction.
            m_Rigidbody.AddForce(moveDirection * m_MovePower);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (collision.gameObject.name.Contains("Fire"))
            {
                takeDmg(false);
            } else
            takeDmg(true);
            Destroy(collision.gameObject);
        }
    }

}