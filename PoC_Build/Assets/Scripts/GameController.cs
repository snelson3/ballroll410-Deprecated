using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    public float start_x = 0;
    public float start_y = 0;
    public float start_z = 0;
    public Text morselText;
    public Text gameOverText;
    public Text startText;
    public int minMorsels;

    private int totalMorsels;
    private int morselCount;
    public bool gameOver;
    private GameObject cam;

    void Start()
    {
        morselCount = 0;
        totalMorsels = GameObject.FindGameObjectsWithTag("Morsel").Length;
        morselText.text = string.Format("{0}/{1} Morsels", morselCount, totalMorsels);
        minMorsels = Mathf.Clamp(minMorsels, 0, totalMorsels);
        gameOver = false;
        player = Instantiate(player, new Vector3(0, 0, 0), player.transform.rotation) as GameObject;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<CameraController>().SetPlayer(player);
        StartCoroutine(StartLevelMessage());
    }

    void Update()
    {
        if(gameOver && Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator LevelComplete()
    {
        gameOverText.text = "Level Complete!";
        player.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2);
        gameOverText.text = "\n" + gameOverText.text + "\nPress Any Key to Restart";
        gameOver = true;
    }
    public int MorselCount()
    {
        return morselCount;
    }
    void UpdateUI()
    {
        morselText.text = string.Format("{0}/{1} Morsels", morselCount, totalMorsels);
    }

    [SerializeField]
    public void AddMorsel()
    {
        morselCount++;
        UpdateUI();
    }

    [SerializeField]
    public void PlayerDestroy()
    {
        Destroy(player.gameObject);
        StartCoroutine(GameOver());
    }

    [SerializeField]
    public void ExitReached()
    {
        if (morselCount >= minMorsels)
        {
            StartCoroutine(LevelComplete());
        }
    }

    IEnumerator StartLevelMessage()
    {
        gameOverText.text = string.Format("Level Goal: {0}/{1} Morsels", minMorsels, totalMorsels);
        yield return new WaitForSeconds(3);
        gameOverText.text = "";
    }

    IEnumerator GameOver()
    {
        gameOverText.text = "Game Over";
        yield return new WaitForSeconds(2);
        gameOverText.text = "\n" + gameOverText.text + "\nPress Any Key to Restart";
        gameOver = true;
    }
}
