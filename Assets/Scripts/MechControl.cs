using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechControl : MonoBehaviour
{
    public InputAction baseMoveAction;
    [SerializeField] Vector2 baseMoveInput;
    [SerializeField] float baseMoveSpeed, baseRotateSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MechMovement();
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, baseMoveInput.y) * baseMoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddRelativeTorque(transform.up * baseMoveInput.x * baseRotateSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void MechMovement()
    {
        baseMoveInput = baseMoveAction.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        baseMoveAction.Enable();
    }
    private void OnDisable()
    {
        baseMoveAction.Disable();
    }
}
