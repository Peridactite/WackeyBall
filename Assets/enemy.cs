using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private Ball ballFigure;
    [SerializeField] private Transform enemyTrans1;
    [SerializeField] private Transform enemyTrans2;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private Transform ballTrans;
    private Vector3 destination = Vector3.zero;
    private Vector3 towards = Vector3.zero;
    private Vector3 teamTowards = Vector3.zero;
    private Vector3[] playerArea = new[] { new Vector3(12f, 0f, 25f), new Vector3(-12f, 0f, 25f), new Vector3(12f, 0f, 10f), new Vector3(-12f, 0f, 10f) };
    private Vector3[] serve = new[] { new Vector3(12f, 0f, 25f), new Vector3(-12f, 0f, 25f) };
    private float distToBall = 0;
    public float maxSpeed = 2f;
    public float radius = .5f;
    public float hitAngle = 75;
    public float hitRadius = 1f;
    bool ballHitByPlayer = false;
    bool targetPosition = true;
    bool enemyHitBall = false;
    bool enemy1Closest = true;
    bool inZone = false;
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
            print("Ball hit by player");
            //find landing position
            if (targetPosition)
            {
                print("finding target position");
                destination = DetermineBallLand();
                targetPosition = false;
            }
            //move to land position
            towards = destination - enemyTrans1.position;
            teamTowards = destination - enemyTrans2.position;
            towards.y = 0;
            teamTowards.y = 0;

            if ((towards.sqrMagnitude) <= (teamTowards.sqrMagnitude)){
                enemy1Closest = true;
                print("enemy 1 closest");
            }
            else
            {
                print("enemy 2 closest");
                enemy1Closest = false;
            }
            if ((towards.magnitude > radius && inZone) && enemy1Closest)
            {
                print("Moving enemy 1");
                moveEnemy(enemyTrans1, towards);
            }
            else if((teamTowards.magnitude > radius && inZone) && !enemy1Closest)
            {
                print("Moving enemy 2");
                moveEnemy(enemyTrans2, teamTowards);
            }
            //Hit Ball if in radius of enemy
            distToBall = Vector3.Distance(ballTrans.position, transform.position);
            if (distToBall < hitRadius)
            {
                Vector3 target = findTarget(playerArea);
                //Debug.Log("Targeting: " + target);
                Vector3 Force = findForce(target);
                //Debug.Log("Directional Force: " + Force);
                ballFigure.enemyStrike(Force);
                ballHitByPlayer = false;
                targetPosition = true;
            }

        }
        else
        {
            distToBall = Vector3.Distance(ballTrans.position, transform.position);
            if (distToBall < hitRadius)
            {
                Vector3 target = findTarget(serve);
                //Debug.Log("Targeting: " + target);
                Vector3 Force = findForce(target);
                //Debug.Log("Directional Force: " + Force);
                ballFigure.enemyStrike(Force);
                ballHitByPlayer = false;
                targetPosition = true;
            }
        }

    }
    private Vector3 DetermineBallLand()
    {
        Vector3 ballPosition = ballFigure.transform.position;
        float velocityZ = hitVelocity.z * hitVelocity.z;
        float velocityX = hitVelocity.x * hitVelocity.x;
        float velocityX2 = Mathf.Sqrt(velocityZ + velocityX);
        //float velocityX2 = Mathf.Sqrt((hitVelocity.z * hitVelocity.z)+(hitVelocity.x * hitVelocity.x));
        float initialVelocity = Mathf.Sqrt((hitVelocity.y * hitVelocity.y) + (velocityX2 * velocityX2));
        //float initialVelocity = Mathf.Sqrt((hitVelocity.y * hitVelocity.y) + (Mathf.Sqrt((hitVelocity.z * hitVelocity.z) + (hitVelocity.x * hitVelocity.x))));
        float theta = Mathf.PI * (360 - angle) / 180;
        float g = Physics.gravity.y * -1;
        //R=V^2*sin(2theta)/g
        float range = (initialVelocity * initialVelocity) * Mathf.Sin(2 * theta) / (g);


        Vector3 point = new Vector3(hitVelocity.x,0f,hitVelocity.z).normalized* range;
        destination =  new Vector3(ballPosition.x + point.x, 0f, ballPosition.z + point.z);
        
        if((destination.x > 17f || destination.x < -17f) || (destination.z >0 || destination.z < -34f))
        {
            inZone = false;
        }
        else
        {
            inZone = true;
        }
        //Debug.Log("Will drop by point: " + destination);
        return destination;
    }
    public void TransferInfo(float x, Vector3 y, bool z = true)
    {
        angle = x;
        hitVelocity = y;
        ballHitByPlayer = z;
    }

    private void moveEnemy(Transform transform, Vector3 twd)
    {
        twd.Normalize();
        //multiplies vector by maxSpeed
        twd *= maxSpeed * Time.deltaTime;
        //assigns transform position to position + towards 
        transform.position = new Vector3(transform.position.x + twd.x, transform.position.y, transform.position.z + twd.z);
        //Debug.Log("Moving to point: " + destination);
    }
   
    private Vector3 findTarget(Vector3[] targets)
    {
        float dist = 0;
        float highestDist = 0;
        float x = 0;
        float y = 0;
        Vector3 highest = Vector3.zero;
        for(int i = 0; i < targets.Length; i++)
        {
            x = targets[i].x - playerTrans.position.x;
            y = targets[i].z - playerTrans.position.z;
            dist = Mathf.Sqrt((x * x) + (y * y));
            if (dist > highestDist)
            {
                highestDist = dist;
                highest = targets[i];
            }
        }
        return highest;
    }
    private Vector3 findForce(Vector3 target)
    {

        float x = target.x - ballTrans.position.x;
        float y = target.z - ballTrans.position.z;
        //find total force used in projectile trajectory
        float distance = Mathf.Sqrt((x * x) + (y * y));
        float initialY = ballTrans.position.y;
        float g = Physics.gravity.y * -1;
        float Voxz = Mathf.Cos(hitAngle);
        float Voy = Mathf.Sin(hitAngle);
        float distVox = distance / Voxz;

        //THIS IS THE MATH PROOF where V is the Total Force
        //vertical motion
        // 0 = initialY + Voy*t + (1/2)*-9.8*t^2

        //horizantal motion
        //distance = 0 + V*Voxz*t + 0

        //t = distance / V*Voxz

        // 0 = initialY + Voy*(dist / V*Voxz) + (1/2)*-9.8*(dist / V*Voxz)^2
        // 0 = initialY + Voy*(dist / V*Voxz) + (1/2)*-9.8*(dist^2 / V^2*Voxz^2)
        // (1/2)*9.8*dist^2 / (V^2*Voxz^2) = initialY + Voy*(dist / Voxz)
        // (1/2)*9.8*dist^2 / (Voxz^2) = initialY + Voy*(dist / Voxz) * (V^2)
        // V^2 = (1/2)*9.8*dist^2 / [(Voxz^2) * (initialY + Voy*(dist / Voxz)]
        // V = sqrt(  4.9*(dist/Vox)^2 / [initialY + Voy*(dist / Voxz)]

        float totalForce = Mathf.Sqrt((4.9f*distVox *distVox) / (initialY+Voy*distVox));

        //float theta = hitAngle * Mathf.PI / 180;
        //float totalForce = Mathf.Sqrt((distance * g)/Mathf.Sin(2 * theta));
        //Debug.Log("TotalForce: " + totalForce);
        //find force in each dimension
        Vector3 AB = new Vector3(x, 0f, y);
        float Fy = totalForce * Mathf.Sin(hitAngle);
        float Fxz = totalForce * Mathf.Cos(hitAngle);
        float angleZ = Mathf.Atan2(AB.z, AB.x);
        float Fz = Fxz * Mathf.Sin(angleZ);
        float Fx = Fxz * Mathf.Cos(angleZ);
        return new Vector3(Fx,Fy,Fz);
    }
    public void restart()
    {
        ballHitByPlayer = false;
        targetPosition = true;
        enemyHitBall = false;
        inZone = false;
        enemy1Closest = true;
    }
}
