using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    public InputAction switchCam;
    public Transform tpPosition, fpPosition;

    public bool firstPerson;

    void Start()
    {
        firstPerson = false;
    }

    void Update()
    {
        if (switchCam.WasReleasedThisFrame())
        {
            if (!firstPerson) { firstPerson = true; }
            else { firstPerson = false; }
        }

        if (!firstPerson)
        {
            transform.position = tpPosition.position;
            transform.rotation = tpPosition.rotation;
        }
        else
        {
            transform.position = fpPosition.position;
            transform.rotation = fpPosition.rotation;
        }
    }

    private void OnEnable()
    {
        switchCam.Enable();
    }
    private void OnDisable()
    {
        switchCam.Disable();
    }
}
