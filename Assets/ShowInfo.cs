using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
