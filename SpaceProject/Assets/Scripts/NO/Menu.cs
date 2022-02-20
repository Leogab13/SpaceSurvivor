/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    //Login variables
    [Header("Login")]
    public static TMP_InputField emailLoginField;
    public static TMP_InputField passwordLoginField;
    public static TMP_Text warningLoginText;
    public static TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public static TMP_InputField usernameRegisterField;
    public static TMP_InputField emailRegisterField;
    public static TMP_InputField passwordRegisterField;
    public static TMP_InputField passwordRegisterVerifyField;
    public static TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public static TMP_InputField usernameField;
    public static TMP_Text scoreText;
    public static GameObject scoreElement;
    public static Transform scoreboardContent;

   
    public static void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public static void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public static void MenuLoginButton()
    {
        if (FirebaseManager.instance.user == null)
        {
            UIManager.instance.LoginScreen();
        }
        else
        {
            StartCoroutine(FirebaseManager.instance.LoadUserData());

            usernameField.text = FirebaseManager.instance.user.DisplayName;
            UIManager.instance.UserDataScreen(); // Change to user data UI
        }
    }

    //Function for the login button
    public static void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(FirebaseManager.instance.Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public static void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(FirebaseManager.instance.Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    public static void SignOutButton()
    {
        FirebaseManager.instance.auth.SignOut();
        FirebaseManager.instance.user = null;
        UIManager.instance.LoginScreen();
        ClearRegisterFields();
        ClearLoginFields();
    }
    //Function for the change username button
    public static void ChangeUsernameButton()
    {
        StartCoroutine(FirebaseManager.instance.UpdateUsernameAuth(usernameField.text));
        StartCoroutine(FirebaseManager.instance.UpdateUsernameDatabase(usernameField.text));
    }
    public static void DisplayWarningLoginText(string _message)
    {
        warningLoginText.text = _message;
    }
    public static void DisplayConfirmLoginText(string _message)
    {
        confirmLoginText.text = _message;
    }
    public static void DisplayScoreText(string _score)
    {
        scoreText.text = _score;
    }
    public static void DisplayUsernameField(string _message)
    {
        usernameField.text = _message;
    }
    public static void DisplayWarningRegisterText(string _message)
    {
        warningRegisterText.text = _message;
    }
}*/
