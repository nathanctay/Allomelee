using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, .5f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Rigidbody2D target = other.gameObject.GetComponent<Rigidbody2D>();
            CharacterHealth targetHealth = target.GetComponent<CharacterHealth>();
            // CharacterFlakes flakes = attacker.GetComponent<CharacterFlakes>();
            targetHealth.TakeDamage(1); 
            // targetHealth.TakeDamage((float)(1f * System.Math.Pow(1.3f, flakes.currentFlakes)))
        }
        Destroy (gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
