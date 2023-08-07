using UnityEngine;

public class Camera_script : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0.2f, -1);
        Camera Camera = transform.GetComponent<Camera>();
        if (Input.GetMouseButtonDown(2))
        {            
            Camera.orthographicSize = 20;
        }
        if(Input.GetMouseButtonUp(2))
        {
            Camera.orthographicSize = 1;
        }
    }
}