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
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();       //I can't figure out how to instatiate an instance of GameController
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //get trig values of the current camera angle
        float angle = Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad;
        float trig1 = Mathf.Cos(angle);
        float trig2 = Mathf.Sin(angle);

        //defines the partial vertical and horizontal sub-components of movement
        //by adding trig-defined magnitudes
        float adjustedHorizontal = moveHorizontal * trig1 + moveVertical * trig2;
        float adjustedVertical = moveVertical * trig1 - moveHorizontal * trig2;
        
        Vector3 movement = Vector3.ClampMagnitude(new Vector3(adjustedHorizontal, 0, adjustedVertical), 1.0f);
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
