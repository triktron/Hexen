using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveCommandsProviders;
using GameSystem.Views;
using MoveSystem;
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
        private ChessPiece _selectedPiece;
        public ChessPiece SelectedPiece => _selectedPiece;

        public event EventHandler Initialized;
        public Board<ChessPiece> Board { get; } = new Board<ChessPiece>(8, 8);

        private int _currentPlayerID;
        public int CurrentPlayerID => _currentPlayerID;

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
            MoveManager = new MoveManager<ChessPiece>(Board);
            MoveManager.Register(PawnMoveCommandProvider.Name, new PawnMoveCommandProvider());
            MoveManager.Register(KnightMoveCommandProvider.Name, new KnightMoveCommandProvider());
            MoveManager.Register(BishopMoveCommandProvider.Name, new BishopMoveCommandProvider());
            MoveManager.Register(RookMoveCommandProvider.Name, new RookMoveCommandProvider());
            MoveManager.Register(KingMoveCommandProvider.Name, new KingMoveCommandProvider());
            MoveManager.Register(QueenMoveCommandProvider.Name, new QueenMoveCommandProvider());

            MoveManager.MoveComandProviderChanged += OnMoveComandManagerChanged;
        }

        public void Select(ChessPiece chessPiece)
        {
            if (chessPiece == null || _selectedPiece == chessPiece) return;

            if (chessPiece != null && chessPiece.PlayerID != CurrentPlayerID)
            {
                var tile = Board.TileOf(chessPiece);
                Select(tile);
                return;
            }

            
            MoveManager.Deactivate();


            _selectedPiece = chessPiece;

            MoveManager.ActivateFor(_selectedPiece);

            //Board.Highlight(MoveManager.Tiles());
        }

        public void Select(Tile tile)
        {
            if (_selectedPiece != null && _currentMoveComand != null)
            {
                var tiles = _currentMoveComand.Tiles(Board, _selectedPiece);
                if (!tiles.Contains(tile)) return;

                Board.UnHightlight(tiles);

                _currentMoveComand.Execute(Board, _selectedPiece, tile);

                MoveManager.Deactivate();

                _selectedPiece = null;
                _currentMoveComand = null;


                _currentPlayerID++;
                if (_currentPlayerID >= 2) _currentPlayerID = 0;
            }
        }

        private IMoveCommand<ChessPiece> _currentMoveComand;
        public void Select(IMoveCommand<ChessPiece> moveComand)
        {
            if (_currentMoveComand != null)
                Board.UnHightlight(_currentMoveComand.Tiles(Board, _selectedPiece));

            _currentMoveComand = moveComand;

            if (_currentMoveComand != null)
                Board.Highlight(_currentMoveComand.Tiles(Board, _selectedPiece));
        }

        protected virtual void OnInitialized(EventArgs arg)
        {
            EventHandler handler = Initialized;
            handler?.Invoke(this, arg);
        }

        private void OnMoveComandManagerChanged(object sender, MoveCommandProviderChanged<ChessPiece> e)
        {
            if (_currentMoveComand == null || _selectedPiece == null) return;
            var tiles = _currentMoveComand.Tiles(Board, _selectedPiece);

            Board.UnHightlight(tiles);
        }
    }

}