using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boxers;

public class BoxerMeasurements : Entity
{
    public double Heigth { get; set; } = 180;
    // In pounds
    public int Weight { get; set; } = 160;
    public double Reach { get; set; } = 178;

    private BoxerMeasurements() { }
    public BoxerMeasurements (double heigth, int weight, double reach)
    {
        Heigth = heigth;
        Weight = weight;
        Reach = reach;
    }

    public WeightClass GetWeightClass()
    {
        foreach (WeightClass wc in Enum.GetValues(typeof(WeightClass)))
        {
            if (Weight <= (double)wc)
                return wc;
        }
        return WeightClass.Heavyweight;
    }
}
