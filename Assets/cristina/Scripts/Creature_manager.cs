using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_manager : MonoBehaviour
{

    public float changeSizeValue;
    public float changeColourValue;
    public float shrinkFactor;
    public float minimSize = 0.01f;
    public Material matToChange;

    //public GameObject creature; //the object to be duplicated

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
            ChangeColourCreature((matToChange.color.r) - changeColourValue);
            //Destroy(other.gameObject);
        }

        if (other.CompareTag("Fire"))
        {
            ChangeColourCreature((matToChange.color.r) + changeColourValue);
            //Destroy(other.gameObject);
        }
    }

    public void ChangeColourCreature(float value)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;
        Debug.Log(value);
        Debug.Log(matToChange.color.ToString());
        matToChange.color = new Color(value, 0f, 0.35f, 1);
        Debug.Log(matToChange.color.ToString());

        //check if it should duplicate
        if (matToChange.color.r > 0.99) duplicateCreature();
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

    private void duplicateCreature()
    {   float offsetX = this.gameObject.transform.localScale.x/2f;
        //change the colour so it won't duplicate again
        matToChange.color = new Color(matToChange.color.r*0.2f, 0f, 0.35f, 1);

        //calculating how many creatures to make
        int creatures = (int) (this.gameObject.transform.localScale.x % 1 * 10f);
        while (creatures > 1)
        {
            GameObject newCreature = Instantiate(this.gameObject, this.gameObject.transform);
            float newXPos = newCreature.transform.localPosition.x - offsetX;
            newCreature.transform.localPosition = new Vector3(newXPos, newCreature.transform.localPosition.y, Random.Range(0, newXPos));//offsetting a bit
            creatures--;
        }
        
       
    }
}
