using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimUi : MonoBehaviour
{
    public Animator animUi01, animUi02;
    void Start()
    {
        StartCoroutine("MyAnimation01");
    }

    IEnumerator MyAnimation01()
    {
        animUi01.SetBool("Transformation", false);
        animUi02.SetBool("Transformation", true);

        yield return new WaitForSeconds(1);

        yield return StartCoroutine("MyAnimation02");
    }
    IEnumerator MyAnimation02()
    {
        animUi01.SetBool("Transformation", true);
        animUi02.SetBool("Transformation", false);

        yield return new WaitForSeconds(1);

        yield return StartCoroutine("MyAnimation01");
    }
    /*
       Dajay's Note:
       My Animation 01 & 02:
       This code animate the images on the menu screen
       */
}
