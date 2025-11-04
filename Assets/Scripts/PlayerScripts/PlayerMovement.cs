using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject arrowPivot;
    public GameObject swordPivot;
    
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    [SerializeField] private UI_Inventory uiInventory;
    private PlayerInventory playerInventory;
    private Animator animator;

    public float currentMoveSpeed;
    private float dashSpeed = 25f;
    private float dashTime = .2f;
    private float dashCD = 2f;

    private float dashCounter;
    private float dashCDManager;

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        playerInventory = GetComponent<PlayerInventory>();
        uiInventory.SetInventory(playerInventory);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        else
        {
            transform.localScale = new Vector3(3, 3, 3);
        }

        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        arrowPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        swordPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        moveDirection = new Vector2(horizontal, vertical).normalized;

        //transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCDManager <= 0 && dashCounter <= 0)
            {
                currentMoveSpeed = dashSpeed;
                dashCounter = dashTime;
                animator.SetTrigger("dash");
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                currentMoveSpeed = moveSpeed;
                dashCDManager = dashCD;
            }
        }

        if (dashCDManager > 0)
        {
            dashCDManager -= Time.deltaTime;
        }

        if (rb.linearVelocity == new Vector2(0, 0))
        {
            animator.SetBool("canRun", false);
        }
        else
        {
            animator.SetBool("canRun", true);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * currentMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
