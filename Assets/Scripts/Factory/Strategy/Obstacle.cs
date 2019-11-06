using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour, IMovement
{
    protected bool right;
    protected float speed;

    /// <summary>
    /// Handle object moving out of game bounds.
    /// </summary>
    protected abstract void ResetPosition();

    public void MovementConstructor(float speed, bool directionRight)
    {
        right = directionRight;
        this.speed = speed;
    }

    protected void Move()
    {
        if (!right)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else if (right)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// Flip sprite on X axis
    /// </summary>
    protected void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
}
