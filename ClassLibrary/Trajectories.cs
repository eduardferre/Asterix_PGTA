using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
namespace ClassLibrary
{
    public class PointWithTime
    {
        public PointLatLng point = new PointLatLng();
        public int time;
        public PointWithTime(PointLatLng p, int t)
        {
            this.point = p;
            this.time = t;
        }
    }

    public class Trajectories
    {
        public string CAT;
        public string SAC;
        public string SIC;
        public string targetIdentification;
        public string targetAdd;
        public int lastTOD;
        public int firstTOD;
        public string trackNum;
        public string type;
        public string detectionMode;
        public List<PointLatLng> listPoints = new List<PointLatLng>();
        public List<PointWithTime> listTimePoints = new List<PointWithTime>();

      public Trajectories(string Callsign, int Time, double lat, double lon, string emitter, string TargetAddress, string DetectionMode, string CAT, string SAC, string SIC, string Track_number)
        {
            PointLatLng p = new PointLatLng(lat, lon);
            PointWithTime pt = new PointWithTime(p, Time);
            this.listTimePoints.Add(pt);
            this.CAT = CAT;
            this.targetIdentification = Callsign;
            this.SAC = SAC;
            this.SIC = SIC;
            this.targetAdd = TargetAddress;
            this.firstTOD = Time;
            if (emitter == "car") { this.type = "Surface vehicle"; }
            else if (emitter == "plane") { this.type = "Aircraft"; }
            else { this.type = emitter; }
            this.detectionMode = DetectionMode;
            this.trackNum = Track_number;
            this.listPoints.Add(p);
        }

          public void AddPoint(double lat, double lon, int time)
        {
            PointLatLng p = new PointLatLng(lat, lon);
            this.listPoints.Add(p);
            this.lastTOD = time;
        }

      public void AddTimePoint(double lat, double lon, int time)
        {
            PointLatLng p = new PointLatLng(lat, lon);
            PointWithTime TimePoint = new PointWithTime(p, time);
            listTimePoints.Add(TimePoint);

        }

        public int CountTimepoint()
        {
            return this.listTimePoints.Count();
        }

       public string GetTrajectorieKML()
        {
            StringBuilder KMLBuilder = new StringBuilder();
            string caption;
            if (targetIdentification != null) { caption = targetIdentification; }
            else if (targetAdd != null) { caption = "T.A:" + targetAdd; }
            else if (trackNum != null) { caption = "T.N:" + trackNum; }
            else { caption = "No Data"; }
            string color;
            if (detectionMode == "SMR") { color = "ff00ff00"; }
            else if (detectionMode == "MLAT") { color = "ff00ffff"; }
            else { color = "ff0080ff"; }
            KMLBuilder.AppendLine("<Placemark>");
            KMLBuilder.AppendLine("<Style id='yellowLineGreenPoly'>");
            KMLBuilder.AppendLine("<LineStyle>");
            KMLBuilder.AppendLine("<color>" + color + "</color>");
            KMLBuilder.AppendLine("<width>4</width>");
            KMLBuilder.AppendLine("</LineStyle>");
            KMLBuilder.AppendLine("<PolyStyle>");
            KMLBuilder.AppendLine("<color>7f00ff00</color>");
            KMLBuilder.AppendLine("</PolyStyle>");
            KMLBuilder.AppendLine("</Style>");
            KMLBuilder.AppendLine("<name>" + caption + "</name>");
            KMLBuilder.AppendLine("<description>" + GetDescription() + "</description>");
            KMLBuilder.AppendLine("<styleUrl>#yellowLineGreenPoly</styleUrl>");
            KMLBuilder.AppendLine("<LineString>");
            KMLBuilder.AppendLine(KMLcoordenates());
            KMLBuilder.AppendLine("</LineString>");
            KMLBuilder.AppendLine("</Placemark>");
            return KMLBuilder.ToString();
        }

         private string GetDescription()
        {
            StringBuilder Description = new StringBuilder();
            if (CAT != null) { Description.Append("CAT: " + CAT); }
            if (SAC != null) { Description.Append("; SAC: " + SAC); }
            if (SIC != null) { Description.Append("; SIC: " + SIC); }
            if (targetIdentification != null) { Description.Append("; Target Id: " + targetIdentification); }
            Description.Append("; Detection Mode: " + detectionMode);
            if (targetAdd != null) { Description.Append("; Target Address: " + targetAdd); }
            if (trackNum != null) { Description.Append("; Track Number: " + trackNum); }
            if (type != null) { Description.Append("; Type of vehicle: " + type); }
            Description.Append(";First detected time: " + ComputeTime(firstTOD));
            Description.Append(";Last detected time: " + ComputeTime(lastTOD));
            return Description.ToString();
        }

         private string ComputeTime(int time)
        {
            int showingtime;
            if (time > 86400) { showingtime = time - 86400; }
            else
            {
                showingtime = time;
            }
            int hour = Convert.ToInt32(Math.Truncate(Convert.ToDouble(showingtime / 3600)));
            int min = Convert.ToInt32(Math.Truncate(Convert.ToDouble((showingtime - (hour * 3600)) / 60)));
            int sec = Convert.ToInt32(Math.Truncate(Convert.ToDouble((showingtime - ((hour * 3600) + (min * 60))))));
            string hours = Convert.ToString(hour).PadLeft(2, '0');
            string minutes = Convert.ToString(min).PadLeft(2, '0');
            string seconds = Convert.ToString(sec).PadLeft(2, '0');
            return (hours + ":" + minutes + ":" + seconds);
        }

         private string KMLcoordenates()
        {
            StringBuilder KMLcoor = new StringBuilder();
            KMLcoor.AppendLine("<coordinates>");
            foreach (PointWithTime p in listTimePoints)
            {
                string Lat = Convert.ToString(p.point.Lat).Replace(",", ".");
                string Lon = Convert.ToString(p.point.Lng).Replace(",", ".");
                KMLcoor.AppendLine(Lon + "," + Lat);
            }
            KMLcoor.AppendLine("</coordinates>");
            return KMLcoor.ToString();
        }
    }
}
