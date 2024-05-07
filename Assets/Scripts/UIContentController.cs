using TMPro;
using UnityEngine;

public class UIContentController : MonoBehaviour
{

    public void UpdateReservationInfoPanel() 
    {
       
    }

    public void UpdateMakeReservationPanel(TMP_Text name) 
    {
        UIManager.Instance.restaurantName.text = name.text;
    }

    public void CheckIfReservationAvailable() 
    {
        
    }
    
    public void UpdateReservationListPanel() 
    {
        
    }

}
