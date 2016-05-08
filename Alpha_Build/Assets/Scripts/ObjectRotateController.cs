using UnityEngine;
using System.Collections;

public class ObjectRotateController : MonoBehaviour {
	
	public float speed;
	public bool horizontalRotate;
	public bool verticalRotate;
	
	private Rigidbody rb;
	private Vector3 rotate;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		float hor = horizontalRotate ? 1 : 0;
		float ver = verticalRotate ? 1 : 0;
		rotate = Vector3.ClampMagnitude(new Vector3(ver, hor, 0.0f), 1.0f);
	}
	
	void FixedUpdate () {
		rb.transform.Rotate(rotate * speed * Time.deltaTime);
	}
}
