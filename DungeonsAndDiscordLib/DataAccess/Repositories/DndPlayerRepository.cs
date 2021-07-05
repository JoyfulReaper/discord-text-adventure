using DiscordBotLib.Services;
using Microsoft.Extensions.Logging;
using DungeonsAndDiscordLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDiscordLib.DataAccess.Repositories
{
    public class DndPlayerRepository : Repository<Player>, IDndPlayerRepository
    {
        private readonly ILogger<DndPlayerRepository> _logger;

        public DndPlayerRepository(ISettings settings,
            ILogger<DndPlayerRepository> logger) : base(settings, logger)
        {
            _logger = logger;
        }

        public async Task<Player> GetPlayerById(ulong id)
        {
            var queryResult = await QuerySingleOrDefaultAsync<Player>($"SELECT d.* FROM {TableName} d " +
                $"INNER JOIN User u ON d.UserId = u.id" +
                $"WHERE u.UserId = @UserId",
                new { UserId = id });

            return queryResult;
        }

        public override async Task AddAsync(Player entity)
        {
            var queryResult = await QuerySingleAsync<ulong>($"INSERT INTO {TableName} (UserId, Level, Hp) " +
                $"VALUES (@UserId, @Level, @Hp); select last_insert_rowid();",
                entity);

            entity.Id = queryResult;
        }

        public override async Task DeleteAsync(Player entity)
        {
            await ExecuteAsync($"DELETE FROM {TableName} WHERE ID = @Id;", entity);
        }

        public override async Task EditAsync(Player entity)
        {
            await ExecuteAsync($"UPDATE {TableName} SET UserId = @UserId, Level = @Level, Hp = @Hp " +
                $"WHERE Id = @Id", entity);
        }
    }
}
