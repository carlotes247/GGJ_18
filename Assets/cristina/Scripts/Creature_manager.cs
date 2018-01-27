using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_manager : MonoBehaviour
{

    public float changeSizeValue;
    public float changeColourValue;
    public float shrinkFactor;
    public float minimSize = 0.01f;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(Scale());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sugar"))
        {
            ChangeSizeCreature(changeSizeValue);
            Destroy(collision.gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ice"))
        {
            ChangeColourCreature((transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color.r) - changeColourValue);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Fire"))
        {
            ChangeColourCreature((transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color.r) + changeColourValue);
            Destroy(other.gameObject);
        }
    }

    public void ChangeColourCreature(float value)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;
        Debug.Log(value);
        Debug.Log(transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color.ToString());
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color = new Color(value, 0f, 0.35f, 1);
        Debug.Log(transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().materials[1].color.ToString());
    }

    public void ChangeSizeCreature(float value)
    {

        Vector3 auxScale = this.gameObject.transform.localScale;

        Debug.Log(auxScale.ToString());

        auxScale += new Vector3(value, value, value);

        Debug.Log(auxScale.ToString());

        transform.localScale = auxScale;
    }


    IEnumerator Scale()
    {
        while (minimSize < transform.localScale.x)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * shrinkFactor;
            yield return null;
        }

        if (transform.localScale.x < minimSize) Destroy(gameObject);


    }
}
