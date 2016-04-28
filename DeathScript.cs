using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("player")) {
			gameManager.GetComponent<GameController>().PlayerDestroy();
		}
	}
