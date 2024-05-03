using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class triggerEvents : MonoBehaviour
{
    public UnityEvent enteredTrigger, exitedTrigger, stayInTrigger; // Create unity events for trigger actions

    // On Trigger enter
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // When something enters this tag called player
            // we take the position when the objects collides and trigger the event
            enteredTrigger.Invoke();
        }
    }

    // On Trigger stay
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Player")
        {
            stayInTrigger.Invoke();
        }
    }

    // On Trigger exit
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player")
        {
            // When something enters this tag called player
            // we take the position when the objects collides and trigger the event
            exitedTrigger.Invoke();
        }
    }    
}
