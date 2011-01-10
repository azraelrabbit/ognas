﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Enum
{
    public enum SystemMessage : byte
    {
        None,
        RegisterUserName,
        CreateRoom,
        ServerRoomPort,
        EnterRoom,
        Other
    }
}