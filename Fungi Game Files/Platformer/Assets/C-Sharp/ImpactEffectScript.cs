using UnityEngine;

public class ImpactEffectScript : MonoBehaviour
{
    private int frames, seconds;
    public Manager RefToGameMan;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //// Sprite renderer of the impact effect will be disabled after this timer because destroying the object causes too much issues
        frames++;

        if (frames >= 30)
        {
            GetComponent<Renderer>().enabled = false;
        }
        if (frames >= 60)
        {
            frames = 0;
            seconds++;
        }
    }
}