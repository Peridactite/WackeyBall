using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] Transform shadow;
    [SerializeField] Transform enemy1;
    [SerializeField] Transform enemy2;
    [SerializeField] Transform player1;


    public Rigidbody rb;
    public Vector3 currentVelocity;
    public enemy enemy;
    public UIController UI;
    private bool playerStart = true;
    private bool playerPoint = false;
    private bool enemyStart = false;
    private bool enemyPoint = false;
    private bool round = false;
    private bool doubleHit = false;
    private float teamHit = 0;
    private float team1 = 0;
    private float team2 = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        shadow.position = new Vector3(transform.position.x, .1f, transform.position.z);
        float shadowScale = 3 / transform.position.y;
        if (shadowScale < .5)
        {
            shadowScale = .5f;
        }
        shadow.localScale = new Vector3(shadowScale, .001f, shadowScale);

        if (playerStart)
        {
            transform.position = new Vector3(0, 10, 23);
        }
        else if (enemyStart)
        {
            transform.position = new Vector3(-7.5f, 10f, -24f);
        }

        if(playerStart && Input.GetKeyDown("space"))
        {
            playerStart = false;
            rb.velocity = new Vector3(0, 10, 0);
        }
        else if(enemyStart && Input.GetKeyDown("space"))
        {
            enemyStart = false;
            rb.velocity = new Vector3(0, 10, 0);
        }



        //Delete this later it will slow things down
        currentVelocity = rb.velocity;
        if (round)
        {
            if (transform.position.y < 2 && teamHit > 0)
            {
                //if in bounds left side
                if ((transform.position.x < 15 && transform.position.x > -15) && (transform.position.z > -30 && transform.position.z <= 0))
                {
                    playerPoint = true;
                    Debug.Log(" Landed on Team 2 Side!");
                }
                //if in bounds right side
                else if ((transform.position.x < 15 && transform.position.x > -15) && (transform.position.z < 30 && transform.position.z > 0))
                {
                    enemyPoint = true;
                    Debug.Log(" Landed on Team 1 Side!");
                }
                //if out of bounds
                else
                {
                    if (teamHit == 1)
                    {
                        enemyPoint = true;
                  
                    }
                    else
                    {
                        playerPoint = true;
                        
                    }
                    Debug.Log(" Out of Bounds!");
                }
                enemy.restart();
                restartGame();
            }
            else if (doubleHit)
            {
                if (teamHit == 1)
                {
                    enemyPoint = true;
                }
                //else
                //{
                //    playerPoint = true;
                //}
                Debug.Log(" Double Hit! ");
                enemy.restart();
                restartGame();
            }
        }
        
    }

    public void StrikeBall(Vector3 aimRotation, float powerLevel)
    {
        if(teamHit == 1)
        {
            doubleHit = true;
        }
        //rb.velocity = new Vector3(0,0,0); //freeze ball before striking
        transform.rotation = Quaternion.Euler(aimRotation);
        
        //Debug.Log("vectorUp: " + Vector3.up);
        //Debug.Log("powerLevel: " + powerLevel);
        //Debug.Log("aimRotation: " + aimRotation);
        //Debug.Log("ballRotation: " + transform.rotation.eulerAngles);

        enemy.TransferInfo(aimRotation.x, transform.forward * powerLevel);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * powerLevel, ForceMode.Impulse);
        //rb.AddForce(transform.forward * powerLevel);
        teamHit = 1;
        round = true;
    }
    public void enemyStrike(Vector3 Force)
    {
        //if (teamHit == 2)
        //{
        //    doubleHit = true;
        //}
        rb.velocity = Vector3.zero;
        rb.velocity = Force;
        teamHit = 2;
        round = true;
    }

    public Vector3 getVelocity()
    {
        return rb.velocity;
    }
    public void restartGame()
    {

        if (enemyPoint)
        {
            team2++;
            UI.AddEnemy();

            enemyPoint = false;
            playerStart = true;
        }
        else if (playerPoint)
        {
            team1++;
            UI.AddPlayer();

            playerPoint = false;
            enemyStart = true;
        }
        Debug.Log("*** Team 1: " + team1 + " ******* Team 2: " + team2 + " ***");
        //Debug.Log("Restarting positions");
        

        player1.position = new Vector3(0f,2.5f,25f);
        enemy1.position = new Vector3(-7.5f, 2.5f, -24f);
        enemy2.position = new Vector3(7.5f, 2.5f, -24f);

        doubleHit = false;
        round = false;
        teamHit = 0;
    }

}
