using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerManager : MonoBehaviour
{
    public GameObject character;
    public Transform spawnPoint;
    public GameObject canvas;
    public String playerName;
    public int playerNumber;
    public TextMeshProUGUI playerNameText;
    public GameObject instance;

    public Movement movement;
    public CharacterMelee melee;
    private CharacterHealth health;
    private GameObject healthBar;

    public void Setup() {
        movement = instance.GetComponent<Movement>();
        melee = instance.GetComponent<CharacterMelee>();
        canvas = instance.GetComponentInChildren<Canvas>().gameObject;
        health = instance.GetComponent<CharacterHealth>();
        health.playerManager = this;
        playerNameText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        healthBar = canvas.GetComponentInChildren<Slider>().gameObject;

        playerNameText.text = playerName;

        movement.playerNumber = playerNumber;
        melee.playerNumber = playerNumber;
    }

    public void Move(string controllerStateString) {
        string[] controllerStateStrings = controllerStateString.Split(',');
        int[] controllerState = new int[2];
        for (int i = 0; i < 2; i++) {
            controllerState[i] = int.Parse(controllerStateStrings[i]);
        }
    }

    public void DisableControl() {
        movement.enabled = false;
        melee.enabled = false;
        healthBar.SetActive(false);
    }
    public void EnableControl() {
        movement.enabled = true;
        melee.enabled = true;
        healthBar.SetActive(true);
    }
    public void EnableMovement() {
        movement.enabled = true;
        melee.enabled = false;
        healthBar.SetActive(false);
    }

    public void PauseControl() {
        movement.enabled = false;
        melee.enabled = false;
    }

    public void ResumeControl() {
        movement.enabled = true;
        melee.enabled = true;
    }

    public void Reset() {
        instance.transform.position = spawnPoint.position;
        instance.SetActive(false);
        instance.SetActive(true);
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
