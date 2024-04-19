using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDamage : MonoBehaviour

{
    [SerializeField] private BossHealth boss;
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lever")) 
        {
            Destroy(collision.gameObject);
            boss.bossBar.value -= 40f;

        }
    }
}