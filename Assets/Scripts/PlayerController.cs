using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;

    [SerializeField] GameObject gameOver;
    public static bool GameOvered = false;

    //the following variables are only for Lab Assignment 12.
    private string _dataPath;
    private string _textFile;
    private string _xmlWeapons;
    private string _jsonWeapons;

    [Serializable]
    public struct TestWeapon
    {
        public string name;
        public int damage;

        public TestWeapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }
    }

    private List<TestWeapon> weapons = new List<TestWeapon>
    {
        new TestWeapon("Fireball", 10),
        new TestWeapon("Lightning Blast", 15),
        new TestWeapon("Basic Sword", 5),
    };

    //for Lab Assignment 12. Can probably remove or repurpose later
    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
        _textFile = _dataPath + "Save_Data.txt";

        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists.");
        }

        else
        {
            Directory.CreateDirectory(_dataPath);
            Debug.Log("New Directory Created!");
        }

        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists.");
        }

        else
        {
            File.WriteAllText(_textFile, "<SAVE DATA>\n");
            Debug.Log("New File Created!");
        }

        _xmlWeapons = _dataPath + "Weapons.xml";

        var xmlSerializer = new XmlSerializer(typeof(List<TestWeapon>));

        using (FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weapons);
        }

        Debug.Log("Created XML File.");

        _jsonWeapons = _dataPath + "Weapons.json";

        string jsonString1 = JsonUtility.ToJson(weapons[0], true);
        string jsonString2 = JsonUtility.ToJson(weapons[1], true);
        string jsonString3 = JsonUtility.ToJson(weapons[2], true);

        using (StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString1);
            stream.WriteLine(jsonString2);
            stream.WriteLine(jsonString3);
        }

        Debug.Log("Created JSON File.");
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            GameOvered = true;
        }
        if (GameOvered && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            gameOver.SetActive(false);
            GameOvered = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneAnimated");
        }
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            //UI updating Health
            UIDisplay.instance.RemoveHealth();
            //UI updating Points
            UIDisplay.instance.AddPoint();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
