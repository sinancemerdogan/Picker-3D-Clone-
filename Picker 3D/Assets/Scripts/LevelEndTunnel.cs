using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTunnel : MonoBehaviour
{
    private bool hasPlayerArrived = false;
  
    private void OnTriggerEnter(Collider collision) {
        if(collision.CompareTag("Player")) {
            hasPlayerArrived = true;
        }
    }

    private void OnDisable() {
        hasPlayerArrived = false;
    }

    public bool GetHasPlayerArrived() {
        return hasPlayerArrived;
    }
}
