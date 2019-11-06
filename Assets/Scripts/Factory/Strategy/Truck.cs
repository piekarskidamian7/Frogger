using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Obstacle
{
    void FixedUpdate()
    {
        ResetPosition();

        Move();
    }

    protected override void ResetPosition()
    {
        if (!right && transform.position.x <= -9f)
        {
            transform.position = new Vector3(9f, transform.position.y, 0f);
        }
        else if (right && transform.position.x >= 9f)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0f);
        }
    }
}
