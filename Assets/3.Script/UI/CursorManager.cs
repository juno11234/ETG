using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    Texture2D cursor;   
    
    void Start()
    {
        ChangeCursor(cursor);
    }

    public void ChangeCursor(Texture2D newcursor)
    {
        Vector2 hotpot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(newcursor, hotpot, CursorMode.Auto);
    }
   
}
