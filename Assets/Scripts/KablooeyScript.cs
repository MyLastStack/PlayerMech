using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class KablooeyScript : MonoBehaviour
{
    private bool explode = false;
    private bool alreadyExploded = false;
    private float dmg = 30f;

    private Vector3 lastHitPlaced;
    public GameObject explosionEffect;
    [SerializeField] AudioSource impactSound;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * 50f;

        Invoke("ForcedExplosion", 10f);
    }

    void Update()
    {
        if (explode && !alreadyExploded)
        {
            GameObject impactGO = Instantiate(explosionEffect, lastHitPlaced, Quaternion.LookRotation(lastHitPlaced));
            alreadyExploded = true;
            Destroy(gameObject, 2f);
        }
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
        DamageableScript target = other.gameObject.GetComponent<DamageableScript>();
        if (target != null)
        {
            Debug.Log("Found");
            if (alreadyExploded)
            {
                target.TakeDamage(dmg);
                impactSound.Play();
            }
        }
    }
}
