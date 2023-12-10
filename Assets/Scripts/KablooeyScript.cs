using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class KablooeyScript : MonoBehaviour
{
    private bool explode = false;
    private float dmg = 25f;

    private Vector3 lastHitPlaced;
    public GameObject explosionEffect;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Invoke("ForcedExplosion", 10f);
    }

    void Update()
    {
        if (explode)
        {
            GameObject impactGO = Instantiate(explosionEffect, lastHitPlaced, Quaternion.LookRotation(lastHitPlaced));
            Destroy(gameObject, 2f);
        }
    }

    public void Fire(float speed, Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    void ForcedExplosion()
    {
        rb.velocity = Vector3.zero;
        lastHitPlaced = transform.position;
        explode = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        lastHitPlaced = transform.position;
        explode = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableScript target = other.GetComponent<DamageableScript>();
        if (target != null)
        {
            target.TakeDamage(dmg);
        }
    }
}
