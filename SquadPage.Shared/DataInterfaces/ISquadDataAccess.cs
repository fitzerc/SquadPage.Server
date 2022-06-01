using SquadPage.Shared.Models;

namespace SquadPage.Shared.DataInterfaces;

public interface ISquadDataAccess
{
    SquadInfoResp GetSquadInfo(Int64 squadId);
    IEnumerable<SquadInfoResp> GetSquads();
    SquadInfoResp GetSquadInfoByNumber(int squadNumber);
}