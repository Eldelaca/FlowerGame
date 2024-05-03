using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // References
    private ThirdPersonMovement moveScript;
    public Slider staminaBar;
    
    // For Dash
    public float dashStaminaCost, dashSpeed, dashTime;
    


    // Stamina Value
    private float maxStamina = 100f;
    private float currentStamina;
    
    
    // For regen
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;
    

    private void Awake()
    {
        // start on awake
        instance = this;
    }

    void Start()
    {
        
        moveScript = GetComponent<ThirdPersonMovement>();
        
        currentStamina = maxStamina; // declares what the new current health is
        staminaBar.maxValue = maxStamina;   // reference to the slider declaring it as health
        staminaBar.value = maxStamina;   // this declares what the value of the current health
    }

    void Update()

    {
        // Dash Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    public void UseStamina(float amount)
    {

        
        // stamina regen
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if(regen != null)
                StopCoroutine(regen);


            regen = StartCoroutine(RegenStamina());
            
            
        }
        

    }

    // Dashing code
    IEnumerator Dash()
    {
        if (currentStamina - dashStaminaCost >= 0) // Determine if value is above 0 player can dash
        {
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

                yield return null;
            }

            // Deduct stamina after dashing
            UseStamina(dashStaminaCost);
        }
        else
        {
            Debug.Log("Not enough stamina to dash!");
        }
    }


    private IEnumerator RegenStamina()
    {
        // Stamina regen Timer
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100f;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }

}
