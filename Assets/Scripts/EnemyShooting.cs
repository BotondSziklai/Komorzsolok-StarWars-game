using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject laserPrefab; // A Tie Fighter lövedéke
    public Transform firePoint; // A lövedék spawn pontja
    public float fireInterval = 3f; // Lövési időköz
    public float shootingChance = 0.5f; // 50% esély, hogy lőni fog
    
    void Start()
    {
        if (Random.value < shootingChance) // Csak bizonyos Tie Fighterek lőnek
        {
            InvokeRepeating("FireLaser", fireInterval, fireInterval);
        }
    }

    void FireLaser()
    {
        if (laserPrefab != null && firePoint != null)
        {
            Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
