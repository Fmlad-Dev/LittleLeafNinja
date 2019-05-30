using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopattack : MonoBehaviour
{
    public FPSControl fpc;
    public void stopAttacking()
    {
       fpc.anim.SetBool("attack", false);
        fpc.isAttacking = false;
    }
}
