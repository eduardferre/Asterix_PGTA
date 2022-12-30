using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class CATALL
    {
        public int msgNum;
        public string CAT;
        public string SAC;
        public string SIC;
        public string targetIdentification;
        public string targetAddress;
        public int timeOfDay;
        public int listTimeOfDay;
        public string trackNumber;
        public double latitudeInWGS84;
        public double longitudeInWGS84;
        public string flightLevel;
        public string type;
        public string detectionMode;
        public int direction;
        public int refreshratio = 0;

        public CATALL(CAT10 msg, int firstTimeOfDay, int firstTime)
        {
            this.msgNum = msg.msgNum;
            this.CAT = msg.CAT;
            this.SAC = msg.SAC;
            this.SIC = msg.SIC;
            this.targetIdentification = msg.TargetId;
            this.targetAddress = msg.TargetAdd;
            this.trackNumber = msg.TrackNum;
            this.latitudeInWGS84 = msg.LatitudeMapWGS84;
            this.longitudeInWGS84 = msg.LongitudeMapWGS84;
            this.flightLevel = msg.FlightLevel;
            if (msg.TimeOfDaySec < firstTime) { this.listTimeOfDay = msg.TimeOfDaySec + 86400 + firstTimeOfDay; }
            else { this.listTimeOfDay = msg.TimeOfDaySec + firstTimeOfDay; }
            if (msg.TOT == "Ground vehicle") { type = "car"; }
            else if (msg.TOT == "Aircraft") { type = "plane"; }
            else { type = "undetermined"; }
            if (msg.SIC == "7") { detectionMode = "SMR"; }
            if (msg.SIC == "107") { detectionMode = "MLAT"; }
        }

        public CATALL(CAT21 msg, int firstTimeOfDay, int firstTime)
        {
            this.msgNum = msg.msgNum;
            this.CAT = msg.CAT;
            this.SAC = msg.SAC;
            this.SIC = msg.SIC;
            this.targetIdentification = msg.TargetId;
            this.targetAddress = msg.TargetAdd;
            this.trackNumber = msg.trackNumber;
            this.latitudeInWGS84 = msg.LatitudeMapWGS84;
            this.longitudeInWGS84 = msg.LongitudeMapWGS84;
            this.flightLevel = msg.fligthLevel;
            if (msg.timeOfDaySeconds < firstTime) { this.listTimeOfDay = msg.timeOfDaySeconds + 86400 + firstTimeOfDay; }
            else { this.listTimeOfDay = msg.timeOfDaySeconds + firstTimeOfDay; }
            if (msg.ECAT == "Surface emergency vehicle" || msg.ECAT == "Surface service vehicle") { type = "car"; }
            else if (msg.ECAT == "Light aircraft" || msg.ECAT == "Small aircraft" || msg.ECAT == "Medium aircraft" || msg.ECAT == "Heavy aircraft") { type = "plane"; }
            else { type = "undetermined"; }
            detectionMode = "ADSB";
        }


    }
}
