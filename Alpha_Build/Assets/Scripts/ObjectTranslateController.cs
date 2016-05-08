using UnityEngine;
using System.Collections;

public class ObjectTranslateController : MonoBehaviour {
	
	public float speed;
	public float waitTime;
	public float offset;
	public bool startReverse;
	public float end_x;
	public float end_y;
	public float end_z;
	
	private Rigidbody rb;
	private Vector3 startPosition, endPosition;
	private bool reverse, canMove;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		offset = Mathf.Clamp(offset, 0, 1.0f);
		startPosition = rb.transform.position;
		endPosition = new Vector3(end_x, end_y, end_z);
		rb.transform.position = Vector3.Lerp(startPosition, endPosition, offset);
		canMove = true;
		reverse = startReverse;
	}
	
	IEnumerator WaitToMove() {
		yield return new WaitForSeconds(waitTime);
		canMove = true;
	}
	
	void FixedUpdate() {
		if(canMove) {
			if(!reverse) {
				rb.transform.position = Vector3.MoveTowards(rb.transform.position, endPosition, Time.deltaTime * speed);
				if(rb.transform.position == endPosition) {
					reverse = true;
					canMove = false;
					StartCoroutine(WaitToMove());
				}
			}
			else {
				rb.transform.position = Vector3.MoveTowards(rb.transform.position, startPosition, Time.deltaTime * speed);
				if(rb.transform.position == startPosition) {
					reverse = false;
					canMove = false;
					StartCoroutine(WaitToMove());
				}
			}
		}
	}
}
