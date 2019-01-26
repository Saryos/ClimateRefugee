using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour
{
    public float speed_ = 1;

    private List<Vector2> route_ = new List<Vector2>();

    public void moveHere(Vector3 position)
    {
        route_.Add(new Vector2(position.x, position.y));
    }

    public Vector2 isoToCartesian(Vector2 isoCoord)
    {
        float carX = (isoCoord.x + isoCoord.y * 2)/ 2;
        return new Vector2(carX, - isoCoord.x + carX);
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
