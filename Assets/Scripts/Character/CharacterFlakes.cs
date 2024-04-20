using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlakes : MonoBehaviour
{
    public float currentFlakes;
    private void OnEnable() {
        currentFlakes = 0;
    }

    public void AddFlake(float flake) {
        currentFlakes += flake;
    }


   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (collision.gameObject.CompareTag("Flake"))
        // {
        //     onGround = true;
        // }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Flake")) {
            AddFlake(1);
            Destroy (other.gameObject);
        }
    }
}
