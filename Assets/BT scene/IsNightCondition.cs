using BBSamples.PQSG;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Condition("Chapter09/IsNight")]
[Help("Checks whether it is night time.")]
public class IsNightCondition : ConditionBase
{
    private DoneDayNightCycle light;
    public override bool Check()
    {
        return SearchLight() && light.isNight;
    }

    public override TaskStatus MonitorCompleteWhenTrue()
    {
        if(Check())
        {
            return TaskStatus.COMPLETED;
        }
        if(light !=null)
        {
            light.OnChanged += OnSunset;
        }
        return TaskStatus.SUSPENDED;
    }

    public override TaskStatus MonitorFailWhenFalse()
    {
        if (!Check())
        {

            return TaskStatus.FAILED;

        }
        light.OnChanged += OnSunrise;
        return TaskStatus.SUSPENDED;
    }
    private bool SearchLight()
    {
        if (light != null)
        {
            return true;
        }
        GameObject lightGO = GameObject.FindGameObjectWithTag("MainLight");
        if(lightGO == null)
        {
            return false;
        }
        light = lightGO.GetComponent<DoneDayNightCycle>();
        return light != null;
    }

    public void OnSunset(object sender, System.EventArgs night)
    {
        light.OnChanged -= OnSunset;
        EndMonitorWithSuccess();
    }

    public void OnSunrise(object sender, System.EventArgs e)
    {
        light.OnChanged -= OnSunrise;
        EndMonitorWithFailure();
    }
}
