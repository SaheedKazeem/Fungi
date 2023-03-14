using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class nextLevel : MonoBehaviour
{
    public GameObject endUI;
    public GameObject GoalClover;

    private void Start()
    {
        GoalClover = GameObject.Find("Goal Clover");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(NextLevelLoading());
          
        }

    }
    public IEnumerator NextLevelLoading() //// <---- Time for reloading scene
    {
        GoalClover.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GoalClover.GetComponent<Rigidbody2D>().AddForce(transform.up * 4, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.95f);
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            endUI.SetActive(true);
        }


    }
    /*
    Dajay's note:
    When the Player collides with the goal they'll proceed to next level,
    this will only work for the first and second levels, for the third level
    a menu will pop-up from there the player will have to decide if they want to replay the game or quit the application.
    */
}