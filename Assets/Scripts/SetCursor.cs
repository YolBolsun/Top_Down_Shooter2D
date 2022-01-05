using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
#pragma warning disable
    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private Texture2D cursorTextureLowRes = null;
#pragma warning enable
    [SerializeField] private Vector2 hotSpot;
    [SerializeField] private CursorMode cursorMode;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        if (cursorTextureLowRes)
        {
            float xspot = cursorTextureLowRes.width / 2;
            float yspot = cursorTextureLowRes.height / 2;
            Vector2 hotSpot = new Vector2(xspot, yspot);
            Cursor.SetCursor(cursorTextureLowRes, hotSpot, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(cursorTextureLowRes, hotSpot, CursorMode.ForceSoftware);
        }
#else
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
