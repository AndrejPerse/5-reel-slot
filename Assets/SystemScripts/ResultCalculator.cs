using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResultCalculator
{
    public static string FindScore(int[] results)
    {
        int[][] reelResults = new int[5][];

        reelResults = AssignResults(results);

        return FindCombination(reelResults);
    }

    private static int[][] AssignResults(int[] results)
    {
        int[][] reelsOrder = new int[5][];
        reelsOrder[0] = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        reelsOrder[1] = new int[] { 6, 3, 2, 4, 1, 5, 8, 7 };
        reelsOrder[2] = new int[] { 4, 8, 3, 2, 1, 6, 7, 5 };
        reelsOrder[3] = new int[] { 2, 6, 1, 5, 3, 8, 7, 4 };
        reelsOrder[4] = new int[] { 7, 5, 3, 2, 8, 6, 1, 4 };

        int[][] reelsResults = new int[5][];
        reelsResults[0] = new int[3];
        reelsResults[1] = new int[3];
        reelsResults[2] = new int[3];
        reelsResults[3] = new int[3];
        reelsResults[4] = new int[3];

        for (int i = 0; i < reelsResults.Length; i++)
        {
            // middle line of the slot
            reelsResults[i][1] = reelsOrder[i][results[i]];
            
            // top line of the slot
            if (results[i] == 7)
            {
                reelsResults[i][0] = reelsOrder[i][0];
            }
            else
            {
                reelsResults[i][0] = reelsOrder[i][results[i]+1];
            }

            // bottom line of the slot
            if (results[i] == 0)
            {
                reelsResults[i][2] = reelsOrder[i][7];
            }
            else
            {
                reelsResults[i][2] = reelsOrder[i][results[i]-1];
            }
        }

        return reelsResults;
    }

    private static string FindCombination(int[][] reelsResults)
    {
        string scoreString = "Bad luck!";
        int scoreInt = 0;

        foreach (int symbolOne in reelsResults[0])
        {
            foreach (int symbolTwo in reelsResults[1])
            {
                foreach (int symbolThree in reelsResults[2])
                {
                    if (symbolThree == symbolOne && symbolThree == symbolTwo)
                    {
                        if (scoreInt <= 3)
                        {
                            scoreString = "Three in a row!";
                            scoreInt = 3;
                        }

                        foreach (int symbolFour in reelsResults[3])
                        {
                            if (symbolFour == symbolThree)
                            {
                                if (scoreInt <= 4)
                                {
                                    scoreString = "Four in a row!";
                                    scoreInt = 4;
                                }

                                foreach (int symbolFive in reelsResults[4])
                                {
                                    if (symbolFive == symbolThree)
                                    {
                                        scoreString = "Five in a row!";
                                        scoreInt = 5;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return scoreString;
    }
}
