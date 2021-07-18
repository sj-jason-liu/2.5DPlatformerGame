using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "MovingBox")
        {
            if(Vector3.Distance(transform.position, other.transform.position) < 0.1f)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.isKinematic = true;
                }
                MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
                if(meshRenderer != null)
                {
                    meshRenderer.material.color = Color.blue;
                }
            }
        }
    }
}
