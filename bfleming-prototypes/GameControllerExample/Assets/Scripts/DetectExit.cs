using UnityEngine;
using System.Collections;

public class DetectExit : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().ExitReached();
        }
    }
}
