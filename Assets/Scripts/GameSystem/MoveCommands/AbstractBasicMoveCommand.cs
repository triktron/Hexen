using BoardSystem;
using Deck;
using GameSystem.Modals;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<Piece>
    {
        protected ReplayManager ReplayManager;
        public string _name;
        internal Card _card;

        protected AbstractBasicMoveCommand(ReplayManager replayManager, string name)
        {
            ReplayManager = replayManager;
            _name = name;
        }

        public virtual bool CanExecute(Board<Piece> board, Piece piece)
        {
            if (Tiles(board, piece).Count == 0) return false;
            return true;
        }

        public virtual void Execute(Board<Piece> board, Piece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            var fromTile = board.TileOf(piece);
            var hasMoved = piece.HasMoved;

            Action forward = () =>
            {
                if (toPiece != null)
                {
                    board.Take(toTile);
                }

                
                board.Move(fromTile, toTile);

                piece.HasMoved = true;
            };

            Action backward = () =>
            {
                piece.HasMoved = hasMoved;
                board.Move(toTile, fromTile);
                if (toPiece != null)
                {
                    board.Place(toTile, toPiece);
                }
            };

            var replayComand = new DelegateReplayComand(forward, backward);

            ReplayManager.Execute(replayComand);
        }

        public abstract List<Tile> Tiles(Board<Piece> board, Piece piece);
        public abstract List<Tile> Action(Board<Piece> board, Piece piece, Tile tile);

        public Card GetCard()
        {
            return _card;
        }

        public void SetCard(Card card)
        {
            _card = card;
        }

        public string GetName()
        {
            return _name;
        }
    }
}