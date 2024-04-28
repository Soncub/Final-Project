using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsMenuScript : MonoBehaviour
{
    public static int WeaponType;
    public static int SpellType;
    public AudioClip triggerclip;

    [SerializeField] Button _BackMenu;
    [SerializeField] Button _FightButton;
    [SerializeField] Button _WeaponButton1;
    [SerializeField] Button _WeaponButton2;
    [SerializeField] Button _WeaponButton3;
    [SerializeField] Button _SpellButton1;
    [SerializeField] Button _SpellButton2;
    [SerializeField] Button _SpellButton3;

    // Start is called before the first frame update
    void Start()
    {
        _BackMenu.onClick.AddListener(GoBack);
        _FightButton.onClick.AddListener(BattleSwitch);
        _FightButton.interactable = false;
        _SpellButton1.interactable = false;
        _SpellButton2.interactable = false;
        _SpellButton3.interactable = false;
        WeaponType = 0;
        SpellType = 0;
    }

    // Update is called once per frame
    private void GoBack()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.StartMenu);
        Audio.Instance.PlaySound(triggerclip);
    }
    private void BattleSwitch()
    {
        ScenesManager.Instance.LoadBattleScreen();
        Audio.Instance.PlaySound(triggerclip);
    }
    public void SpellChange()
    {
        _FightButton.interactable = true;
        Debug.Log("Spell Type:" + SpellType);
    }
    public void WeaponChange()
    {
        _SpellButton1.interactable = true;
        _SpellButton2.interactable = true;
        _SpellButton3.interactable = true;
        Debug.Log("Weapon Type:" + WeaponType);
    }
    public void TypeSwitch1()
    {
        WeaponType = 1;
        Audio.Instance.PlaySound(triggerclip);
    }
    public void TypeSwitch2()
    {
        WeaponType = 2;
        Audio.Instance.PlaySound(triggerclip);
    }
    public void TypeSwitch3()
    {
        WeaponType = 3;
        Audio.Instance.PlaySound(triggerclip);
    }
    public void SpellSwitch1()
    {
        SpellType = 1;
        Audio.Instance.PlaySound(triggerclip);
    }
    public void SpellSwitch2()
    {
        SpellType = 2;
        Audio.Instance.PlaySound(triggerclip);
    }
    public void SpellSwitch3()
    {
        SpellType = 3;
        Audio.Instance.PlaySound(triggerclip);
    }
}