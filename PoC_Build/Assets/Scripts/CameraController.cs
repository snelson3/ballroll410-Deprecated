using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distance;
    public float start_x, start_y, start_z;
    public float max_angle;
    public float min_angle;

    private Vector3 target_pos;

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = new Vector3(start_x, start_y, start_z);
        transform.position = new Vector3(0, 0, 0);
        //UpdateCameraPosition();
    }

    void Update()
    {
        Vector3 target_angles = transform.eulerAngles;
        float verMove = Input.GetAxis("VerticalCam");
        float horMove = Input.GetAxis("HorizontalCam");
        target_angles += new Vector3(0, horMove * -speed * Time.deltaTime, 0);
        target_angles += new Vector3(verMove * -speed * Time.deltaTime, 0, 0);

        //keeps cam within angle boundaries (prevents nasty behavior)
        if ((target_angles.x > max_angle && target_angles.x < 360 + min_angle) || target_angles.x < min_angle)
        {
            target_angles += Mathf.Abs(target_angles.x - max_angle) < Mathf.Abs(target_angles.x - min_angle) ? new Vector3(max_angle - target_angles.x, 0, 0) : new Vector3(360 + min_angle - target_angles.x, 0, 0);
        }

        //Makes a nice, smooth rotation
        Quaternion rotate = Quaternion.Euler(target_angles);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            UpdateCameraPosition();
        }
    }

    void UpdateCameraPosition()
    {
        //places the camera around the player in a spherical fashion at a radius of distance
        float height = Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.x) * distance;
        float x_dist = -Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y) * distance;
        float z_dist = -Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y) * distance;
        float fact = Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.x);
        transform.position = player.transform.position + new Vector3(fact * x_dist, height, fact * z_dist);
    }

    [SerializeField]
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
