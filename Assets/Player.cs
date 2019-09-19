using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public float moveSpeed = 1f;
    //public float chargeSpeed = .1f;
    //public float strikeForce = .01f;
    //public float strikeRange = 4f;

    public float moveSpeed;
    public float chargeSpeed;
    public float strikeForce;
    public float strikeRange;

    //public AimPivot aimPivot;
    public GameObject chargePivot;
	public Ball volleyBall;
	public GameObject aimPivot;

	private bool charging = false;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Player movement (relies on project input settings)
		transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f,moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);

		//Scroll print values
		var scrollInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollInput > 0f)
		{
			// scroll up

		}
		else if (scrollInput < 0f)
		{
			// scroll down

		}


	}

	void FixedUpdate()
	{
		////AIM ROTATION CONTROL
		//Aim rotation values
		float smooth = 5.0f;
		float tiltAngle = 20.0f;
		//GameObject aimObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;

		if (Input.GetAxis("Mouse ScrollWheel") != 0){

			float tiltAroundX = Input.GetAxis("Mouse ScrollWheel") * tiltAngle;
			aimPivot.transform.rotation = Quaternion.Euler(tiltAroundX + aimPivot.transform.eulerAngles.x, -180, 0);
		}
		// Smoothly tilts a transform towards a target rotation.
		//float tiltAroundX = Input.GetAxis("Mouse ScrollWheel") * tiltAngle;
		// Rotate the cube by converting the angles into a quaternion.
		//Quaternion target = Quaternion.Euler(tiltAroundX + aimObject.transform.eulerAngles.x, 0, 0);
		// Dampen towards the target rotation
		//aimObject.transform.rotation = Quaternion.Slerp(aimObject.transform.rotation, target, Time.deltaTime * smooth);

		////CHARGE STRIKE CONTROL
		//Strike power is determined by the Y scale of the aimObject. The Power varies between 0 and 1.
		if (Input.GetButton("Fire1"))
		{
			if (!charging)
			{
				chargePivot.transform.localScale = new Vector3(chargePivot.transform.localScale.x, 0, chargePivot.transform.localScale.z);
				charging = true;
			}
			else
			{
				float newYScale = chargePivot.transform.localScale.y + chargeSpeed;//FILLS WAY TOO FAST
				if (newYScale > 1f)
				{
					newYScale = 1f;//clamp
				}
				//temporarily made this y = 5 since newYscale needs debugging
				chargePivot.transform.localScale = new Vector3(chargePivot.transform.localScale.x, newYScale, chargePivot.transform.localScale.z);

			}

		}
		else if (charging && !Input.GetButton("Fire1"))
		{

			//hit volleyball
			Vector3 strikeAngle = aimPivot.transform.localRotation.eulerAngles;
			float powerLevel = chargePivot.transform.localScale.y;
            Debug.Log("Y scale (powerlevel): " + powerLevel);
            float distanceToBall = Vector3.Distance(volleyBall.transform.position, transform.position);
            //Debug.Log("Distance to ball: " + distanceToBall);
            if(distanceToBall < strikeRange)
            {
                powerLevel *= strikeForce;
                volleyBall.StrikeBall(strikeAngle, powerLevel);
            }


            //reset charge bar
            charging = false;
			chargePivot.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
        //PlotTrajectory(volleyBall.transform.position, volleyBall.getVelocity(), Time.fixedDeltaTime, Time.maximumDeltaTime);

    }


    //TRAJECTORY https://answers.unity.com/questions/296749/display-arc-for-cannons-ball-trajectory.html

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    //TRAJECTORY https://answers.unity.com/questions/296749/display-arc-for-cannons-ball-trajectory.html
    //This does not help with predicting trajectory, only plotting old trajectory
    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Debug.Log("Start: " + start);
        Debug.Log("Start Velocity: " + startVelocity);
        Debug.Log("timestep: " + timestep);
        Debug.Log("maxTime: " + maxTime);
        Vector3 prev = start;
        for (int i = 1; ; i++)
        {
            float t = timestep * i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos)) break;
            Debug.Log("prev: " + prev);
            Debug.Log("pos: " + pos);
            Debug.DrawLine(prev, pos, Color.red, 100f);
            prev = pos;
        }
    }
}
