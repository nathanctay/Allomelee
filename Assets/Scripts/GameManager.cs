using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    // public GameObject nameObject;

    // public Canvas code;
    public TextMeshProUGUI codeText;
    // public TextMeshProUGUI startText;
    // public TextMeshProUGUI titleText;


    private PlayerJoin[] playerJoins = new PlayerJoin[0];
    private int playerCount = 0;
    private int maxPlayers = 4;
    public PlayerManager[] players;
    public GameObject player1Prefab;
    public GameObject player2Frefab;
    public GameObject player3Frefab;
    public GameObject player4Frefab;
    public GameObject[] spawnPoints;
    public GameObject background;
    public Sprite[] backgrounds;
    public float startDelay = 3;
    private WaitForSeconds startWait;

    

    // Madder functions that you may call
    // These functions should be conditionally called based on if this is inside a WebGL build, not the editor
    [DllImport("__Internal")]
    private static extern void MessageToPlayer(string userName, string message);
    [DllImport("__Internal")]
    private static extern void MessageToAllPlayers(string message);
    [DllImport("__Internal")]
    private static extern void Exit();
    [DllImport("__Internal")]
    private static extern void UpdateStats(string userName, string stats);

    void Start()
    {
        System.Random rand = new System.Random();
        startWait = new WaitForSeconds(startDelay);
        Sprite newBackground = backgrounds[rand.Next(0, backgrounds.Length)];
        SpriteRenderer backdrop = background.GetComponent<SpriteRenderer>();
        backdrop.sprite = newBackground;
        // codeText = code.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        // // Testing Madder functions
        // // TODO: This code should be commented out or removed before submission

        // Test RoomCode
        // TODO: Any of the following code may be modified or deleted
        if (Input.GetKeyDown(KeyCode.R))
        {
            RoomCode("ABCD");
        }

        // Test PlayerJoined
        // TODO: Any of the following code may be modified or deleted
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerJoin playerJoin = new PlayerJoin();
            playerJoin.name = "Player " + playerJoins.Length;
            playerJoin.stats = new GameStats();
            string jsonPlayerJoin = JsonUtility.ToJson(playerJoin);
            PlayerJoined(jsonPlayerJoin);
        }

        // Test PlayerLeft
        // TODO: Any of the following code may be modified or deleted
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (playerJoins.Length == 0)
            {
                return;
            }
            PlayerLeft("Player 0");
        }

        // // Test PlayerControllerState for Player 0
        // // TODO: Any of the following code may be modified or deleted
        if (playerJoins.Length > 0)
        {
            Joystick joystick = new Joystick(0, 0);
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                joystick.y = 100;
            }
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                joystick.y = -100;
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                joystick.x = -100;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                joystick.x = 100;
            }
            ControllerState controllerState = new ControllerState();
            controllerState.name = playerJoins[0].name;
            controllerState.joystick = joystick;
            controllerState.circle = false;
            controllerState.triangle = false;
            controllerState.plus = false;
            string jsonControllerState = JsonUtility.ToJson(controllerState);
            PlayerControllerState(jsonControllerState);
        }

        // // Test HandleExit
        // // TODO: Any of the following code may be modified or deleted
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     HandleExit();
        // }
    }

    // TODO: The following function may be modified or deleted
    void HandleExit()
    {
        // Remove all player names from canvas
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }

        // Reset playerJoins array
        playerJoins = new PlayerJoin[0];
    }

    /*
    * Madder function: RoomCode
    * This function is called when the uniquely generated code is received from the server
    * You will typically use this code to display the room code on the screen
    */
    public void RoomCode(string roomCode) 
    {
        // TODO: Any of the following code may be modified or deleted
        Debug.Log("Room Code: " + roomCode);
        codeText.text = roomCode;
    }

    /*
    * Madder function: PlayerJoined
    * This function is called when a new player joins the game
    * You will typically use this function to create a character instance for this player
    *   and keep track of the player's stats
    */

    public void PlayerJoined(string jsonPlayerJoin)
    {
        // Destructure jsonPlayerJoin
        PlayerJoin playerJoin = JsonUtility.FromJson<PlayerJoin>(jsonPlayerJoin);

        // TODO: Any of the following code may be modified or deleted

        // Initialize player stats if they are null or have missing fields
        if (playerJoin.stats == null)
        {
            playerJoin.stats = new GameStats();
        }
        if (playerJoin.stats.gamesPlayed == null)
        {
            playerJoin.stats.gamesPlayed = new Stat("Games Played", 0);
        }
        if (playerJoin.stats.gamesWon == null) {
            playerJoin.stats.gamesWon = new Stat("Games Won", 0);
        }

        // Create player name on canvas
        // GameObject name = Instantiate(nameObject, canvas.transform);
        // name.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        // name.GetComponent<NameScript>().SetName(playerJoin.name);
        
        // Add player to playerJoins array
        PlayerJoin[] newPlayerJoins = new PlayerJoin[playerJoins.Length + 1];
        for (int i = 0; i < playerJoins.Length; i++)
        {
            newPlayerJoins[i] = playerJoins[i];
        }
        newPlayerJoins[playerJoins.Length] = playerJoin;
        playerJoins = newPlayerJoins;
        playerCount ++;

        // Add game played to player stats
        playerJoin.stats.addGamePlayed();
        // Update player stats on server
        #if UNITY_WEBGL && !UNITY_EDITOR // Only call this function if this is a WebGL build
        string jsonStats = JsonUtility.ToJson(playerJoin.stats);
        UpdateStats(playerJoin.name, jsonStats);
        #endif
    }

    private void AddPlayer(PlayerJoin playerJoin) {
        PlayerManager newPlayer = new PlayerManager
        {
            character = player1Prefab,
            spawnPoint = spawnPoints[players.Length].transform
        };
        newPlayer.instance = Instantiate(newPlayer.character, newPlayer.spawnPoint.position, newPlayer.spawnPoint.rotation ) as GameObject;
        newPlayer.Setup();
        PlayerManager[] newPlayers = new PlayerManager[players.Length - 1];
        for (int i = 0; i < players.Length; i ++) {
            newPlayers[i] = players[i];
        }
        newPlayers[newPlayers.Length - 1] = newPlayer;
        players = newPlayers;
    }

    /*
    * Madder function: PlayerLeft
    * This function is called when a player leaves the game
    * You will typically use this function to remove the character instance of this player
    */
    public void PlayerLeft(string playerName)
    {
        // TODO: Any of the following code may be modified or deleted

        // Remove player from playerJoins array
        PlayerJoin[] newPlayerJoins = new PlayerJoin[playerJoins.Length - 1];
        int j = 0;
        for (int i = 0; i < playerJoins.Length; i++)
        {
            if (playerJoins[i].name != playerName)
            {
                newPlayerJoins[j] = playerJoins[i];
                j++;
            }
            else
            {
                // Exit game if first player (host) leaves
                #if UNITY_WEBGL && !UNITY_EDITOR // Only call this function if this is a WebGL build
                if (i == 0)
                {
                    Exit();
                }
                #endif
                HandleExit();
                return;
            }
        }
        playerJoins = newPlayerJoins;

        // Remove player name from canvas
        foreach (Transform child in canvas.transform)
        {
            if (child.GetComponent<NameScript>().GetName() == playerName)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    /*
    * Madder function: PlayerControllerState
    * This function is called when the controller state of a player is updated
    * You will typically use this function to move the character instance of this player
    *   or perform any other action based on button activity
    */
    public void PlayerControllerState(string jsonControllerState)
    {
        // Destructure jsonControllerState
        ControllerState controllerState = JsonUtility.FromJson<ControllerState>(jsonControllerState);
        // TODO: Any of the following code may be modified or deleted

        // Move player based on joystick
        // foreach (Transform child in canvas.transform)
        // {
        //     if (child.GetComponent<NameScript>().GetName() == controllerState.name)
        //     {
        //         child.GetComponent<NameScript>().UpdateXY(controllerState.joystick.x, controllerState.joystick.y);
        //     }
        // }
    }

    /*
    * Madder class: Message
    * This class is used to serialize messages sent to controllers (both individual and all controllers)
    * The following Message names work with Madder controllers:
    *   - "vibrate": Vibrate the player's controller
    * The message parameter has no current use for Madder controllers
    */
    public class Message {
        public string name;
        public string message;
    }

    /*
    * Madder class: Stat
    * This class is used to store and update a stat of a player across sessions
    * The title is the name of the stat and is REQUIRED
    * The value is the value of the stat and is REQUIRED
    * You may create children classes of Stat to store more complex stats
    */
    [System.Serializable]
    public class Stat {
        public string title;
        public int value;

        public Stat(string initTitle, int initValue) {
            title = initTitle;
            value = initValue;
        }
    }
    // TODO: Add any additional children classes of Stat here

    /*
    * Madder class: GameStats
    * This class is used to store and update the stats of a player for your game across sessions
    * All fields must be of type Stat or a child class of Stat
    * No fields or methods are required and you can add any additional fields or methods
    */
    [System.Serializable]
    public class GameStats {
        // TODO: Add/Remove any fields of type Stat or a child class of Stat here
        public Stat gamesPlayed;
        public Stat gamesWon;
        public Stat greatHouse;
        public GameStats() {
            gamesPlayed = new Stat("Games Played", 0);
            gamesWon = new Stat("Games Won", 0);
            greatHouse = new Stat("Great House", 0);
        }
        public void addGamePlayed() {
            gamesPlayed.value++;
        }
        public void addGameWon() {
            gamesWon.value++;
        }
        public void chooseHouse(int house) {
            greatHouse.value = house;
        }
    }

    /*
    * Madder class: PlayerJoin
    * This class is used to serialize the data sent to the PlayerJoined function
    * This class should not be altered
    */
    public class PlayerJoin {
        public string name;
        public GameStats stats; 
    }

    /*
    * Madder class: Joystick
    * This class is used to serialize the joystick data sent to the PlayerControllerState function
    * This class should not be altered for the Madder controller
    */
    [System.Serializable]
    public class Joystick {
        public int x;
        public int y; 
        public Joystick(int initX, int initY) {
            x = initX;
            y = initY;
        }
    }

    /*
    * Madder class: ControllerState
    * This class is used to serialize the data sent to the PlayerControllerState function
    * This class should not be altered for the Madder controller
    */
    public class ControllerState {
        public string name;
        public Joystick joystick;
        public bool circle;
        public bool triangle;
        public bool plus;
    }
}
