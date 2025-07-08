using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UndoMove
{
    public GameObject Object;
    public Vector3 PreviousPosition;

    public UndoMove(GameObject obj, Vector3 position)
    {
        Object = obj;
        PreviousPosition = position;
    }
}

public static class UndoManager
{
    public static Stack<UndoMove> undoMoveStack = new Stack<UndoMove>();
    public static Stack<GameObject> undoDrawStack = new Stack<GameObject>();

    public static void PushMoveEvent(UndoMove moveData)
    {
        undoMoveStack.Push(moveData);
    }

    public static void PushDrawEvent(GameObject drawObject)
    {
        undoDrawStack.Push(drawObject);
    }

    public static UndoMove PopMoveEvent()
    {
        if (undoMoveStack.Count > 0)
        {
            return undoMoveStack.Pop();
        }
        return default; // Return default if stack is empty
    }

    public static GameObject PopDrawEvent()
    {
        if (undoMoveStack.Count > 0)
        {
            return undoDrawStack.Pop();
        }
        return default; // Return default if stack is empty
    }
}
