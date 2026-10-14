using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public ParticleSystem impactFX;
    
    private ObjectPool<Rigidbody> pool;
    private Rigidbody rb;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    public void Initialize(ObjectPool<Rigidbody> targetPool)
    {
        pool = targetPool;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (impactFX != null)
        {
            Instantiate(impactFX, transform.position, Quaternion.identity);
        }
        
        

        if (collision.gameObject.CompareTag("PlayerTank"))
        {
            collision.gameObject.GetComponent<TankHealth>()?.takeDamage(20);
        }

        if (collision.gameObject.CompareTag("EnemyTank"))
        {
            collision.gameObject.GetComponent<EnemyTankHealth>()?.takeDamage(20);
        }

        
        if (pool != null)
        {
            pool.Release(rb);
        }
        else
        {
            Destroy(gameObject); 
        }

    }
}
