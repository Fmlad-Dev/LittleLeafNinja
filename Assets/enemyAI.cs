using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class enemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPos;
    public LookAtConstraint lookat;
    public Collision inSwing;
    public Rigidbody enemyrb;
    public Sprite fowardSprite;
    public Sprite backSprite;
    public Sprite sideRSprite;
    public Sprite sideLSprite;
    public SpriteRenderer SpriteRend;
    public Vector2 AngleFrontRange = new Vector2(0,45);
    public Vector2 AngleFront2Range = new Vector2(315, 360);
    public Vector2 AngleSideLeftRange = new Vector2(45, 135);
    public Vector2 AngleBackRange = new Vector2(135, 225);
    public Vector2 AngleSideRightRange = new Vector2(225, 315);
    public bool Chasing = false;
    public bool isClose = false;
    public float speed = 1.0f;
    public int state = 1; // 1-forward, 2-right, 3-back, 4-left
    public string isLooking = "up";
    public float desiredRotation = 90;


    void Start(){
        SpriteRend = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update() {
        //var angle = Quaternion.FromToRotation(transform.right, transform.position - player.transform.position).eulerAngles.y;
        //var angle = Quaternion.Euler(0f, desiredRotation, 0f) * Vector3.forward;
        var forward = Quaternion.Euler(0f, desiredRotation, 0f) * Vector3.forward;
        var angle = Quaternion.FromToRotation(forward, transform.position - player.transform.position).eulerAngles.y;

        if (Chasing == false)
        {
            if (angle >= AngleFrontRange.x && angle <= AngleFrontRange.y || angle >= AngleFront2Range.x && angle <= AngleFront2Range.y)
            {
                SpriteRend.sprite = fowardSprite; //show the front of enemy sprite
                state = 1; // current standing state is 1~facing forwards
            }
            if (angle >= AngleSideLeftRange.x && angle <= AngleSideLeftRange.y)
            {
                SpriteRend.sprite = sideLSprite; //show the side of enemy sprite
                state = 2; // current standing state is 2~facing left 
            }
            if (angle >= AngleBackRange.x && angle <= AngleBackRange.y)
            {
                SpriteRend.sprite = backSprite; //show the side of enemy sprite
                state = 3;  // current standing state is 3~facing right
            }
            if (angle >= AngleSideRightRange.x && angle <= AngleSideRightRange.y)
            {
                SpriteRend.sprite = sideRSprite; //show the back of enemy sprite
                state = 4;  // current standing state is 4~facing backwards
            }
            Debug.Log(angle);
        }
        else
        {
            SpriteRend.sprite = fowardSprite;
            Debug.Log("I see you!");
            Chase();
        }
    }

    void Chase() {
        // move towards players position slowly..
        float step = speed * Time.deltaTime;
        enemyPos.transform.position = Vector3.MoveTowards(enemyPos.transform.position, player.transform.position, step/2.5f);
    }

    void Attack() {
        Debug.Log("Enemy Swang at you");
    }

    void OnCollisionEnter(Collision inSwing) {
        if (inSwing.gameObject.CompareTag("Player")) {
            Attack();
        }
    }

    void OnCollisionExit(Collision inSwing) {
        if (inSwing.gameObject.CompareTag("Player")) {
            Attack();
        }
    }
}
