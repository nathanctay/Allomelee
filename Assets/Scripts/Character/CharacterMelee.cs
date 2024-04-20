using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMelee : MonoBehaviour
{
    public int playerNumber = 1;
    public Collider2D punch;
    public Transform PunchTransform;

    private string PunchButton;
    public float punchRate;
    private float timer;

    private bool punched;
    // GameManager.PlayerControllerState playerControllerState = new PlayerControllerState;
    // Start is called before the first frame update

    void Start()
    {
        PunchButton = "Punch" + playerNumber;
        punchRate = 0.5f;
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < punchRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown(PunchButton))
            {
                Punch();
                timer = 0;
            }
        }

    }

    private void Punch()
    {
        Collider2D punchInstance = Instantiate(punch, PunchTransform.position, PunchTransform.rotation) as Collider2D;

    }
}
