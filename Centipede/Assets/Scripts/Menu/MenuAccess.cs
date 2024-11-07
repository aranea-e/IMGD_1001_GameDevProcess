using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAccess : MonoBehaviour
{
    
    public void ButtonPress(int i) 
    {
        FindObjectOfType<MenuManager>().LoadScene(i, true);
        
    }
}
