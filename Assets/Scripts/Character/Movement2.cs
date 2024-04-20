// ﻿using UnityEngine;
// using System.Runtime.InteropServices;

// public class TankMovement : MonoBehaviour
// {
//     public int m_PlayerNumber = 1;         
//     public float m_Speed = 3f;            
//     public AudioSource m_MovementAudio;    
//     public AudioClip m_EngineIdling;       
//     public AudioClip m_EngineDriving;      
//     public float m_PitchRange = 0.2f;
//     public float m_MovementInputValue = 0f;
//     public float m_AngleInputValue = 0f;

//     public float m_MaxBoostDuration = 1.5f;
//     public float m_MinBoostDuration = 0.25f;
//     public float m_MaxBoostSpeed = 15f;
//     public float m_MinBoostSpeed = 6f;
//     public bool m_BoostReleased = true;
//     private bool m_IsBoosting = false;
//     private float m_PowerPercentage = 0f;
//     private float m_BoostEndTime = 0f;

    
//     private string m_MovementAxisName;     
//     private string m_TurnAxisName;         
//     private Rigidbody m_Rigidbody;         
//     private float m_OriginalPitch;   
//     [DllImport("__Internal")]
//     private static extern void MessageToPlayer(string userName, string message);      


//     private void Awake()
//     {
//         m_Rigidbody = GetComponent<Rigidbody>();
//     }


//     private void OnEnable ()
//     {
//         m_Rigidbody.isKinematic = false;
//         m_MovementInputValue = 0f;
//     }


//     private void OnDisable ()
//     {
//         m_Rigidbody.isKinematic = true;
//     }


//     private void Start()
//     {
//         m_MovementAxisName = "Vertical" + m_PlayerNumber;
//         m_TurnAxisName = "Horizontal" + m_PlayerNumber;

//         m_OriginalPitch = m_MovementAudio.pitch;
//     }
    

//     private void Update()
//     {
//         EngineAudio();
//     }


//     public float bounceForce = 1f;

//     private void OnCollisionEnter(Collision collision)
//     {
//         // Calculate the reflection direction
//         Vector3 reflectionDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

//         // Apply a force in the reflection direction
//         GetComponent<Rigidbody>().AddForce(reflectionDirection * bounceForce, ForceMode.Impulse);
//     }


//     private void EngineAudio()
//     {
//         if (Mathf.Abs(m_MovementInputValue) < 0.1f)
//         {
//             if (m_MovementAudio.clip == m_EngineDriving)
//             {
//                 m_MovementAudio.clip = m_EngineIdling;
//                 m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
//                 m_MovementAudio.Play();
//             }
//         }
//         else
//         {
//             if (m_MovementAudio.clip == m_EngineIdling)
//             {
//                 m_MovementAudio.clip = m_EngineDriving;
//                 m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
//                 m_MovementAudio.Play();
//             }
//         }
//     }


//     private void FixedUpdate()
//     {
//         if (m_IsBoosting && Time.time < m_BoostEndTime)
//         {
//             float boostSpeed = m_MinBoostSpeed + (m_MaxBoostSpeed - m_MinBoostSpeed) * m_PowerPercentage;
//             Move(boostSpeed, 1f);
//         }
//         else if (m_IsBoosting && Time.time >= m_BoostEndTime)
//         {
//             // End the boost
//             m_IsBoosting = false;
//             Move(m_Speed, m_MovementInputValue);
//         }
//         else
//         {
//             // Normal movement
//             Move(m_Speed, m_MovementInputValue);
//         }
//         Turn();
//     }


//     private void Move(float speed, float inputValue)
//     {
//         Vector3 movement = transform.forward * inputValue * speed * Time.deltaTime;

//         m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
//     }


//     private void Turn()
//     {
//         m_Rigidbody.rotation = Quaternion.Euler(0f, m_AngleInputValue + 45f, 0f);
//     }

//     public void Boost(string name, float boostPower)
//     {
//         if (!m_IsBoosting)
//         {
//             #if UNITY_WEBGL == true && UNITY_EDITOR == false
//             Message message = new Message();
//             message.name = "boost-registered";
//             string json = JsonUtility.ToJson(message);
//             MessageToPlayer(name, json);
//             #endif
//             m_PowerPercentage = boostPower / 100f;
//             m_IsBoosting = true;
//             m_BoostEndTime = Time.time + m_PowerPercentage * m_MaxBoostDuration;
//         }
//     }
// }