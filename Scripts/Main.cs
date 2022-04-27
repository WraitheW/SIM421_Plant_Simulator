using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    public Web Web;
    public UserInfo UserInfo;
    public Login Login;

    public GameObject UserProfile;
    public GameObject Inventory;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Web = GetComponent<Web>();
        UserInfo = GetComponent<UserInfo>();
    }
}
