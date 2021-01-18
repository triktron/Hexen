using BoardSystem;
using Deck;
using GameSystem.Modals;
using GameSystem.MoveCommandsProviders;
using GameSystem.States;
using GameSystem.Utils;
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
            var enemyPlacer = GetComponent<RandomEnemyPlacer>();
            enemyPlacer.PlaceRandomEnemies();

            ReplayManager replayManager = new ReplayManager();
            _stateMachine = new StateMachine<GameStateBase>();

            var moveManager = new MoveManager<Modals.Piece>(Board);

            var playGameState = new PlayGameState(Board, moveManager);
            _stateMachine.RegisterState(GameStates.EnemyPhase1, new EnemyPhase1GameState());
            _stateMachine.RegisterState(GameStates.EnemyPhase2, new EnemyPhase2GameState());
            _stateMachine.RegisterState(GameStates.Play, playGameState);


            moveManager.Register(PlayerMoveCommandProvider.Name, new PlayerMoveCommandProvider(playGameState, replayManager));

            ConnectMoveCommandProviderView(moveManager);
            ConnectPieceViews(moveManager);
            ConnectTileViews(Board);
            ConnectBoardView(Board);

            _stateMachine.MoveTo(GameStates.EnemyPhase1);
        }

        private void ConnectBoardView(Board<Modals.Piece> board)
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.Modal = board;
            board.Deck = FindObjectOfType<DeckSystem>();
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

        public void Select(Tile tile)
        {
            _stateMachine.CurrentSate.Select(tile);
        }
        public void Select(IMoveCommand<Modals.Piece> moveComand)
        {
            _stateMachine.CurrentSate.Select(moveComand);
        }
        public void Hover(Tile tile)
        {
            _stateMachine.CurrentSate.Hover(tile);
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