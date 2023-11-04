using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform movePoint;
    private float moveOffset = 2.5f;

    [SerializeField] private LayerMask collisionLayer;

    private Vector3 newCameraPos;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movePoint = GameObject.FindGameObjectWithTag("MovePoint").transform;
        movePoint.parent = null;
        newCameraPos = Camera.main.transform.position;
    }

    private bool pressOnce = true;
    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(movement.x) == 1f && pressOnce)
        {
            Collider[] colliders =
                Physics.OverlapSphere(movePoint.position + new Vector3(moveOffset * movement.x, 0f, 0f), 0.2f,
                    collisionLayer);
            if (colliders.Length == 0)
            {
                movePoint.position += new Vector3(moveOffset * movement.x, 0f, 0f);
                newCameraPos += new Vector3(moveOffset * movement.x, 0f, 0f);
                pressOnce = false;
            }
        }
        if (movement == Vector3.zero)
        {
            pressOnce = true;
        }
        if (Mathf.Abs(movement.z) == 1f && pressOnce)
        {
            Collider[] colliders =
                Physics.OverlapSphere(movePoint.position + new Vector3(0f, 0f, moveOffset * movement.z), 0.2f,
                    collisionLayer);
            if (colliders.Length == 0)
            {
                movePoint.position += new Vector3(0f, 0f, moveOffset * movement.z);
                newCameraPos += new Vector3(0f, 0f, moveOffset * movement.z);
                pressOnce = false;
            }
        }

        Vector3 z = Vector3.zero;
        Vector3 z2 = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, movePoint.position, ref z, 0.02f);
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position,
            newCameraPos, ref z2, 0.05f);
    }

}
