using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float speed;
	public float minDistance;
    public float maxDistance;
    public float start_x, start_y, start_z;
    public float max_angle;
    public float min_angle;

    private GameObject player;
    private Vector3 target_pos;
	private float positive_min;
    
    void Start()
    {
        transform.eulerAngles = new Vector3(start_x, start_y, start_z);
        transform.position = new Vector3(0, 0, 0);
		positive_min = ((min_angle % 360) + 360) % 360;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 target_angles = transform.eulerAngles;
            float verMove = Input.GetAxis("VerticalCam");
            float horMove = Input.GetAxis("HorizontalCam");
            target_angles += new Vector3(0, horMove * -speed * Time.deltaTime, 0);
            target_angles += new Vector3(verMove * -speed * Time.deltaTime, 0, 0);

            //keeps cam within angle boundaries (prevents nasty behavior)
			float positive_angle = (target_angles.x + 360) % 360;
            if (positive_angle > max_angle && positive_angle < positive_min)
            {
                target_angles.x = positive_angle - max_angle < positive_min - positive_angle ? max_angle : min_angle;
            }

            //Makes a nice, smooth rotation
            Quaternion rotate = Quaternion.Euler(target_angles);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, speed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            UpdateCameraPosition();
        }
    }
	
	float GetCameraDistance(Vector3 direction) {
		RaycastHit hit;
		return Physics.SphereCast(player.transform.position, 0.3f, direction, out hit, maxDistance) ? hit.distance : maxDistance;
	}

    void UpdateCameraPosition()
    {
        if (player != null)
        {
            //places the camera around the player in a spherical fashion at a radius of distance
            float height = Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.x);
            float x_dist = -Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y);
            float z_dist = -Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y);
            float fact = Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.x);
			Vector3 direction = new Vector3(fact * x_dist, height, fact * z_dist);
			float newDistance = GetCameraDistance(direction);
            transform.position = player.transform.position + direction * newDistance;
			
			if(newDistance < minDistance) {
				player.GetComponent<Renderer>().enabled = false;
			}
			else {
				player.GetComponent<Renderer>().enabled = true;
			}
        }
    }

    [SerializeField]
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
