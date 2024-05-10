using Firebase.Auth;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContentController : MonoBehaviour
{
    public static UIContentController Instance;


    [SerializeField]
    private Button deleteButton;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateReservationInfoPanel(Reservation currentReservation) 
    {
        UIManager.Instance.info_time.text = "Time : " + String.Format("{0}:{1}", currentReservation.hours, currentReservation.minutes);

        UIManager.Instance.info_date.text = "Date : " + String.Format("{0}-{1}-2024", currentReservation.day, currentReservation.month);

        UIManager.Instance.info_Atendees.text = "Atendees : " + currentReservation.atendees.ToString();

        UIManager.Instance.info_Location.text = "Location : " + currentReservation.location;

        UIManager.Instance.info_restaurantName.text = currentReservation.restaurant;

        UIManager.Instance.info_TableNo.text = "Table no : " + currentReservation.tableNo.ToString();

        string key = String.Format("{0}{1}{2}{3}", currentReservation.month, currentReservation.day,
           currentReservation.hours, currentReservation.minutes);

        UIManager.Instance.deleteButton.onClick.AddListener( () => { ReservationController.Instance.DeleteReservation(key); });

    }

    public void UpdateMakeReservationPanel(TMP_Text name) 
    {

        UIManager.Instance.restaurantName.text = name.text;
    }

   
    public void UpdateReservationListPanel() 
    {
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;


        foreach (KeyValuePair<string, Reservation> entry in DataManager.Instance.data.reservationByUserID[userID].reservations)
        {
            // do something with entry.Value or entry.Key
            if (DataManager.Instance.data.reservationByUserID[userID] != null)
                AddNewReservationUI(entry.Key, entry.Value);
        }
    }

    public void AddNewReservationUI (string key, Reservation newReservation)
    {
        GameObject newReservationUI = Instantiate(UIManager.Instance.reservtaionListItems);

        newReservationUI.transform.SetParent(UIManager.Instance.reservationList);

        newReservationUI.transform.localScale = Vector3.one;

        newReservationUI.GetComponentInChildren<Button>().onClick.AddListener(() => {

            UIManager.Instance.SetReservationInfoPanel(true);

            UIManager.Instance.SetAccountInformationPanel(false);

            UIContentController.Instance.UpdateReservationInfoPanel(newReservation);

        });

        TMP_Text restaurantName = newReservationUI.GetComponentsInChildren<TMP_Text>()[0];

        TMP_Text reservationID = newReservationUI.GetComponentsInChildren<TMP_Text>()[1];

        reservationID.text = "#" + key;

        restaurantName.text = newReservation.restaurant;
    }

    public void UpdateName() 
    {
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        UIManager.Instance.displayedName.text = DataManager.Instance.data.userInfo[userID].firstName + " " +
            DataManager.Instance.data.userInfo[userID].lastName;
    }


    public void EmptyReservationList() 
    {
        for (var i = UIManager.Instance.reservationList.gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(UIManager.Instance.reservationList.gameObject.transform.GetChild(i).gameObject);
        }
    }
}
