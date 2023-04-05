using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationPointer
{

    private PointerTypes currentPointerType = PointerTypes.PT_Default;

    private static PointerType[] applicationPointers = new PointerType[]
    {
        new PointerType(PointerTypes.PT_Default, Resources.Load<Keybindings>("PtrInfo/Keybindings/KB_Default"), Resources.Load<Texture2D>("PtrInfo/Sprites/Selector")),
        new PointerType(PointerTypes.PT_Draw, Resources.Load<Keybindings>("PtrInfo/Keybindings/KB_Default"), Resources.Load<Texture2D>("PtrInfo/Sprites/Pencil")),
        new PointerType(PointerTypes.PT_Erase, Resources.Load<Keybindings>("PtrInfo/Keybindings/KB_Default"), Resources.Load<Texture2D>("PtrInfo/Sprites/TestingPointerSize")),
        new PointerType(PointerTypes.PT_Move, Resources.Load<Keybindings>("PtrInfo/Keybindings/KB_Default"), Resources.Load<Texture2D>("PtrInfo/Sprites/Selector")),
        new PointerType(PointerTypes.PT_Rotate, Resources.Load<Keybindings>("PtrInfo/Keybindings/KB_Default"), Resources.Load<Texture2D >("PtrInfo/Sprites/Rotate")),
    };

    //Managing Pointer Type State
    public PointerTypes GetPointerType() { return currentPointerType; }
    public void SetPointerType(PointerTypes newPointerType, bool forceType = false)
    {
        //Set to default if trying to re-set the pointer type (toggle) unless forcing type (ie. through keybinding)
        if(currentPointerType == newPointerType && !forceType)
        {
            currentPointerType = PointerTypes.PT_Default;
            SetPointerSprite();
        }
        else
        {
            currentPointerType = newPointerType;
            SetPointerSprite();
        }
    }

    private void SetPointerSprite()
    {
        /*
        Texture2D cursor = GetCursor(currentPointerType);

        //set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(cursor.width/2, cursor.height / 2);

        //Future Work:: Update Pointer Sprite to indicate current state!
#if UNITY_WEBGL
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorSprite.texture, cursorOffset, CursorMode.Auto);
#endif
        */
    }


    //Operation of Pointer
    public void Update()
    {

        //Future Work:: While in default mode update the pointer as modifier keys are pressed!
    }

    private Texture2D GetCursor(PointerTypes pointerType)
    {
        foreach (PointerType type in applicationPointers)
        {
            if (type.GetPointerType() == pointerType)
            {
                return type.GetPointerSprite();
            }
        }

        return applicationPointers[0].GetPointerSprite();
    }
}
