using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.DataAccess.Repositories
{
    public interface IDndPlayerRepository
    {
        Task AddAsync(Player entity);
        Task DeleteAsync(Player entity);
        Task EditAsync(Player entity);
        Task<Player> GetPlayerById(ulong id);
    }
}