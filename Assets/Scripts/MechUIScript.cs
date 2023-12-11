using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MechUIScript : MonoBehaviour
{
    [Header("Player Scripts")]
    [SerializeField] MechControl player;

    [Header("UI Connect")]
    [SerializeField] Image LShoulder;
    [SerializeField] Image RShoulder;
    [SerializeField] Image LArm;
    [SerializeField] Image Rarm;

    [Header("Ammo Counter")]
    [SerializeField] TextMeshProUGUI lCannon;
    [SerializeField] TextMeshProUGUI rCannon;
    [SerializeField] TextMeshProUGUI mgAmmo;
    [SerializeField] TextMeshProUGUI flamethrower;

    void Start()
    {
        
    }

    void Update()
    {
        lCannon.text = $"{player.leftCannonAmmo}";
        rCannon.text = $"{player.rightCannonAmmo}";
        mgAmmo.text = $"{player.machineGunAmmo}";
        flamethrower.text = $"{player.flameThrowerAmmo}";

        switch (player.selectedWeapon)
        {
            case MechControl.currentSelect.LShoulder:
                LShoulder.color = Color.yellow;

                RShoulder.color = Color.white;
                LArm.color = Color.white;
                Rarm.color = Color.white;
                break;
            case MechControl.currentSelect.RShoulder:
                RShoulder.color = Color.yellow;

                LShoulder.color = Color.white;
                LArm.color = Color.white;
                Rarm.color = Color.white;
                break;
            case MechControl.currentSelect.LArm:
                LArm.color = Color.yellow;

                LShoulder.color = Color.white;
                RShoulder.color = Color.white;
                Rarm.color = Color.white;
                break;
            case MechControl.currentSelect.RArm:
                Rarm.color = Color.yellow;

                LShoulder.color = Color.white;
                RShoulder.color = Color.white;
                LArm.color = Color.white;
                break;
            default:
                break;
        }
    }
}
