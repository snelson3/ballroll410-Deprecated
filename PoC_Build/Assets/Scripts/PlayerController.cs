using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jump; //how long in seconds to jump
	public float jumpScale; //How high to jump every second

	private Rigidbody rb;
    private GameController game;

	private bool isJumping;
	private float jumpStart, jumpEnd;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();       //I can't figure out how to instatiate an instance of GameController
		isJumping=false;
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
		float moveHeight;

		if (!isJumping && Input.GetKeyDown ("space"))
		{
			isJumping = true;
			jumpStart = Time.time;
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

        //get trig values of the current camera angle
        float angle = Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad;
        float trig1 = Mathf.Cos(angle);
        float trig2 = Mathf.Sin(angle);

        //defines the partial vertical and horizontal sub-components of movement
        //by adding trig-defined magnitudes
        float adjustedHorizontal = moveHorizontal * trig1 + moveVertical * trig2;
        float adjustedVertical = moveVertical * trig1 - moveHorizontal * trig2;
        
        ///Vector3 movement = Vector3.ClampMagnitude(new Vector3(adjustedHorizontal, moveHeight, adjustedVertical), 1.0f);
		Vector3 movement = new Vector3(adjustedHorizontal,moveHeight,adjustedVertical);
		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Morsel"))
		{
			other.gameObject.SetActive (false);
            game.AddMorsel();
		}
        if (other.gameObject.CompareTag("PowerUp"))
        {
            //power up code needs to be added
        }
        if (other.gameObject.CompareTag("Goal") && (game.MorselCount() == game.minMorsels))
        {
            game.gameOver = true;
        }
        else if (other.gameObject.CompareTag("Goal") && (game.MorselCount() != game.minMorsels))
        {
            game.gameOverText.text = "You need " + game.minMorsels + " morsels to finish :(";
        }
	}
    
}
