using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using System.Net;


public class DataManager : MonoBehaviour
{
    public static SavedData data;

    DatabaseReference databaseRef;

    private void Awake()
    {
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void UploadData() 
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        string json = JsonUtility.ToJson(data);

        databaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public void DownloadData() 
    {
        StartCoroutine(DownloadDataEnum());
    }

    private IEnumerator DownloadDataEnum()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var serverData = databaseRef.Child("users").Child(userId).GetValueAsync();

        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("Download completed!");

        DataSnapshot snapshot = serverData.Result;

        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            data = JsonUtility.FromJson<SavedData>(jsonData);
        }
        else
        {
            Debug.Log("No data has been found!");
        }
    }
}

public class Reservation 
{
    public int atendees, tableNo;

    public int day, month;

    public int hours, minutes;

    public string location;

    public string[] foodOrdered;

}

public class ProfileInfo {

    public string firstName, lastName;

    public string phone;
}

public class SavedData {

    public Dictionary<string, Reservation> reservations;

    public ProfileInfo profileInfo = new ProfileInfo();
}