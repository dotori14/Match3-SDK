using Common.Enums;
using Common.GridTiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneState : StatefulGridTile
{
    private bool _isLocked = true;
    private bool _canContainItem;
    private int _group = (int)TileGroup.Stone;

    // Defines the tile group id.
    public override int GroupId => _group;

    // Prevents the block from move.
    public override bool IsLocked => _isLocked;

    // Prevents the item creation.
    public override bool CanContainItem => _canContainItem;

    // Occurs when all block states have completed.
    protected override void OnComplete()
    {
        _isLocked = false;
        _canContainItem = true;
        _group = (int)TileGroup.Available;
    }

    // Occurs when the block state is reset.
    protected override void OnReset()
    {
        _isLocked = true;
        _canContainItem = false;
        _group = (int)TileGroup.Stone;
    }
}