using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet
{
    private List<GameObject> BulletPositions;
    private int Position;
    public bool IsActive;


    public void Fire(List<GameObject> pBulletPositions)
    {
        Position = 0;
        BulletPositions = pBulletPositions;
        IsActive = true;
    }

    public void Advance()
    { 
        if (Position + 1 >= BulletPositions.Count - 1)
        {
            IsActive = false;
            BulletPositions[Position].SetActive(false);
            return;
        }

        if (WillHitPlayer(BulletPositions[Position].transform.position,
            BulletPositions[Position + 1].transform.position))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        BulletPositions[Position].SetActive(false);
        Position++;
        BulletPositions[Position].SetActive(true);
    }

    private bool WillHitPlayer(Vector3 pOldPos, Vector3 pNewPos)
    {
        if (Vector3.Distance(pOldPos, PositionManager.PlayerPos) +
            Vector3.Distance(pNewPos, PositionManager.PlayerPos)
            == Vector3.Distance(pOldPos, pNewPos))
        {
            return true;
        }
        return false;
    }
}

public class CanonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] BulletLanes = null;

    [SerializeField] 
    private float ShotAdvanceDelay = 1.0f;

    [SerializeField] 
    private float ShotCooldown = 1.0f;

    [SerializeField]
    private float ShotReduction = 0.1f;

    private float ShotAdvanceTimer = 0.0f;
    private float ShotTimer = 0.0f;

    private List<Bullet> Bullets = new List<Bullet>();

    void Start()
    {
        
    }

    void Update()
    {
        ValidateBullets();
        UpdateTimers();
        if (ShotAdvanceTimer < 0)
        {
            AdvanceBullets();
        }

        if (ShotTimer < 0)
        {
            ShootRandomCanon();
        }
    }

    private void UpdateTimers()
    {
        ShotAdvanceTimer -= Time.deltaTime;
        ShotTimer -= Time.deltaTime;
    }

    private void AdvanceBullets()
    {
        foreach (Bullet b in Bullets)
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
        int canonNumber = Random.Range(0, 11);
        List<GameObject> chosenBulletLane = new List<GameObject>();
        foreach (Transform child in BulletLanes[canonNumber].transform)
        {
            chosenBulletLane.Add(child.gameObject);
        }
        if (canonNumber > BulletLanes.Length / 2)
        {
            chosenBulletLane.Reverse();
        }
        Bullet newBullet = new Bullet();
        newBullet.Fire(chosenBulletLane);
        Bullets.Add(newBullet);
        ShotTimer = ShotCooldown;
    }
    public void IncreaseFireRate()
    {
        ShotCooldown -= ShotReduction;
    }

    private void ValidateBullets()
    {
        if (Bullets.Count < 0)
        {
            return;
        }

        List<int> removeIterators = new List<int>();
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (!Bullets[i].IsActive)
            {
                removeIterators.Add(i);
            }
        }

        foreach (int i in removeIterators)
        {
            Bullets.RemoveAt(i);
        }
    }
}
