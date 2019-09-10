using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StrikeBall(Vector3 aimRotation, float powerLevel)
    {
        rb.velocity = new Vector3(0,0,0); //freeze ball before striking
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        Debug.Log("aimRotation: " + aimRotation);
        Debug.Log("vectorUp: " + Vector3.up);
        Debug.Log("ballRotation: " + transform.rotation.eulerAngles);
        
        //aimRotation.Normalize();
        
        rb.AddForce(aimRotation * powerLevel);

        //rb.AddForce(aimRotation * powerLevel, ForceMode.Impulse);
    }
}
