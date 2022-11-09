using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool spinReady = true;
    private int spinTime = 3;
    private int[] results = { 0, 0, 0, 0, 0 };
    private float timeDelay = 0.2f;
    // reels
    public GameObject[] reels;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spinReady)
        {
            SpinReels();
        }
    }

    public void SpinReels()
    {
        spinReady = false;

        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(InitiateSpin(i));
        }

        StartCoroutine(WaitForResult());
    }

    private void PrepareSpin()
    {
        spinReady = true;
    }

    private IEnumerator InitiateSpin(int i)
    {
        float delay = i * timeDelay;

        yield return new WaitForSeconds(delay);

        reels[i].GetComponent<ReelController>().StartSpin();
    }

    private IEnumerator WaitForResult()
    {
        yield return new WaitForSeconds(spinTime);
        
        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(GetResult(i));
        }
        StartCoroutine(FinishRound());
    }

    private IEnumerator GetResult(int i)
    {
        float delay = i * timeDelay;

        yield return new WaitForSeconds(delay);

        results[i] = Math.Abs(reels[i].GetComponent<ReelController>().StopSpin());
    }

    private void DisplayScore()
    {
        string score = ResultCalculator.FindScore(results);
        Debug.Log(score);
    }

    private IEnumerator FinishRound()
    {
        yield return new WaitForSeconds(1);

        DisplayScore();
        PrepareSpin();
    }
}
