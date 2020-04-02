using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    ///<summary>
    ///足球是否進入球門
    ///</summary>
    public static bool complete;

    ///<summary>
    ///觸發開始事件 : 碰到勾選 Is Trigger 物件會執行一次
    ///</summary>
    ///<param name="other">碰到的物件碰撞資訊</param>
    private void OnTriggerEnter(Collider other)
    {
        //如果 碰到物件 的 名稱 等於 "進球區域"
        if (other.name == "進球區域")
        {
            // 進入球門
            complete = true;
        }
    }
}
