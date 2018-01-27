using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_size_controller : MonoBehaviour {

    public float changeSizeValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSizeCreature(float value) {

        Vector3 auxScale = this.gameObject.transform.localScale;

        Debug.Log(auxScale.ToString());

        auxScale += new Vector3(value, value, value);

        Debug.Log(auxScale.ToString());

        transform.localScale = auxScale;

        //transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sugar"))
        {
            ChangeSizeCreature(changeSizeValue);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag=="Sugar")
        {
            ChangeSizeCreature(changeSizeValue);
            Destroy(collision.gameObject);
        }

    }
}
