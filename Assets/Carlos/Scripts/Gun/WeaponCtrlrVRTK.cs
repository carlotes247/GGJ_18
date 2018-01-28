using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// The Weapon Controller interface that will make the gun work with VRTK
/// </summary>
public class WeaponCtrlrVRTK : VRTK_InteractableObject {

    public bool Using;
    [SerializeField]
    private WeaponController m_Weapon;

    private void Start()
    {
        if (m_Weapon == null)
        {
            this.GetComponent<WeaponController>();
            if (m_Weapon == null)
            {
                Debug.LogError("Weapon Ctrler not found in " + this.name);
            }
        }
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        Using = true;
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
        Using = false;
    }

    public override void UpdatePass()
    {
        if (Using)
        {
            m_Weapon.ShootFromGun();
        }

        Debug.Log("I'm being used!!");
    }


}
