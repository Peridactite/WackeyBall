using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Ball ballFigure;
    [SerializeField] private Rigidbody ballRB;
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
        float ballPosition = ballFigure.transform.position.y;
        //float velocityZ = ballRB.velocity.z;
        //float velocityX = ballRB.velocity.x;
        //float velocityX2 = sqrt(velocityZ^2 +velocityX^2)
        float velocityX2 = Mathf.Sqrt((hitVelocity.z * hitVelocity.z)+(hitVelocity.x * hitVelocity.x));
        //float initialVelocity = sqrt(velocityY
        float initialVelocity = Mathf.Sqrt((hitVelocity.y * hitVelocity.y) + (Mathf.Sqrt((hitVelocity.z * hitVelocity.z) + (hitVelocity.x * hitVelocity.x))));
        float theta = 360 - angle;
        //R=V^2*sin(2theta)/g
        float range = (initialVelocity * initialVelocity) * Mathf.Sin(2 * (Mathf.PI*theta/180)) / (Physics.gravity.y * -1);
        Debug.Log("range is: " + range);
        Vector3 point = new Vector3(hitVelocity.x,0f,hitVelocity.z).normalized* range;
        Debug.Log("normalized * range: " + point);
        Debug.Log("initial z: " + ballFigure.transform.position.z);
        Debug.Log("initial + point: "+ (ballFigure.transform.position.z + point.z));
        destination =  new Vector3(ballFigure.transform.position.x + point.x, 0f, ballFigure.transform.position.z + point.z);

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
