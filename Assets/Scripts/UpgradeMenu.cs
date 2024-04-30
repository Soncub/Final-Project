using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public AudioClip triggerclip;
    public GameObject upgradeMenu;
    [SerializeField] Button protectButton;
    [SerializeField] Button regenButton;
    [SerializeField] Button saberButton;
    public PlayerController player;
    public static bool isMenuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
    public void BringMenuUp()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        isMenuActive = true;
    }
    public void UpgradeProtect()
    {
        Audio.Instance.PlaySound(triggerclip);
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.protectBuff -= 0.1f;
        Debug.Log("Worked");
        isMenuActive = false;
    }
    public void UpgradeRegen()
    {
        Audio.Instance.PlaySound(triggerclip);
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.pointIncreasePerSecond += 0.1f;
        Debug.Log("Worked");
        isMenuActive = false;
    }
    public void UpgradeSaber()
    {
        Audio.Instance.PlaySound(triggerclip);
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.saberBuff += 0.1f;
        Debug.Log("Worked");
        isMenuActive = false;
        if (WeaponsMenuScript.WeaponType == 1)
            {
               player.attackArea.GetComponent<Sword>().damage = player.attackArea.GetComponent<Sword>().damage * player.saberBuff;
            }

        if (WeaponsMenuScript.WeaponType == 2)
            {
                player.fireball.GetComponent<Fireball>().damage = player.fireball.GetComponent<Fireball>().damage * player.saberBuff;
            }

        if (WeaponsMenuScript.WeaponType == 3)
            {
                player.attackArea.GetComponent<Bell>().damage = player.attackArea.GetComponent<Bell>().damage * player.saberBuff;
            }
    }
}
