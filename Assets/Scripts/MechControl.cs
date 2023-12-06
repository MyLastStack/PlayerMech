using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechControl : MonoBehaviour
{
    [SerializeField] InputAction moveAction;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
    }
}
