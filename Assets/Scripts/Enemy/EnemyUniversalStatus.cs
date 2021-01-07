using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUniversalStatus : MonoBehaviour
{
    [Header("Enemy的基础属性")]
    public float health;
    public float maxHealth;
    public float damge;
    public float critcalRate;
    public float attackCoolDown;
    public List<GameObject> lotDrop;

    [Header("Enemy 的UI相关")]
    public GameObject healthBar;
    public Slider healthBarSlider;

    private void Start()
    {
        healthBar.SetActive(false);
        health = maxHealth;
    }

    /*--- Tigger 相关functions ---*/

    /// <summary>
    /// 受到攻击，给Enmey扣血，检查是否死亡，更新UI
    /// </summary>
    /// <param name="damage">受到伤害的大小</param>
    public void HandleDamage(float damage)
    {
        /* 受到攻击显示healthBar */
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        UpdateEnmeyInfo();
    }

    /// <summary>
    /// 给Enemy回血，检查是否击穿上限，更新UI
    /// </summary>
    /// <param name="healValue">接受治疗的大小</param>
    public void HandleHeal(float healValue)
    {
        health += healValue;
        CheckOverHeal();
        UpdateEnmeyInfo();
    }

    /// <summary>
    /// 在给予的list里给drop item一个随机值
    /// </summary>
    public void LotDropRandom()
    {
        int index = UnityEngine.Random.Range(0, lotDrop.Count);
        Instantiate(lotDrop[index], transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 检查回血是否击穿并且调整
    /// </summary>
    private void CheckOverHeal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// 检查是否死亡，修正击穿，执行随机掉落
    /// </summary>
    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
            LotDropRandom();
        }
    }

    /*--- UI 功能计算相关functions ---*/

    /// <summary>
    /// 更新Enmey 的info
    /// </summary>
    private void UpdateEnmeyInfo()
    {
        healthBarSlider.value = (health / maxHealth);
    }
}