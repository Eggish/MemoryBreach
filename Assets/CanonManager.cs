using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Bullet
{
    private GameObject[] BulletLane;
    private int Position;

    public void Fire(GameObject[] pBulletLane, Direction pDirection)
    {

    }
}

public class CanonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Canons;
    [SerializeField]
    private GameObject[] BulletLanes;

    private List<Bullet> Bullets;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
