using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public Camera Camera;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -1);
        
        Camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Camera.orthographicSize;        
    }
}