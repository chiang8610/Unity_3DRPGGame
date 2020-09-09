using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCDate date;
    [Header("對話資訊")]
    public GameObject panel;
    [Header("對話名稱")]
    public Text textName;
    [Header("對話內容")]
    public Text textcContent;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }



    private IEnumerator Type()
    {
        textcContent.text = "";                                                            //對話內容清空

        string dialog = date.dialogs[0];                                              //取得要顯示的對話

        for (int i = 0; i < dialog.Length; i++)                                    //迴圈執行對話的每個字
        {
            textcContent.text += dialog[i];                                            // 遞增對話內容
            aud.PlayOneShot(date.soundType, 0.5f);                           // 播放打字音效
            yield return new WaitForSeconds(date.speed);                   // 等待

        }

    }


  

    private void NoMission()
    {


    }

    private void Missioning()
    {


    }

    private void Finish()
    {



    }

    private void DialogStart()
    {
        panel.SetActive(true);
        textName.text = name;
        StartCoroutine(Type());
    }

    private void DialogStop()
    {
        panel.SetActive(false);

    }

    /// <summary>
    /// 面向玩家
    /// </summary>
    /// <param name="other"></param>
    private void LookAtPlayer(Collider other)
    {
        Quaternion angle = Quaternion.LookRotation(other.transform.position - transform.position);               //取得面向玩家方向
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 5);                          //角度插值

    }

    /// <summary>
    ///玩家進入觸發區域
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "瘦長人") DialogStart();
            
     }
    /// <summary>
    /// 玩家離開觸發區域
    /// </summary>

    private void OnTriggerExit(Collider other)
        {
        if (other.name == "瘦長人") DialogStop();
       
        }

    /// <summary>
    /// 玩家待在觸發區域會持續執行，約 60 FPS
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "瘦長人") LookAtPlayer(other);
    }
}
