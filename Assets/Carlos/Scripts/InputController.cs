using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this script to interface with the shooting
/// </summary>
public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Shoot the Weapon when the L key is pressed
        if (Input.GetKey(KeyCode.L))
        {
            GameManager.Instance.Weapon.ShootFromGun();
        }
    }
}
