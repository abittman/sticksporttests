using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrasher_Temp : MonoBehaviour {

    public SimplePlayerCurveInput simplePlayer;
    public Rigidbody playerRB;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("WorldCollider"))
        {
            simplePlayer.canMove = false;
            playerRB.constraints = RigidbodyConstraints.None;
            playerRB.useGravity = true;
        }
    }
}
