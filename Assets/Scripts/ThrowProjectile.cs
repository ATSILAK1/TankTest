using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


public class ThrowProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Tooltip("Drag your Rigidbody‐based projectile prefab here.")]
    public Rigidbody projectilePrefab;

    [Tooltip("Where the projectile will be spawned from.")]
    public Transform throwPoint;

    [Tooltip("Initial speed applied to the projectile.")]
    public float launchForce = 15f;

    [Header("Fire Rate")]
    [Tooltip("Minimum time (in seconds) between consecutive shots.")]
    public float timeBetweenShots = 0.5f;

    // Time at which we’re next allowed to shoot
    private float nextTimeToFire = 0f;

   
    private ObjectPool<Rigidbody> pool;


    private void Awake()
    {
        pool = new ObjectPool<Rigidbody>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50
        );
        
    }


    void Update()
    {
        
        // On left mouse‐button down, check cooldown
        if ((Input.GetMouseButtonDown(0) || Input.GetAxis("RT") > 0) && Time.time >= nextTimeToFire)
        {
            ThrowProjectile1();
            nextTimeToFire = Time.time + timeBetweenShots;
        }
    }

    private void ThrowProjectile1()
    {
        if (projectilePrefab == null || throwPoint == null) return;

        
        Rigidbody projInstance = pool.Get();
        
        projInstance.transform.position = throwPoint.position;
        projInstance.transform.rotation = throwPoint.rotation;

        
        projInstance.linearVelocity = Vector3.zero;
         projInstance.angularVelocity = Vector3.zero;
        projInstance.linearVelocity = throwPoint.forward * launchForce;

        
        StartCoroutine(ReturnAfter(projInstance, 3f));
    }
    private Rigidbody CreateItem()
    {
        Rigidbody proj = Instantiate(projectilePrefab);
        proj.gameObject.SetActive(false);
        
        Projectile projInstance = proj.GetComponent<Projectile>();
        if (projInstance != null)
        {
            projInstance.Initialize(pool);
        }
        return proj;
    }

    
    private void OnGet(Rigidbody proj)
    {
        proj.gameObject.SetActive(true);
    }

  
    private void OnRelease(Rigidbody proj)
    {
        proj.gameObject.SetActive(false);
    }


    private void OnDestroyItem(Rigidbody proj)
    {
        if (proj != null)
        {
            Destroy(proj.gameObject);
        }
    }

    private IEnumerator ReturnAfter(Rigidbody proj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
       
        pool.Release(proj);
    }

 
}
