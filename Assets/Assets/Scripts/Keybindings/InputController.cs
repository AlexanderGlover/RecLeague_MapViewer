using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    ApplicationPointer mPointer;

    [SerializeField]
    InputManager mInputManager;

    // Start is called before the first frame update
    void Start()
    {
        mPointer = new ApplicationPointer();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForPointerUpdate();
    }

    void CheckForPointerUpdate()
    {
        //Force state if keybinding is pressed
        if (mInputManager.GetKeyDown(KeybindingAction.KA_Ptr_Default))
        {
            UpdatePointerType(PointerTypes.PT_Default, true);
        }
        if (mInputManager.GetKeyDown(KeybindingAction.KA_Ptr_Select))
        {
            UpdatePointerType(PointerTypes.PT_Move, true);
        }
        if (mInputManager.GetKeyDown(KeybindingAction.KA_Ptr_Rotate))
        {
            UpdatePointerType(PointerTypes.PT_Rotate, true);
        }
        if (mInputManager.GetKeyDown(KeybindingAction.KA_Ptr_Draw))
        {
            UpdatePointerType(PointerTypes.PT_Draw, true);
        }
        if (mInputManager.GetKeyDown(KeybindingAction.KA_Ptr_Erase))
        {
            UpdatePointerType(PointerTypes.PT_Erase, true);
        }
    }

    public void UpdatePointerType(PointerTypes newType, bool forceType)
    {
        mPointer.SetPointerType(newType, forceType);

        mInputManager.SetActiveKeybindings(mPointer.GetPointerType());
    }


    public KeyCode GetKeyForAction(KeybindingAction keybindingAction) { return mInputManager.GetKeyForAction(keybindingAction); }

    public bool GetKeyDown(KeybindingAction key) { return mInputManager.GetKeyDown(key); }

    public bool GetKeyUp(KeybindingAction key) { return mInputManager.GetKeyUp(key); }

    public bool GetKey(KeybindingAction key) { return mInputManager.GetKey(key); }

    public PointerTypes GetPointerType() { return mPointer.GetPointerType(); }
}
