using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechControl : MonoBehaviour
{
    [Header("CharacterAction")]
    public InputAction baseMoveAction;
    [SerializeField] Vector2 baseMoveInput;
    [SerializeField] float baseMoveSpeed, baseRotateSpeed;

    [Header("AudioPlayer")]
    [SerializeField] AudioSource wheelTracks;
    public float mag;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MechMovement();
        mag = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, baseMoveInput.y) * baseMoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddRelativeTorque(transform.up * baseMoveInput.x * baseRotateSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void MechMovement()
    {
        baseMoveInput = baseMoveAction.ReadValue<Vector2>();

        if (rb.velocity.magnitude > 0.02)
        {
            if (!wheelTracks.isPlaying)
            {
                wheelTracks.Play();
            }
        }
        else
        {
            wheelTracks.Pause();
        }
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
