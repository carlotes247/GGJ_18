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
        // Shoot the Weapon when the right button is pressed
        if (Input.GetMouseButton(0))
        {
            GameManager.Instance.Weapon.ShootFromGun();
        }
    }
}
