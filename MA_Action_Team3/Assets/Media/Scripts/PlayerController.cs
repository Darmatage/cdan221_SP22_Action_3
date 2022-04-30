using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour{
	
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, walking, jumping, crouch, death, hurt, fall, punch, punch2, stomp, running;
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
	
    // use this for initialization.
    void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
        currentState = "idle";
		characterScale = transform.localScale;
		SetCharacterState(currentState);
	}

    // Update is called once per frame
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
		
		if (Input.GetButtonDown("Jump"))
		{
		Jump();
		}
		
		if (Input.GetButtonDown("Attack")){
			PlayerAttack();
		}
	}
	
	public void Jump()
	{
		rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
		if (!currentState.Equals("Jumping"))
		{
			previousState = currentState;
		SetCharacterState("Jumping");
		}
	}
	public void PlayerAttack()
	{
		if (!currentState.Equals("Punch1"))
		{
			previousState = "Idle";
		SetCharacterState("Punch1");
		}
	}
}

