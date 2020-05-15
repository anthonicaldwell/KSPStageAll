using KSP.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace StageAll
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class StageAll : MonoBehaviour
    {
        public void OnGUI()
        { 
            if ( GUI.Button(new Rect(32,64,128,32), "Stage All") )
            {
                foreach (Vessel v in FlightGlobals.Vessels)
                {
                    //v.ActionGroups.ToggleGroup(KSPActionGroup.Stage);
                    v.ResumeStaging();
                    FlightGlobals.SetActiveVessel(v);
                    StageManager.ActivateNextStage();

                    foreach (Part p in v.parts)
                    {
                        if ( p.stageOffset == v.currentStage)
                        {
                            List<ModuleEngines> engines;
                            if (p.isEngine(out engines))
                            {
                                foreach (ModuleEngines e in engines)
                                {
                                    e.throttleMin = 100.0f;
                                    e.currentThrottle = 100.0f;
                                    e.UpdateThrottle();
                                }
                            }
                        }
                    }
                }
            }

            if (GUI.Button(new Rect(32, 96, 128, 32), "Toggle SAS All"))
            {
                foreach ( Vessel v in FlightGlobals.Vessels)
                {
                    v.ActionGroups.ToggleGroup(KSPActionGroup.SAS);
                }
            }

            if (GUI.Button(new Rect(32, 128, 128, 32), "Toggle RCS All"))
            {
                foreach (Vessel v in FlightGlobals.Vessels)
                {
                    v.ActionGroups.ToggleGroup(KSPActionGroup.RCS);
                }
            }
        }
    }
}
