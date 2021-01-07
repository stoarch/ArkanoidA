using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    LevelManager manager;

    Rigidbody2D body;
    BoxCollider2D collider;
    BoxCollider2D leftBounds;
    BoxCollider2D rightBounds;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        leftBounds = manager.LeftBounds.GetComponent<BoxCollider2D>();
        rightBounds = manager.RightBounds.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //We can move to left
            if(transform.position.x - collider.bounds.extents.x > leftBounds.bounds.extents.x + leftBounds.transform.position.x )
                body.MovePosition(new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y));
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(transform.position.x + collider.bounds.extents.x < rightBounds.bounds.extents.x + rightBounds.transform.position.x )
            body.MovePosition(new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y));
        }
    }
}
