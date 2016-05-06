using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float upgradeAmt; //How much does the powerup increase speed
	public float powerTime; //How long the powerup lasts in seconds

	private Rigidbody rb;
    private GameController game;
	private AudioSource audioSource;
	private bool isJumping, isPowered;
	private float powerEnd;
	private float upgrade;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();       //I can't figure out how to instatiate an instance of GameController
		isJumping=false;
		audioSource = GetComponent<AudioSource> ();
		upgrade = 1;
    }
	
	bool isGrounded() {
		RaycastHit hit;
		
		//Casts a sphere of radius -0.1f less than the length of the radius of the player's sphere collider. This sphere is sent downward a maximum distance of 0.2f (0.1f below the
		//collider boundary) and attempts to detect a collision with the ground.
		return Physics.SphereCast(gameObject.transform.position, GetComponent<SphereCollider>().radius -0.1f, Vector3.down, out hit, 0.2f);
	}
	
	void OnCollisionEnter(Collision collisionInfo) {
	    //This function is used to reduce occurrences of jumping before hitting the ground due to the isGrounded() implementation.
		//May still occur if player hits a wall before falling to the floor.
		isJumping = false;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		if (!isJumping && isGrounded() && Input.GetKeyDown ("space"))
		{
			isJumping = true;
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpHeight, rb.velocity.z); //vertical velocity is current vertical velocity + jump height
			//rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z); //alternative -- vertical velocity on jump will ALWAYS be the jump height
		}
		
		if (isPowered) {
			if (Time.time > powerEnd) {
				upgrade = 1;
				isPowered = false;
			}
		}

        //get trig values of the current camera angle
        float angle = Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad;
        float trig1 = Mathf.Cos(angle);
        float trig2 = Mathf.Sin(angle);

        //defines the partial vertical and horizontal sub-components of movement
        //by adding trig-defined magnitudes
        float adjustedHorizontal = moveHorizontal * trig1 + moveVertical * trig2;
        float adjustedVertical = moveVertical * trig1 - moveHorizontal * trig2;
        
        ///Vector3 movement = Vector3.ClampMagnitude(new Vector3(adjustedHorizontal, moveHeight, adjustedVertical), 1.0f);
		Vector3 movement = new Vector3(adjustedHorizontal,0.0f,adjustedVertical);
		rb.AddForce(movement * speed * upgrade);
		Vector2 clampedVelocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x, rb.velocity.z), speed * upgrade);
		rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.y);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Cat"))
		{
			other.gameObject.SetActive (false);
			game.AddMorsel();
			audioSource.Play ();
		}
		if (other.gameObject.CompareTag("PowerUp"))
		{
			other.gameObject.SetActive (false);
			isPowered = true;
			upgrade = upgradeAmt;
			powerEnd = Time.time + powerTime;
			audioSource.Play ();
		}
		if (other.gameObject.CompareTag("Goal"))
		{
			game.ExitReached();
		}
	}
    
}