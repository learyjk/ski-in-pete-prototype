using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peteMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask layerMask;
    private BoxCollider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));
    }

    private bool IsGrounded()
    {
        if (col.IsTouchingLayers(layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
