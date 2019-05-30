using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControl : MonoBehaviour
{
    public float speed;
    public float walkingSpeed;
    public float runningSpeed;
    Rigidbody rb;
    Vector3 moveDirection;
    public Animator anim;
    public Animator playerBob;
    private bool isJumping;
    public bool isRunning;
    public hookShot hook;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;

        float anyMovement = horizontalMovement + verticalMovement;

        playerBob.SetFloat("speed", anyMovement);

        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); isJumping = true; }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { isRunning = true; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { isRunning = false; }
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("left clicked");
            anim.SetBool("attack", true);
            isAttacking = true;
        }

        if (isRunning) speed = runningSpeed;
        if (!isRunning) speed = walkingSpeed;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * speed * Time.deltaTime;
        rb.velocity += yVelFix;
    }

    void Jump(){
        if (!isJumping) { 
            rb.velocity = new Vector3(0, 3, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("walkable")) {
            isJumping = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("hitbox")) {
            if (isAttacking) {
                var enemy = other.GetComponent<enemyAI>();
                if (enemy.state == 3) {
                    Debug.Log("Backstab!");
                    Destroy(enemy.transform.parent.gameObject);
                }
                if (enemy.state != 3) {
                    Debug.Log("Hit Enemy");
                    enemy.Chasing = true;
                }
            }
        }
    }
}
