using Common.Enums;
using Common.Interfaces;
using Match3.Core.Enums;
using Match3.Core.Models;

namespace Common
{
    public class GameBoardAgreements : IGameBoardAgreements
    {
        public bool CanSetItem(GridSlot<IUnityItem> gridSlot, TileGroup tileGroup)
        {
            var isAvailableTile = tileGroup == TileGroup.Available || tileGroup == TileGroup.Ice;
            return isAvailableTile && gridSlot.State == GridSlotState.Empty;
        }

        public bool IsLockedSlot(TileGroup tileGroup)
        {
            return tileGroup != TileGroup.Available;
        }

        public bool IsMovableSlot(GridSlot<IUnityItem> gridSlot, TileGroup tileGroup)
        {
            if (IsLockedSlot(tileGroup))
            {
                return false;
            }

            return gridSlot.State == GridSlotState.Occupied;
        }

        public bool IsAvailableSlot(GridSlot<IUnityItem> gridSlot, TileGroup tileGroup)
        {
            if (IsLockedSlot(tileGroup))
            {
                return false;
            }

            return gridSlot.State != GridSlotState.NotAvailable;
        }
    }
}