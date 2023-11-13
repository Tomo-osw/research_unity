using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRM;
using System;
using UnityEngine.UI;
using Samples.UDP;
using TMPro;
using System.Windows.Forms;

public class Udp_rec : MonoBehaviour
{
    [Range(0, 1)] public float A;
    [Range(0, 1)] public float I;
    [Range(0, 1)] public float U;
    [Range(0, 1)] public float E;
    [Range(0, 1)] public float O;
    [Range(0, 1)] public float BLINK;
    [Range(0, 1)] public float BLINK_L;
    [Range(0, 1)] public float BLINK_R;
    [Range(0, 1)] public float ANGRY;
    [Range(0, 1)] public float FUN;
    [Range(0, 1)] public float JOY;
    [Range(0, 1)] public float SORROW;
    [Range(0, 1)] public float EXTRA;
    [Range(0, 1)] public float LOOKUP;
    [Range(0, 1)] public float LOOKDOWN;
    [Range(0, 1)] public float LOOKLEFT;
    [Range(0, 1)] public float LOOKRIGHT;
    [Range(0, 1)] public float SURPRISED;

    public VRMBlendShapeProxy targetProxy;

    public Text rename_text;
    public TextMeshProUGUI sys_text;

    UDPSocket socket;
    public byte[] message;
    public float paramater = 0;
    public int agent_count = 0;
    public AudioClip sound1;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        targetProxy = this.gameObject.GetComponent<VRMBlendShapeProxy>();
        rename_text.text = "";
        sys_text.text = "";
        SendKeys.SendWait("{a}");

