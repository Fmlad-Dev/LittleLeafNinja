using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{

    public float minimumX = -60f;
    public float maximumX = 60f;
    public float minimumY = -360f;
    public float maximumY = 360f;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public Camera cam;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {

        rotationX = Input.GetAxis("Mouse X") * sensitivityX;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, -80, 80);

        player.transform.Rotate(0, rotationX, 0);
        cam.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);

        if (Input.GetKey(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
}
