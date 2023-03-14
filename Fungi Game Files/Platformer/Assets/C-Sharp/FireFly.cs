using System.Collections;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    public enum FlyStates { Idle, Flying, Attacking }

    public FlyStates Flystate;
    public Player RefToPlayer;
    public GameObject RefToGameManager;
    private int frames = 0, seconds = 0;
    private float xdir = 2f;
    private Rigidbody2D rb;
    public Collider2D FireFlyCollider;
    public Animator anim;
    bool CRrunning = false;

    // Start is called before the first frame update
    private void Start()
    {
        FireFlyCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Flystate = FlyStates.Flying;
        RefToGameManager = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (CRrunning == false)
            {
             RefToPlayer.ReloadSceneAfterAnimation();
            }
            
        }
        if (collision.tag == "Bullet")
        {
            //Death animation
            StartCoroutine("deathAnimation");
        }

        if (collision.tag == "AttackArea")
        {
            return; //nothing should happen
        }
    }

    private IEnumerator deathAnimation()
    {
        CRrunning = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        anim.SetBool("gameOver", true);
        Flystate = FlyStates.Idle;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        CRrunning = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (RefToGameManager.GetComponent<Manager>().pause != true)
        {
            if (Flystate == FlyStates.Idle)
            {
            }
            if (Flystate == FlyStates.Flying)
            {
                //Timer
                frames += 1;
                if (frames == 60)
                {
                    frames = 0;
                    seconds += 1;
                }
                if (seconds == 2)
                {
                    xdir = xdir * -1;
                    seconds = 0;
                }
                ///*-----While the FireFly is flying about aimlessly it isnt actually doing that*-----\\\
                ///--------------------->> Dajay here I added deltaTime to code underneath<<--------------------\\\
                this.transform.position += new Vector3(xdir * Time.deltaTime, 0);
                if (xdir >= 0.01f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (xdir <= -0.01f)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                ///*----- its just going back and forth in a predetermined pattern :( *-----\\\
            }
            if (Flystate == FlyStates.Attacking)
            {
            }
        }
    }
}