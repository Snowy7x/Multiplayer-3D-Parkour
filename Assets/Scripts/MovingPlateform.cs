using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public enum Mode
    {
        HorizontalZ,
        HorizontalX,
        Vertical
    }
    public Mode mode;
    public float speed;
    public int maxDistance;

    private Vector3 startPosition;
    public PhysicMaterial friction;
    public Collider collider;
    
    //Rigidbody rb;
    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        
        // freeze rotation on z axis
        //rb.constraints = RigidbodyConstraints.FreezePositionZ | (mode == Mode.Horizontal ? RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.FreezePositionX) | RigidbodyConstraints.FreezeRotation;
    }
    
    private void Update()
    {
        switch (mode)
        {
            case Mode.HorizontalX:
                // Move from left to right
                transform.Translate(new Vector3(Mathf.PingPong(Time.time * speed, maxDistance) - maxDistance / 2, 0, 0));
                //transform.position = new Vector3(Mathf.PingPong(Time.time * speed, maxDistance) + startPosition.x, transform.position.y, transform.position.z);
                //rb.velocity = new Vector3(Mathf.PingPong(Time.time * speed, maxDistance) - (maxDistance / 2), 0, 0);
                break;
            case Mode.HorizontalZ:
                // Move from front to back
                transform.Translate(new Vector3(0, 0, Mathf.PingPong(Time.time * speed, maxDistance) + startPosition.z));
                //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * speed, maxDistance) + startPosition.z);
                break;          
            case Mode.Vertical:
                // Move from bottom to top
                transform.Translate(new Vector3(0, Mathf.PingPong(Time.time * speed, maxDistance) - (maxDistance / 2), 0));
                //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, maxDistance) + startPosition.y, transform.position.z);
                //rb.velocity = new Vector3(0, Mathf.PingPong(Time.time * speed, maxDistance) - (maxDistance / 2), 0);
                break;
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player");
            collision.gameObject.GetComponent<Movement>().OnPlateformCollision(transform);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Movement>().OnPlateformCollisionExit();
        }
    }*/
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        /*
         * 
         */
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player");
            other.gameObject.GetComponentInParent<Movement>()?.OnPlateformCollision(transform);
            collider.material = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInParent<Movement>()?.OnPlateformCollisionExit();
            collider.material = friction;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * maxDistance);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * maxDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * maxDistance);
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * maxDistance);
        
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxDistance);
        Gizmos.DrawLine(transform.position, transform.position + -transform.forward * maxDistance);

    }
}
