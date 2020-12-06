using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public static float health;
    public float moveSpeed;
    public float attackTime;
    public float attackRange;
    public static float damage;

    public WeaponController sword;
    public NavMeshAgent enemy;
    public Transform player;
    public GameObject prefab;
    private float dist;
    public float howClose;
    public float lastAttack;
    
    private Animator anim;
    

    void Awake()
    {
        lastAttack = attackTime;

        if (prefab.name == "Bandit")
        {
            health = 4.0f;
            moveSpeed = 8.0f;
            attackTime = 1.0f;
            attackRange = 2.75f;
            damage = 1f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        GameObject g = GameObject.FindWithTag("EnemyWeapon");
        sword = g.GetComponent<WeaponController>();
         
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().speed = moveSpeed;

        enemy.SetDestination(player.position);
        dist = Vector3.Distance(player.position, transform.position);

        if (dist <= howClose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);   
        }

        // attack 
        if (dist <= attackRange && CanAttack())
        {
            sword.colliding = false;
            Attack();
        }
        else
        {
            lastAttack += Time.deltaTime;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    bool CanAttack()
    {
        return lastAttack >= attackTime;    
    }

    void Attack()
    {
        lastAttack = 0f;

        enemy.SetDestination(transform.position);
        transform.LookAt(player);

        anim.Play("Enemy_AttackSword");
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
