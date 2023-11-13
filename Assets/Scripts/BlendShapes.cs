 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using VRM;
 using System;
 
 public class BlendShapes : MonoBehaviour
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
	
		// 拡張 : (VRoid)
     [Range(0, 1)] public float SURPRISED;
 
     private VRMBlendShapeProxy targetProxy;

     void Start()
     {
 
     }

     void Update(){
        targetProxy = GetComponent<VRMBlendShapeProxy>();
        var proxy = GetComponent<VRMBlendShapeProxy>();

        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), 0.3f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0.05f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), 0.6f);
        proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.15f);
        targetProxy.Apply();
     }
 }
