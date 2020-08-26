﻿
using UnityEngine;
using System.Collections;   //使用協程必須引用此 API (系統.集合)



public class LearnCoroutine : MonoBehaviour
{

    //一般方法:無傳回
 private void   MethodA()
    {


    }

    //有傳回方法
    private int MethodB ()
    {
        return 10;
    }

    //協程方法
    //1.傳回類型:  IEnumerator 
    //2.yield reture 時間 new、WaitForSeconds(秒數)、null 一個影格的時間

    private IEnumerator Test()
    {
        print("我是協程");
        yield return new WaitForSeconds(10);
        print("我是十秒後的程式");

        
    }

    private void Start()
    {
        //呼叫協程
        StartCoroutine(Test());
        //啟動協程(Big());
        StartCoroutine(Big());

    }

    public Transform cube;

    private IEnumerator Big()
    {

        // 迴圈 for 0~10
        for (int i = 0; i < 10; i++)
        {
            cube.localScale += Vector3.one;                            //尺寸 += 三維向量 的 1    .one = x y z 各加+1
            yield return new WaitForSeconds(1);                     //等待1秒

            
        }


    }

}
