using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> SourcePool = new List<AudioSource>();

    [SerializeField]
    private AudioClip CanonShotTick = null;
    [SerializeField]
    private AudioClip CanonShotTock = null;
    [SerializeField]
    private AudioClip MemoryFail = null;
    [SerializeField]
    private AudioClip MemorySuccess = null;
    [SerializeField]
    private AudioClip GameLoss = null;
    [SerializeField]
    private AudioClip GameWin = null;

    private int CanonTickTracker = 0;

    [SerializeField]
    private float GameOverDelay = 2.0f;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PlayCanonShot()
    {
        if (CanonTickTracker == 0)
        {
            CanonTickTracker = 1;
            GetUnusedSource().PlayOneShot(CanonShotTick);
        }
        else
        {
            CanonTickTracker = 0;
            GetUnusedSource().PlayOneShot(CanonShotTock);
        }
    }

    public void PlayMemoryFail()
    {
        GetUnusedSource().PlayOneShot(MemoryFail);
    }

    public void PlayMemorySuccess()
    {
        GetUnusedSource().PlayOneShot(MemorySuccess);
    }

    public void PlayGameOver()
    {
        GetUnusedSource().PlayOneShot(GameLoss);
        
    }

    public void PlayGameWin()
    {
        GetUnusedSource().PlayOneShot(GameWin);
    }

    private AudioSource GetUnusedSource()
    {
        foreach (AudioSource a in SourcePool)
        {
            if (!a.isPlaying)
            {
                return a;
            }
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        SourcePool.Add(newSource);
        return newSource;
    }

    
}
