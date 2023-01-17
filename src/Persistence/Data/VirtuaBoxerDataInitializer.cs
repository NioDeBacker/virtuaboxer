using Domain.Boxers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data;

public class VirtuaBoxerDataInitializer
{
    public static void Seed(VirtuaBoxerDbContext dbContext)
    {
        if (dbContext.Boxers.Any())
            return;
        var boxers = new BoxerFaker()
            .RuleFor(b => b.Id, () => 0)
            .Generate(100);
        dbContext.Boxers.AddRange(boxers);
        dbContext.SaveChanges();
    }
}
