using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for reels - Make reel spin, stop and retrieve result
public class ReelController : MonoBehaviour
{
    // reel position
    private Vector3 startPosition = new Vector3(0, 0, 0);
    private float endPositionY = -8.0f;
    private float spinSpeed = 30.0f;

    private bool isSpinning = false;

    // reel results
    private int minResult = 0;
    private int maxResult = 8;
    private int result;
    public bool hasResult = false;

    void Update()
    {
        if (isSpinning)
        {
            Spin();
        }
    }

    private void Spin()
    {
        // repeat reel image
        if (transform.position.y < (startPosition.y + endPositionY))
        {
            transform.position = startPosition;
        }

        // reel movement
        transform.Translate(spinSpeed * Time.deltaTime * Vector3.down);

        // stop on result value
        if (hasResult)
        {
            transform.position = new Vector3(0, result, 0);
            isSpinning = false;
        }
    }

    public void StartSpin()
    {
        hasResult = false;
        isSpinning = true;
    }

    public int StopSpin()
    {
        result = Random.Range(minResult, -maxResult);
        hasResult = true;
        return result;
    }
}
