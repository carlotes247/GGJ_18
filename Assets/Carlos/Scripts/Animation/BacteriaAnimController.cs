using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaAnimController : MonoBehaviour {

    /// <summary>
    /// The animator of the Bacteria
    /// </summary>
    public Animator Anim;
    /// <summary>
    /// The audio manager attached to the bacteria
    /// </summary>
    [SerializeField]
    private AudioController m_BacteriaAudioCtrler;

    private int m_ReproducingBoolID;
    private int m_DyingBoolID;
    private int m_InteractTriggerID;

    private bool setAllAnimOffCalled;
    /// <summary>
    /// (Property) Flag to control that the function SetAllAnimationsOff is called only once, to avoid infinite loops
    /// </summary>
    public bool SetAllAnimOffCalled { get { return this.setAllAnimOffCalled; } set { this.setAllAnimOffCalled = value; } }


    // Use this for initialization
    void Start () {
        // Finds an animator if there is none attached
        if (Anim == null)
        {
            this.GetComponent<Animator>();
            if (Anim == null)
            {
                // If there is no animator in this gObject
                Debug.LogError("Bacteria Animator not found!");
            }
        }

        if (m_BacteriaAudioCtrler == null)
        {
            this.transform.parent.GetComponentInChildren<AudioController>();
            if (m_BacteriaAudioCtrler == null)
            {
                // If there is no AudioController in this gObject
                Debug.LogError("Bacteria AudioController not found!");

            }
        }

        // We get the codes for the animations
        HashIDs();

        // We set all the animations Off
        SetAllAnimationsOff();
	}
	

    /// <summary>
    /// Gets the ids from all the parameters of the animator
    /// </summary>
    private void HashIDs()
    {
        m_ReproducingBoolID = Animator.StringToHash("Reproducing");
        m_InteractTriggerID = Animator.StringToHash("Interact");
        m_DyingBoolID = Animator.StringToHash("Dying");
    }

    /// <summary>
    /// Deactivates any animation to set the idle one
    /// </summary>
    public void Idle()
    {
        SetAllAnimationsOff();
    }


    /// <summary>
    /// Sets the animation Reproduce to the value
    /// </summary>
    /// <param name="value"></param>
    public void Reproduce (bool value)
    {
        // We only execute the code if the value is not the same
        if (Anim.GetBool(m_ReproducingBoolID) != value)
        {
            SetAllAnimationsOff();
            Anim.SetBool(m_ReproducingBoolID, value);
            m_BacteriaAudioCtrler.PlayBacteriaReprod(value);
        }
    }

    /// <summary>
    /// Sets the animation Die to the value
    /// </summary>
    /// <param name="value"></param>
    public void Die (bool value)
    {
        // We only execute the code if the value is not the same
        if (Anim.GetBool(m_DyingBoolID) != value)
        {
            SetAllAnimationsOff();
            Anim.SetBool(m_DyingBoolID, value);
            m_BacteriaAudioCtrler.PlayBacteriaDeath();
            // TO DO correct sound
        }

    }

    /// <summary>
    /// Activates the interaction animation trigger
    /// </summary>
    public void Interact()
    {
        // Because it is a trigger, it has a bool that is always true (ReturningAll) to come back to idle, so it only happens once
        this.Anim.SetTrigger(m_InteractTriggerID);
        
    }

    /// <summary>
    /// Sets all animations off
    /// </summary>
    public void SetAllAnimationsOff()
    {
        // We only execute this if it is not being called already
        if (!SetAllAnimOffCalled)
        {
            // We protect the function with a flag
            SetAllAnimOffCalled = true;
            Reproduce(false);
            Die(false);
            // We unprotect the function
            SetAllAnimOffCalled = false;
        }
    }


}
