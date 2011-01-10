using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
    public delegate byte[] ResponseSystemMessageDelegate(object sender, object args);

    public delegate byte[] ResponseRoomMessageDelegate(object sender, object args);
}
