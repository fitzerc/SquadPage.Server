using SquadPage.Shared.Models;

namespace SquadPage.Shared.DataInterfaces
{
    public interface IDataAccess
    {
        SquadInfoResp GetSquadInfo();
        IEnumerable<SquadInfoResp> GetSquads();
        IEnumerable<GameDayInfo> GetGameDays(Int64 squadId);
    }
}
