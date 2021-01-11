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
        public Board<Modals.Piece> Board { get; } = new Board<Modals.Piece>(3);

        StateMachine<GameStateBase> _stateMachine;

        private void ConnectPieceViews(MoveManager<Modals.Piece> moveManager)
        {
            var pieceViews = FindObjectsOfType<PieceView>();

            foreach (var pieceView in pieceViews)
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = BoardPositionHelper.WorldToBoardPosition(worldPosition);
                var tile = Board.TileAt(boardPosition);

                var piece = new Modals.Piece(pieceView.PlayerID, pieceView.MovementName);
                Board.Place(tile, piece);
                moveManager.Register(piece, pieceView.MovementName);
                pieceView.Modal = piece;
            }
        }

        private void Awake()
        {
            ReplayManager replayManager = new ReplayManager();
            _stateMachine = new StateMachine<GameStateBase>();

            var moveManager = new MoveManager<Modals.Piece>(Board);

            var playGameState = new PlayGameState(Board, moveManager);
            _stateMachine.RegisterState(GameStates.Play, playGameState);
            _stateMachine.RegisterState(GameStates.Replay, new ReplayGameState(replayManager));
            _stateMachine.MoveTo(GameStates.Play);

            moveManager.Register(PlayerMoveCommandProvider.Name, new PlayerMoveCommandProvider(playGameState, replayManager));

            ConnectMoveCommandProviderView(moveManager);
            ConnectPieceViews(moveManager);
            ConnectTileViews(Board);
            ConnectBoardView(Board);
        }

        private void ConnectBoardView(Board<Modals.Piece> board)
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.Modal = board;
        }

        private void ConnectTileViews(Board<Modals.Piece> board)
        {
            var tileViews = FindObjectsOfType<TileView>();

            foreach (var tileView in tileViews)
            {
                var tilePosition = BoardPositionHelper.WorldToBoardPosition(tileView.transform.position);
                tileView.Modal = board.TileAt(tilePosition);
            }
        }

        private void ConnectMoveCommandProviderView(MoveManager<Modals.Piece> moveManager)
        {
            var moveCommandProviderView = FindObjectOfType<MoveCommandProviderView>();
            moveManager.MoveComandProviderChanged += (sender, args) => moveCommandProviderView.Modal = args.MoveCommandProvider;
        }

        public void Select(Modals.Piece piece)
        {
            _stateMachine.CurrentSate.Select(piece);
        }

        public void Select(Tile tile)
        {
            _stateMachine.CurrentSate.Select(tile);
        }
        public void Select(IMoveCommand<Modals.Piece> moveComand)
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