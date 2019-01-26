using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{


    public int cameraSpeed_ = 10;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.position += new Vector3(0, Time.deltaTime * cameraSpeed_, 0);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.position += new Vector3(0, -Time.deltaTime * cameraSpeed_, 0);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            transform.position += new Vector3(-Time.deltaTime * cameraSpeed_, 0, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            transform.position += new Vector3(Time.deltaTime * cameraSpeed_, 0, 0);
        }
    }
}
