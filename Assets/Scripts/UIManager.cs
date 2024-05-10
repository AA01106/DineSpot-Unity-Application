using Michsky.UI.ModernUIPack;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    #region Fields

    // Static instance of UI Manager class
    public static UIManager Instance;

    // Reference to each UI panel as a Canvas Group
    #region Panels

    [SerializeField]
    private CanvasGroup loadingScreenPanel, signupPanel, loginPanel;

    [SerializeField]
    private CanvasGroup mainAppPanel, restaurantListPanel, makeReservationPanel;

    [SerializeField]
    private CanvasGroup accountInformationPanel, editReservationPanel, reservationInfoPanel;
    #endregion

    // Reference to all UI elements contained in the login panel
    #region Login panel components

    [Header("Login")]

    public TMP_InputField LoginEmail;

    public TMP_InputField loginPassword;

    public TMP_Text logText;

    #endregion

    // Reference to all UI elements contained in the sign up panel
    #region SignUp panel components

    [Header("Sign up")]

    public TMP_InputField signupEmail, signupPassword;

    public TMP_InputField firstName, lastName, phone;

    #endregion

    // Reference to all UI elements contained in reservation list in account information window
    #region User's reservations list

    [Header("Reservations List")]

    public Transform reservationList;

    public GameObject reservtaionListItems;
    #endregion

    // Reference to all UI elements contained in make a reservation panel
    #region Make a reservation panel UI components

    [Header("Make a Reservation")]

    public CustomDropdown TableNo;

    public CustomDropdown Atendees;

    public CustomDropdown Location;

    public TMP_InputField month, day, hours, minutes;

    public TMP_Text restaurantName;

    public TMP_Text available;

    #endregion

    // Reference to all UI elements contained in reservation info panel
    #region Reservation Info Panel

    [Header("Reservation info")]
    public TMP_Text info_TableNo;

    public TMP_Text info_Atendees;

    public TMP_Text info_Location;

    public TMP_Text info_date, info_time;

    public TMP_Text info_restaurantName;

    public Button deleteButton;

    #endregion


    // Reference to all UI elements contained in account information panel
    #region Account Information

    public TMP_Text displayedName;

    #endregion

    #endregion

    #region Custom setters methods

    //************************************
    // Setters to Show/Hide each UI panel
    //************************************

    public void SetLoadingScreen(bool State)
    {
        if (State)
        {

            loadingScreenPanel.alpha = 1f;
            loadingScreenPanel.blocksRaycasts = true;
            loadingScreenPanel.interactable = true;
        }
        else
        {
            loadingScreenPanel.alpha = 0f;
            loadingScreenPanel.blocksRaycasts = false;
            loadingScreenPanel.interactable = false;
        }

    }

    public void SetSignUpPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(signupPanel, 1));
            signupPanel.blocksRaycasts = true;
            signupPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(signupPanel, 0));
            signupPanel.blocksRaycasts = false;
            signupPanel.interactable = false;
        }
    }

    public void SetLogInPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(loginPanel, 1));
            loginPanel.blocksRaycasts = true;
            loginPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(loginPanel, 0));
            loginPanel.blocksRaycasts = false;
            loginPanel.interactable = false;
        }
    }

    public void SetMainAppPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(mainAppPanel, 1));
            mainAppPanel.blocksRaycasts = true;
            mainAppPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(mainAppPanel, 0));
            mainAppPanel.blocksRaycasts = false;
            mainAppPanel.interactable = false;
        }
    }

    public void SetRestaurantListPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(restaurantListPanel, 1));
            restaurantListPanel.blocksRaycasts = true;
            restaurantListPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(restaurantListPanel, 0));
            restaurantListPanel.blocksRaycasts = false;
            restaurantListPanel.interactable = false;
        }
    }

    public void SetMakeReservationPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(makeReservationPanel, 1));
            makeReservationPanel.blocksRaycasts = true;
            makeReservationPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(makeReservationPanel, 0));
            makeReservationPanel.blocksRaycasts = false;
            makeReservationPanel.interactable = false;
        }
    }

    public void SetAccountInformationPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(accountInformationPanel, 1));
            accountInformationPanel.blocksRaycasts = true;
            accountInformationPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(accountInformationPanel, 0));
            accountInformationPanel.blocksRaycasts = false;
            accountInformationPanel.interactable = false;
        }
    }

    public void SetEditReservationPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(editReservationPanel, 1));
            editReservationPanel.blocksRaycasts = true;
            editReservationPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(editReservationPanel, 0));
            editReservationPanel.blocksRaycasts = false;
            editReservationPanel.interactable = false;
        }
    }

    public void SetReservationInfoPanel(bool State)
    {
        if (State)
        {
            StartCoroutine(FadeInOut(reservationInfoPanel, 1));
            reservationInfoPanel.blocksRaycasts = true;
            reservationInfoPanel.interactable = true;
        }
        else
        {
            StartCoroutine(FadeInOut(reservationInfoPanel, 0));
            reservationInfoPanel.blocksRaycasts = false;
            reservationInfoPanel.interactable = false;
        }
    }

    #endregion


    private void Awake()
    {
        Instance = this;
    }

    // Coroutine to increase/decrease the opcaity of specific canvas group
    private IEnumerator FadeInOut(CanvasGroup canvasGroup, float endValue ) 
    {
        while (Mathf.Abs(canvasGroup.alpha - endValue) >= 0.001) 
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, endValue, Time.deltaTime * 25);

            yield return null;
        }

        canvasGroup.alpha = Mathf.Round(canvasGroup.alpha);
    }

}
