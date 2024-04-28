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
using UnityEditor.Experimental.GraphView;

public delegate void EventHandler();
public class PlayerController : MonoBehaviour
{
    //default player stats
    public float speed = 3.0f;
    public float maxHealth = 5.0f;
    public float currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public GameObject player;

    //variables for Animator
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    bool isFacingLeft = true;
    Animator animator;

    //variables for weapon attack generation
    public GameObject attackArea;
    public GameObject effect;
    bool attacking = false;
    public float timeToAttack = 0.25f;
    float timer = 0f;
    public GameObject fireball;
    public Transform firePoint;
    public float bulletSpeed = 50;
    Vector2 lookDirection;
    float lookAngle;

    //variables for inital spell stat changes
    public float protectBuff = 1.0f;
    public float pointIncreasePerSecond = 0.0f;
    public float saberBuff = 1.0f;

    //Game Over event
    [SerializeField] GameObject gameOver;
    public static bool GameOvered = false;
    public delegate void DeathEventHandler();
    public event DeathEventHandler OnPlayerDeath;

    //Sound Effects
    public AudioClip magic, sword, bell, hit, gameover;


    //directory file path
    private string _dataPath;

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

        if (WeaponsMenuScript.WeaponType == 1)
        {
            attackArea = transform.Find("SwordAttack").gameObject;
            effect = transform.Find("SwordEffect").gameObject;
        }

        else if (WeaponsMenuScript.WeaponType == 3)
        {
            attackArea = transform.Find("BellAttack").gameObject;
            effect = transform.Find("BellEffect").gameObject;
        }

        else 
        {
            attackArea = transform.Find("SwordAttack").gameObject;
            effect = transform.Find("SwordEffect").gameObject;
        }

        UseSpell();
        
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

        if (WeaponsMenuScript.WeaponType == 2)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0,0, lookAngle);
        }

        if (WeaponsMenuScript.WeaponType == 1 || WeaponsMenuScript.WeaponType == 3)
        {
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
        }

        if (WeaponsMenuScript.SpellType == 2)
        {
            pointIncreasePerSecond = 0.5f;
            currentHealth += pointIncreasePerSecond * Time.deltaTime;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;

                UIDisplay.instance.health = currentHealth;
                UIDisplay.instance.UpdateHealth(0);
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
        Audio.Instance.PlaySound(gameover);
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
        float playerHealth = currentHealth;

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
        if (WeaponsMenuScript.WeaponType == 1)
        {
            attacking = true;
            attackArea.SetActive(attacking);
            effect.SetActive(attacking);
            Audio.Instance.PlaySound(sword);
        }

        else if (WeaponsMenuScript.WeaponType == 2)
        {
            GameObject bulletClone = Instantiate(fireball);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
            Audio.Instance.PlaySound(magic);
        }
        else if (WeaponsMenuScript.WeaponType == 3)
        {
            attacking = true;
            attackArea.SetActive(attacking);
            effect.SetActive(attacking);
            Audio.Instance.PlaySound(bell);
        }
    }

    void UseSpell()
    {
        if (WeaponsMenuScript.SpellType == 1)
        {
            protectBuff = 0.8f;
        }

        else if (WeaponsMenuScript.SpellType == 3)
        {
            saberBuff = 1.1f;

            if (WeaponsMenuScript.WeaponType == 1)
            {
               attackArea.GetComponent<Sword>().damage = attackArea.GetComponent<Sword>().damage * saberBuff;
            }

            else if (WeaponsMenuScript.WeaponType == 2)
            {
                fireball.GetComponent<Fireball>().damage = fireball.GetComponent<Fireball>().damage * saberBuff;
            }

            else 
            {
                attackArea.GetComponent<Bell>().damage = attackArea.GetComponent<Bell>().damage * saberBuff;
            }
        }

        else 
        {
            Debug.Log("Regen");
        }
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            //UI updating Health
            UIDisplay.instance.UpdateHealth(amount * protectBuff);
            //UI updating Points
            UIDisplay.instance.AddPoint();

        }
        Audio.Instance.PlaySound(hit);
        currentHealth = Mathf.Clamp(currentHealth + (amount * protectBuff), 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void Regen()
    {
        this.ChangeHealth(1.0f);
    }
}

public class SaveData
{
    public float health;
    public Vector3 playerPosition;
}