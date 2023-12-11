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
    [SerializeField] AudioSource mgShots;
    [SerializeField] AudioSource cannonFireL;
    [SerializeField] AudioSource cannonFireR;

    [Header("Weapons")]
    public InputAction firingAction;
    // Machine Gun
    public Camera fpsCam;
    [SerializeField] ParticleSystem mgMuzzleFlash;
    public GameObject gunImpactEffect;
    private float mgNextTimeToFire = 0f;
    private float mgFireRate = 15f;
    // Missile
    public Transform lSide;
    public Transform rSide;
    private float missileProjectileSpeed = 1000f;
    private float missileNextTimeToFire = 0f;
    private float missileFireRate = 3f;
    [SerializeField] private KablooeyScript missile;
    [SerializeField] ParticleSystem lMissileMuzzleFlash;
    [SerializeField] ParticleSystem rMissileMuzzleFlash;
    // Flamethrower
    [SerializeField] GameObject flamethrower;
    public enum currentSelect
    { 
        LShoulder,
        RShoulder,
        LArm,
        RArm
    }
    public currentSelect selectedWeapon = currentSelect.RArm;
    private int lor = 1;

    [Header("Weapon Data")]
    public int machineGunAmmo;
    public int leftCannonAmmo;
    public int rightCannonAmmo;
    public int flameThrowerAmmo;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flamethrower.SetActive(false);
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
            lor = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            selectedWeapon = currentSelect.RShoulder;
            lor = 2;
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
                    if (Time.time >= missileNextTimeToFire && leftCannonAmmo > 0)
                    {
                        missileNextTimeToFire = Time.time + 1 / missileFireRate;
                        MissileShoot(lor);
                    }
                    break;
                case currentSelect.RShoulder:
                    if (Time.time >= missileNextTimeToFire && rightCannonAmmo > 0)
                    {
                        missileNextTimeToFire = Time.time + 1 / missileFireRate;
                        MissileShoot(lor);
                    }
                    break;
                case currentSelect.LArm:
                    if (flameThrowerAmmo > 0)
                    {
                        FlamethrowerUse();
                    }
                    else
                    {
                        flamethrower.SetActive(false);
                    }
                    break;
                case currentSelect.RArm:
                    if (Time.time >= mgNextTimeToFire && machineGunAmmo > 0)
                    {
                        mgNextTimeToFire = Time.time + 1 / mgFireRate;
                        MGShoot();
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            flamethrower.SetActive(false);
        }
    }

    private void MGShoot()
    {
        mgMuzzleFlash.Play();
        mgShots.Play();
        int dmg = 5;
        machineGunAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100f))
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

            GameObject impactGO = Instantiate(gunImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
    private void MissileShoot(int side)
    {
        Transform spawner = gameObject.transform;
        if (side == 1)
        {
            spawner = lSide.transform;
            lMissileMuzzleFlash.Play();
            leftCannonAmmo--;
        }
        else if (side == 2)
        {
            spawner = rSide.transform;
            rMissileMuzzleFlash.Play();
            rightCannonAmmo--;
        }

        var position = spawner.position + spawner.forward;
        var rotation = spawner.rotation;
        KablooeyScript projectile = Instantiate(missile, position, rotation);
    }
    private void FlamethrowerUse()
    {
        flamethrower.SetActive(true);
        flameThrowerAmmo--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MGBox")
        {
            machineGunAmmo = 200;
        }
        if (other.tag == "CannonBox")
        {
            leftCannonAmmo = 10;
            rightCannonAmmo = 10;
        }
        if (other.tag == "FTBox")
        {
            flameThrowerAmmo = 2000;
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
