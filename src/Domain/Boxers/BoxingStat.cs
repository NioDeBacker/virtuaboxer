using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boxers;

public enum BoxingStat
{
    // Attacking
    JAB,
    CROSS,
    LEAD_HOOK,
    REAR_HOOK,
    UPPERCUT,
    // Defending
    SLIPPING,
    BLOCKING,
    CLINCHING,
    BOBBING,
    FOOTWORK,
    // Physical
    POWER,
    SPEED,
    STAMINA,
    RESILIENCE,
    REFLEXES,
}
