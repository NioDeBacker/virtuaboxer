using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boxers;

public class Boxer : Entity
{
    public string Name { get; set; } = String.Empty;
    public string Nickname { get; set; } = string.Empty;
    public int Level { get; set; } = 0;
    public int Experience { get; set; } = 0;

    public BoxerMeasurements Measurements { get; set; } = new BoxerMeasurements(0,0,0);

    public BoxingClass BoxingClass { get; set; }

    public List<Stat> AttackingBoxingStats { get; set; } = new List<Stat>{
        new Stat(BoxingStat.JAB),
        new Stat(BoxingStat.CROSS),
        new Stat(BoxingStat.LEAD_HOOK),
        new Stat(BoxingStat.REAR_HOOK),
        new Stat(BoxingStat.UPPERCUT)
    };
    public List<Stat> PhysicalBoxingStats { get; set; } = new List<Stat>{
        new Stat(BoxingStat.POWER),
        new Stat(BoxingStat.SPEED),
        new Stat(BoxingStat.STAMINA),
        new Stat(BoxingStat.RESILIENCE),
        new Stat(BoxingStat.REFLEXES)
    };
    public List<Stat> DefensiveBoxingStats { get; set; } = new List<Stat>{
        new Stat(BoxingStat.SLIPPING),
        new Stat(BoxingStat.BOBBING),
        new Stat(BoxingStat.BLOCKING),
        new Stat(BoxingStat.FOOTWORK),
        new Stat(BoxingStat.CLINCHING)
    };

    private Boxer() { }
    public Boxer(string name,string nickname, BoxingClass boxingClass, BoxerMeasurements measurements)
    {
        Name = name;
        Nickname = nickname;

        // Set class
        switch (boxingClass)
        {
            case BoxingClass.BRAWLER:
                IncreaseAttackingStat(BoxingStat.CROSS, 5);
                IncreaseAttackingStat(BoxingStat.UPPERCUT, 5);

                IncreaseDefensiveStat(BoxingStat.CLINCHING, 5);
                IncreaseDefensiveStat(BoxingStat.BLOCKING, 5);

                IncreasePhysicalStat(BoxingStat.POWER, 10);
                IncreasePhysicalStat(BoxingStat.RESILIENCE, 10);
                break;
            case BoxingClass.SWARMER:
                IncreaseAttackingStat(BoxingStat.LEAD_HOOK, 10);
                IncreaseAttackingStat(BoxingStat.UPPERCUT, 10);

                IncreaseDefensiveStat(BoxingStat.SLIPPING, 5);
                IncreaseDefensiveStat(BoxingStat.FOOTWORK, 5);

                IncreasePhysicalStat(BoxingStat.SPEED, 5);
                IncreasePhysicalStat(BoxingStat.STAMINA, 5);
                break;
            case BoxingClass.OUTFIGHTER:
                IncreaseAttackingStat(BoxingStat.JAB, 5);
                IncreaseAttackingStat(BoxingStat.CROSS, 5);

                IncreaseDefensiveStat(BoxingStat.BOBBING, 10);
                IncreaseDefensiveStat(BoxingStat.FOOTWORK, 10);

                IncreasePhysicalStat(BoxingStat.REFLEXES, 5);
                IncreasePhysicalStat(BoxingStat.STAMINA, 5);
                break;
            default:

                break;
        }

        Measurements = measurements;
    }

    private void IncreaseAttackingStat(BoxingStat type, int val)
    {
        AttackingBoxingStats.Where(s => s.Statistic == type).First().IncreaseVal(val);
    }
    private void IncreasePhysicalStat(BoxingStat type, int val)
    {
        PhysicalBoxingStats.Where(s => s.Statistic == type).First().IncreaseVal(val);
    }
    private void IncreaseDefensiveStat(BoxingStat type, int val)
    {
        DefensiveBoxingStats.Where(s => s.Statistic == type).First().IncreaseVal(val);
    }

}
