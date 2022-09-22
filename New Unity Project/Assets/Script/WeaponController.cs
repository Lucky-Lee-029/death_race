using UnityEngine;

public class WeaponController : MonoBehaviour
{

    //[SerializeField] private GameObject car;
    
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            //Debug.Log(collision.gameObject.name);
            //var carController = collision.gameObject.GetComponent<CarController>();
            //carController.effectHp(50);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            //Debug.Log(other.gameObject.name);
            //var carController = other.gameObject.GetComponent<CarController>();
            //carController.effectHp(50);
        }
    }
}
