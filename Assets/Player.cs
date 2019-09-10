﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float moveSpeed = 1f;
	public float chargeSpeed = .1f;

	//public AimPivot aimPivot;
	public GameObject chargePivot;
	public Ball volleyBall;

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
		float tiltAngle = 500.0f;
		GameObject aimObject = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;

		if (Input.GetAxis("Mouse ScrollWheel"){

		}
		// Smoothly tilts a transform towards a target rotation.
		float tiltAroundX = Input.GetAxis("Mouse ScrollWheel") * tiltAngle;
		// Rotate the cube by converting the angles into a quaternion.
		Quaternion target = Quaternion.Euler(tiltAroundX + aimObject.transform.eulerAngles.x, 0, 0);
		// Dampen towards the target rotation
		aimObject.transform.rotation = Quaternion.Slerp(aimObject.transform.rotation, target, Time.deltaTime * smooth);

		////CHARGE STRIKE CONTROL
		//Strike power is determined by the Y scale of the aimObject. The Power varies between 0 and 1.
		if (Input.GetButton("Fire1"))
		{
			if (!charging)
			{
				chargePivot.transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
				charging = true;
			}
			else
			{
				float newYScale = transform.localScale.y + chargeSpeed;//FILLS WAY TOO FAST
				if (newYScale > 1f)
				{
					newYScale = 1f;//clamp
				}
				//temporarily made this y = 5 since newYscale needs debugging
				chargePivot.transform.localScale = new Vector3(transform.localScale.x, 5, transform.localScale.z);

			}

		}
		else if (charging && !Input.GetButton("Fire1"))
		{
			//reset charge bar
			charging = false;
			chargePivot.transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

			//hit volleyball
			Vector3 strikeAngle = aimObject.transform.rotation.eulerAngles;
			float powerLevel = transform.localScale.y;
			volleyBall.StrikeBall(strikeAngle, powerLevel);
		}

	}
}
