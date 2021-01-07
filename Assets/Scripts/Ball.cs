using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D body;

    Vector2 direction;
    Vector2 oldPosition;
    Vector2 startPosition;

    bool isMoving = false;

    [SerializeField]
    float radius;
    [SerializeField]
    LevelManager manager;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }


    /// <summary>
    /// Launch with random direction and speed
    /// </summary>
    internal void Launch()
    {
        direction = (new Vector2(5, 5));
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if (!isMoving)
            return;

        oldPosition = transform.position;
        body.MovePosition((Vector2)transform.position + direction * Time.fixedDeltaTime);
    }

    GameObject LeftBounds { get { return manager.LeftBounds; } }
    GameObject RightBounds { get { return manager.RightBounds; } }
    GameObject UpperBounds { get { return manager.UpperBounds; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider as BoxCollider2D;
        var otherCollider = collision.otherCollider as CircleCollider2D;

        var block = collider.gameObject.GetComponent<Block>();
        if (block != null) 
        {
            manager.IncreaseScore(block.Data.Value);
            manager.RemoveBlock(block);
            Destroy(collider.gameObject);
        }

        body.position = oldPosition;

        //Change direction 
        if(collider.gameObject == RightBounds)
        {
            direction.x = -direction.x;
            return;
        }    
        if(collider.gameObject == LeftBounds)
        {
            direction.x = -direction.x;
            return;
        }    
        if(collider.gameObject == UpperBounds)
        {
            direction.y = -direction.y;
        }

        //If we hit from bottom, top - change y
        if((collision.contacts[0].point.y >= collider.transform.position.y + collider.bounds.extents.y)||
        (collision.contacts[0].point.y <= collider.transform.position.y - collider.bounds.extents.y))
        {
            direction.y = -direction.y;
            return;
        }

        //If we hit from left or right
        if((collision.contacts[0].point.x >= collider.transform.position.x + collider.bounds.extents.x)||
        (collision.contacts[0].point.x <= collider.transform.position.x - collider.bounds.extents.x))
        {
            direction.x = -direction.x;
        }
    }

    internal void Stop()
    {
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == manager.LowerBounds)
        {
            manager.HitPlayer();
            body.position = startPosition;
            Stop();
            manager.PrepareRound();
        }
    }
}
