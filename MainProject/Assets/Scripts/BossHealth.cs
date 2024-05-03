using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider bossBar;

    public float bossHealth = 100f;
    public float currentBossHealth;

    public static BossHealth instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentBossHealth = bossHealth;
        bossBar.maxValue = bossHealth;
        bossBar.value = bossHealth;
    }
}
