using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    int healthPoints = 50;

    [SerializeField] GameObject missionFail;

    void Start()
    {
        if (missionFail != null)
        {
            missionFail.SetActive(false);
        }
    }

    void Update()
    {
        hpText.text = healthPoints.ToString();
    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0)
        {
            Time.timeScale = 0f;
            if (missionFail != null)
            {
                missionFail.SetActive(true);
            }

        }
    }
}
