using Firebase.Extensions;
using System.Collections;
using Firebase.Auth;
using UnityEngine;
using Firebase;


public class EmailPassLogin : MonoBehaviour
{

    private void Start()
    {
        DataManager.Instance.DownloadData();
    }

    public void SignUp()
    {

        UIManager.Instance.SetLoadingScreen(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        string email = UIManager.Instance.signupEmail.text;

        string password = UIManager.Instance.signupPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.

            UIManager.Instance.SetLoadingScreen(false);

            UIManager.Instance.SetLogInPanel(true);

            UIManager.Instance.SetSignUpPanel(false);

            UIManager.Instance.signupEmail.text = "";

            UIManager.Instance.signupPassword.text = "";

            SyncUserData();
        });
    }


    public void Login()
    {
        UIManager.Instance.SetLoadingScreen(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        string email = UIManager.Instance.LoginEmail.text;

        string password = UIManager.Instance.loginPassword.text;

        Credential credential = EmailAuthProvider.GetCredential(email, password);

        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {

            UIManager.Instance.SetLoadingScreen(false);

            if (task.IsCanceled)
            {
                UIManager.Instance.logText.text = "The login has been canceled";

                return;
            }

            if (task.IsFaulted)
            {
                UIManager.Instance.logText.text = "You have entered an invalid username or password.";

                return;
            }

            UIManager.Instance.SetMainAppPanel(true);

            UIManager.Instance.SetLogInPanel(false);

            UIManager.Instance.LoginEmail.text = "";

            UIManager.Instance.loginPassword.text = "";

            UIManager.Instance.logText.text = "";

            UIManager.Instance.firstName.text = "";

            UIManager.Instance.lastName.text = "";

            UIManager.Instance.phone.text = "";
        });
    }

    public void SyncUserData() {

        User newUser = new User();

        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        newUser.firstName = UIManager.Instance.firstName.text;

        newUser.lastName = UIManager.Instance.lastName.text;

        newUser.phone = UIManager.Instance.phone.text;

        if (DataManager.Instance.data.userInfo == null)
            DataManager.Instance.data.userInfo = new System.Collections.Generic.Dictionary<string, User>();

        DataManager.Instance.DownloadData();

        DataManager.Instance.data.userInfo.Add(userID, newUser);

        DataManager.Instance.UploadData();
    }

    public void Logout() 
    {
        FirebaseAuth.DefaultInstance.SignOut();

        StartCoroutine(LoadingScreenWithTime(2));

        UIManager.Instance.SetMainAppPanel(false);

        UIManager.Instance.SetLogInPanel(true);
    }

    IEnumerator LoadingScreenWithTime(int seconds) 
    {
        UIManager.Instance.SetLoadingScreen(true);

        yield return new WaitForSeconds(seconds);

        UIManager.Instance.SetLoadingScreen(false);

    }
}