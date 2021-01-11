using StateSystem;
using GameSystem.Modals;
using BoardSystem;
using MoveSystem;

namespace GameSystem.States
{
    public abstract class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { set; get; }

        virtual public void OnEnter()
        {
        }

        virtual public void OnExit()
        {
        }

        virtual public void Select(Tile tile)
        {
        }
        virtual public void Select(IMoveCommand<Modals.Piece> moveComand)
        {
        }
        virtual public void Forward()
        {
        }

        virtual public void Backward()
        {
        }

        virtual public void Hover(Tile tile)
        {

        }
    }
}