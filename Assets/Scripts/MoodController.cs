using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodController : MonoBehaviour
{
    TentacleController tc;
    [SerializeField] AudioClip[] moodClips;
    [SerializeField] Material[] arenaMats;
    [SerializeField] MeshRenderer arena;

    [SerializeField] Material waterClean;
    [SerializeField] MeshRenderer water;

    AudioSource auS;
    public int moodLevel = 0;

    void Start()
    {
        //arenaMaterial.
        tc = GetComponent<TentacleController>();
        auS = GetComponent<AudioSource>();

        auS.clip = moodClips[0];
        auS.Play();
    }

    void Update()
    {
        switch(moodLevel)
        {
            case 0:
                if (tc.getKilledTentacles() >= 3)
                {
                    arena.material = arenaMats[0];
                    auS.Stop();
                    auS.clip = moodClips[1];
                    auS.Play();
                    moodLevel = 1;
                    tc.permaSkeletons = 5;
                    tc.freeSkeletons = 2;
                }
                break;
            case 1:
                if (tc.getKilledTentacles() >= 5)
                {
                    arena.material = arenaMats[1];
                    auS.Stop();
                    auS.clip = moodClips[2];
                    auS.Play();
                    moodLevel = 2;
                    tc.permaSkeletons = 5;
                    tc.freeSkeletons = 4;
                }
                break;
            case 2:
                if (tc.getKilledTentacles() >= 7)
                {
                    arena.material = arenaMats[2];
                    auS.Stop();
                    auS.clip = moodClips[3];
                    auS.Play();
                    moodLevel = 3;
                    tc.freeSkeletons = 8;
                }
                break;
            case 3:
                if (tc.getKilledTentacles() >= 9)
                {
                    water.material = waterClean;
                    arena.material = arenaMats[3];
                    auS.Stop();
                    auS.clip = moodClips[4];
                    auS.Play();
                    moodLevel = 4;
                    tc.freeSkeletons = 16;
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
