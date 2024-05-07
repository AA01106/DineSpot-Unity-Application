using UnityEngine.UI;
using UnityEngine;
using System;
using Firebase.Auth;
using System.Collections.Generic;


public class ReservationController : MonoBehaviour
{
    public void AddNewReservation() 
    {
        GameObject newReservationUI = Instantiate(UIManager.Instance.reservtaionListItems);

        newReservationUI.transform.SetParent( UIManager.Instance.reservationList);

        newReservationUI.transform.localScale = Vector3.one;

        newReservationUI.GetComponentInChildren<Button>().onClick.AddListener( () => {

            UIManager.Instance.SetReservationInfoPanel(true);

            UIManager.Instance.SetAccountInformationPanel(false);
        
        });

        ReservationByUserID newReservationByUserID = new ReservationByUserID();

        newReservationByUserID.reservations = new Dictionary<string, Reservation>();

        Reservation newReservation = new Reservation();

        newReservation.tableNo = Int32.Parse(UIManager.Instance.TableNo.selectedText.text);

        newReservation.atendees = Int32.Parse(UIManager.Instance.Atendees.selectedText.text);

        newReservation.location = UIManager.Instance.Atendees.selectedText.text;

        newReservation.restaurant = UIManager.Instance.restaurantName.text;

        newReservation.day = Int32.Parse(UIManager.Instance.day.text);

        newReservation.month = Int32.Parse(UIManager.Instance.month.text);

        newReservation.hours = Int32.Parse(UIManager.Instance.hours.text);

        newReservation.minutes = Int32.Parse(UIManager.Instance.minutes.text);

        string key = String.Format("{0}{1}{2}{3}", newReservation.month, newReservation.day,
            newReservation.hours, newReservation.minutes);

        newReservationByUserID.reservations.Add(key, newReservation);

        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        if (DataManager.Instance.data.reservationByUserID == null)
            DataManager.Instance.data.reservationByUserID = new Dictionary<string, ReservationByUserID>();

        DataManager.Instance.data.reservationByUserID.Add(userId, newReservationByUserID);

        DataManager.Instance.UploadData();
    }

    public void EditReservation() 
    {
        
    }

    public void DeleteReservation() 
    {
        
    }
}
