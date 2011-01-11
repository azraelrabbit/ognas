using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Cards;
using Platform.OgnasEventArgs;

namespace Platform.Model
{
    #region Game about
    /// <summary>
    /// 游戏开始事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void GameStarted(object sender, GameEventArgs args);

    public delegate void ShuffleCardComplete(object sender, GameEventArgs args);

    public delegate void SetUpSeatComplete(object sender, GameEventArgs args);

    public delegate void SetUpUserRoleComplete(object sender, GameEventArgs args);

    public delegate void DealCardBegin(object sender, DealCardsArgs args);

    public delegate void SendMessageToUserClient(object sender, SendMessageArgs args);

    public delegate void ShogunSelectionBegin(object sender, ShogunSelectArgs args);

    #endregion Game about

    #region User action about
    /// <summary>
    /// 用户出牌事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void UserCardUse(object sender, CardUseEventArgs args);

    /// <summary>
    /// 摸牌事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void UserGetCards(object sender, DealCardsArgs args);

    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void UserDropCards(object sender, DealCardsArgs args);

    #endregion


    public class EventCenter
    {
        //public event void InitializePlay;
    }
}
