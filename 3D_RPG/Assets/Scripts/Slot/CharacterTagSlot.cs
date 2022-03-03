using System.Collections.Generic;
using UnityEngine;

public class CharacterTagSlot : CharacterSlot
{
    private int _index;

    protected override void OnClick()
    {
        if (Character == null)
        {
            return;
        }

        transform.SetAsFirstSibling();

        _characterInventorySlotManager.OnClickTagSlot();
    }

    public void SetIndex()
    {
        if (Character == null)
        {
            return;
        }

        _index = transform.GetSiblingIndex();

        _image.sprite = Resources.Load<Sprite>($"Images/Tag Slot/{Character.Name}");

        if (0 == _index)
        {
            Character.CharacterPwan.SetActive(true);
        }
        else
        {
            Character.CharacterPwan.SetActive(false);
        }
    }

}
