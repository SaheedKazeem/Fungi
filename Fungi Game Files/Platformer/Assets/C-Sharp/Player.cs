using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform playerPos;

    public enum PlayerState { Alive, Dead }

    public PlayerState PlayerStates;
    public Vector3 startPos;
    public ParticleSystem RefToDust;
    public Sprite waterBullet;
    public Manager refToManager;
    [HideInInspector] public GameObject firePoint, waterIcon, subWaterIcon01, subWaterIcon02, subWaterIcon03, mushroom;
    private bool checkit;
    public int waterCount, UpTimer;
    private float Speed, XInput;

    //PlayerState: A = Absorb, N = Normal, D = Death
    [HideInInspector] public bool Right, Left, A, N, D, isGrounded, IsBig;

    private Vector3 OrigCamPos;
    public GameObject ReftoCam;

    [HideInInspector] public Animator anim;

    private void Awake()
    {
        startPos = mushroom.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TileMap")
        {
            anim.SetBool("isGrounded", true);
        }
        if (collision.gameObject.tag == "mudWall")
        {
            anim.SetBool("isGrounded", true);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        OrigCamPos = ReftoCam.transform.position;
        Speed = 200;
        N = true;
    }

    private void Update()
    {
        if (refToManager.GetComponent<Manager>().pause != true)
        {
            ////*-------------> This checks if the Sprite has flipped
            if (GetComponent<SpriteRenderer>().flipX != checkit)
            {
                checkit = GetComponent<SpriteRenderer>().flipX;

                if (GetComponent<SpriteRenderer>().flipX == true)
                {
                    CreateDust();
                }
                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    CreateDust(); /////------> Adds the darn dust.
                }
            }

            waterIcon.SetActive(false);
            subWaterIcon01.SetActive(false);
            subWaterIcon02.SetActive(false);
            subWaterIcon03.SetActive(false);

            Animation();
            CameraClamp();
            if (refToManager.pause == false)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) Right = true;
                else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) Right = false;
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) Left = true;
                else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) Left = false;
            }

            if (Left == true)
            {
                firePoint.transform.rotation = Quaternion.Inverse(Quaternion.Euler(0, 180, 0));
                Flip();
            }
            if (Right == true)
            {
                firePoint.transform.rotation = Quaternion.Inverse(Quaternion.Euler(0, 0, 0));
                Flip();
            }

            //Absorb Mode
            if (A == true)
            {
                Absorb();
                anim.SetBool("Shoot", false);
            }

            //Shoot Mode
            if (N == true) { Shoot(); }

            //Weight Gain
            if (waterCount == 0) Speed = 200;
            else if (waterCount == 1) { Speed = 175; subWaterIcon03.SetActive(true); }
            else if (waterCount == 2) { Speed = 150; subWaterIcon03.SetActive(true); subWaterIcon02.SetActive(true); }
            else if (waterCount == 3) { Speed = 125; subWaterIcon01.SetActive(true); subWaterIcon02.SetActive(true); subWaterIcon03.SetActive(true); }

            if (this.transform.position.y < 0)
            {
                isGrounded = Physics2D.OverlapCapsule(playerPos.position, playerPos.GetComponent<CapsuleCollider2D>().size, 0, groundLayer);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadSceneAfterAnimation(); // <--- Update: Changed the reset position to resetting scene to get over a few bugs
            }
        }
    }

    private void FixedUpdate()
    {
        XInput = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(XInput, GetComponent<Rigidbody2D>().velocity.y); // <--UPDATE: changed the y component from 0 to this because of the clamp
    }

    private void Shoot()
    {
        if (waterCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject Bullets = new GameObject("Bullets", typeof(SpriteRenderer));
                Bullets.GetComponent<SpriteRenderer>().sprite = waterBullet;
                Bullets.transform.localScale = new Vector3(2f, 2f);
                Bullets.transform.position = firePoint.transform.position;
                Bullets.transform.rotation = firePoint.transform.rotation;
                Bullets.GetComponent<SpriteRenderer>().sortingOrder = 10;
                Bullets.AddComponent<bullet>();
                Bullets.AddComponent<CircleCollider2D>();
                Bullets.GetComponent<CircleCollider2D>().isTrigger = true;
                Bullets.GetComponent<CircleCollider2D>().radius = 0.12f;
                Bullets.AddComponent<Rigidbody2D>();
                Bullets.GetComponent<Rigidbody2D>().freezeRotation = true;
                Bullets.GetComponent<Rigidbody2D>().gravityScale = 0;
                Bullets.tag = "Bullet";
                waterCount--;
            }
        }
    }

    private void Absorb()
    {
        if (Input.GetKeyDown(KeyCode.Space)) waterCount++;
        if (waterCount > 3) waterCount = 3;
        waterIcon.SetActive(true);
    }

    private void Flip()
    {
        if (Left == true)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Right == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void Animation()
    {
        //walking animation
        anim.SetFloat("Speed", Mathf.Abs(XInput));
        //absorb animation
        if (waterCount > 0) anim.SetBool("Absorb", true);
        else if (waterCount == 0) anim.SetBool("Absorb", false);
        //shoot animation
        if (Input.GetKeyDown(KeyCode.Space)) anim.SetBool("Shoot", true);
        if (Input.GetKeyUp(KeyCode.Space)) anim.SetBool("Shoot", false);
        //death animation
    }

    public void ReloadSceneAfterAnimation() //// <----- This function should play the death animation before reloading the scene
    {
        if (waterCount <= 0)
        {
            anim.Play("regular death");
        }
        else if (waterCount >= 0)
        {
            anim.Play("fat death");
        }

        StartCoroutine(ReloadAnimation());
    }

    public IEnumerator ReloadAnimation() //// <---- Time for reloading scene
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CreateDust()
    {
        RefToDust.Play();
    }

    private void CameraClamp() //Clamping Camera >_<
    {
        if (ReftoCam.transform.position.y < OrigCamPos.y)
        {
            ReftoCam.transform.position = new Vector3(this.transform.position.x, OrigCamPos.y, -10);
        }
    }
}