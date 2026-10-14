using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public ParticleSystem impactFX;
    public float lifetime = 3f; 

    private ObjectPool<Rigidbody> pool;
    private Rigidbody rb;
    private Coroutine lifetimeCoroutine;
    private bool isReleased = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(ObjectPool<Rigidbody> targetPool)
    {
        pool = targetPool;
    }

    private void OnEnable()
    {
        isReleased = false;
    
        if (lifetimeCoroutine != null) StopCoroutine(lifetimeCoroutine);
        lifetimeCoroutine = StartCoroutine(AutoReturnRoutine());
    }

    private IEnumerator AutoReturnRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (impactFX != null)
        {
            Instantiate(impactFX, transform.position, Quaternion.identity);
        }

       

        if (collision.gameObject.CompareTag("PlayerTank"))
        {
            collision.gameObject.GetComponent<TankHealth>().takeDamage(20);
        }

        if (collision.gameObject.CompareTag("EnemyTank"))
        {
            collision.gameObject.GetComponent<EnemyTankHealth>().takeDamage(20);
        }

        ReturnToPool();
    }
    private void ReturnToPool()
    {
        if (isReleased) return; 
        isReleased = true;

        if (lifetimeCoroutine != null)
        {
            StopCoroutine(lifetimeCoroutine);
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
