using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class FlamethrowerScript : MonoBehaviour
{
    private int dmg = 10;
    [SerializeField] AudioSource flamethrower;
    void Start()
    {
        flamethrower.Play();
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        DamageableScript target = other.gameObject.GetComponent<DamageableScript>();
        if (target != null)
        {
            target.TakeDamage(dmg);
        }
    }
}
