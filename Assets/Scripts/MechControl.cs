using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechControl : MonoBehaviour
{
    [Header("Character Action")]
    public InputAction baseMoveAction;
    [SerializeField] Vector2 baseMoveInput;
    [SerializeField] float baseMoveSpeed, baseRotateSpeed;

    [Header("Audio Player")]
    [SerializeField] AudioSource wheelTracks;

    [Header("Weapon Selection")]
    [SerializeField] bool LShoulderUsed;
    [SerializeField] bool RShoulderUsed;
    [SerializeField] bool LArmUsed;
    [SerializeField] bool RArmUsed;
    public enum currentSelect
    { 
        LShoulder,
        RShoulder,
        LArm,
        RArm
    }
    public currentSelect selectedWeapon = currentSelect.RArm;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MechMovement();
        WeaponSelecting();
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

    private void WeaponSelecting()
    {
        if (Input.GetKeyDown("1"))
        {
            selectedWeapon = currentSelect.LShoulder;
        }
        else if (Input.GetKeyDown("2"))
        {
            selectedWeapon = currentSelect.RShoulder;
        }
        else if (Input.GetKeyDown("3"))
        {
            selectedWeapon = currentSelect.LArm;
        }
        else if (Input.GetKeyDown("4"))
        {
            selectedWeapon = currentSelect.RArm;
        }
    }

    #region InputAction Enables and Disables
    private void OnEnable()
    {
        baseMoveAction.Enable();
    }
    private void OnDisable()
    {
        baseMoveAction.Disable();
    }
    #endregion
}
