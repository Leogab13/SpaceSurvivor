/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseReference : MonoBehaviour
{
    public static FirebaseReference instance;
    //Firebase variables
    [Header("Firebase")]
    public static FirebaseAuth auth;
    public static FirebaseUser user;
    public static DatabaseReference DBreference;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this.gameObject);
        }

        UpdateData();
    }

    public static void UpdateData()
    {
        auth = FirebaseManager.auth;
        user = FirebaseManager.user;
        DBreference = FirebaseManager.DBreference;
    }
}*/
