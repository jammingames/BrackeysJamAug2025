using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject ground;
	FPS_Control       inputControl;
	Vector2           move;
	public float             movementForce=50000;
	public float             jumpForce  =500;
	Rigidbody         rb;
	bool              isGrounded =true;
	// Start is called before the first frame update
	void Start()
	{
		inputControl=new FPS_Control();
		inputControl.Player_Map.Enable();
		rb   = GetComponent<Rigidbody>();
		move = Vector2.zero;
		
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		GroundCheck();
		PlayerMove();
		if(isGrounded && inputControl.Player_Map.Jump.ReadValue<float>()>0)
		{
			Playerjump();
		} 

       

	}
	void GroundCheck()
	{
		if(transform.position.y-ground.transform.position.y>1.5)
		{
			isGrounded=false;

			rb.linearDamping=0.1f;
		}
		else{
			isGrounded = true;
			rb.linearDamping    = 3f;
		}

	}
	void PlayerMove()
	{
		move=inputControl.Player_Map.Movement.ReadValue<Vector2>();
		float forcez =move.x          *movementForce *Time.deltaTime;    
		float forcex =move.y          *movementForce *Time.deltaTime;    
		rb.AddForce(transform.forward *forcex, ForceMode.Force);
		rb.AddForce(transform.right   *forcez, ForceMode.Force);

	}
	void Playerjump()
	{
		rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); 
	}
}