﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS
public class NativeAPI {
    [DllImport("__Internal")]
    public static extern void showHostMainWindow();
}
#endif
public class Ch_Main : MonoBehaviour
{
  private Transform tr;
  public float moveSpeed = 30.0f;
  public GameObject Target;
  GameObject[] gameobj;

  // Start is called before the first frame update
  // Update is called once per frame
  void Start()
  {
    tr = GetComponent<Transform>();
    gameobj = GameObject.FindGameObjectsWithTag("Character");
  }
  void Update()
  {

    if (Application.platform == RuntimePlatform.Android)
      if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
  }
  void hideObject(string charName)
  {
    Debug.Log(gameobj.Length);
    foreach (GameObject ex in gameobj)
    {
      Debug.Log(ex.name);

      if (charName == ex.name)
      {
        ex.SetActive(false);
      }
    }

  }
  void onObject(string charName)
  {
    foreach (GameObject ex in gameobj)
    {
      Debug.Log(ex.name);

      if (charName == ex.name)
      {
        ex.SetActive(true);
      }
    }

  }
  void Walking()
  {
    tr.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
  }
  void Stop()
  {
    tr.Translate(Vector3.forward * 0 * Time.deltaTime, Space.Self);
  }
  void showHostMainWindow()
  {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.company.product.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            overrideActivity.Call("showMainActivity");
        } catch(Exception e)
        {
            
        }
#endif
  }
  void OnGUI()
  {
    GUIStyle style = new GUIStyle("button");
    style.fontSize = 30;

    if (GUI.Button(new Rect(3, -3, 200, 200), "걷자", style)) Walking();
    if (GUI.Button(new Rect(500, 0, 200, 200), "캐릭터 숨김", style)) hideObject("Ch_01");
    if (GUI.Button(new Rect(300, 0, 200, 200), "캐릭터 표시", style)) onObject("Ch_01");
  }
}
