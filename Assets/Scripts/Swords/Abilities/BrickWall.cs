using UnityEngine;
using System.Collections;

public class BrickWall : MonoBehaviour
{
    Animator brickAnimator;
    float countDown;

    // Use this for initialization
    void Start()
    {
        brickAnimator = GetComponent<Animator>();
        countDown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (brickAnimator.GetCurrentAnimatorStateInfo(0).IsName("BrickWallTimerDespawn"))
        {
            StartCoroutine(StartCountDownToDestroy());
        }
    }

    IEnumerator StartCountDownToDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
