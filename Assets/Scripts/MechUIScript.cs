using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        
    }

    void Update()
    {
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
