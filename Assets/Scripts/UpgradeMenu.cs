using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;
    [SerializeField] Button protectButton;
    [SerializeField] Button regenButton;
    [SerializeField] Button saberButton;
    public PlayerController playerController;
    public GameObject player;
    public float buffProtect;
    public float buffRegen;
    public float buffSaber;
    // Start is called before the first frame update
    void Start()
    {
        buffProtect = GameObject.Find("Player").GetComponent<PlayerController>().protectBuff;
        buffRegen = GameObject.Find("Player").GetComponent<PlayerController>().pointIncreasePerSecond;
        buffSaber = GameObject.Find("Player").GetComponent<PlayerController>().saberBuff;
    }
    void BringMenuUp()
    {
        Cursor.lockState = CursorLockMode.None;
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void UpgradeProtect()
    {
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        buffProtect -= 0.05f;
        Debug.Log("Worked");
    }
    public void UpgradeRegen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        buffRegen += 0.5f;
        Debug.Log("Worked");
    }
    public void UpgradeSaber()
    {
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        buffSaber += 0.1f;
        Debug.Log("Worked");
    }
}
