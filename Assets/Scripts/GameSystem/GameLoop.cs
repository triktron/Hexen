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
        public Board<Piece> Board { get; } = new Board<Piece>(3);

        StateMachine<GameStateBase> _stateMachine;

        private LayerMask _tileLayer;

        private void ConnectPieceViews(MoveManager<Piece> moveManager)
        {
            var pieceViews = FindObjectsOfType<PieceView>();

            foreach (var pieceView in pieceViews)
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = BoardPositionHelper.WorldToBoardPosition(worldPosition);
                var tile = Board.TileAt(boardPosition);

                var piece = new Piece(pieceView.PlayerID, pieceView.MovementName);
                Board.Place(tile, piece);
                moveManager.Register(piece, pieceView.MovementName);
                pieceView.Modal = piece;
            }
        }

        private void Awake()
        {
            var enemyPlacer = GetComponent<RandomEnemyPlacer>();
            enemyPlacer.PlaceRandomEnemies();

            _tileLayer = LayerMask.GetMask("Tiles");

            ReplayManager replayManager = new ReplayManager();
            _stateMachine = new StateMachine<GameStateBase>();

            var moveManager = new MoveManager<Piece>(Board);

            var playGameState = new PlayGameState(Board, moveManager);
            _stateMachine.RegisterState(GameStates.EnemyPhase1, new EnemyPhase1GameState(Board));
            _stateMachine.RegisterState(GameStates.EnemyPhase2, new EnemyPhase2GameState(Board));
            _stateMachine.RegisterState(GameStates.Play, playGameState);


            moveManager.Register(PlayerMoveCommandProvider.Name, new PlayerMoveCommandProvider(playGameState, replayManager));

            ConnectMoveCommandProviderView(moveManager);
            ConnectPieceViews(moveManager);
            ConnectTileViews(Board);
            ConnectBoardView(Board);

            _stateMachine.MoveTo(GameStates.EnemyPhase1);
        }

        private void Update()
        {
            _stateMachine.CurrentSate.Hover(GetTileUnderMouse());
        }

        private void ConnectBoardView(Board<Piece> board)
        {
            var boardView = FindObjectOfType<BoardView>();
            boardView.Modal = board;
            board.Deck = FindObjectOfType<DeckSystem>();
        }

        private void ConnectTileViews(Board<Piece> board)
        {
            var tileViews = FindObjectsOfType<TileView>();

            foreach (var tileView in tileViews)
            {
                var tilePosition = BoardPositionHelper.WorldToBoardPosition(tileView.transform.position);
                tileView.Modal = board.TileAt(tilePosition);
            }
        }

        private void ConnectMoveCommandProviderView(MoveManager<Piece> moveManager)
        {
            var moveCommandProviderView = FindObjectOfType<MoveCommandProviderView>();
            moveManager.MoveComandProviderChanged += (sender, args) => moveCommandProviderView.Modal = args.MoveCommandProvider;
        }

        public void Select()
        {
            _stateMachine.CurrentSate.Select(GetTileUnderMouse());
        }
        public void Select(IMoveCommand<Piece> moveComand)
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

        public Tile GetTileUnderMouse()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 100, _tileLayer))
            {
                var tileView = hitInfo.transform.GetComponent<TileView>();
                if (tileView == null) return null;

                return tileView.Modal;
            }

            return null;
        }
    }

}