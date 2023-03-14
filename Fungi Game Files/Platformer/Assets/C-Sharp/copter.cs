using UnityEngine;

public class copter : MonoBehaviour
{
    public Player refToPlayer;
    private GameObject player;

    [Range(1, 20)]
    public float JumpVelocity = 11f;

    public float fallMultiplier = 2.5f, LowJumpMultiplier = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("player");
        refToPlayer = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            refToPlayer.N = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpVelocity; // <-- Damn Saheed you added a nice looking jump?? Increase the Jump Velocity to make jumps higher
            ////------> This script here just makes the jumps more realistic and mario like
            if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                player.GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                player.GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
            }
            ////--------> Because honestly bro who doesnt like mario?
            ///--------------Animation----------------\\\
            player.GetComponent<Player>().anim.SetBool("isGrounded", false);
            player.GetComponent<Player>().anim.SetTrigger("DoubleJump"); // <-- Trigger should activate if player landed on another clover
            player.GetComponent<Player>().anim.SetTrigger("FatDoubleJump"); //Fat version
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            refToPlayer.N = true;
        }
    }

    /*
    Dajay's Note:
    Key:
    1)N = Normal state

    On Trigger Enter:
    'normal state (N)' will be false this prevent the player from shooting when they're inside the collider.

    On trigger Exit:
    Exiting the collider 'normal state (N)' will be true allowing the player to shoot mid-flight.
    */
}