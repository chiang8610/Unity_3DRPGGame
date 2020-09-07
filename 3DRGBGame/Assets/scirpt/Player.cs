﻿using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位:基本資料

    [Header("移動速度"), Range(0, 1000)]
    public float speed = 5;
    [Header("旋轉速度"), Range(0, 1000)]
    public float turn = 5;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 20;
    [Header("血量"), Range(0, 500)]
    public float hp = 250;
    [Header("魔力"), Range(0, 500)]
    public float mp = 50;

    public float exp;
    public int Iv  =1;

    private Animator ani;
    private Rigidbody rig;
    #endregion

    #region 方法:功能
    private void Move()
    {

       float h= Input.GetAxis("Horizontal");      //A  D  &  左右:  A 左 -1 , D 右 1 , 沒按 0
       float v = Input.GetAxis("Vertical");          //S W  &  下上: S 下 -1 , W 上 1 , 沒按 0

        // Vector3 pos = new Vector3(h, 0, v);                                               //要移動的座標 -世界座標版本
       // Vector3 pos = transform.forward * v + transform.right * h;           //要移動的座標 -區域座標版本
        Vector3 pos = cam.forward * v + cam.right * h;                               //要移動的座標 -攝影機的區域座標版本


        //剛體.移動作標(本身座標+要移動的座標 * 速度 * 1/50)
        rig.MovePosition(transform.position + pos * speed*Time.fixedDeltaTime);
        //動畫.設定浮點數("參數名稱"，前後 + 左右 的 絕對值)
        ani.SetFloat("移動", Mathf.Abs(v) + Mathf.Abs(h));

    }

    #endregion

    /// <summary>
    /// 攝影機根物件
    /// </summary>
    private Transform cam;


    #region 事件:入口
    /// <summary>
    /// 喚醒:會在 start 前執行一次
    /// </summary>
    private void Awake()
    {

        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

        cam = GameObject.Find("攝影機根物件").transform;     //遊戲物件.搜尋("物件名稱") -建議不要在 Update
    }
    /// <summary>
    /// 固定更新 : 固定 50 FPS     每秒更新50次
    /// 有物理運動在這裡呼叫 Rigidbody  剛體有關
    /// </summary>

    private void FixedUpdate()
    {
        Move();

    }



    #endregion
}
