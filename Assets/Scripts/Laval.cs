using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laval : MonoBehaviour
{
    
    public float speed = 1f;

    private void Update()
    {
        // scale
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + speed * Time.deltaTime, transform.localScale.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
