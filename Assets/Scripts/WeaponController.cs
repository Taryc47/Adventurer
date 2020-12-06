using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    GameObject[] enemies;

    public bool colliding;
    //public bool isEnemyProjectile = false;
    
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var enemy in enemies)
        {
            if (other.gameObject.tag == "Player")
            {
                if (colliding == false)
                {
                    Colliding();
                    PlayerController.health -= enemy.GetComponent<NewEnemyController>().damage;
                    Debug.Log("Player Hit");

                }
            }

            else if (other.gameObject.tag == "Enemy")
            {
                if (colliding == false)
                {
                    Colliding();
                    enemy.GetComponent<NewEnemyController>().health -= PlayerController.damage;
                    Debug.Log("Enemy Hit!");

                }
            }
        }
    }

        /*if (other.gameObject.tag == "Player")
        {
            if (colliding == false)
            {
                Colliding();
                PlayerController.health -= enemy.GetComponent<NewEnemyController>().damage;
                Debug.Log("Player Hit");
                
            }
        }

        else if (other.gameObject.tag == "Enemy")
        {
            if (colliding == false)
            {
                Colliding();
                enemy.GetComponent<NewEnemyController>().health -= PlayerController.damage;
                Debug.Log("Enemy Hit!");
                
            }
        }
    }*/

    bool Colliding()
    {
       return colliding = true;
    }
}

