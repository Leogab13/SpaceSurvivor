using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public static FirebaseAuth auth;
    public static FirebaseUser user;
    public static DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_Text scoreText;

    //Leaderboard variables
    [Header("Leaderboard")]
    public Transform leaderboardRows;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void MenuLoginButton()
    {
        if (user == null)
        {
            UIManager.instance.LoginScreen();
        }
        else
        {
            StartCoroutine(LoadUserData());

            usernameField.text = user.DisplayName;
            UIManager.instance.UserDataScreen(); // Change to user data UI
        }
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    public void SignOutButton()
    {
        auth.SignOut();
        user = null;
        UIManager.instance.LoginScreen();
        ClearRegisterFields();
        ClearLoginFields();
    }
    //Function for the change username button
    public void ChangeUsernameButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));
    }
    //Function for the leaderboard button
    public void LeaderboardButton()
    {
        StartCoroutine(LoadLeaderboardData());
        //Go to scoareboard screen
        UIManager.instance.LeaderboardScreen();
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            StartCoroutine(LoadUserData());

            yield return new WaitForSeconds(1);

            usernameField.text = user.DisplayName;
            UIManager.instance.UserDataScreen(); // Change to user data UI
            confirmLoginText.text = "";
            ClearLoginFields();
            ClearRegisterFields();
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                user = RegisterTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearRegisterFields();
                        ClearLoginFields();
                    }
                }
            }
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            scoreText.text = "0";            
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            if (!snapshot.HasChild("score"))
            {
                //no score exists yet
                scoreText.text = "0";
            }
            else
            {
                scoreText.text = snapshot.Child("score").Value.ToString();
            }
        }
    }
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = user.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    /*private IEnumerator LoadLeaderboardData()
    {
        //Get all the users data ordered by score
        var DBTask = DBreference.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Reset the leaderboard
            foreach (Transform row in leaderboardRows.transform)
            {
                foreach (Transform field in row.transform)
                {
                    TMP_Text textField = field.GetComponent<TMP_Text>();
                    textField.text = "";
                }
            }

            //SortedList<int, string> leaderboardScores = new SortedList<int, string>(new InvertedComparer());
            //SortedList <int, string> leaderboardScores = new SortedList <int, string>();
            SortedList leaderboardScores = new SortedList();
            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                leaderboardScores.Add(score, username);
            }
            for (int i = 0; i < leaderboardRows.transform.childCount && i < leaderboardScores.Count; i++)
            {
                Transform row = leaderboardRows.transform.GetChild(i);
                TMP_Text usernameField = row.transform.GetChild(0).GetComponent<TMP_Text>();
                TMP_Text scoreField = row.transform.GetChild(1).GetComponent<TMP_Text>();
                usernameField.text = leaderboardScores.GetByIndex(i).ToString();
                scoreField.text = leaderboardScores.GetKey(i).ToString();
            }
        }
    }*/

    private IEnumerator LoadLeaderboardData()
    {
        var DBTask = DBreference.Child("users").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Reset the leaderboard
            foreach (Transform row in leaderboardRows.transform)
            {
                foreach (Transform field in row.transform)
                {
                    TMP_Text textField = field.GetComponent<TMP_Text>();
                    textField.text = "";
                }
            }
            List<Row> leaderboardScores = new List<Row>();
            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                bool validUsername = childSnapshot.Child("username").Value != null;
                bool validScore = childSnapshot.Child("score").Value != null;
                if (validUsername && validScore)
                {
                    string username = childSnapshot.Child("username").Value.ToString();
                    int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                    leaderboardScores.Add(new Row(username, score));
                }
                /*string username = childSnapshot.Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                leaderboardScores.Add(new Row(username, score));*/
            }
            leaderboardScores.Sort();
            for (int i = 0; i < leaderboardRows.transform.childCount && i < leaderboardScores.Count; i++)
            {
                Transform row = leaderboardRows.transform.GetChild(i);
                TMP_Text usernameField = row.transform.GetChild(0).GetComponent<TMP_Text>();
                TMP_Text scoreField = row.transform.GetChild(1).GetComponent<TMP_Text>();
                usernameField.text = leaderboardScores[i].getUsername();
                scoreField.text = leaderboardScores[i].getScore().ToString();
            }
        }
    }

    private class Row : IComparable
    {
        private string username;
        private int score;
        public Row(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
        public string getUsername()
        {
            return username;
        }
        public int getScore()
        {
            return score;
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Row that = obj as Row;
            if (that != null)
                return that.getScore().CompareTo(score);
            else
                throw new ArgumentException("Object is not a Row");
        }        
    }

    /*private class InvertedComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }*/
}

