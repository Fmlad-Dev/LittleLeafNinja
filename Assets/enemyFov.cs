using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFov : MonoBehaviour
{
    public enemyAI enemy;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemy.state == 1 && enemy.Chasing == false) {
                enemy.Chasing = true;
                enemy.enemyrb.AddForce(Vector3.up * 1, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (enemy.state == 1) {
                enemy.Chasing = false;
                enemy.state = 3;
                Debug.Log("Hey where you going?!");
            }
        }
    }

}
