using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MassageManager : MonoBehaviour
{
     public TextMeshProUGUI score_massage;
 
      // 初期化
      void Start () {
        // テキストの表示を入れ替える
        score_massage.text = "システムスタート";
      }
}