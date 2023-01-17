using Domain.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boxers;

public class Stat : Entity
{
    public int levelcap => Level * 10;
    public BoxingStat Statistic { get; }
    public int Value { get; set; }

    public int Level { get; set; } = 0;

    public Stat(BoxingStat stat, int value = 0)
    {
        Statistic = stat;
        Value = value;
    }

    private Stat() { }
    public void IncreaseVal (int addedExp)
    {
        Value += addedExp;
        while (Value > levelcap)
        {
            Value -= levelcap;
            Level++;
        }
    }

}
