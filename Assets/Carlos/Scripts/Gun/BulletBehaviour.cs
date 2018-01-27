//---------------------------------------------------------------------------
// Carlos Gonzalez Diaz - TFG - Simulador Virtual Carabina M4 - 2016
// Universidad Rey Juan Carlos - ETSII
//---------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The behaviour of the bullet
/// </summary>
[AddComponentMenu("CarlosFramework/BulletBehaviour")]
public class BulletBehaviour : MonoBehaviour {

    #region Properties

    /// <summary>
    /// (Field) The tag to compare when the bullet hits an objective
    /// </summary>
    [SerializeField, Tooltip("If empty, will affect everything")]
    private string m_TagToCompare;

    /// <summary>
    /// (Field) Flag that controls if the bullets will affect the player
    /// </summary>
    [SerializeField]
    private bool m_AvoidPlayer;

    /// <summary>
    /// (Field) The particles of the bullet (if any)
    /// </summary>
    [SerializeField]
    private ParticleSystem m_BulletParticles;

    /// <summary>
    /// (Field) The bullet that is being actually thrown
    /// </summary>
    [SerializeField]
    private GameObject m_VisualBullet;

    /// <summary>
    /// (Field) The rigidbody of the bullet, the one to move.
    /// </summary>
    [SerializeField]
    private Rigidbody m_BulletRigidbody;
    
    /// The velocity of the bullet
    [SerializeField]
    float velocity;

    /// The vector to follow when moving
    // Field
    Vector3 bulletDirection;
    // Property
    public Vector3 BulletDirection { get { return bulletDirection; } set { bulletDirection = value; } }

    /// The damage to apply
    [SerializeField]
    float damageToApply; 

    /// The seconds that the bullet will last
    [SerializeField]
    float secondsAlive;
    // Property
    public float SecondsAlive { get { return secondsAlive; } set { secondsAlive = value; } }

    #endregion

    /// This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        //this.GetComponent<Rigidbody>().velocity = bulletDirection * velocity;

        // We reset the position of the visualBullet
        this.m_VisualBullet.transform.position = this.transform.position;

        // We set the velocity of the rigidbody to move 
        this.m_BulletRigidbody.velocity = bulletDirection * velocity;



        StartCoroutine("LifeCountDown");
    }

	/// Use this for initialization
	void Start () {
        
	}
	
	/// Update is called once per frame
	void Update () {
	
	}

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    public void FixedUpdate()
    {
        
    }

    /// This function will set the direction of the bullet
    void UpdateDirection (Vector3 dir)
    {
        this.bulletDirection = dir;
    }

    /// This function will handle the damage we apply to the target
    void ApplyDamage(GameObject target)
    {
        //target.SendMessage("RemoveLife", damageToApply, SendMessageOptions.DontRequireReceiver);
        // We have switched from send message to getComponent due to the performace improvement
        //LifeController aux = target.GetComponent<LifeController>();
        //if (aux != null)
        //{
        //    aux.RemoveLife(damageToApply);
        //}
        
    }

    /// <summary>
    /// This function will deactivate this gameObject
    /// </summary>
    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Deactivates the gameObject passed in
    /// </summary>
    /// <param name="target"> The GameObject to deactivate </param>
    void Deactivate (GameObject target)
    {
        target.SetActive(false);
        //Debug.Log("target deactivated!");
    }

    /// OnCollisionEnter is called when this collider/rigidbody has began touching another rigidbody/collider
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Bullet OnCollisionEnter");

        // If the string is null or empty we always invoke the event
        if (String.IsNullOrEmpty(m_TagToCompare))
        {
            // We invoke the event
            ApplyDamage(collision.gameObject);
            Deactivate(this.gameObject);
        }
        // If it has something, we compare the tag of the passed in collider
        else if (collision.collider.CompareTag(m_TagToCompare))
        {
            // We invoke the event
            ApplyDamage(collision.gameObject);
            Deactivate(this.gameObject);
        }
        // If we need to avoid the player...
        else if (m_AvoidPlayer)
        {
            // If the collision is against the player...
            if (collision.collider.CompareTag("Player"))
            {
                // We exit the method
                return;
            }
        }
        Deactivate(this.gameObject);
    }

    /// OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {        
        //Debug.Log("Bullet OnTriggerEnter");

        // If the string is null or empty we always invoke the event
        if (String.IsNullOrEmpty(m_TagToCompare))
        {
            // We invoke the event
            ApplyDamage(other.gameObject);
            Deactivate(this.gameObject);
        }
        // If it has something, we compare the tag of the passed in collider
        else if (other.CompareTag(m_TagToCompare))
        {
            // We invoke the event
            ApplyDamage(other.gameObject);
            Deactivate(this.gameObject);
        }
        // If we need to avoid the player...
        else if (m_AvoidPlayer)
        {
            // If the collision is against the player...
            if (other.CompareTag("Player"))
            {
                // We exit the method
                return;
            }
        }

        // If the object should ignore bullets...
        if (other.CompareTag("IgnoreBullets"))
        {
            // We exit the method
            return;            
        }

        // If the object is interactable to bullets...
        if (other.CompareTag("Interactable"))
        {
            // We add a force
            other.GetComponent<Rigidbody>().AddForce(bulletDirection * 20f, ForceMode.Impulse);
        }
        
        // We deactivate the bullet when hits the object
        Deactivate(this.gameObject);
    }

    /// The coroutine to deactivate bullets
    IEnumerator LifeCountDown() 
    {
        float secondsLeft = this.SecondsAlive;
		while (secondsLeft > 0) {
			secondsLeft -= 1;
			yield return new WaitForSeconds(1);
		}
        Deactivate(this.gameObject);
        StopLifeCountDown();
    }
    
    /// The method in charge of stopping the coroutine
    void StopLifeCountDown()
    {
        StopCoroutine("LifeCountDown");
    }




    


}
