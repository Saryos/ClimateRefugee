using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerControl : MonoBehaviour
{

    public Movement selectedWorker_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("tried to move worker to position " + target.ToString());


            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                selectedWorker_.AddWaypoint(target);
            }
            else
            {
                selectedWorker_.FirstMove(target);
            }

        }
        
    }
}
