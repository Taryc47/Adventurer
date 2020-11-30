using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputAction movement,
                        mainhand,
                        offhand,
                        dodge,
                        block,
                        blockReleased,
						magic;

	
	public CharacterController controller;
    public WeaponController sword;
    //public PlayerController player;
    private Animator anim;

    public float newSpeed;
    public bool blocking = false;
	
	void OnEnable()
	{
		movement.Enable();
		dodge.Enable();
		mainhand.Enable();
        block.Enable();
        blockReleased.Enable();
    }
	
	void OnDisable()
	{
		movement.Disable();
		dodge.Disable();
		mainhand.Disable();
        block.Disable();
        blockReleased.Disable();
    }
	
	// Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
		
        controller.GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
        GameObject g = GameObject.FindWithTag("PlayerWeapon");
        sword = g.GetComponent<WeaponController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        newSpeed = PlayerController.speed;
        PlayerMovement();
		PlayerActions();
    }
	
	// Input for player movement
	void PlayerMovement()
	{
		Vector2 inputVector = movement.ReadValue<Vector2>();
		
		Vector3 finalVector = new Vector3();
		finalVector.x = inputVector.x;
		finalVector.z = inputVector.y; // switches the y vector input to move on the z axis
		
		controller.Move(finalVector * Time.deltaTime * newSpeed);
		
		Vector3 newPosition = new Vector3(finalVector.x, 0.0f, finalVector.z);

        // Player will face whatever direction they were facing when block is pressed.
        if (!blocking)
        {
            transform.LookAt(newPosition + transform.position);
        }

        transform.Translate(newPosition * newSpeed * Time.deltaTime, Space.World);
	}
	
	void PlayerActions()
	{
		if(dodge.triggered)
		{
			Dodge();
		}
		
		if(mainhand.triggered)
		{
			Attack();
		}

        if (block.triggered)
        {
            Block();
        }

        if (blockReleased.triggered)
        {
            BlockReleased();
        }
	}
	
	void Dodge()
	{
		transform.localScale *= 1.1f;
	}
	
	void Attack()
	{
        sword.colliding = false;

        anim.Play("Attack_Sword1");
	}

    void Block()
    {
        blocking = true;
    }

    void BlockReleased()
    {
        blocking = false;
    }

}
