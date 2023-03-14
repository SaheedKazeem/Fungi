using System.Collections;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public enum SnailStates { Idle, Moving, Invulnerable }

    public SnailStates SnailState;
    public Player RefToPlayer;
    private float Xdir = 2F;//-0.013f
    public int frames = 0, Collframes = 0, seconds = 0, Collseconds = 0, lives = 3;
    public bool FacingLeft;
    public Animator snailAnim;
    public GameObject RefToGameManager;
    private bool snailcollided = false, CRrunning = false;

    // Start is called before the first frame update
    private void Start()
    {
        FacingLeft = true;
        RefToGameManager = GameObject.Find("Game Manager");
        snailAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
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
            Xdir *= 0.65f;

            if (snailcollided == false)
            {
                snailcollided = true;
                lives--;
            }

            if (lives == 0)
            {
                StartCoroutine("DeathAnimation");
            }
        }

        if (collision.tag == "TileMap")
        {
            Xdir *= -1;
           
        }
        if (collision.tag == "mudWall")
        {
            Xdir *= -1;
        
        }

        if (collision.tag == "Bullet" && collision.tag == "TileMap")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y);
        }
    }

    private void Update()
    {
        if (RefToGameManager.GetComponent<Manager>().pause != true)
        {
            snailAnimation();
            if (SnailState == SnailStates.Idle)
            {
            }
            if (SnailState == SnailStates.Moving)
            {
                //------------------------*
                Collframes++;
                if (Collframes >= 30 && snailcollided == true)
                {
                    Collframes = 0;
                    snailcollided = false;
                }
                //------------------------*
                frames++;
                if (frames == 60)
                {
                    frames = 0;
                    seconds += 1;
                }
                if (seconds == 7)
                {
                    seconds = 0;
                }
                //------------------------*
                //--------------------->> Dajay here I added deltaTime to code underneath<<--------------------*
                this.transform.position += new Vector3(Xdir * Time.deltaTime, 0);
                if (Xdir > 0)
                {
                    Flip();
                    FacingLeft = false;
                }
                else if (Xdir < 0)
                {
                    Flip();
                    FacingLeft = true;
                }
            }

            if (SnailState == SnailStates.Invulnerable)
            {
            }
            //Flips Snail
            void Flip()
            {
                if (FacingLeft == true)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    FacingLeft = false;
                }
                else if (FacingLeft == false)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    FacingLeft = true;
                }
            }

            void snailAnimation()
            {
                if (lives == 2) snailAnim.SetInteger("Lives", 2);
                if (lives == 1) snailAnim.SetInteger("Lives", 1);
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        CRrunning = true;
        GetComponent<Rigidbody2D>().isKinematic = true; // <== Colliders should still work but force cant do anything to it
        snailAnim.SetInteger("Lives", 0);
        SnailState = SnailStates.Idle;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        CRrunning = false;
    }
}