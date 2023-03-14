using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject ImpactEffect;
    public GameObject RefToGameManager;
    private int millisec;

    private void Start()
    {
        ImpactEffect = GameObject.Find("Impact Effect");
        RefToGameManager = GameObject.Find("Game Manager");
    }

    private void Update()
    {
        if (RefToGameManager.GetComponent<Manager>().pause != true)
        {
            this.transform.Translate(5.5f * Time.deltaTime, 0, 0);
            millisec++;
            if (millisec > 250)
            {
                millisec = 250;
                Destroy(gameObject);
            }
        }

        /*
        Dajay's Note:
        This code here display water bullet lifespan and movement.
        Wherever the player is facing the bullet will shoot in that direction,
        in addition once 'millisec' reaches the maximum water bullet gameObject will be destroyed.
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "snail")
        {
            ImpactEffect.GetComponent<Renderer>().enabled = true;
            Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.tag == "mudWall")
        {
            ImpactEffect.GetComponent<Renderer>().enabled = true;
            Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.tag == "TileMap")
        {
            ImpactEffect.GetComponent<Renderer>().enabled = true;
            Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.tag == "FireFly")
        {
            ImpactEffect.GetComponent<Renderer>().enabled = true;
            Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        /*
        Dajay's Note:
        whenever water bullets collide with any of these gameObject it will destroy on impact.
        */
    }
}