        socket = new(50007);
        socket.OnReceive += Socket_OnReceive;
        StartCoroutine(StartCoroutine());
        StartCoroutine(CheckCoroutine());
    }

    private void Socket_OnReceive(byte[] bytesRead, string address, int port){
        string text = Encoding.UTF8.GetString(bytesRead);
        float f_text = float.Parse(text);
        paramater = f_text;
    }

    private void Socket_OnSend(int sentByteSize){
        Debug.Log(sentByteSize + "bytes has been sent.");
    }

    private void OnApplicationQuit(){
        socket.Close();
    }

    private IEnumerator StartCoroutine(){
        StartCoroutine(BlendCoroutine());
        socket.Listen();
        yield return null;
    }

    public IEnumerator CheckCoroutine(){
        while(true){
            if (paramater == 101){
                Debug.Log("実験1-1A1B1");
                StartCoroutine(E1_1_A1B1Coroutine());
                paramater = 0;
            }else if (paramater == 102){
                Debug.Log("実験1-1A1B2");
                StartCoroutine(E1_1_A1B2Coroutine());
                paramater = 0;
            }else if (paramater == 103){
                Debug.Log("実験1-1A2B1");
                StartCoroutine(E1_1_A2B1Coroutine());
                paramater = 0;
            }else if (paramater == 104){
                Debug.Log("実験1-1A2B2");
                StartCoroutine(E1_1_A2B2Coroutine());
                paramater = 0;
            }else if (paramater == 111){
                Debug.Log("実験1-2C1D1");
                StartCoroutine(E1_2_C1D1Coroutine());
                paramater = 0;
            }else if (paramater == 112){
                Debug.Log("実験1-2C1D2");
                StartCoroutine(E1_2_C1D2Coroutine());
                paramater = 0;
            }else if (paramater == 113){
                Debug.Log("実験1-2C2D1");
                StartCoroutine(E1_2_C2D1Coroutine());
                paramater = 0;
            }else if (paramater == 114){
                Debug.Log("実験1-2C2D2");
                StartCoroutine(E1_2_C2D2Coroutine());
                paramater = 0;
            }else if (paramater == 201){
                Debug.Log("実験2A1");
                StartCoroutine(E2_A1Coroutine());
                paramater = 0;
            }else if (paramater == 202){
                Debug.Log("実験2A2");
                StartCoroutine(E2_A2Coroutine());
                paramater = 0;
            }else if (paramater == 203){
                Debug.Log("実験2A3");
                StartCoroutine(E2_A3Coroutine());
                paramater = 0;
            }else if (paramater == 301){
                Debug.Log("実験3A1");
                StartCoroutine(E3_A1Coroutine());
                paramater = 0;
            }else if (paramater == 302){
                Debug.Log("実験3A2");
                StartCoroutine(E3_A2Coroutine());
                paramater = 0;
            }else if (paramater == 303){
                Debug.Log("実験3A3");
                StartCoroutine(E3_A3Coroutine());
                paramater = 0;
            }else{
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public IEnumerator BlendCoroutine(){
        targetProxy = GetComponent<VRMBlendShapeProxy>();
        var proxy = GetComponent<VRMBlendShapeProxy>();

        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.I), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.U), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.E), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_L), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_R), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Unknown), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.LookUp), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.LookDown), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.LookLeft), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.LookRight), 0f);
        //proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Surprised), 0f);
        targetProxy.Apply();
        yield return null;
    }

    public IEnumerator agent_move(){

        while(true){
            if(agent_count == 1){
                SendKeys.SendWait("{a}");
                yield return null;
            }else if(agent_count == 2 ){
                SendKeys.SendWait("{d}");
                yield return null;
            }else if(agent_count == 3){
                SendKeys.SendWait("{s}");
                yield return null;
            }else if(agent_count == 4){
                SendKeys.SendWait("{w}");
                yield return null;
            }else if(agent_count == 0){
                break;
            }
        }
        yield return null;
    }

    public IEnumerator Send_massage(string s_id, string s_texts){
        message = Encoding.UTF8.GetBytes(s_id);
        socket.Send(message,50008);
        socket.OnSend += Socket_OnSend;
        yield return new WaitForSeconds(0.5f);
        message = Encoding.UTF8.GetBytes(s_texts);
        socket.Send(message,50008);
        socket.OnSend += Socket_OnSend;
        yield return null;
    }

    public IEnumerator Send_serial(string se_id, string se_texts){
        message = Encoding.UTF8.GetBytes(se_id);
        socket.Send(message,50008);
        socket.OnSend += Socket_OnSend;
        yield return new WaitForSeconds(0.25f);
        message = Encoding.UTF8.GetBytes(se_texts);
        socket.Send(message,50008);
        socket.OnSend += Socket_OnSend;
        yield return null;
    }

    public IEnumerator E1_1_A1B1Coroutine(){
        rename_text.text = "";
        string id = "";
        string texts = "";
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        yield return new WaitForSeconds(7.2f);
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.30f);
        agent_count = 0;
        yield return null;
    }

    public IEnumerator E1_1_A1B2Coroutine(){
        rename_text.text = "";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.30f);
        agent_count = 0;
        yield return null;
    }

    public IEnumerator E1_1_A2B1Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "";
        string texts = "";
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.30f);
        agent_count = 0;
        yield return null;
        
    }

    public IEnumerator E1_1_A2B2Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.30f);
        agent_count = 0;
        yield return null;
    }

    public IEnumerator E1_2_C1D1Coroutine(){
        rename_text.text = "感情状態：無関心";
        string id = "23";
        string texts = "初めまして。こんにちは。大丈夫ですか";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        id = "23";
        texts = "あなたの状態はあまりよくないようです";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "23";
        texts = "私はあなたのことを心配しています";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        yield return null;

    }

    public IEnumerator E1_2_C1D2Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "25";
        texts = "つらいですよね・・・わかります・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        yield return null;

    }

    public IEnumerator E1_2_C2D1Coroutine(){
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        string id = "24";
        string texts = "初めまして！こんにちは！大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.5f);
        agent_count = 0;
        id = "24";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        yield return null;

    }

    public IEnumerator E1_2_C2D2Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        sys_text.text = texts;
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.30f);
        agent_count = 0;
        yield return null;

    }

    public IEnumerator E2_A1Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        sys_text.text = texts;
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(9f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.3f);
        agent_count = 0;
        yield return null;

    }

    public IEnumerator E2_A2Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        sys_text.text = texts;
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！！手を握りますね！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(9f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.3f);
        agent_count = 0;
        yield return new WaitForSeconds(8.2f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return null;
    }

    public IEnumerator E2_A3Coroutine(){
        rename_text.text = "感情状態：あなたを心配しています";
        string id = "25";
        string texts = "初めまして！こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(5.2f);
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        sys_text.text = texts;
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        id = "25";
        texts = "あなたの状態はあまりよくないようです・・・";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(7.2f);
        sys_text.text = texts;
        id = "24";
        texts = "大丈夫です！安心してください！私があなたの状態をなんとかして見せます！手を握りますね！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(9f);
        sys_text.text = texts;
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        rename_text.text = "感情状態：あなたをなんとかして助けたいと思っています";
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.3f);
        agent_count = 0;
        yield return new WaitForSeconds(8.2f);
        agent_count = 1;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.8f);
        agent_count = 4;
        yield return new WaitForSeconds(1.65f);
        agent_count = 2;
        yield return new WaitForSeconds(0.95f);
        agent_count = 3;
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        yield return new WaitForSeconds(4.0f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        audioSource.Stop();
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "0";
        texts = "サーボモータを180度に";
        StartCoroutine(Send_serial(id, texts));
        yield return new WaitForSeconds(6.5f);
        id = "1";
        texts = "サーボモータを0度に";
        StartCoroutine(Send_serial(id, texts));
        yield return null;
    }

    public IEnumerator E3_A1Coroutine(){
        rename_text.text = "";
        string id = "10";
        string texts = "1つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "23";
        texts = "こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(3.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(4f);
        id = "10";
        texts = "2つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "23";
        texts = "続ければ治るので頑張りましょう";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(3.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(4f);
        id = "10";
        texts = "3つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "23";
        texts = "落ち着きましたか。念の為、体調の確認をしておきましょう";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(6f);
        sys_text.text = texts;
    }

    public IEnumerator E3_A2Coroutine(){
        rename_text.text = "";
        string id = "10";
        string texts = "1つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "25";
        texts = "こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(3.5f);
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0.2f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow), 0.6f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        sys_text.text = texts;
        yield return new WaitForSeconds(5f);
        id = "10";
        texts = "2つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "25";
        texts = "しんどいですよね。辛いですよね。。でも、続ければ治るから、、頑張ろう";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(6.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(8f);
        id = "10";
        texts = "3つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "25";
        texts = "落ち着きましたか？念の為、体調の確認をしておきましょう...";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(6f);
        sys_text.text = texts;

    }

    public IEnumerator E3_A3Coroutine(){
        rename_text.text = "";
        string id = "10";
        string texts = "1つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "24";
        texts = "こんにちは。大丈夫ですか？";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(3.5f);
        var proxy = GetComponent<VRMBlendShapeProxy>();
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
        agent_count = 3;
        StartCoroutine(agent_move());
        yield return new WaitForSeconds(0.25f);
        agent_count = 0;
        sys_text.text = texts;
        yield return new WaitForSeconds(4f);
        id = "10";
        texts = "2つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "24";
        texts = "続ければすぐにきっと治るから！笑顔で笑って毎日をすごそ！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(6.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(8f);
        id = "10";
        texts = "3つ目のセリフを発話してください";
        yield return new WaitForSeconds(0.5f);
        sys_text.text = texts;
        yield return new WaitForSeconds(6f);
        id = "24";
        texts = "落ち着きましたか？？！念の為、体調の確認をしておきましょう！大丈夫です！！";
        StartCoroutine(Send_massage(id, texts));
        yield return new WaitForSeconds(6f);
        sys_text.text = texts;
    }
}


