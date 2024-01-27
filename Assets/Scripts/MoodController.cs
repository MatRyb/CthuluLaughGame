using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodController : MonoBehaviour
{
    TentacleController tc;
    int moodLevel = 0;

    void Start()
    {
        tc = GetComponent<TentacleController>();
    }

    void Update()
    {
        switch(moodLevel)
        {
            case 0:
                if (tc.getKilledTentacles() >= 3)
                {
                    moodLevel = 1;
                    tc.permaSkeletons = 2;
                    tc.freeSkeletons = 2;
                }
                break;
            case 1:
                if (tc.getKilledTentacles() >= 5)
                {
                    moodLevel = 2;
                    tc.permaSkeletons = 3;
                    tc.freeSkeletons = 4;
                }
                break;
            case 2:
                if (tc.getKilledTentacles() >= 7)
                {
                    moodLevel = 3;
                    tc.permaSkeletons = 5;
                    tc.freeSkeletons = 8;
                }
                break;
            case 3:
                if (tc.getKilledTentacles() >= 9)
                {
                    moodLevel = 4;
                    tc.permaSkeletons = 5;
                    tc.freeSkeletons = 15;
                }
                break;
            case 4:
                if (tc.getKilledTentacles() >= 12)
                {
                    moodLevel = 5;
                }
                break;
            case 5:
                WinGame();
                break;
            default:
                Debug.LogWarning("Unwanted mood change");
                break;

        }
    }

    void WinGame()
    {
        Debug.Log("Congratulations!");
    }
}
