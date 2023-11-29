using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float forwardSpeed = 600f;
    private float dragSpeed = 50000f;
    private Rigidbody playerRigidbody;
    private bool isStopped = false;
    private bool waitForTap = true;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (waitForTap) {
            WaitForTapToStart();
        }
        else if (!isStopped) {
            AutoMove();
            HandleInput();
        }
    }

    void WaitForTapToStart() {
        if (Input.GetMouseButtonDown(0)) {
            waitForTap = false;
        }
    }

    void AutoMove() {
        Vector3 autoMoveVelocity = new Vector3(forwardSpeed, 0, playerRigidbody.velocity.z) * Time.fixedDeltaTime;
        playerRigidbody.velocity = autoMoveVelocity;
    }

    void HandleInput() {
        if (Input.GetMouseButton(0)) {
            Vector3 touchPos = Input.mousePosition;
            touchPos.z = Camera.main.nearClipPlane;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(touchPos);
            targetPos.y = transform.position.y;
            Vector3 newVelocity = playerRigidbody.velocity;
            newVelocity.z = (targetPos - transform.position).normalized.z * dragSpeed * Time.deltaTime;
            playerRigidbody.velocity = newVelocity;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Phase End") {
            StopPlayer();
        }
    }

    private void StopPlayer() {
        isStopped = true;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
    }

    public bool GetIsStopped() {
        return this.isStopped;
    }

    public void SetIsStopped(bool isStopped) {
        this.isStopped = isStopped;
    }

    public bool GetWaitForTap() {
        return this.waitForTap;
    }
}
