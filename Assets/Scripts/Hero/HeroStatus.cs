using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStatus : MonoBehaviour
{
    public static HeroStatus heroStatus;
    public GameObject hero;
    private Animator animator;
    public GameObject deathPanel;
    public GameObject attributePanel;
    [Header("Hero基本属性")]
    public float health;

    public float maxHealth;
    public float mp;
    public float maxMp;
    public float level;
    public float exp;

    [Header("Hero物品属性")]
    public int coints;

    public int skillPoints;

    [Header("[UI]Hero属性相关")]
    public Text healthNum;

    public Slider healthSlider;
    public Text mpNum;
    public Slider mpSlider;
    public Text levelNUm;
    public Text expNum;

    [Header("[UI]Hero物品相关")]
    public Text coinNum;

    /// <summary>
    /// 检查是否存在PlayerStatus
    /// </summary>
    private void Awake()
    {
        if (heroStatus != null)
        {
            Destroy(heroStatus);
        }
        else
        {
            heroStatus = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        /* 初始化的变量*/
        animator = hero.GetComponent<Animator>();
        health = maxHealth; //TEST设定为满血
        mp = maxMp;         //TEST设定为满mp
        SetHealthUI();
        mpSlider.value = 1;
        UpdateHeroInfo();
    }

    /*--- Tigger 使hero status 产生变化相关functions ---*/

    /// <summary>
    /// 受到攻击，给HERO扣血，检查是否死亡，更新UI
    /// </summary>
    /// <param name="damage">受到伤害的大小</param>
    public void HandleDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
        UpdateHeroInfo();
    }

    /// <summary>
    /// 给HERO回血，检查是否击穿上限，更新UI
    /// </summary>
    /// <param name="healValue">接受治疗的大小</param>
    public void HandleHeal(float healValue)
    {
        health += healValue;
        CheckOverHeal();
        SetHealthUI();
        UpdateHeroInfo();
    }

    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthNum.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();
    }
    public void HandleDrop()
    {
    }

    /// <summary>
    /// 检查是否击穿治疗上限并且修正
    /// </summary>
    private void CheckOverHeal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// 检查Hero是否挂了并且防止击穿
    /// </summary>
    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(hero);
            deathPanel.SetActive(true);
            attributePanel.SetActive(false);
}
    }

    /*--- UI 功能计算相关functions ---*/

    /// <summary>
    /// 更新所有出现在INFO界面，与HERO相关的信息
    /// </summary>
    private void UpdateHeroInfo()
    {
        /* health 相关*/
        healthSlider.value = (health / maxHealth);
        healthNum.text = "HP:   " + Mathf.Ceil(health).ToString() + " / "
                                 + Mathf.Ceil(maxHealth).ToString();
        /* mp 相关*/
        mpSlider.value = (mp / maxMp);
        mpNum.text = "MP：  " + Mathf.Ceil(mp).ToString() + " / "
                               + Mathf.Ceil(maxMp).ToString();
        /* 等级经验相关*/
    }

    float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}