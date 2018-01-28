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

    [Tooltip("When the dying animation should start playing before it reaches the minimun size")]
    public float dyingAnimThreshold;
    public GameObject BacteriaVisual;
    public BacteriaAnimController BacteriaAnimator;
    public AudioController BacteriaAudioCtrler;
    public Collider BacteriaCollider;


    public Texture newCreatureTexture; //Ceiling_MetallicSmoothness
    //public GameObject creature; //the object to be duplicated

    // Use this for initialization
    void Start()
    {
        if (BacteriaAnimator == null)
        {
            this.GetComponentInChildren<BacteriaAnimController>();
            if (BacteriaAnimator == null)
            {
                Debug.LogError("Bacteria Animator Ctrler not found in Creature Manager!");
            }
        }

        if (BacteriaAnimator == null)
        {
            this.GetComponentInChildren<AudioController>();
            if (BacteriaAnimator == null)
            {
                Debug.LogError("Bacteria Audio Ctrler not found in Creature Manager!");
            }
        }
        if (BacteriaCollider == null)
        {
            this.GetComponent<Collider>();
            if (BacteriaCollider == null)
            {
                Debug.LogError("Bacteria Collider not found in Creature Manager!");
            }
        }

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

            // Bacteria Animates and plays the shrinking sounds
            BacteriaAudioCtrler.PlayBacteriaShrinkSad();
            BacteriaAnimator.Interact();

            //Destroy(other.gameObject);
        }

        if (other.CompareTag("Fire"))
        {
            ChangeColourCreature((matToChange.color.r) + changeColourValue);

            // Bacteria Animates and plays the growing sounds
            BacteriaAudioCtrler.PlayBacteriaGrowHappy();
            BacteriaAnimator.Interact();
            //Destroy(other.gameObject);
        }
    }

    public void ChangeColourCreature(float value)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;
        matToChange.color = new Color(value, 0f, 0.35f, 1);

        //check if it should duplicate
        if (matToChange.color.r > 0.99) duplicateCreature();
    }

    public void ChangeSizeCreature(float value)
    {

        Vector3 auxScale = this.gameObject.transform.localScale;

        auxScale += new Vector3(value, value, value);

        transform.localScale = auxScale;
    }


    IEnumerator Scale()
    {
        while (minimSize < transform.localScale.x)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * shrinkFactor;

            if (transform.localScale.x < dyingAnimThreshold)
            {
                // Play dying sound (it should take a bit of time, but for the moment it is ok)
                BacteriaAnimator.Die(true);
                // We also deactivate the trigger collider to make sure nothing interacts with this

            }

            yield return null;
        }

        if (transform.localScale.x < minimSize) {
            // DYING CODE

            // We deactivate it safely
            SetActiveSafely(false);
            //Destroy(gameObject);
        } 


    }

    private void duplicateCreature()
    {

        // Animate and play sound of creature (inside animation already, dirty code I know)
        BacteriaAnimator.Reproduce(true);

        float offsetX = this.gameObject.transform.localScale.x/2f;
        //change the colour so it won't duplicate again
        matToChange.color = new Color(matToChange.color.r*0.2f, 0f, 0.35f, 1);

        //calculating how many creatures to make
        int creatures = (int)this.gameObject.transform.localScale.x + 1;
        Debug.Log("creatures:"+creatures);

        for (int i = 0; i < creatures; i++)
        {
            GameObject newCreature = Instantiate(this.gameObject, this.gameObject.transform);
            float newXPos = newCreature.transform.position.x + (Random.Range(-offsetX, offsetX));
            newCreature.transform.position = new Vector3(newXPos, newCreature.transform.position.y, Random.Range(-newXPos, newXPos));//offsetting
            newCreature.transform.parent = null;

            //Renderer rend = newCreature.GetComponent<Renderer>();
            //rend.material = new Material(Shader.Find("M_BacteriaWhite_001"));
            //matToChange = rend.material;

            ////Find the Standard Shader
            //Material myNewMaterial = new Material(Shader.Find("Standard"));
            ////Set Texture on the material
            //myNewMaterial.SetTexture("_MainTex", newCreatureTexture);
            ////Apply to GameObject
            //newCreature.transform.GetComponent<MeshRenderer>().material = myNewMaterial;
        }

        BacteriaAnimator.Reproduce(false);
        
       
    }

    /// <summary>
    /// Activates the right components from the creature
    /// </summary>
    /// <param name="value">true to activate</param>
    public void SetActiveSafely (bool value)
    {
        // No more animations
        BacteriaAnimator.SetAllAnimationsOff();

        // Disable the bacteria
        this.gameObject.SetActive(value);
    }
}
