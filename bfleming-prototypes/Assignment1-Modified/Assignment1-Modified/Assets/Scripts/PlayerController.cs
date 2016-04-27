using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;

    private GameObject gc;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

	void FixedUpdate()
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
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            gc.GetComponent<GameController>().AddToCount();
        }
    }
}
