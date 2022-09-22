using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody rigidbody;

    public void Start()
    {
    
        rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Weapon")
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Weapon")
        {
            Object.Destroy(gameObject);
        }
    }
}
