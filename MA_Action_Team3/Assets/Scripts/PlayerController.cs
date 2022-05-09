using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour{
	
	public SkeletonAnimation skeletonAnimation;
	
	//Trigger item #1: a class variable for the AnimationReferenceAsset
	public AnimationReferenceAsset idle, walking, jumping, crouch, death, hurt, fall, punch, punch2, stomp, running, wave;
	public string currentState;
	public float speed;
	public float movement;
	private Rigidbody2D rigidbody;
	private Vector3 characterScale;
	public string currentAnimation;
	public float jumpSpeed;
	public string previousState;
	
	// Added Content to create more controls
	public bool isAlive = true;
	public float startSpeed = 3f;
	public float runSpeed = 6f;
	public bool isHit1 = true;
	
	//jump variables
	public Transform feet;
	public bool canJump = false;
	public int jumpTimes = 0;
	public LayerMask groundLayer;
    public LayerMask enemyLayer;
	
	
    void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
        currentState = "idle";
		characterScale = transform.localScale;
		SetCharacterState(currentState);
	}


    void Update(){
        Move();	
		
    }
	
	// Sets character animation 
	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
	{
		if(animation.name.Equals(currentAnimation))
		{
			return;
		}
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
		animationEntry.TimeScale = timeScale;
		animationEntry.Complete += AnimationEntry_Complete;
		currentAnimation = animation.name;
	}
	
	private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
	{
		if(currentState.Equals("Jumping"))
		{
			SetCharacterState(previousState);
		}
		
		//Trigger item #5: After animation is done, set the state back to the previous state
		if((currentState.Equals("Punch1"))||(currentState.Equals("Punch2")))
		{
			SetCharacterState(previousState);
		}
		
	}
	
	//checks character state and sets the animation accordingly.
	public void SetCharacterState(string state)
	{
		if (state.Equals("Idle"))
		{
			SetAnimation(idle, true, 1f);
		}
		else if (state.Equals("Walking"))
		{
			SetAnimation(walking, true, 2f);
		}
		else if (state.Equals("Running"))
		{
			SetAnimation(running, true, 1f);
		}
		
		//Trigger item #2: set the connection between the state and the animation (animation name, looping=false, speed multiplier=1f)
		else if (state.Equals("Punch1"))
		{
			SetAnimation(punch, false, 1f);
		}
		else if (state.Equals("Punch2"))
		{
			SetAnimation(punch2, false, 1f);
		}
		else if (state.Equals("Jumping"))
		{
			SetAnimation(jumping, false, 0.35f);
		}
	else
		{
			SetAnimation(idle, true, 1f);
		}
		
		currentState = state;
	}
	
	public void Move()
	{
		
		
			movement = Input.GetAxis("Horizontal");
			rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
		if (movement != 0)
		{
			if ((!currentState.Equals("Jumping"))&&(Input.GetButtonDown("Run"))){
			SetCharacterState("Running");
			speed=runSpeed;
			}
			else if ((!currentState.Equals("Jumping"))&& (!currentState.Equals("Running"))){
			SetCharacterState("Walking");
			speed=startSpeed;
			}
			
			
			
				if(movement > 0)
				{
					transform.localScale = new Vector2(characterScale.x, characterScale.y);
				}	
				else	
				{
					transform.localScale = new Vector2(-characterScale.x, characterScale.y);
				}
		}
		
		
		else	
		{
			if ((!currentState.Equals("Jumping"))&&(!currentState.Equals("Punch1"))&&(!currentState.Equals("Punch2")))
				{
					SetCharacterState("Idle");
				}
		}
		
		if (Input.GetButtonDown("Jump")){
			Jump();
		}
		
		//Trigger item #3: set the input in the Move() function
		if (Input.GetButtonDown("Attack")){
			PlayerAttack();
		}
	}
	
	public void Jump(){
		if ((IsGrounded())||(jumpTimes <= 1)){canJump=true;}
		else if (jumpTimes > 1){canJump=false;}
		
		if (canJump){
			jumpTimes+=1;
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
			if (!currentState.Equals("Jumping")){
				previousState = currentState;
				SetCharacterState("Jumping");
			}
		}
	}
	
	public bool IsGrounded() {
		Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
		Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
		if ((groundCheck != null) || (enemyCheck != null)) {
			jumpTimes = 0;
			return true;
			//Debug.Log("I can jump now!");
		}
		return false;
	}
	
	
	//Trigger item #4: a function that records previous state (to return to idle)
	public void PlayerAttack(){
		if ((!currentState.Equals("Punch1"))||(!currentState.Equals("Punch2"))){
			previousState = "Idle";
			//SetCharacterState("Punch1");
			
			if (isHit1){SetCharacterState("Punch1");}
			else {SetCharacterState("Punch2");}
			isHit1 = !isHit1;
			//add PlayerAttackMelee script to the player, following tutorial for hitpoint and enemies layer
		}
	}
	
}

