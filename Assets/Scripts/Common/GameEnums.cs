﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnums
{
    public enum EventID
    {
        OnLogoTweenCompleted,
        OnStartGame,
        OnReceiveNotiParam,
        OnReceiveQuestInfo,
        OnTrackedImageSuccess,
        OnRefreshLobby,
        OnCheckGameplayState, //lock/unlock gameplay dựa trên số ng
        OnCanPlay, //bắn sau khi đã đặt tên thành công
        OnRefreshLeaderboard,
        OnUpdatePlayerData, //gửi thông tin player để cập nhật bxh
        OnReceiveQuest, //từ host gửi cho tất cả player quest mới

    }

    public enum EPopupID
    {
        PopupHint = 0,
        PopupLeaderboard = 1,
        PopupSetting = 2,
        PopupLeaveGame = 3,
        PopupInformation = 4,
        PopupReward = 5,
        PopupLobby = 6,
        PopupConfigRoom = 7,
        PopupShop = 8,
        PopupEnterName = 9,

    }

    public enum ESoundName
    {
        BGM,
        Button1SFX,
        Button2SFX,
    }
}
