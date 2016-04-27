using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    public float start_x;
    public float start_y;
    public float start_z;
    public Text morselText;
    public Text gameOverText;
    public int minMorsels;

    private int totalMorsels;
    private int morselCount;
    private bool gameOver;
    private GameObject cam;

    void Start()
    {
        morselCount = 0;
        totalMorsels = GameObject.FindGameObjectsWithTag("Morsel").Length;
        minMorsels = Mathf.Clamp(minMorsels, 0, totalMorsels);
        gameOver = false;
        player = Instantiate(player, new Vector3(start_x, start_y, start_z), player.transform.rotation) as GameObject;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<CameraController>().SetPlayer(player);
        UpdateUI();
        gameOverText.text = "";
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

    void UpdateUI()
    {
        morselText.text = string.Format("{0}/{1} Morsels", morselCount, totalMorsels);
    }

    [SerializeField]
    public void AddMorsel()
    {
        morselCount++;
        UpdateUI();

        if(morselCount >= minMorsels)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Wall");
            for(int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i].SetActive(false);
            }
        }
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
        StartCoroutine(LevelComplete());
    }

    IEnumerator GameOver()
    {
        gameOverText.text = "Game Over";
        yield return new WaitForSeconds(2);
        gameOverText.text = "\n" + gameOverText.text + "\nPress Any Key to Restart";
        gameOver = true;
    }
}
