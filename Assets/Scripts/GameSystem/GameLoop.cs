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
        

        public event EventHandler Initialized;
        public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

        StateMachine<GameStateBase> _stateMachine;

        private void Start()
        {
            ConnectViewaToModal();

            StartCoroutine(OnPostStart());
        }

        private IEnumerator OnPostStart()
        {
            yield return new WaitForEndOfFrame();

            OnInitialized(EventArgs.Empty);
        }

        private void ConnectViewaToModal()
        {
            var pieceViews = FindObjectsOfType<ChessPieceView>();

            foreach (var pieceView in pieceViews)
            {
                var worldPosition = pieceView.transform.position;
                var boardPosition = BoardPositionHelper.WorldToBoardPosition(worldPosition);
                var tile = Board.TileAt(boardPosition);

                var piece = new ChessPiece(pieceView.PlayerID, pieceView.FacingBack);
                Board.Place(tile, piece);
                MoveManager.Register(piece, pieceView.MovementName);
                pieceView.Modal = piece;
            }
        }


        public MoveManager<ChessPiece> MoveManager { get; internal set; }

        private void Awake()
        {
            ReplayManager replayManager = new ReplayManager();
            _stateMachine = new StateMachine<GameStateBase>();

            MoveManager = new MoveManager<ChessPiece>(Board);
            var playGameState =  new PlayGameState(Board, MoveManager);
            _stateMachine.RegisterState(GameStates.Play, playGameState);
            _stateMachine.RegisterState(GameStates.Replay, new ReplayGameState(replayManager));
            _stateMachine.MoveTo(GameStates.Play);

            
            MoveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider(playGameState, replayManager));
            MoveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider(playGameState, replayManager));
            MoveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider(playGameState, replayManager));
            MoveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider(playGameState, replayManager));
            MoveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider(playGameState, replayManager));
            MoveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider(playGameState, replayManager));
        }

        protected virtual void OnInitialized(EventArgs arg)
        {
            EventHandler handler = Initialized;
            handler?.Invoke(this, arg);
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