using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 currentVelocity;
    public EnemyScript enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Delete this later it will slow things down
        currentVelocity = rb.velocity;
    }

    public void StrikeBall(Vector3 aimRotation, float powerLevel)
    {
        //rb.velocity = new Vector3(0,0,0); //freeze ball before striking
        transform.rotation = Quaternion.Euler(aimRotation);

        Debug.Log("vectorUp: " + Vector3.up);
        Debug.Log("powerLevel: " + powerLevel);
        Debug.Log("aimRotation: " + aimRotation);
        Debug.Log("ballRotation: " + transform.rotation.eulerAngles);

        enemy.TransferInfo(aimRotation.x, transform.forward * powerLevel);
        rb.AddForce(transform.forward * powerLevel, ForceMode.Impulse);
        //rb.AddForce(transform.forward * powerLevel);
    }

    public Vector3 getVelocity()
    {
        return rb.velocity;
    }
}
