using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
     public GameObject score_object = null; // Textオブジェクト
     public Text score_text;
 
      // 初期化
      void Start () {
        // オブジェクトからTextコンポーネントを取得
        score_text = score_object.GetComponent<Text> ();
        // テキストの表示を入れ替える
        score_text.text = "感情状態：";
        //score_text.text = "感情状態：";
      }
}