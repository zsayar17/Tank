using UnityEngine;

public enum CursorIconMode { AIM,DEFAULT}

public class MouseCursors
{
    private Texture2D cursor2D;

    private Vector2 cursorHotSpot;
    public MouseCursors()
    {
        //cursor2D = Resources.Load<Texture2D>("CursorsIcons/aim_blue");

        //cursorHotSpot = new Vector2(cursor2D.width / 2, cursor2D.height / 2);
    }

    public void SetCursor(CursorIconMode mode)
    {
        switch (mode)
        {
            case CursorIconMode.AIM:
                Cursor.SetCursor(cursor2D, cursorHotSpot, CursorMode.Auto);
                break;
            case CursorIconMode.DEFAULT:
                Cursor.SetCursor(null, cursorHotSpot, CursorMode.Auto);
                break;
        }
    }

}
