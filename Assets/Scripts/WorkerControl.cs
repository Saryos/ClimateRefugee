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

        // select
        if (Input.GetMouseButtonDown(0))
        {

        }

        // order
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(target.x, target.y), -Vector2.up,0f);


            if (hit.collider != null)
            {
                Collectable collect = hit.transform.gameObject.GetComponent<Collectable>();

                if (collect != null)
                {
                    selectedWorker_.collectResource(hit.transform.gameObject,
                        Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                }
                else
                {
                    print("Hit a non collectible");
                }

            }
            else
            {
                
                Debug.Log("tried to move worker to position " + target.ToString());

                selectedWorker_.MoveHere(target,
                    Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            }
        }
        
    }
}
