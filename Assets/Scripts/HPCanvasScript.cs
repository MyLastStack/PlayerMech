using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPCanvasScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    DamageableScript damageableScript;

    [SerializeField] Transform station;
    [SerializeField] Transform playCam;

    void Start()
    {
        damageableScript = GetComponent<DamageableScript>();
    }

    void Update()
    {
        text.text = damageableScript.health.ToString();
        station.LookAt(playCam);
    }
}
