using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAPI
{
    public interface IGameScreen : IMessenger, IScreen
    {
        event Action<StepInfo> Step;

        void StartGame();
        void UpdateField(IReadOnlyField f);
        void SetRender(IRender r);
        void Message(string msg);
        void GameOver(bool win);
    }

    //public abstract class AbstractGameScreen : UserControl, IMessenger
    //{
    //    public event Action<StepInfo> Step;

    //    public abstract void StartGame();
    //    public abstract void UpdateField(IField<IChessFigure> f);
    //    public abstract void SetRender(IRender r);
    //    public abstract void Message(string msg);
    //    public abstract void GameOver(bool win);

    //    public abstract event Action<string> Send;

    //    public abstract void Receive(string message);
    //}

}
