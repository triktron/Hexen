using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveCommandsProviders;
using GameSystem.States;
using GameSystem.Views;
using MoveSystem;
using ReplaySystem;
using StateSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace GameSystem
{
    public class GameLoop : MonoBehaviourSingleton<GameLoop>
    {
        public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

        StateMachine<GameStateBase> _stateMachine;

        private void ConnectChessPieceViews(MoveManager<ChessPiece> moveManager)
        {
            var pieceViews = FindObjectsOfType<ChessPieceView>();

            foreach (var pieceView in pieceViews)
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = BoardPositionHelper.WorldToBoardPosition(worldPosition);
                var tile = Board.TileAt(boardPosition);

                var piece = new ChessPiece(pieceView.PlayerID, pieceView.FacingBack, pieceView.MovementName);
                Board.Place(tile, piece);
                moveManager.Register(piece, pieceView.MovementName);
                pieceView.Modal = piece;
            }
        }

        private void Awake()
        {
            ReplayManager replayManager = new ReplayManager();
            _stateMachine = new StateMachine<GameStateBase>();

            var moveManager = new MoveManager<ChessPiece>(Board);

            var playGameState = new PlayGameState(Board, moveManager);
            _stateMachine.RegisterState(GameStates.Play, playGameState);
            _stateMachine.RegisterState(GameStates.Replay, new ReplayGameState(replayManager));
            _stateMachine.MoveTo(GameStates.Play);

            moveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider(playGameState, replayManager));
            moveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider(playGameState, replayManager));
            moveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider(playGameState, replayManager));
            moveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider(playGameState, replayManager));
            moveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider(playGameState, replayManager));
            moveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider(playGameState, replayManager));

            ConnectMoveCommandProviderView(moveManager);
            ConnectChessPieceViews(moveManager);
            ConnectTileViews(Board);
            ConnectBoardView(Board);
        }

        private void ConnectBoardView(Board<ChessPiece> board)
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.Modal = board;
        }

        private void ConnectTileViews(Board<ChessPiece> board)
        {
            var tileViews = FindObjectsOfType<TileView>();

            foreach (var tileView in tileViews)
            {
                var tilePosition = BoardPositionHelper.WorldToBoardPosition(tileView.transform.position);
                tileView.Modal = board.TileAt(tilePosition);
            }
        }

        private void ConnectMoveCommandProviderView(MoveManager<ChessPiece> moveManager)
        {
            var moveCommandProviderView = FindObjectOfType<MoveCommandProviderView>();
            moveManager.MoveComandProviderChanged += (sender, args) => moveCommandProviderView.Modal = args.MoveCommandProvider;
        }

        public void Select(ChessPiece chessPiece)
        {
            _stateMachine.CurrentSate.Select(chessPiece);
        }

        public void Select(Tile tile)
        {
            _stateMachine.CurrentSate.Select(tile);
        }
        public void Select(IMoveCommand<ChessPiece> moveComand)
        {
            _stateMachine.CurrentSate.Select(moveComand);
        }
        public void Forward()
        {
            _stateMachine.CurrentSate.Forward();
        }

        public void Backward()
        {
            _stateMachine.CurrentSate.Backward();
        }
    }

}