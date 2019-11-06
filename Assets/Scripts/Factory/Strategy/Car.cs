using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle
{
    private void Start()
    {
        if (right)
        {
            Flip();
        }
    }

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
