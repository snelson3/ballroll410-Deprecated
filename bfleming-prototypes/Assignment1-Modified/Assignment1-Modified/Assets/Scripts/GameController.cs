using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject player;
    public GameObject cam;
    public Text countText;
    public Text winText;

    private int count;

    // Use this for initialization
    void Start ()
    {
        count = 0;
        SetCountText();
        winText.text = "";
        cam.GetComponent<CameraController>().SetPlayer((GameObject)Instantiate(player, player.transform.position, player.transform.rotation));
	}
	
	[SerializeField]
    public void AddToCount()
    {
        count++;
        SetCountText();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
