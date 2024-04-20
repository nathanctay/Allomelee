using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public PlayerManager playerManager;
    public float startingHealth;
    private float currentHealth;
    private bool dead;
    private void OnEnable() {
        currentHealth = startingHealth;
        dead = false;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0 && !dead) {
            OnDeath();
        }
    }

    public void OnDeath() {
        dead = true;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
