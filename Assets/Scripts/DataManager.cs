using System.Collections.Generic;
using System.Collections;
using Firebase.Database;
using Newtonsoft.Json;
using Firebase.Auth;
using UnityEngine;
using System.Xml;



public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public SavedData data;

    public DatabaseReference databaseRef;

   
    private void Awake()
    {
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;



        Instance = this;

    }

    public void UploadData() 
    {
        string json = JsonConvert.SerializeObject(data);

        databaseRef.SetRawJsonValueAsync(json);

        Debug.Log(json);
    }

    public void DownloadData() 
    {
        StartCoroutine(DownloadDataEnum());
    }

    private IEnumerator DownloadDataEnum()
    {
        data = new SavedData();

        var serverData = databaseRef.GetValueAsync();

        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        DataSnapshot snapshot = serverData.Result;

        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            data = JsonConvert.DeserializeObject<SavedData>(jsonData);
        }
        else
        {
            Debug.Log("No data has been found!");
        }
    }
}

public class Reservation {

    public int atendees = 0, tableNo = 0;

    public int day = 0, month = 0;

    public int hours = 0, minutes = 0;

    public string location = "";

    public string restaurant;

}

public class User
{
    public string firstName, lastName;

    public string phone;
}

public class ReservationByUserID
{

    public Dictionary<string, Reservation> reservations;
}

public class ReservationByRestaurant
{

    public Dictionary<string, Reservation> reservations;
}

public class SavedData
{

    public Dictionary<string, ReservationByUserID> reservationByUserID;

    public Dictionary<string, ReservationByRestaurant> reservationByRestaurant;

    public Dictionary<string, User> userInfo;
}

