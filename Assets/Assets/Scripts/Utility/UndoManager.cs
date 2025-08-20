using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

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
    public static Stack<StrokeUndo> undoDrawStack = new Stack<StrokeUndo>();

    public static void PushMoveEvent(UndoMove moveData)
    {
        undoMoveStack.Push(moveData);
    }

    public static void PushDrawEvent(StrokeUndo drawObject)
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
}
