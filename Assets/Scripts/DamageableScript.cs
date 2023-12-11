using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    public int health = 50;

    [SerializeField] GameObject destructible;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        if (destructible != null)
        {
            Instantiate(destructible, transform.position, transform.rotation);
        }
    }
}
