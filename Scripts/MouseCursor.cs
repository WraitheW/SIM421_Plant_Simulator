using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite defaultCursor;
    public Sprite canCursor;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = defaultCursor;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

    }

    public void setAsCan()
    {
        rend.sprite = canCursor;
    }

    public void setAsDefault()
    {
        rend.sprite = defaultCursor;
    }
}
