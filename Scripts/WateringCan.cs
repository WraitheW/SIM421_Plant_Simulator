using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Texture2D wateringCanCursor;

    public void RemoveCan()
    {
        Cursor.SetCursor(wateringCanCursor, Vector2.zero, CursorMode.Auto);
    }

    public void GrabCan()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }
}
