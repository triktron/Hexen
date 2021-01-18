using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using ReplaySystem;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class PlayerTeleportMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerTeleportMoveCommand(ReplayManager replayManager) : base(replayManager, "Teleport")
        {
        }

        public override List<Tile> Tiles(Board<Piece> board, Piece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .All(MovementHelper.IsEmpty)
                .Generate();

            return validTiles;
        }

        public override List<Tile> Action(Board<Piece> board, Piece _piece, Tile tile)
        {
            var validTiles = new List<Tile>() { tile };

            return validTiles;
        }
    }
}
