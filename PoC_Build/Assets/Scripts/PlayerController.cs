using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
    private GameController game;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        //game = GameObject.FindGameObjectsWithTag("GameController");       //I can't figure out how to instatiate an instance of GameController

    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
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
