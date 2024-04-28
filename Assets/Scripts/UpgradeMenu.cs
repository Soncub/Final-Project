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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void BringMenuUp()
    {
        Cursor.lockState = CursorLockMode.None;
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void UpgradeProtect()
    {
        Audio.Instance.PlaySound(triggerclip);
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.protectBuff -= 0.1f;
        Debug.Log("Worked");
    }
    public void UpgradeRegen()
    {
        Audio.Instance.PlaySound(triggerclip);
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.pointIncreasePerSecond += 0.1f;
        Debug.Log("Worked");
    }
    public void UpgradeSaber()
    {
        Audio.Instance.PlaySound(triggerclip);
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        player.saberBuff += 0.1f;
        Debug.Log("Worked");
    }
}
