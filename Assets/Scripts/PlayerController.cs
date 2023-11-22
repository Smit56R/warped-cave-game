using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Movement
	public float maxSpeed;
	private Rigidbody2D myRB;
	private Animator myAnim;
	private bool facingRight;
	public float jumpVelocity;
	public CircleCollider2D circleCollider;
	[SerializeField] private LayerMask platformsLayerMask;

	//Shooting
	public Transform origin;
	public GameObject shot;
	public float fireRate = 2f;
	float nextFire = 0f;
	public bool Isavailable = false;

	//Ducking
	public CircleCollider2D duckCollider;

	public TextMeshProUGUI textObject;




	void Start()
	{
		myRB = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		facingRight = true;
	}

	// Update is called once per frame
	void Update()
	{
		duckCollider.offset = new Vector2(0f, 0.04f);

		//Player jump
		if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
			myAnim.SetTrigger("Jump");
			myRB.velocity = Vector2.up * jumpVelocity;
		
		}

		//player shooting
		if (IsGrounded() && Input.GetAxisRaw("Fire1") > 0)
		{
			//myRB.velocity = new Vector2(0f,0f);
			myAnim.SetTrigger("Shoot");
			
			fireShot();

        }

		//player duck
		if (IsGrounded() && Input.GetKeyDown(KeyCode.X))
		{
			myAnim.SetTrigger("Duck");
			duckCollider.offset = new Vector2(0f, -0.14f);
			
		}
		

	}

	
	

	void FixedUpdate()
	{

		if (IsGrounded())
		{
			float move = Input.GetAxis("Horizontal");
			myAnim.SetFloat("Speed", Mathf.Abs(move));
			myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

			if (move > 0 && !facingRight)
			{
				flip();
			}
			else if (move < 0 && facingRight)
			{
				flip();
			}
		}


	}
	void flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private bool IsGrounded() 
	{
		RaycastHit2D raycastHit2d = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
		Debug.Log(raycastHit2d.collider);
		return raycastHit2d.collider != null;
	}

	void fireShot()
	{
		if ( Isavailable && Time.time > nextFire )
		{
			nextFire = Time.time + 1f/fireRate;
			if (facingRight)
			{
				Instantiate(shot, origin.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			}
			else if (!facingRight)
			{
				Instantiate(shot, origin.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
			}
		}
		else if(!Isavailable)
        {
			textObject.GetComponent<NoEnergyTextScript>().Appear();
		}
	}

	void OnDrawGizmosSelected()
	{

		Gizmos.DrawWireCube(circleCollider.bounds.center, circleCollider.bounds.size);
	}

	

}
