using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;

public class EnterUserInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main.instance.Web.GetUserLevel(Main.instance.UserInfo.UserID));
    }
}
