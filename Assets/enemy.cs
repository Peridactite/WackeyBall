using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private Ball ballFigure;
    bool ballHitByPlayer = false;
    Vector3 destination = Vector3.zero;
    bool targetPosition = false;
    float angle = 0;
    Vector3 hitVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detect ball hit
        if (ballHitByPlayer)
        {
            //find landing position
            if (!targetPosition)
            {
                destination = DetermineBallLand();
                targetPosition = true;
            }
            //move to land position
            //moveToBall()
            //if moveToBall == destination
            //targetPosition = false

        }

    }
    Vector3 DetermineBallLand()
    {
        Vector3 ballPosition = ballFigure.transform.position;
        float velocityZ = hitVelocity.z * hitVelocity.z;
        Debug.Log("Vz is: " + hitVelocity.z);
        float velocityX = hitVelocity.x * hitVelocity.x;
        Debug.Log("Vx: " + hitVelocity.x);
        float velocityX2 = Mathf.Sqrt(velocityZ + velocityX);
        Debug.Log("Vx2 is: " + velocityX2);
        //float velocityX2 = Mathf.Sqrt((hitVelocity.z * hitVelocity.z)+(hitVelocity.x * hitVelocity.x));
        float initialVelocity = Mathf.Sqrt((hitVelocity.y * hitVelocity.y) + (velocityX2 * velocityX2));
        Debug.Log("Initial Velocity is: " + initialVelocity);
        //float initialVelocity = Mathf.Sqrt((hitVelocity.y * hitVelocity.y) + (Mathf.Sqrt((hitVelocity.z * hitVelocity.z) + (hitVelocity.x * hitVelocity.x))));
        Debug.Log("angle is : " + (360 - angle));
        float theta = Mathf.PI * (360 - angle) / 180;
        Debug.Log("theta is: " + theta);
        float g = Physics.gravity.y * -1;
        Debug.Log("gravity is: " + g);
        //R=V^2*sin(2theta)/g
        float range = (initialVelocity * initialVelocity) * Mathf.Sin(2 * theta) / (g);
        Debug.Log("range is: " + range);
        Vector3 point = new Vector3(hitVelocity.x,0f,hitVelocity.z).normalized* range;
        Debug.Log("normalized * range: " + point);
        Debug.Log("initial z: " + ballFigure.transform.position.z);
        Debug.Log("initial + point: "+ (ballFigure.transform.position.z + point.z));
        destination =  new Vector3(ballPosition.x + point.x, 0f, ballPosition.z + point.z);

        Debug.Log("will drop by point: " + destination);
        Debug.DrawLine(destination, new Vector3(destination.x, 10f, destination.z),Color.red);
        return destination;
    }
    public void TransferInfo(float x, Vector3 y, bool z = true)
    {
        angle = x;
        hitVelocity = y;
        ballHitByPlayer = z;
    }
   
}
