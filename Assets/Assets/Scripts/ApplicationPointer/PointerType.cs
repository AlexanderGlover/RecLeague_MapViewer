using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointerTypes
{
    PT_NONE,    // 0
    PT_Default, // 1
    PT_Move,    // 2
    PT_Rotate,  // 3
    PT_Draw,    // 4
    PT_Erase    // 5
}

[System.Serializable]
public class PointerType
{
    [SerializeField]
    PointerTypes mPointerType;
    [SerializeField]
    Keybindings mKeybind;
    [SerializeField]
    Texture2D mCursor;


    public PointerType(PointerTypes type, Keybindings keybinding, Texture2D cursor)
    {
        mPointerType = type;
        mKeybind = keybinding;
        mCursor = cursor;
    }

    public PointerTypes GetPointerType() { return mPointerType; }
    public Keybindings GetKeybindings() { return mKeybind; }
    public Texture2D GetPointerSprite() { return mCursor; }
}