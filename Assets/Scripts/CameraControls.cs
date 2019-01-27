using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{


    public int cameraSpeed_ = 10;

    private int levelWidth = 64;
    private int levelHeight = 64;


    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            if (transform.position.y + Time.deltaTime * cameraSpeed_ < levelHeight / 2)
            {
                transform.position += new Vector3(0, Time.deltaTime * cameraSpeed_, 0);
            }
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            if (transform.position.y + Time.deltaTime * cameraSpeed_ > 0)
            {
                transform.position += new Vector3(0, -Time.deltaTime * cameraSpeed_, 0);
            }
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            if (transform.position.x + Time.deltaTime * cameraSpeed_ > -levelWidth / 2)
            {
                transform.position += new Vector3(-Time.deltaTime * cameraSpeed_, 0, 0);
            }
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            if (transform.position.x + Time.deltaTime * cameraSpeed_ < levelWidth / 2)
            {
                transform.position += new Vector3(Time.deltaTime * cameraSpeed_, 0, 0);
            }
        }
    }
}
