//---------------------------------------------------------------------------
// Carlos Gonzalez Diaz - TFG - Simulador Virtual Carabina M4 - 2016
// Universidad Rey Juan Carlos - ETSII
//---------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

/// <summary>
/// Invokes an event when a Collider enters the Trigger
/// </summary>
[RequireComponent(typeof(Collider))]
[AddComponentMenu("CarlosFramework/OnTriggerEnterInvokeEvent")]
public class OnTriggerEnterInvokeEventController : MonoBehaviour
{

    /// <summary>
    /// (Field) The tag for the objective to compare
    /// </summary>
    [SerializeField, Tooltip("If empty, the trigger will always be activated")]
    private string m_TagToCompare;

    /// <summary>
    /// (Field) Flag to control the call of the trigger once
    /// </summary>
    [SerializeField, Tooltip("If active, will only call the trigger once")]
    private bool m_InvokeOnce;
    /// <summary>
    /// (Field) Was the trigger already called
    /// </summary>
    private bool m_AlreadyInvoked;

    /// <summary>
    /// The event list to invoke 
    /// </summary>
    [SerializeField]
    private UnityEvent m_EventToInvoke;

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        Inititalize();
    }

    /// <summary>
    /// Intializes the component's members to default values
    /// </summary>
    private void Inititalize()
    {
        // The trigger was not invoked yer
        m_AlreadyInvoked = false;
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        // If the string is null or empty we always invoke the event
        if (String.IsNullOrEmpty(m_TagToCompare))
        {
            // If the event was not already invoked...
            if (!m_AlreadyInvoked)
            {
                // If we can only invoke ONCE
                if (m_InvokeOnce)
                {
                    // We block the invoking again
                    m_AlreadyInvoked = true;
                }

                // We invoke the event
                m_EventToInvoke.Invoke();
            }


        }
        // If it has something, we compare the tag of the passed in collider
        else if (other.CompareTag(m_TagToCompare))
        {
            // If the event was not already invoked...
            if (!m_AlreadyInvoked)
            {
                // If we can only invoke ONCE
                if (m_InvokeOnce)
                {
                    // We block the invoking again
                    m_AlreadyInvoked = true;
                }

                // We invoke the event
                m_EventToInvoke.Invoke();
            }
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    public void OnCollisionEnter(Collision collision)
    {
        // If the string is null or empty we always invoke the event
        if (String.IsNullOrEmpty(m_TagToCompare))
        {
            // If the event was not already invoked...
            if (!m_AlreadyInvoked)
            {
                // If we can only invoke ONCE
                if (m_InvokeOnce)
                {
                    // We block the invoking again
                    m_AlreadyInvoked = true;
                }

                // We invoke the event
                m_EventToInvoke.Invoke();
            }


        }
        // If it has something, we compare the tag of the passed in collider
        else if (collision.transform.CompareTag(m_TagToCompare))
        {
            // If the event was not already invoked...
            if (!m_AlreadyInvoked)
            {
                // If we can only invoke ONCE
                if (m_InvokeOnce)
                {
                    // We block the invoking again
                    m_AlreadyInvoked = true;
                }

                // We invoke the event
                m_EventToInvoke.Invoke();
            }
        }
    }
}
