using UnityEngine;

public class mudWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }

    /*
    Dajay's note:
    Whenever the player shoot a water bullet at the mud block
    both gameObjects will destroy on impact.
    */
}