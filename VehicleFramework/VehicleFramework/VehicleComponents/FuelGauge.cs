﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VehicleFramework
{
    public class FuelGauge : MonoBehaviour
	{
		public ModVehicle mv;
        private bool wasPowered = false;
        public void Update()
        {
            if (mv.IsPowered())
            {
                if (!wasPowered)
                {
                    BroadcastMessage("OnPowerUp");
                }
                wasPowered = true;
            }
            else
            {
                if (wasPowered)
                {
                    BroadcastMessage("OnPowerDown");
                }
                wasPowered = false;
            }
        }
    }
}
