using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Enums
{
    public enum SystemMessage : byte
    {
        None,
        RegisterUser,
        CreateRoom,
        ServerRoomPort,
        EnterRoom,
        UdpMessage,
        ExitRoom,
        Other,
        DealSeat,
        DealCards,
        DealRole,
        SelectShogun,
        StateChange
    }
}