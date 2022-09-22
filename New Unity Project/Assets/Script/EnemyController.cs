using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject stone1;
    [SerializeField] GameObject stone2;
    [SerializeField] GameObject stone3;
    [SerializeField] GameObject stone4;
    [SerializeField] GameObject stone5;
    [SerializeField] GameObject stone6;
    [SerializeField] GameObject stone7;
    [SerializeField] GameObject stone8;
    [SerializeField] GameObject stone9;
    [SerializeField] GameObject stone10;
    [SerializeField] GameObject stone11;
    [SerializeField] GameObject stone12;
    private GameObject[] listStone = new GameObject[12];
    private const int NUM_STONE = 12;
    public float speed;
    public int currentStone;
    private Rigidbody rigidbody;

    public void Start()
    {
        listStone[0] = stone1;
        listStone[1] = stone2;
        listStone[2] = stone3;
        listStone[3] = stone4;
        listStone[4] = stone5;
        listStone[5] = stone6;
        listStone[6] = stone7;
        listStone[7] = stone8;
        listStone[8] = stone9;
        listStone[9] = stone10;
        listStone[10] = stone11;
        listStone[11] = stone12;

        rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        CheckStone();
        MoveToNextStone();
    }

    private void CheckStone()
    {
        int nextStone = 0;
        if (currentStone < NUM_STONE -1)
            nextStone = currentStone + 1;
        if(IsReachStone(listStone[nextStone].gameObject.transform.position, transform.position))
        {
            Debug.Log("Cuu toi voi, cu toi!!!   "  + currentStone + "--"+ nextStone);
            currentStone++;
            if (currentStone > NUM_STONE - 1)
                currentStone = 0;
            if (nextStone < NUM_STONE - 1)
                nextStone++;
            else
                nextStone = 0;
            transform.LookAt(listStone[nextStone].transform);
        }
    }

    private bool IsReachStone(Vector3 pos1, Vector3 pos2)
    {
        if ((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.z - pos2.z) * (pos1.z - pos2.z) < 1)
            return true;
        return false;
    }

    private void MoveToNextStone()
    {
        int nextStone = 0;
        if (currentStone < NUM_STONE - 1)
            nextStone = currentStone + 1;
        var targetPos = listStone[nextStone].gameObject.transform.position;
        rigidbody.velocity = (targetPos - transform.position).normalized * speed;
        //float angle = Vector3.Angle(targetPos - transform.position, unit.transform.forward);
        var angle = Vector3.Angle(transform.forward, transform.position - targetPos);
        
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