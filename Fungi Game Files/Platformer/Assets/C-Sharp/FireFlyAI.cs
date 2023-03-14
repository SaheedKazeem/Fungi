using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FireFlyAI : MonoBehaviour
{
    public Transform Target;
    public float speed = 0.025f;
    public float nextWaypointDistance = 3f;
    public FireFly RefToFireFly;
    Path path;
    int CurrentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Finds seeker and Rigidbody components
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            //Creating a path, defining start of position and end goal position
            if(RefToFireFly.Flystate == FireFly.FlyStates.Attacking)
            {
                seeker.StartPath(rb.position, Target.position, OnPathComplete);
            }
           
    }
    void OnPathComplete(Path p)
    {
        //Only if the path is finished
        if (!p.error)
        {
            path = p;
            CurrentWaypoint = 0;
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (RefToFireFly.Flystate == FireFly.FlyStates.Attacking)
        {
            //If there ain't no path there ain't no action
            if (path == null)
                return;
            //If the Path has ended then the function should end
            if (CurrentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            //Physics: Direction and the force of said vector
            Vector2 direction = ((Vector2)path.vectorPath[CurrentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            float distance = Vector2.Distance(rb.position, path.vectorPath[CurrentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                //New waypoint should be created <<<<
                CurrentWaypoint++;
            }
            //Sprite should uh rotate to the direction of the player
            if (rb.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
