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

public delegate void EventHandler();
public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public int currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    bool isFacingLeft = true;
    Animator animator;

    public GameObject attackArea;
    public GameObject effect;
    bool attacking = false;
    float timeToAttack = 0.25f;
    float timer = 0f;

    [SerializeField] GameObject gameOver;
    public static bool GameOvered = false;

    //the following variables are only for Lab Assignment 12.
    private string _dataPath;

    public delegate void DeathEventHandler();
    public event DeathEventHandler OnPlayerDeath;

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
        new TestWeapon("Sword", 10),
        new TestWeapon("Bow", 7),
        new TestWeapon("Bell", 8)
    };

    //for Lab Assignment 12. Can probably remove or repurpose later
    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);

        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists.");
        }

        else
        {
            Directory.CreateDirectory(_dataPath);
            Debug.Log("New Directory Created!");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        Time.timeScale = 1;

        attackArea = transform.Find("BellAttack").gameObject;
        effect = transform.Find("Effect").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                effect.SetActive(attacking);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (currentHealth <= 0)
        {
            GameOver();
        }
        if (GameOvered && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        GameOvered = true;
        OnPlayerDeath?.Invoke();
    }

    void Restart()
    {
        Time.timeScale = 1f;
        gameOver.SetActive(false);
        GameOvered = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScreen");
    }

    void Save()
    {
        Vector3 playerPosition = transform.position;
        int playerHealth = currentHealth;

        SaveData saveData = new SaveData
        {
            health = playerHealth,
            playerPosition = playerPosition,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_dataPath + "save.txt", json);
    }

    void Load()
    {
        try
        {
            string saveString = File.ReadAllText(_dataPath + "save.txt");

            SaveData load = JsonUtility.FromJson<SaveData>(saveString);
            
            transform.position = load.playerPosition;
            currentHealth = load.health;
            
            UIDisplay.instance.health = currentHealth;
            UIDisplay.instance.UpdateHealth(0);
        }

        catch (FileNotFoundException)
        {
            Debug.Log("Save file not found.");
        }
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (isFacingLeft && horizontal > 0)
        {
            FlipSprite();
        }
        else if (!isFacingLeft && horizontal < 0)
        {
            FlipSprite();
        }

    }

    public void FlipSprite()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        effect.SetActive(attacking);
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
            UIDisplay.instance.UpdateHealth(amount);
            //UI updating Points
            UIDisplay.instance.AddPoint();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}

public class SaveData
{
    public int health;
    public Vector3 playerPosition;
}