using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jump; //how long in seconds to jump
	public float jumpScale; //How high to jump every second
	public float upgradeAmt; //How much does the powerup increase speed
	public float powerTime; //How long the powerup lasts in seconds

	private Rigidbody rb;
    private GameController game;

	private bool isJumping, isPowered;
	private float upgrade;
	private bool canJump;
	private float jumpEnd;
	private float powerEnd;
	private AudioSource audioSource;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();       //I can't figure out how to instatiate an instance of GameController
		isJumping=false;
		audioSource = GetComponent<AudioSource> ();
		upgrade = 1;
    }

	void OnCollisionEnter(Collision collisionInfo) {
		canJump = true;
	}

	void OnCollisionExit(Collision collisionInfo) {
		canJump = false;
	}


	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
		float moveHeight;

		if (canJump && Input.GetKeyDown ("space"))
		{
			isJumping = true;
			jumpEnd = Time.time + jump;
		}

		if (isJumping) {
			//check that the alloted time hasn't passed
			if (Time.time < jumpEnd) {
				moveHeight = jumpScale;
			} else 
			{
				moveHeight = jumpScale;
				isJumping = false;
			}
		} else {
			moveHeight = 0;
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

		Vector3 clampedMovement = Vector3.ClampMagnitude(new Vector3(adjustedHorizontal,moveHeight,adjustedVertical),0.8f);
		Vector3 movement = new Vector3 (clampedMovement.x, moveHeight, clampedMovement.z);
		rb.AddForce(movement * speed * upgrade);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Morsel"))
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