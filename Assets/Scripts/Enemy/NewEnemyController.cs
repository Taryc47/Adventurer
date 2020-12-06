using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Attack,
    Die
};

public enum EnemyType
{
    Melee,
    Ranged
};

public class NewEnemyController : MonoBehaviour
{
    GameObject player;
    WeaponController sword;
    //public GameObject arrowPrefab;

    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public Vector3 playerPos;

    public float health;
    public float sightRange;
    public float speed;
    public float attackRange;
    public float coolDown;
    public float projectileSpeed;
    public float damage;

    public bool notInRoom = false;
    private bool attackCoolDown = false;
    private bool chooseDir = false;
    private bool dead = false;

    private Animator anim;
    private Vector3 randomDir;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        GameObject g = GameObject.FindWithTag("EnemyWeapon");
        sword = g.GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3 (player.transform.position.x, this.transform.position.y, player.transform.position.z);

        switch(currState)
        {
            /*case (EnemyState.Idle):
                Idle();
                break;*/
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                Die();
                break;
        }
        if (!notInRoom)
        {
            if (IsPlayerInRange(sightRange) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(sightRange) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }

            if (health <= 0)
            {
                currState = EnemyState.Die;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float sightRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) 
            <= sightRange;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        // Random direction, rotate on y axis
        randomDir = new Vector3(0, Random.Range(0, 360), 0);
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, 
            Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    private IEnumerator CoolDown()
    {
        attackCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        attackCoolDown = false;
    }

    void Idle()
    {

    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(sightRange))
        {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        transform.LookAt(playerPos);

        if (!attackCoolDown)
        {
            switch (enemyType)
            {
                case(EnemyType.Melee):
                    sword.colliding = false;
                    anim.Play("Enemy_AttackSword");
                    
                    StartCoroutine(CoolDown());
                    break;
                    // Will not be implemented in time
                    /*case (EnemyType.Ranged):
                        GameObject arrow = Instantiate(arrowPrefab, transform.position, 
                            Quaternion.identity) as GameObject;
                        arrow.GetComponent<WeaponController>().GetPlayer(player.transform);
                        //arrow.AddComponent<Rigidbody>();
                        arrow.GetComponent<WeaponController>().isEnemyProjectile = true;
                    StartCoroutine(CoolDown());*/
            }
        }
    }

    public void Die()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        dead = true;
        Destroy(this.gameObject);
    }
}

