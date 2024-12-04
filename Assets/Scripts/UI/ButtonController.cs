using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public enum ButtonIndex
{
    Start = 0,
    Tutorial = 1,
    Settings = 2,
    About = 3
}

public class ButtonController : MonoBehaviour
{
    public void OnClick(int index)
    {
        SoundsManager.Instance.PlaySfx(ESoundName.Button1SFX);
        switch (index) 
        {
            case (int)ButtonIndex.Start:
                UIManager.Instance.TogglePopup(EPopupID.PopupLobby, true);
                break;

            case (int)ButtonIndex.Tutorial:

                break;

            case (int)ButtonIndex.Settings:

                break;

            case (int)ButtonIndex.About:

                break;
        }
    }
}
