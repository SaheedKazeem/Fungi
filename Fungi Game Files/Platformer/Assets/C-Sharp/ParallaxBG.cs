using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    // Start is called before the first frame update
    private float length, startpos;

    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}