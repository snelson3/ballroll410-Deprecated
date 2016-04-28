using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
    public GameController game;

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
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
            //Sam needs to create the powerup function and call it here
        }

    }

	//void SetCountText ()
	//{
		//countText.text = "Count: " + count.ToString ();
		//if (count >= 12)
		//{
			//winText.text = "You Win!";
		//}
	//}
}
