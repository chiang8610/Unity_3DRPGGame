using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class learnloop : MonoBehaviour
{

    private void Start()
    {
        if (true)
        {
            print("我是判斷式");

        }

        //迴圈 while
        //重複處理相同的程式
        //當布林植為true 時持續執行
        int a = 1;
        
        while(a<=10)    //a<=10時跑下面的東西
        {
            print ("我是迴圈 while!!! 迴圈次數: "+a);
            a++;                //每次跑到這a+1

        }


        //迴圈for
        //(初始值   ;     條件    ;迭代器初始值增減)
        // (int i =0  ; i < length ; i++)
   
        for (int x = 1; x  <= 10; x++)    
        {
            print("我是迴圈for!!!迴圈次數:" + x);

        }


        //延伸學習
        //do whilerup、foreah


    }
  


}
