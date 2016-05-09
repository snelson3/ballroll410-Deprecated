using UnityEngine;
using System.Collections;

public class ObjectVanishController : MonoBehaviour {
	
	public float vanishTime;
	public float reappearWaitTime;
	
	private bool isVanished;

	// Use this for initialization
	void Start () {
		isVanished = false;
	}
	
	void OnCollisionEnter(Collision other) {
		if(other.gameObject.CompareTag("Player") && !isVanished) {
			isVanished = true;
			StartCoroutine(Vanish());
		}
	}
	
	IEnumerator Vanish() {
		Color color = gameObject.GetComponent<Renderer>().material.color;
		for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / vanishTime) {
			gameObject.GetComponent<Renderer>().material.color = new Color(Mathf.Lerp(color.r, 1, t), Mathf.Lerp(color.g, 0, t), Mathf.Lerp(color.b, 0, t), 1);
			yield return null;
		}
		gameObject.GetComponent<Collider>().enabled = false;
		gameObject.GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(reappearWaitTime);
		isVanished = false;
		gameObject.GetComponent<Collider>().enabled = true;
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<Renderer>().material.color = color;
	}
}
