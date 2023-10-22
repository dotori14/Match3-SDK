using Common.Interfaces;
using Match3.App;
using Match3.App.Interfaces;
using Match3.Core.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShapeDetector : ISequenceDetector<IUnityGridSlot>
{
    private readonly GridPosition[][] _squareLookupDirections;

    public SquareShapeDetector()
    {
        _squareLookupDirections = new[]
        {
            new[] { GridPosition.Up, GridPosition.Left, GridPosition.Up + GridPosition.Left },
            new[] { GridPosition.Up, GridPosition.Right, GridPosition.Up + GridPosition.Right },
            new[] { GridPosition.Down, GridPosition.Left, GridPosition.Down + GridPosition.Left },
            new[] { GridPosition.Down, GridPosition.Right, GridPosition.Down + GridPosition.Right },
        };
    }

    public ItemSequence<IUnityGridSlot> GetSequence(IGameBoard<IUnityGridSlot> gameBoard, GridPosition gridPosition)
    {
        var sampleGridSlot = gameBoard[gridPosition];
        var resultGridSlots = new List<IUnityGridSlot>(4);

        foreach (var lookupDirections in _squareLookupDirections)
        {
            foreach (var lookupDirection in lookupDirections)
            {
                var lookupPosition = gridPosition + lookupDirection;
                if (gameBoard.IsPositionOnBoard(lookupPosition) == false)
                {
                    break;
                }

                var lookupGridSlot = gameBoard[lookupPosition];
                if (lookupGridSlot.HasItem == false)
                {
                    break;
                }

                if (lookupGridSlot.Item.ContentId == sampleGridSlot.Item.ContentId)
                {
                    resultGridSlots.Add(lookupGridSlot);
                }
            }

            if (resultGridSlots.Count == 3)
            {
                resultGridSlots.Add(sampleGridSlot);
                break;
            }

            resultGridSlots.Clear();
        }

        return resultGridSlots.Count > 0 ? new ItemSequence<IUnityGridSlot>(GetType(), resultGridSlots) : null;
    }
}