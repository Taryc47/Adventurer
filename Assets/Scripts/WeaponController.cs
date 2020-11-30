using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;

    public bool colliding;
    //public float playerDamage;

    void Start()
    {
        
    }

    private void Update()
    {
        //playerDamage = PlayerController.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (colliding == false)
            {
                Colliding();
                PlayerController.health -= enemy.damage;
                Debug.Log("Player Hit");
            }
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (colliding == false)
            {
                Colliding();
                other.gameObject.GetComponent<EnemyController>().health -= PlayerController.damage;
                Debug.Log("Enemy Hit!");
            }
        }
    }

    bool Colliding()
    {
       return colliding = true;
    }
}

