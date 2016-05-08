using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlayerDestroy();		}
	}
}