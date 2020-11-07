using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{	
	public InputAction 	movement, 
						mainhand, 
						offhand, 
						dodge, 
						magic;
	
	public CharacterController controller;
	public float speed;
	
	private Animator anim;
	
	void OnEnable()
	{
		movement.Enable();
		dodge.Enable();
		mainhand.Enable();
	}
	
	void OnDisable()
	{
		movement.Disable();
		dodge.Disable();
		mainhand.Disable();
		
	}
	
	// Start is called before the first frame update
    void Start()
    {
		speed = 5f;
        controller.GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		
		
    }

    // Update is called once per frame
    void Update()
    {
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
		
		controller.Move(finalVector * Time.deltaTime * speed);
		
		Vector3 newPosition = new Vector3(finalVector.x, 0.0f, finalVector.z);
        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * speed * Time.deltaTime, Space.World);
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
	}
	
	void Dodge()
	{
		transform.localScale *= 1.1f;
	}
	
	void Attack()
	{
		anim.Play("Attack_Sword1");
	}
	
}
