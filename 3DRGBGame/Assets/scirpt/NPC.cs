using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCDate data;
    [Header("對話資訊")]
    public GameObject panel;
    [Header("對話名稱")]
    public Text textName;
    [Header("對話內容")]
    public Text textcContent;
    [Header("第一段對話顯示的物件")]
    public GameObject objectShow;
    [Header("任務資訊")]
    public RectTransform rectMission;


    private AudioSource aud;
    private Animator ani;
    private Player player;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();

        //透過類型尋找物件<類型>()  *僅限此類型在場景上只有一個
        player = FindObjectOfType<Player>();
        data.state = StateNPC.NoMission;                                        //預設為任務狀態
    }



    private IEnumerator Type()
    {
        PlayAnimation();

        player.stop = true;                                                                   //停止

        textcContent.text = "";                                                            //對話內容清空

        string dialog = data.dialogs[(int)data.state];                          //取得要顯示的對話 *舉得列舉的整數方式:(int)列舉

        for (int i = 0; i < dialog.Length; i++)                                    //迴圈執行對話的每個字
        {
            textcContent.text += dialog[i];                                            // 遞增對話內容
            aud.PlayOneShot(data.soundType, 0.1f);                           // 播放打字音效
            yield return new WaitForSeconds(data.speed);                   // 等待

        }

        player.stop = false;                                                                     //恢復
        NoMission();
    }

    private void PlayAnimation()
    {
        if (data.state != StateNPC.Finish)                                               //如果不是完成狀態
        {
            ani.SetBool("對話開關", true);                                               //撥放對話動畫
        }
        else
        {
            ani.SetTrigger("完成觸發");                                                    //否則 撥放完成動畫
        }
    }

    /// <summary>
    /// 第一階段 : 尚未取得任務
    /// </summary>
    private void NoMission()
    {
        if (data.state != StateNPC.NoMission) return;                          //如果狀態不是位階任務就跳出

        data.state = StateNPC.Missioning;                                          //進入任務進行中階段         
        objectShow.SetActive(true);                                                    //顯示物件

        StartCoroutine(ShowMission());                                                //啟動顯示任務協成
    }


    /// <summary>
    /// 顯示任務
    /// </summary>
    /// <returns></returns>

    private IEnumerator ShowMission()

    {
        while(rectMission.anchoredPosition.x >73)                                                           //當 x 大於 0持續執行
        {
            rectMission.anchoredPosition -= new Vector2(300*Time.deltaTime,0);            //x遞減
            yield return null;                                                                                                  //等待
        }
    }

    private void Missioning()
    {


    }

    public void Finish()
    {
        //切換為完成狀態
        data.state = StateNPC.Finish;


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
        ani.SetBool("對話開關", false);
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
