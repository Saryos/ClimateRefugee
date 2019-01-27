using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour
{
    public float speed_ = 1;

    private List<Vector2> route_ = new List<Vector2>();

    private List<GameObject> toCollect_ = new List<GameObject>();

    private float maxCollectDistance_ = 1.0f;

    private bool isCollecting = false;


    private Generator gen_ = null;


    void cancelCollection()
    {
        foreach (GameObject res in toCollect_)
        {
            Collectable c2 = res.GetComponent<Collectable>();
            c2.CancelCollect();
        }
        toCollect_.Clear();
    }

    public void collectResource(GameObject resource, bool waypoint)
    {
        MoveHere(resource.transform.position, waypoint);
        Debug.Log("Ordered a collection");

        toCollect_.Add(resource);

        Collectable c = resource.GetComponent<Collectable>();
        c.WantToCollect();
    }

    public void MoveHere(Vector3 position, bool waypoint)
    {
        Debug.Log("Adding the movement");
        if (!waypoint)
        {
            route_.Clear();
            cancelCollection();
        }

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(position.x, position.y);

        if (route_.Count != 0)
        {
            startPosition = route_[route_.Count - 1];
        }

        AStar(startPosition, targetPosition);
    }

    void AStar(Vector2 startPosition, Vector2 endPosition)
    {
        if (gen_ == null)
        {
            GameObject[] gens = GameObject.FindGameObjectsWithTag("Generator");
            gen_ = gens[0].GetComponent<Generator>();
        }

        if (gen_.IsTileWalkable(endPosition))
        {
            route_.Add(new Vector2(endPosition.x, endPosition.y));
        }

    }

    public Vector2 isoToCartesian(Vector2 isoCoord)
    {
        float carX = (isoCoord.x + isoCoord.y * 2) / 2;
        return new Vector2(carX, -isoCoord.x + carX);
    }

    public Vector2 cartesianToIso(Vector2 cartesian)
    {
        return new Vector2(cartesian.x - cartesian.y, (cartesian.x + cartesian.y) / 2);
    }


    // Update is called once per frame
    void Update()
    {
        if (route_.Count != 0)
        {
            Vector2 cartesianPos = isoToCartesian(transform.position);
            Vector2 cartCollectPos;

            if (toCollect_.Count != 0)
            {
                cartCollectPos = isoToCartesian(toCollect_[0].transform.position);
                Vector2 collectDistance = cartCollectPos - cartesianPos;
                if (collectDistance.magnitude < maxCollectDistance_)
                {
                    Debug.Log("We are collecting");

                    isCollecting = true;
                    Collectable c = toCollect_[0].GetComponent<Collectable>();

                    if (c.collect(Time.deltaTime))
                    {
                        if (route_.Count != 0)
                        {
                            route_.RemoveAt(0);
                        }
                        toCollect_.RemoveAt(0);
                        c.CancelCollect();
                    }

                }
                else
                {
                    isCollecting = false;
                }
            }
            else
            {
                isCollecting = false;
            }

            if (!isCollecting)
            {
                Vector2 cartesianTarget = isoToCartesian(route_[0]);

                Vector2 trip = cartesianTarget - cartesianPos;

                float movement = speed_ * Time.deltaTime;
                if (trip.magnitude < movement)
                {
                    transform.position = route_[0];
                    route_.RemoveAt(0);
                }
                else
                {
                    Vector2 newCarPos = cartesianPos + trip.normalized * movement;
                    transform.position = cartesianToIso(newCarPos);
                }

            }
        }
    }
}
