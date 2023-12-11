using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class FlamethrowerScript : MonoBehaviour
{
    private float dmg = 6f;
    [SerializeField] AudioSource flamethrower;
    void Start()
    {
        flamethrower.Play();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableScript target = other.transform.GetComponent<DamageableScript>();
        if (target != null)
        {
            target.TakeDamage(dmg);
        }
    }
}
