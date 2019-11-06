using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle
{
    void FixedUpdate()
    {
        ResetPosition();

        Move();
    }

    protected override void ResetPosition()
    {
        if (!right)
        {
            if (gameObject.name.Contains("Log 6x1") && transform.position.x <= -11f)
            {
                transform.position = new Vector3(11f, transform.position.y, 0f);
            }
            else if (gameObject.name.Contains("Log 4x1") && transform.position.x <= -10f)
            {
                transform.position = new Vector3(10, transform.position.y, 0f);
            }
            else if (transform.position.x <= -9f && !gameObject.name.Contains("Log 6x1") && !gameObject.name.Contains("Log 4x1"))
            {
                transform.position = new Vector3(9f, transform.position.y, 0f);
            }
        }
        else if (right)
        {
            if (gameObject.name.Contains("Log 6x1") && transform.position.x >= 11f)
            {
                transform.position = new Vector3(-11f, transform.position.y, 0f);
            }
            else if (gameObject.name.Contains("Log 4x1") && transform.position.x >= 10f)
            {
                transform.position = new Vector3(-10f, transform.position.y, 0f);
            }
            else if (transform.position.x >= 9f && !gameObject.name.Contains("Log 6x1") && !gameObject.name.Contains("Log 4x1"))
            {
                transform.position = new Vector3(-9f, transform.position.y, 0f);
            }
        }
    }
}
