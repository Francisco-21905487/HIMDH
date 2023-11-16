using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject PistolPivot;
    public GameObject SwordPivot;
    public Animator animator;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    void Update()
    {
 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        PistolPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        SwordPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        moveDirection = new Vector2(horizontal, vertical).normalized;

        //transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (moveSpeed == 0)
        {
            animator.SetTrigger("Idle");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
