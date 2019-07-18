using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cyan;

    [SerializeField]
    private GameObject pink;

    [SerializeField]
    private GameObject orange;

    [SerializeField]
    private float swapSpeed = 10f;

    [SerializeField]
    Vector3 middlePosition = new Vector3(0, -3.5f, 0);

    public Vector3 leftPosition;
    private Vector3 rightPosition;

    private void Start()
    {
        leftPosition = new Vector3(cyan.transform.position.x, cyan.transform.position.y);
        rightPosition = new Vector3(pink.transform.position.x, pink.transform.position.y);
        GameManager.instance.cyanTarget = rightPosition;
    }

    public void Swap()
    {
        if (GameManager.instance.cyanTarget == rightPosition)
        {
            GameManager.instance.cyanTarget = leftPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, rightPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, leftPosition, swapSpeed);
        }
        else
        {
            GameManager.instance.cyanTarget = rightPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, leftPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, rightPosition, swapSpeed);
        }
    }

    internal void LerpToMiddle()
    {
        cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, middlePosition, swapSpeed);
        pink.GetComponent<LerpTo>().Lerp(pink.transform.position, middlePosition, swapSpeed);
    }

    internal void LerpToTarget()
    {
        orange.SetActive(false);
        if (GameManager.instance.cyanTarget == rightPosition)
        {
            GameManager.instance.cyanTarget = leftPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, rightPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, leftPosition, swapSpeed);
        }
        else
        {
            GameManager.instance.cyanTarget = rightPosition;
            cyan.GetComponent<LerpTo>().Lerp(cyan.transform.position, leftPosition, swapSpeed);
            pink.GetComponent<LerpTo>().Lerp(pink.transform.position, rightPosition, swapSpeed);
        }
    }

    private void Update()
    {
        if (cyan.transform.position == middlePosition && Input.GetButton("Fire1"))
        {
            orange.SetActive(true);
        }
    }
}
