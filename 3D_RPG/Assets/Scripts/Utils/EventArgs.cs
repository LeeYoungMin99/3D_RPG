using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OnSlotClickEventArgs : EventArgs
{
    public CharacterData _characterData;
}

public class AutoButtonEventArgs : EventArgs
{
    public bool CurAuto;
}
