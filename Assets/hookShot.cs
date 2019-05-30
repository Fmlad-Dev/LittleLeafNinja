using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookShot : MonoBehaviour
{

    public Camera cam;
    public RaycastHit hit;

    public LayerMask cullingmask;
    public int maxDistance;

    public bool IsFlying;
    public Vector3 loc;

    public float speed = 10;
    public Transform hand;

    public LineRenderer LR;


    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)){ Findspot(); }

        if (IsFlying) { Flying(); }

        if(Input.GetKey(KeyCode.Space) && IsFlying) {
            IsFlying = false;
            LR.enabled = false;
        }
    }

    public void Findspot() {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance)) {
                if (hit.collider.gameObject.CompareTag("hook")) {
                    IsFlying = true;
                    loc = hit.point;
                    LR.enabled = true;
                    LR.SetPosition(1, loc);
                }
                else {
                    Debug.Log("hit wall?");
                }
        }
    }

    public void Flying() {
        transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        LR.SetPosition(0, hand.position);

        if (Vector3.Distance(transform.position, loc) < .5f){
            IsFlying = false;
            LR.enabled = false;
        }
    }
}
