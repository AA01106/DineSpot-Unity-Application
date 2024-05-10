using UnityEngine.UI;
using UnityEngine;
using System;
using Firebase.Auth;
using System.Collections.Generic;


public class ReservationController : MonoBehaviour
{
    public static ReservationController Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void AddNewReservation() 
    {

        ReservationByUserID newReservationByUserID = new ReservationByUserID();

        ReservationByRestaurant newReservationByRestaurant = new ReservationByRestaurant();

        newReservationByUserID.reservations = new Dictionary<string, Reservation>();

        newReservationByRestaurant.reservations = new Dictionary<string, Reservation>();

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

        newReservationByRestaurant.reservations.Add(key, newReservation);

        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        DataManager.Instance.DownloadData();

        if (DataManager.Instance.data.reservationByUserID == null)
            DataManager.Instance.data.reservationByUserID = new Dictionary<string, ReservationByUserID>();

        if (DataManager.Instance.data.reservationByRestaurant == null)
            DataManager.Instance.data.reservationByRestaurant = new Dictionary<string, ReservationByRestaurant>();

        if (!DataManager.Instance.data.reservationByUserID.TryAdd(userId, newReservationByUserID)) 
        {
            if (!DataManager.Instance.data.reservationByUserID[userId].reservations.TryAdd(key, newReservation))
            {
                UIManager.Instance.available.text = "Table is not available!";

            }
        }

        if (!DataManager.Instance.data.reservationByRestaurant.TryAdd(newReservation.restaurant, newReservationByRestaurant))
        {
            DataManager.Instance.data.reservationByUserID[newReservation.restaurant].reservations.Add(key, newReservation);
        }

        DataManager.Instance.UploadData();

        UIManager.Instance.SetMakeReservationPanel(false);

        UIManager.Instance.SetRestaurantListPanel(true);

        UIContentController.Instance.AddNewReservationUI(key, newReservation);

        UIManager.Instance.TableNo.selectedText.text = "1";

        UIManager.Instance.day.text = "";

        UIManager.Instance.month.text = "";

        UIManager.Instance.hours.text = "";

        UIManager.Instance.minutes.text = "";
    }

    public void DeleteReservation(string key) 
    {
        DataManager.Instance.databaseRef.Child("reservationByUserID").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child(key).RemoveValueAsync();

        Reservation reservation = DataManager.Instance.data.reservationByUserID[FirebaseAuth.DefaultInstance.CurrentUser.UserId].reservations[key];

        DataManager.Instance.databaseRef.Child("reservationByRestaurant").Child(reservation.restaurant).Child(key).RemoveValueAsync();
    }
}
