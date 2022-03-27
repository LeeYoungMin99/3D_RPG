using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OnSlotClickEventArgs : EventArgs
{
    public CharacterData CharacterData;
}

public class AutoButtonEventArgs : EventArgs
{
    public bool CurAuto;
}

public class DeathEventArgs : EventArgs
{
    public GameObject GameObject;
    public int Experience;
}