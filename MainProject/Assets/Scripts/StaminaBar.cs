using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider staminaBar;

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
        
        currentStamina = maxStamina; // declares what the new current health is
        staminaBar.maxValue = maxStamina;   // reference to the slider declaring it as health
        staminaBar.value = maxStamina;   // this declares what the value of the current health
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
        else
        {
            Debug.Log("Not enough stamina");
        }

    }

    private IEnumerator RegenStamina()
    {
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
