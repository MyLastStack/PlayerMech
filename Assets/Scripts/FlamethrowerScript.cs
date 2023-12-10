using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class FlamethrowerScript : MonoBehaviour
{
    private float dmg = 3f;
    void Start()
    {
        
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
