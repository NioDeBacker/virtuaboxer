using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtuaBoxer.Shared.Boxers;

public static class BoxerDto
{
    public class Index
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public string WeightClass { get; set; }
    }

    public class Detail : Index
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Reach { get; set; }
        public IList<StatDto.Index> PhysicalStats { get; set; }
        public IList<StatDto.Index> AttackingStats { get; set; }
        public IList<StatDto.Index> DefensiveStats { get; set; }
    }

    public class Mutate
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Reach { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {

            }
        }
    }
}
