using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boxers;

public class BoxerFaker : Faker<Boxer>
{
    public BoxerFaker()
    {
        CustomInstantiator(f => new Boxer(f.Name.FullName(), f.Name.JobTitle(), f.PickRandom<BoxingClass>(), new BoxerMeasurements(f.Random.Double(150, 200), f.Random.Int(120, 200), f.Random.Double(150, 200))));
        RuleFor(x => x.Id, f => f.Random.Int(1));
        UseSeed(1337);
    }
}
