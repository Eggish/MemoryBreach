using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet
{
    private List<GameObject> BulletPositions;
    private int PositionIterator;
    public bool IsActive;
    public Vector3 Position = Vector3.positiveInfinity;

    private SoundManager SoundManager;

    public void Fire(List<GameObject> pBulletPositions, SoundManager pSoundManager)
    {
        PositionIterator = 0;
        BulletPositions = pBulletPositions;
        IsActive = true;
        SoundManager = pSoundManager;
    }

    public void Advance()
    {
        if (PositionIterator + 1 >= BulletPositions.Count - 1)
        {
            IsActive = false;
            BulletPositions[PositionIterator].SetActive(false);
            return;
        }

        if (WillHitPlayer(BulletPositions[PositionIterator].transform.position,
            BulletPositions[PositionIterator + 1].transform.position))
        {
            //Debug.Log("Bullet : " + Position + "Player : " + PositionManager.PlayerPos);
            SoundManager.PlayGameOver();
            GameManager.GameOver();
            return;
        }


        BulletPositions[PositionIterator].SetActive(false);

        PositionIterator++;
        Position = BulletPositions[PositionIterator].transform.position;

        BulletPositions[PositionIterator].SetActive(true);
    }

    private bool WillHitPlayer(Vector3 pOldPos, Vector3 pNewPos)
    {
        if (Vector3.Distance(pOldPos, PositionManager.PlayerPos) +
            Vector3.Distance(pNewPos, PositionManager.PlayerPos) -
            Vector3.Distance(pOldPos, pNewPos) < 0.1f)
            return true;
        return false;
    }
}

public class CanonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] BulletLanes = null;

    [SerializeField] private float ShotAdvanceDelay = 1.0f;

    [SerializeField] private float ShotCooldown = 1.0f;

    [SerializeField] private float ShotReduction = 0.1f;

    private float ShotAdvanceTimer = 0.0f;
    private float ShotTimer = 0.0f;

    private List<Bullet> Bullets = new List<Bullet>();

    [SerializeField] private SoundManager SoundManager = null;

    private void Start()
    { }

    private void Update()
    {
        if (GameManager.GameIsOver)
            return;

        ValidateBullets();
        UpdateTimers();
        if (ShotAdvanceTimer < 0) AdvanceBullets();

        if (ShotTimer < 0) ShootRandomCanon();
    }

    private void UpdateTimers()
    {
        ShotAdvanceTimer -= Time.deltaTime;
        ShotTimer -= Time.deltaTime;
    }

    private void AdvanceBullets()
    {
        foreach (var b in Bullets)
        {
            if (b == null)
            {
                Bullets.Remove(b);
                continue;
            }

            b.Advance();
            ShotAdvanceTimer = ShotAdvanceDelay;
        }
    }

    private void ShootRandomCanon()
    {
        var canonNumber = Random.Range(0, 11);
        var chosenBulletLane = new List<GameObject>();
        foreach (Transform child in BulletLanes[canonNumber].transform) chosenBulletLane.Add(child.gameObject);
        if (canonNumber > BulletLanes.Length / 2) chosenBulletLane.Reverse();
        var newBullet = new Bullet();
        newBullet.Fire(chosenBulletLane, SoundManager);
        Bullets.Add(newBullet);
        ShotTimer = ShotCooldown;

        SoundManager.PlayCanonShot();
    }

    public void IncreaseFireRate()
    {
        if (ShotCooldown > ShotReduction + 0.2f) ShotCooldown -= ShotReduction;
    }

    private void ValidateBullets()
    {
        if (Bullets.Count < 0) return;

        var removeIterators = new List<int>();
        for (var i = 0; i < Bullets.Count; i++)
            if (!Bullets[i].IsActive)
                removeIterators.Add(i);

        foreach (var i in removeIterators) Bullets.RemoveAt(i);
    }

    public List<Bullet> GetBullets()
    {
        return Bullets;
    }
}