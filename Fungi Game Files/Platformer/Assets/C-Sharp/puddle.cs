using UnityEngine;

public class puddle : MonoBehaviour
{
    public Player refToPlayer;
    private GameObject player;
    public ParticleSystem RefToSplash;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("player");
        refToPlayer = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CreateSplash();
            refToPlayer.A = true;
            refToPlayer.N = false;
        }
    }

    public void CreateSplash()
    {
        RefToSplash.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            refToPlayer.A = false;
            refToPlayer.N = true;
        }
    }

    /*
    Dajay's Note:
    Key:
    1)A = Absorb state
    2)N = Normal state

    On Trigger Enter:
    'Absosrb state (A)' is true allowing the player to stock-up on ammo,
    'Normal state (N)' is false to prevent the player from shooting at the same time
    while collecting the ammo.

    On trigger Exit:
    Once the player leaves the collider 'Absosrb state (A)' is false,
    'Normal state (N)' is true allowing the player to shoot.
    */
}