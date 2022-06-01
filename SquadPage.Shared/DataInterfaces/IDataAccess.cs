﻿using SquadPage.Shared.Models;

namespace SquadPage.Shared.DataInterfaces
{
    public interface IDataAccess
    {
        SquadInfoResp GetSquadInfo(Int64 squadId);
        IEnumerable<SquadInfoResp> GetSquads();
        SquadInfoResp GetSquadInfoByNumber(int squadNumber);

        IEnumerable<GameDayInfo> GetGameDays(Int64 squadId);
        GameDayInfo GetGameDay(Int64 gameDayId);
        GameDayDetails GetGameDayDetails(Int64 gameDayId, Int64 squadId);
        IEnumerable<GameDayDetails> GetGamesDetailsBySquad(Int64 squadId);
    }
}
