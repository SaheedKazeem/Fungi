using UnityEngine;

public class AttackingAreaScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FireFly RefToFireflyScript;
        RefToFireflyScript = this.GetComponentInParent<FireFly>();
        //Temporary variables, a neat thing I just learnt <-- Saheed **
        if (collision.tag == "Player")
        {
            RefToFireflyScript.Flystate = FireFly.FlyStates.Attacking;
        }
        else RefToFireflyScript.Flystate = FireFly.FlyStates.Flying;

        if (collision.tag == "FireFly")
        {
            return; //nothing should happen
        }
        /* Uh so basically since I separated the colliders and to stop the triggers from reacting to each
           other I added a kinematic rigid body on this collider/gameobject and added a line of code so it doesn't
           react to the other trigger <-- Saheed **
        */
    }
}