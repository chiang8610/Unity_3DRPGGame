﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("攻擊"), Range(0, 100)]
    public float attack = 20f;
    [Header("血量"), Range(0, 1000)]
    public float hp = 350f;
    [Header("經驗值"), Range(0, 1000)]
    public float exp = 30f;
    [Header("掉落道具的機率"), Range(0f, 1f)]
    public float prop = 0.3f;
    [Header("掉落的道具")]
    public Transform skull;
    [Header("停止距離:攻擊距離"),Range(0, 10)]
    public float rangeAttack = 1.5f;
    [Header("攻擊冷卻時間"), Range(0, 10)]
    public float cd = 3.5f;
    [Header("面向玩家的速度"), Range(0, 100)]
    public float ture = 10;
   

    private NavMeshAgent nma;
    private Animator ani;
    private Player player;
    private Rigidbody rig;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    #endregion

    #region 方法

    /// <summary>
    /// 移動方法:追蹤玩家
    /// </summary>
    private void Move()
    {

        //代理器. 設定目的地(玩家.變形.座標)
        nma.SetDestination(player.transform.position);
        ani.SetFloat("移動", nma.velocity.magnitude);    //nma.velocity.magnitude 加速度.長度


        // 如果 剩下的距離 <= 攻擊範圍 就 攻擊
        if (nma.remainingDistance <= rangeAttack) Attack();


      

    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);            // 取得面向的角度 B 角度 = 四元.面向角度(玩家 - 自己)
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * ture);                     //怪物.角度 = 四元.差值(A 角度，B 角度， 百分比)

        timer += Time.deltaTime;                                           //累加時間

        if(timer >= cd)                                                            //如果 計時器 >= 冷卻時間
        {
            timer = 0;                                                               // 計時器歸零
            ani.SetTrigger("攻擊觸發");                                 // 攻擊動畫
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    public void Hit(float damage, Transform direction)
    {

        hp -= damage;
        rig.AddForce(direction.forward * 100 + direction.up * 500);


        ani.SetTrigger("受傷觸發");

        if (hp <= 0) Dead();                                               //如果 血量 <=0 就 死亡

    }

    /// <summary>
    /// 死亡: 關閉腳本 碰撞動畫 呼叫掉道具  傳經驗值給玩家
    /// </summary>
    private void Dead()
    {
        //this.enabled = false;  //第一種寫法 , this 此腳本
        enabled = false;                                                             //此腳本.啟動= 否
        GetComponent<Collider>().enabled = false;
        ani.SetBool("死亡開關", true);                                     //死亡動畫
        DropProp();
        player.Exp(exp);                                                            //傳經驗值
    }

    /// <summary>
    /// 掉落道具
    /// </summary>
    private void DropProp()
    {
        float r = Random.Range(0f, 1f);                                                        //取得隨機值介於 0~1

        if( r <= prop)                                                                                      //如果 隨機值 小於等於 機率
        {
            //生成(物件，座標，角度)
            Instantiate(skull, transform.position+transform.up*1.5f, transform.rotation);
        }

    }
    #endregion

    #region 事件
    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

        player = FindObjectOfType<Player>();

        nma.speed = speed;                                       //更新速度
        nma.stoppingDistance = rangeAttack;          //更新停止距離
    }
   

    private void Update()
    {
        Move();
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(0.8f, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, rangeAttack);

    }

    private void OnTriggerEnter(Collider other)
    {
       

        if( other.name == "瘦長人" )
            {

            other.GetComponent<Player>().Hit(attack, transform);

        }
    }
    #endregion
}
