using Domain.Boxers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using VirtuaBoxer.Shared.Boxers;

namespace Services;

public class BoxerService : IBoxerService
{
    public BoxerService(VirtuaBoxerDbContext dbContext)
    {
        _dbContext = dbContext;
        _boxers = dbContext.Boxers;
        _stats = dbContext.Stats;
    }

    private readonly VirtuaBoxerDbContext _dbContext;
    private readonly DbSet<Boxer> _boxers;
    private readonly DbSet<Stat> _stats;

    private IQueryable<Boxer> GetBoxerById(int id) => _boxers
        //   .Include(x => x.PhysicalBoxingStats)
        //     .Include(x => x.AttackingBoxingStats)
        //      .Include(x => x.DefensiveBoxingStats)
        .AsNoTracking()
        .Where(p => p.Id == id);
        


    public async Task<int> CreateAsync(BoxerDto.Mutate model)
    {
        var boxer = _boxers.Add(new Boxer(
            model.Name,
            model.Nickname,
            BoxingClass.BRAWLER,
            new BoxerMeasurements(
                model.Height,
                model.Weight,
                model.Reach
                )
            ));
        await _dbContext.SaveChangesAsync();
        return boxer.Entity.Id;
    }

    public async Task DeleteAsync(int boxerId)
    {
        _boxers.RemoveIf(b => b.Id == boxerId);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(int boxerId, BoxerDto.Mutate model)
    {
        var boxer = await GetBoxerById(boxerId).SingleOrDefaultAsync();

        if (boxer is not null)
        {
            boxer.Name = model.Name;
            boxer.Nickname = model.Nickname;
            boxer.Measurements = new BoxerMeasurements(model.Height, model.Weight, model.Reach);

            _dbContext.Entry(boxer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<BoxerDto.Detail> GetDetailAsync(int boxerId)
    {
        var boxer = await GetBoxerById(boxerId).SingleOrDefaultAsync();
        Console.WriteLine($"ATTACK STATS{String.Join(",", boxer.AttackingBoxingStats)}");
        var response = await GetBoxerById(boxerId)
            .Select(b => new BoxerDto.Detail
            {
                Id = b.Id,
                Name = b.Name,
                Nickname = b.Nickname,
                Level = b.Level,
                Class = b.BoxingClass.ToString(),
                WeightClass = b.Measurements.GetWeightClass().ToString(),
                Weight = b.Measurements.Weight,
                Height = (int) b.Measurements.Heigth,
                Reach = (int) b.Measurements.Reach,
                AttackingStats = b.AttackingBoxingStats.AsEnumerable().Select(stat => new StatDto.Index {Name = stat.Statistic.ToString(), Exp = stat.Value, Level = stat.Level}).ToList(),
                DefensiveStats = b.DefensiveBoxingStats.AsEnumerable().Select(stat => new StatDto.Index { Name = stat.Statistic.ToString(), Exp = stat.Value, Level = stat.Level }).ToList(),
                PhysicalStats = b.PhysicalBoxingStats.AsEnumerable().Select(stat => new StatDto.Index { Name = stat.Statistic.ToString(), Exp = stat.Value, Level = stat.Level }).ToList(),
            })
            .SingleOrDefaultAsync();


        return response!;

    }

    public async Task<BoxerResponse.GetIndex> GetIndexAsync(BoxerRequest.GetIndex request)
    {
        BoxerResponse.GetIndex response = new();
        var query = _boxers.AsQueryable().AsNoTracking();

        response.TotalAmount = query.Count();

        response.Boxers = await query
            .Select(b => new BoxerDto.Index
            {
                Id = b.Id,
                Name = b.Name,
                Nickname = b.Nickname,
                Level = b.Level,
                Class = b.BoxingClass.ToString(),
                WeightClass = b.Measurements.GetWeightClass().ToString(),
            })
            .ToListAsync();
        return response;
    }
}