using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;


using GMap.NET;
using System.Reflection;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace ClassLibrary
{
    class Maps
    {
    }
    public class markerWithInfo : GMap.NET.WindowsForms.GMapMarker
    {
        public readonly string caption;
        public string Callsign;
        public int Time;
        public PointLatLng p;
        public int number;
        public string emitter;
        public string TargetAddress;
        public string DetectionMode;
        public string CAT;
        public string SAC;
        public string SIC;
        public string Flight_level;
        public string Track_number;
        public int type = 1;
        public int direction;
        public int refreshratio;

        public markerWithInfo(PointLatLng p, string Callsign, int time, int num, string emmiter, string TargetAdd, string DetectionMode, string CAT, string SIC, string SAC, string Flight_level, string Track_number, int direction, int refreshratio)
        : base(p)
        {
            if (Callsign != null) { this.caption = Callsign; }
            else
            {
                if (TargetAdd != null) { this.caption = "A.: " + TargetAdd; }
                else
                {
                    if (Track_number != null) { this.caption = "N:" + Track_number; }
                    else { this.caption = "No data"; }
                }
            }
            this.DetectionMode = DetectionMode;
            this.emitter = emmiter;
            this.p = p;
            this.refreshratio = refreshratio;
            this.direction = direction;
            this.Callsign = Callsign;
            this.Time = time;
            this.TargetAddress = TargetAdd;
            this.Track_number = Track_number;
            this.number = num;
            this.CAT = CAT;
            this.SIC = SIC;
            this.SAC = SAC;
            if (Flight_level != null && Flight_level.Contains(":"))
            {
                this.Flight_level = Flight_level.Substring(Flight_level.IndexOf(':') + 1, (Flight_level.Length - Flight_level.IndexOf(':')) - 1);
            }
            else { this.Flight_level = Flight_level; }
        }

    }
}
