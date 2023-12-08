using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonFollowAngle : MonoBehaviour
{
    [SerializeField] Transform shoulderAngle;
    void Update()
    {
        transform.rotation = shoulderAngle.rotation;
    }
}
