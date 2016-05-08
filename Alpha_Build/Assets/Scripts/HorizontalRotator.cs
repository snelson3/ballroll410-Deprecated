using UnityEngine;
using System.Collections;

public class HorizontalRotator : MonoBehaviour {
	
	public float speed;
	
	private Rigidbody rb;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		rb.transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}
}
