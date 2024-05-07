using Firebase.Extensions;
using System.Collections;
using Firebase.Auth;
using UnityEngine;
using Firebase;

public class EmailPassLogin : MonoBehaviour
{

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

            AuthResult result = task.Result;

            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            UIManager.Instance.SetLogInPanel(true);

            UIManager.Instance.SetSignUpPanel(false);

            UIManager.Instance.signupEmail.text = "";

            UIManager.Instance.signupPassword.text = "";

            //SendEmailVerification();

            SyncUserData();

        });
    }

    public void SendEmailVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    IEnumerator SendEmailForVerificationAsync()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                switch (error)
                {
                    case AuthError.None:
                        break;
                    case AuthError.Unimplemented:
                        break;
                    case AuthError.Failure:
                        break;
                    case AuthError.InvalidCustomToken:
                        break;
                    case AuthError.CustomTokenMismatch:
                        break;
                    case AuthError.InvalidCredential:
                        break;
                    case AuthError.UserDisabled:
                        break;
                    case AuthError.AccountExistsWithDifferentCredentials:
                        break;
                    case AuthError.OperationNotAllowed:
                        break;
                    case AuthError.EmailAlreadyInUse:
                        break;
                    case AuthError.RequiresRecentLogin:
                        break;
                    case AuthError.CredentialAlreadyInUse:
                        break;
                    case AuthError.InvalidEmail:
                        break;
                    case AuthError.WrongPassword:
                        break;
                    case AuthError.TooManyRequests:
                        break;
                    case AuthError.UserNotFound:
                        break;
                    case AuthError.ProviderAlreadyLinked:
                        break;
                    case AuthError.NoSuchProvider:
                        break;
                    case AuthError.InvalidUserToken:
                        break;
                    case AuthError.UserTokenExpired:
                        break;
                    case AuthError.NetworkRequestFailed:
                        break;
                    case AuthError.InvalidApiKey:
                        break;
                    case AuthError.AppNotAuthorized:
                        break;
                    case AuthError.UserMismatch:
                        break;
                    case AuthError.WeakPassword:
                        break;
                    case AuthError.NoSignedInUser:
                        break;
                    case AuthError.ApiNotAvailable:
                        break;
                    case AuthError.ExpiredActionCode:
                        break;
                    case AuthError.InvalidActionCode:
                        break;
                    case AuthError.InvalidMessagePayload:
                        break;
                    case AuthError.InvalidPhoneNumber:
                        break;
                    case AuthError.MissingPhoneNumber:
                        break;
                    case AuthError.InvalidRecipientEmail:
                        break;
                    case AuthError.InvalidSender:
                        break;
                    case AuthError.InvalidVerificationCode:
                        break;
                    case AuthError.InvalidVerificationId:
                        break;
                    case AuthError.MissingVerificationCode:
                        break;
                    case AuthError.MissingVerificationId:
                        break;
                    case AuthError.MissingEmail:
                        break;
                    case AuthError.MissingPassword:
                        break;
                    case AuthError.QuotaExceeded:
                        break;
                    case AuthError.RetryPhoneAuth:
                        break;
                    case AuthError.SessionExpired:
                        break;
                    case AuthError.AppNotVerified:
                        break;
                    case AuthError.AppVerificationFailed:
                        break;
                    case AuthError.CaptchaCheckFailed:
                        break;
                    case AuthError.InvalidAppCredential:
                        break;
                    case AuthError.MissingAppCredential:
                        break;
                    case AuthError.InvalidClientId:
                        break;
                    case AuthError.InvalidContinueUri:
                        break;
                    case AuthError.MissingContinueUri:
                        break;
                    case AuthError.KeychainError:
                        break;
                    case AuthError.MissingAppToken:
                        break;
                    case AuthError.MissingIosBundleId:
                        break;
                    case AuthError.NotificationNotForwarded:
                        break;
                    case AuthError.UnauthorizedDomain:
                        break;
                    case AuthError.WebContextAlreadyPresented:
                        break;
                    case AuthError.WebContextCancelled:
                        break;
                    case AuthError.DynamicLinkNotActivated:
                        break;
                    case AuthError.Cancelled:
                        break;
                    case AuthError.InvalidProviderId:
                        break;
                    case AuthError.WebInternalError:
                        break;
                    case AuthError.WebStorateUnsupported:
                        break;
                    case AuthError.TenantIdMismatch:
                        break;
                    case AuthError.UnsupportedTenantOperation:
                        break;
                    case AuthError.InvalidLinkDomain:
                        break;
                    case AuthError.RejectedCredential:
                        break;
                    case AuthError.PhoneNumberNotFound:
                        break;
                    case AuthError.InvalidTenantId:
                        break;
                    case AuthError.MissingClientIdentifier:
                        break;
                    case AuthError.MissingMultiFactorSession:
                        break;
                    case AuthError.MissingMultiFactorInfo:
                        break;
                    case AuthError.InvalidMultiFactorSession:
                        break;
                    case AuthError.MultiFactorInfoNotFound:
                        break;
                    case AuthError.AdminRestrictedOperation:
                        break;
                    case AuthError.UnverifiedEmail:
                        break;
                    case AuthError.SecondFactorAlreadyEnrolled:
                        break;
                    case AuthError.MaximumSecondFactorCountExceeded:
                        break;
                    case AuthError.UnsupportedFirstFactor:
                        break;
                    case AuthError.EmailChangeNeedsVerification:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                print("Email successfully send");
            }
        }
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

            AuthResult result = task.Result;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            UIManager.Instance.SetMainAppPanel(true);

            UIManager.Instance.SetLogInPanel(false);

            UIManager.Instance.LoginEmail.text = "";

            UIManager.Instance.loginPassword.text = "";

            UIManager.Instance.logText.text = "";

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

        DataManager.Instance.data.userInfo.Add(userID, newUser);

        DataManager.Instance.DownloadData();
    
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