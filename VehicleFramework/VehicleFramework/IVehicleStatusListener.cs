﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFramework
{
    public enum VehicleStatus
    {
        OnPlayerEntry,
        OnPlayerExit,
        OnPilotBegin,
        OnPilotEnd,
        OnPowerUp,
        OnPowerDown,
        OnHeadLightsOn,
        OnHeadLightsOff,
        OnInteriorLightsOn,
        OnInteriorLightsOff,
        OnFloodLightsOn,
        OnFloodLightsOff,
        OnTakeDamage,
        OnAutoLevel,
        OnAutoPilotBegin,
        OnAutoPilotEnd,
        OnBatteryLow,
        OnBatteryDepletion
    }
    public interface IVehicleStatusListener
    {
        void OnPlayerEntry();
        void OnPlayerExit();
        void OnPilotBegin();
        void OnPilotEnd();
        void OnPowerUp();
        void OnPowerDown();
        // TODO
        //Something is pulsing OnExteriorLightsOn every frame, but not sure what
        void OnHeadLightsOn();
        void OnHeadLightsOff();
        void OnInteriorLightsOn();
        void OnInteriorLightsOff();
        void OnFloodLightsOn();
        void OnFloodLightsOff();
        void OnTakeDamage();
        void OnAutoLevel();
        void OnAutoPilotBegin();
        void OnAutoPilotEnd();
        void OnBatteryLow();
        void OnBatteryDepletion();
    }
}
