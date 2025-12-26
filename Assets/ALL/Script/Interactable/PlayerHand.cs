using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Hold Settings")]
    public Transform holdPoint; // Posisi hold object
    public float holdDistance = 1.5f; // Jarak dari kamera
    public LayerMask obstacleLayer; // Layer untuk meja, dinding, dll

    public GameObject ObjectInHand { get; set; }

    private Camera playerCamera;
    private Rigidbody heldObjectRb;
    private Collider heldObjectCollider;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (ObjectInHand != null)
        {
            UpdateObjectPosition();
        }
    }

    public void HoldObject(GameObject obj)
    {
        ObjectInHand = obj;

        // Disable physics
        heldObjectRb = obj.GetComponent<Rigidbody>();
        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = true;
        }

        // Disable collider agar tidak bertabrakan
        heldObjectCollider = obj.GetComponent<Collider>();
        if (heldObjectCollider != null)
        {
            heldObjectCollider.enabled = false;
        }
    }

    void UpdateObjectPosition()
    {
        // Raycast dari kamera ke depan
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        float targetDistance = holdDistance;

        // Jika ada obstacle di depan
        if (Physics.Raycast(ray, out hit, holdDistance, obstacleLayer))
        {
            // Kurangi jarak agar objek tidak tembus
            targetDistance = hit.distance - 0.2f; // Offset 0.2 unit dari obstacle
        }

        // Set posisi objek
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * targetDistance;
        ObjectInHand.transform.position = targetPosition;
        ObjectInHand.transform.rotation = holdPoint.rotation;
    }

    public void ReleaseObject()
    {
        if (ObjectInHand != null)
        {
            // Enable kembali physics & collider
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = false;
            }

            if (heldObjectCollider != null)
            {
                heldObjectCollider.enabled = true;
            }

            ObjectInHand = null;
            heldObjectRb = null;
            heldObjectCollider = null;
        }
    }
}