using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Obstacle
{
    [HideInInspector] public bool isDiving;
    private float timeToDive;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ResetPosition();

        Move();

        if (isDiving)
        {
            Dive();
        }
    }

    protected override void ResetPosition()
    {
        if (!right && transform.position.x <= -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
        else if (right && transform.position.x >= 10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }
    }

    void Dive()
    {
        timeToDive += Time.deltaTime;

        if (timeToDive >= 2.5f)
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;

            if (!gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                animator.SetBool("isDivingNow", true);
                //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                animator.SetBool("isDivingNow", false);
                //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            timeToDive = 0f;
        }
    }
}
