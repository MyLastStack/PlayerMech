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

    [Header("Weapons")]
    public InputAction firingAction;
    public Camera fpsCam;
    [SerializeField] ParticleSystem mgMuzzleFlash;
    public GameObject impactEffect;
    private float nextTimeToFire = 0f;
    private float fireRate = 15f;
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
        WeaponInteraction();
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

    private void WeaponInteraction()
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

        if (Input.GetButton("Fire1"))
        {
            switch (selectedWeapon)
            {
                case currentSelect.LShoulder:
                    break;
                case currentSelect.RShoulder:
                    break;
                case currentSelect.LArm:
                    break;
                case currentSelect.RArm:
                    if (Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1 / fireRate;
                        MGShoot();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void MGShoot()
    {
        mgMuzzleFlash.Play();
        float dmg = 10f;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 50f))
        {
            DamageableScript target = hit.transform.GetComponent<DamageableScript>();
            if (target != null)
            {
                target.TakeDamage(dmg);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 30);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }

    #region InputAction Enables and Disables
    private void OnEnable()
    {
        baseMoveAction.Enable();
        firingAction.Enable();
    }
    private void OnDisable()
    {
        baseMoveAction.Disable();
        firingAction.Disable();
    }
    #endregion
}
