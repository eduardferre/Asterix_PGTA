using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class MapsFunctions
    {
        public static readonly object PositionRadarMatrixLock = new Object();
        public static readonly object RotationRadarMatrixLock = new Object();
        public const double METERS2FEET = 3.28084;
        public const double FEET2METERS = 0.3048;
        public const double METERS2NM = 1 / MapsFunctions.NM2METERS;
        public const double NM2METERS = 1852.0;
        public const double DEGS2RADS = Math.PI / 180.0;
        public const double RADS2DEGS = 180.0 / Math.PI;
        public double A = 6378137.0;
        public double B = 6356752.3142;
        public double E2 = 0.00669437999013;
        public const double ALMOST_ZERO = 1e-10;
        public const double REQUIERED_PRECISION = 1e-8;
        public CoordinatesWGS84 centerProjection;
        private GeneralMatrix T1;
        private GeneralMatrix R1;
        public double R_S = 0;
        private Dictionary<CoordinatesWGS84, GeneralMatrix> rotationMatrixHT = null;
        private Dictionary<CoordinatesWGS84, GeneralMatrix> translationMatrixHT = null;
        private Dictionary<CoordinatesWGS84, GeneralMatrix> positionRadarMatrixHT = null;
        private Dictionary<CoordinatesWGS84, GeneralMatrix> rotationRadarMatrixHT = null;

        public MapsFunctions() { }

        public MapsFunctions(double E, double A)
        {
            this.E2 = E * E;
            this.A = A;
            setCenterProjection(new CoordinatesWGS84());
        }
        public MapsFunctions(double E, double A, CoordinatesWGS84 centerProjection)
        {
            this.E2 = E * E;
            this.A = A;
            setCenterProjection(centerProjection);
        }
        static public CoordinatesWGS84 LatLonStringBoth2Radians(string line, double height)
        {
            CoordinatesWGS84 res = LatLonStringBoth2Radians(line);
            res.Height = height;
            return res;
        }
        static public CoordinatesWGS84 LatLonStringBoth2Radians(string line)
        {
            string pattern = @"([-+]?)([0-9]+):([0-9]+):([0-9][0-9]*[.]*[0-9]+)([NS]?)\s+([-+]?)([0-9]+):([0-9]+):([0-9][0-9]*[.]*[0-9]+)([EW]?)[\s]*([0-9][0-9]*[.]*[0-9]+)?[.]*";
            

            Regex reggie = new Regex(pattern);
            MatchCollection matches = reggie.Matches(line);
            string latMinus = string.Empty, lonMinus = string.Empty, latNS = string.Empty, lonEW = string.Empty;
            double lat1 = 0, lat2 = 0, lat3 = 0, lon1 = 0, lon2 = 0, lon3 = 0, height = 0;
            try
            {
                System.Globalization.NumberFormatInfo myInv = System.Globalization.NumberFormatInfo.InvariantInfo;
                latMinus = matches[0].Groups[1].Captures[0].Value;
                lat1 = Convert.ToDouble(matches[0].Groups[2].Captures[0].Value);
                lat2 = Convert.ToDouble(matches[0].Groups[3].Captures[0].Value);
                lat3 = Convert.ToDouble(matches[0].Groups[4].Captures[0].Value, myInv);
                latNS = matches[0].Groups[5].Captures[0].Value;

                lonMinus = matches[0].Groups[6].Captures[0].Value;
                lon1 = Convert.ToDouble(matches[0].Groups[7].Captures[0].Value);
                lon2 = Convert.ToDouble(matches[0].Groups[8].Captures[0].Value);
                lon3 = Convert.ToDouble(matches[0].Groups[9].Captures[0].Value, myInv);
                lonEW = matches[0].Groups[10].Captures[0].Value;

                if (matches[0].Groups[11].Captures.Count > 0)
                    height = Convert.ToDouble(matches[0].Groups[11].Captures[0].Value, myInv);
                else
                    height = 0;
            }
            catch 
            {
            }
            CoordinatesWGS84 res = new CoordinatesWGS84();
            int n = 0;
            if ((latMinus.Length > 0 && latMinus.Substring(0, 1).Equals("-")) || latNS.Equals("S"))
            {
                n = 1; if (lat1 < 0) lat1 *= -1; 
            }
            res.Lat = MapsFunctions.LatLon2Radians(lat1, lat2, lat3, n);
            n = 0;
            if ((lonMinus.Length > 0 && lonMinus.Substring(0, 1).Equals("-")) || lonEW.Equals("W"))
            {
                n = 1; if (lon1 < 0) lon1 *= -1; 
            }
            res.Lon = MapsFunctions.LatLon2Radians(lon1, lon2, lon3, n);
            res.Height = height;
            return res;
        }

        static public double LatLon2Degrees(double d1, double d2, double d3, int ns)
        {
            double d = d1 + (d2 / 60.0) + (d3 / 3600.0);
            if (ns == 1)
                d *= -1.0;
            return d;
        }

        static public double LatLon2Radians(double d1, double d2, double d3, int ns)
        {
            double d = d1 + (d2 / 60.0) + (d3 / 3600.0);
            if (ns == 1)
                d *= -1.0;
            return d * MapsFunctions.DEGS2RADS;
        }

        static public double LatLonString2Degrees(string s1, string s2, string s3, int ns)
        {
            double d = 0;
            try
            {
                double d1 = double.Parse(s1);
                double d2 = double.Parse(s2);
                double d3 = double.Parse(s3);
                d = MapsFunctions.LatLon2Degrees(d1, d2, d3, ns);
            }
            catch (FormatException) { }
            return d;
        }
        static public void Degrees2LatLon(double d, out double d1, out double d2, out double d3, out int ns)
        {
            if (d < 0) { d *= -1.0; ns = 1; } else { ns = 0; }
            d1 = Math.Floor(d);
            d2 = Math.Floor((d - d1) * 60.0);
            d3 = (((d - d1) * 60.0) - d2) * 60.0;
        }
        static public void Radians2LatLon(double d, out double d1, out double d2, out double d3, out int ns)
        {
            d *= MapsFunctions.RADS2DEGS;
            if (d < 0) { d *= -1.0; ns = 1; } else { ns = 0; }
            d1 = Math.Floor(d);
            d2 = Math.Floor((d - d1) * 60.0);
            d3 = (((d - d1) * 60.0) - d2) * 60.0;
        }

        static public CoordinatesWGS84 CenterCoordinates(List<CoordinatesWGS84> l)
        {
            double maxLat = -999, maxLon = -999, minLat = 999, minLon = 999, maxHeight = -999;
            if (l != null && l.Count > 0)
            {
                foreach (CoordinatesWGS84 c in l)
                {
                    if (maxLat < c.Lat) maxLat = c.Lat;
                    if (maxLon < c.Lon) maxLon = c.Lon;
                    if (minLat > c.Lat) minLat = c.Lat;
                    if (minLon > c.Lon) minLon = c.Lon;
                    if (maxHeight < c.Height) maxHeight = c.Height; 
                }
                CoordinatesWGS84 res = new CoordinatesWGS84();
                res.Lat = (maxLat + minLat) / 2.0;
                res.Lon = (maxLon + minLon) / 2.0;
                res.Height = maxHeight;
                return res;
            }
            else
                return (CoordinatesWGS84)null;
        }

        public CoordinatesXYZ change_geodesic2geocentric(CoordinatesWGS84 c)
        {
            if (c == null) return (CoordinatesXYZ)null;
            CoordinatesXYZ res = new CoordinatesXYZ();
            double nu = this.A / Math.Sqrt(1 - this.E2 * Math.Pow(Math.Sin(c.Lat), 2.0));
            res.X = (nu + c.Height) * Math.Cos(c.Lat) * Math.Cos(c.Lon);
            res.Y = (nu + c.Height) * Math.Cos(c.Lat) * Math.Sin(c.Lon);
            res.Z = (nu * (1 - this.E2) + c.Height) * Math.Sin(c.Lat);
            return res;
        }
        public CoordinatesWGS84 change_geocentric2geodesic(CoordinatesXYZ c)
        {
            if (c == null) return null;
            CoordinatesWGS84 res = new CoordinatesWGS84();
            double b = 6356752.3142;

            if ((Math.Abs(c.X) < MapsFunctions.ALMOST_ZERO) && (Math.Abs(c.Y) < MapsFunctions.ALMOST_ZERO))
            {
                if (Math.Abs(c.Z) < MapsFunctions.ALMOST_ZERO)
                {
                    // the point is at the center of earth :)
                    res.Lat = Math.PI / 2.0;
                }
                else
                {
                    res.Lat = (Math.PI / 2.0) * ((c.Z / Math.Abs(c.Z)) + 0.5);
                }
                res.Lon = 0;
                res.Height = Math.Abs(c.Z) - b;
                return res;
            }

            double d_xy = Math.Sqrt(c.X * c.X + c.Y * c.Y);
 
            res.Lat = Math.Atan((c.Z / d_xy) /
                (1 - (this.A * this.E2) / Math.Sqrt(d_xy * d_xy + c.Z * c.Z)));
      
            double nu = this.A / Math.Sqrt(1 - this.E2 * Math.Pow(Math.Sin(res.Lat), 2.0));
     
            res.Height = (d_xy / Math.Cos(res.Lat)) - nu;

 
            double Lat_over;
            if (res.Lat >= 0) { Lat_over = -0.1; } else { Lat_over = 0.1; }

            int loop_count = 0;
            while ((Math.Abs(res.Lat - Lat_over) > MapsFunctions.REQUIERED_PRECISION)
                && (loop_count < 50))
            {
                loop_count++;
                Lat_over = res.Lat;
                res.Lat = Math.Atan((c.Z * (1 + res.Height / nu)) /
                    (d_xy * ((1 - this.E2) + (res.Height / nu))));
                nu = this.A / Math.Sqrt(1 - this.E2 * Math.Pow(Math.Sin(res.Lat), 2.0));
                res.Height = d_xy / Math.Cos(res.Lat) - nu;
            }
            res.Lon = Math.Atan2(c.Y, c.X);
           
            return res;
        }
        public CoordinatesWGS84 setCenterProjection(CoordinatesWGS84 c)
        {
            if (c == null) return null;

            CoordinatesWGS84 c2 = new CoordinatesWGS84(c.Lat, c.Lon, 0); //c.Height);
            this.centerProjection = c2;
            double nu = this.A / Math.Sqrt(1 - this.E2 * Math.Pow(Math.Sin(c2.Lat), 2.0));

            this.R_S = (this.A * (1.0 - this.E2)) /
                Math.Pow(1 - this.E2 * Math.Pow(Math.Sin(c2.Lat), 2.0), 1.5);


            this.T1 = MapsFunctions.CalculateTranslationMatrix(c2, this.A, this.E2);
            this.R1 = MapsFunctions.CalculateRotationMatrix(c2.Lat, c2.Lon);

            return this.centerProjection;
        }

        public CoordinatesWGS84 getCenterProjection() { return this.centerProjection; }

        public CoordinatesXYZ change_geocentric2system_cartesian(CoordinatesXYZ geo)
        {

            if (this.centerProjection == null || this.R1 == null ||
                this.T1 == null || geo == null) return (CoordinatesXYZ)null;

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = geo.X; coefInput[1][0] = geo.Y; coefInput[2][0] = geo.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            inputMatrix.SubtractEquals(this.T1);
            GeneralMatrix R2 = this.R1.Multiply(inputMatrix);

            CoordinatesXYZ res = new CoordinatesXYZ(R2.GetElement(0, 0),
                                        R2.GetElement(1, 0),
                                        R2.GetElement(2, 0));
            return res;
        }


        public CoordinatesXYZ change_geodesic2system_cartesian(CoordinatesWGS84 ObjectPos, CoordinatesWGS84 RadarPos)
        {
            CoordinatesXYZ ObjectGeocentric = change_geodesic2geocentric(ObjectPos);
            this.setCenterProjection(RadarPos);
            CoordinatesXYZ ObjectCartesian = change_geocentric2system_cartesian(ObjectGeocentric);
            return ObjectCartesian;
        }
        public CoordinatesXYZ change_system_cartesian2geocentric(CoordinatesXYZ car)
        {

            if (car == null) return (CoordinatesXYZ)null;

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = car.X; coefInput[1][0] = car.Y; coefInput[2][0] = car.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            GeneralMatrix R2 = this.R1.Transpose();
            GeneralMatrix R3 = R2.Multiply(inputMatrix);
            R3.AddEquals(this.T1);

            CoordinatesXYZ res = new CoordinatesXYZ(R3.GetElement(0, 0),
                                                    R3.GetElement(1, 0),
                                                    R3.GetElement(2, 0));
            return res;
        }


        public CoordinatesWGS84 change_system_cartesian2geodesic(CoordinatesXYZ Objectcartesian, CoordinatesWGS84 Radargeodesic)
        {
            this.setCenterProjection(Radargeodesic);
            CoordinatesXYZ Objectgeocentric = change_system_cartesian2geocentric(Objectcartesian);
            CoordinatesWGS84 Objectgeodesic = change_geocentric2geodesic(Objectgeocentric);
            return Objectgeodesic;
        }
        public double change_system_xyh2system_z(CoordinatesXYH c)
        {
            double z = 0.0;
            if (c == null) return 0.0;

            double xh = c.X / (this.R_S + c.Height);
            double yh = c.Y / (this.R_S + c.Height);
            double temp = xh * xh + yh * yh;
            if (temp > 1)
            {
                z = -(this.R_S + this.centerProjection.Height);
            }
            else
            {
                z = (this.R_S + c.Height) * Math.Sqrt(1.0 - temp) -
                    (this.R_S + this.centerProjection.Height);
            }
            return z;
        }
        public CoordinatesUVH change_system_cartesian2stereographic(CoordinatesXYZ c)
        {
            if (c == null) return (CoordinatesUVH)null;

            CoordinatesUVH res = new CoordinatesUVH();
            double d_xy2 = c.X * c.X + c.Y * c.Y;
            res.Height = Math.Sqrt(d_xy2 +
                (c.Z + this.centerProjection.Height + this.R_S) *
                (c.Z + this.centerProjection.Height + this.R_S)) - this.R_S;
            double k = (2 * this.R_S) /
                (2 * this.R_S + this.centerProjection.Height + c.Z + res.Height);
            res.U = k * c.X;
            res.V = k * c.Y;
            return res;
        }
        public CoordinatesXYZ change_stereographic2system_cartesian(CoordinatesUVH c)
        {

            if (c == null) return (CoordinatesXYZ)null;

            CoordinatesXYZ res = new CoordinatesXYZ();
            double d_uv2 = c.U * c.U + c.V * c.V;
            res.Z = (c.Height + this.R_S) * ((4 * this.R_S * this.R_S - d_uv2) /
                (4 * this.R_S * this.R_S + d_uv2)) -
                (this.R_S + this.centerProjection.Height);
            double k = (2 * this.R_S) / (2 * this.R_S + this.centerProjection.Height + res.Z + c.Height);
            res.X = c.U / k;
            res.Y = c.V / k;
            return res;
        }
        static public double CalculateElevation(CoordinatesWGS84 centerCoordinates, double R, double rho, double h)
        {
            if ((rho < MapsFunctions.ALMOST_ZERO) || (R == -1.0) || (centerCoordinates == null))
            {
                return 0;
            }
            else
            {
                double temp = (2 * R *
                    (h - centerCoordinates.Height) + h * h -
                    centerCoordinates.Height * centerCoordinates.Height - rho * rho) /
                    (2 * rho * (R + centerCoordinates.Height));
                if ((temp > -1.0) && (temp < 1.0))
                {
                    return Math.Asin(temp);
                }
                else
                {
                    return (Math.PI / 2.0);
                }
            }
        }
        static public double CalculateAzimuth(double x, double y)
        {
            double theta;
            if (Math.Abs(y) < MapsFunctions.ALMOST_ZERO)
            {
                theta = (x / Math.Abs(x)) * Math.PI / 2.0;
            }
            else
            {
                theta = Math.Atan2(x, y);
            }

            if (theta < 0.0)
            {
                theta += 2 * Math.PI;
            }
            return theta;
        }
        public double CalculateEarthRadius(CoordinatesWGS84 geo)
        {
            Double ret = Double.NaN;
            if (geo != null)
            {
                ret = (this.A * (1.0 - this.E2)) /
                    Math.Pow(1 - this.E2 * Math.Pow(Math.Sin(geo.Lat), 2.0), 1.5);

            }

            return ret;

        }
        static public GeneralMatrix CalculateRotationMatrix(double lat, double lon)
        {
            double[][] coefR1 = { new double[3], new double[3], new double[3] };

            coefR1[0][0] = -(Math.Sin(lon));
            coefR1[0][1] = Math.Cos(lon);
            coefR1[0][2] = 0;
            coefR1[1][0] = -(Math.Sin(lat) * Math.Cos(lon));
            coefR1[1][1] = -(Math.Sin(lat) * Math.Sin(lon));
            coefR1[1][2] = Math.Cos(lat);
            coefR1[2][0] = Math.Cos(lat) * Math.Cos(lon);
            coefR1[2][1] = Math.Cos(lat) * Math.Sin(lon);
            coefR1[2][2] = Math.Sin(lat);
            GeneralMatrix m = new GeneralMatrix(coefR1, 3, 3);
            return m;
        }
        static public GeneralMatrix CalculateTranslationMatrix(CoordinatesWGS84 c, double A, double E2)
        {
            double nu = A / Math.Sqrt(1 - E2 * Math.Pow(Math.Sin(c.Lat), 2.0));
            double[][] coefT1 = { new double[1], new double[1], new double[1] };
            coefT1[0][0] = (nu + c.Height) * Math.Cos(c.Lat) * Math.Cos(c.Lon);
            coefT1[1][0] = (nu + c.Height) * Math.Cos(c.Lat) * Math.Sin(c.Lon);
            coefT1[2][0] = (nu * (1 - E2) + c.Height) * Math.Sin(c.Lat);
            GeneralMatrix m = new GeneralMatrix(coefT1, 3, 1);
            return m;
        }
        static public GeneralMatrix CalculatePositionRadarMatrix(GeneralMatrix T1, GeneralMatrix t, GeneralMatrix r)
        {

            GeneralMatrix R1 = T1.Subtract(t);
            GeneralMatrix res = r.Multiply(R1);

            return res;
        }
        static public GeneralMatrix CalculateRotationRadarMatrix(GeneralMatrix R1, GeneralMatrix r)
        {

            GeneralMatrix R2 = R1.Transpose();
            GeneralMatrix res = r.Multiply(R2);
            return res;
        }
        static public CoordinatesXYZ change_radar_spherical2radar_cartesian(CoordinatesPolar polarCoordinates)
        {
            if (polarCoordinates == null) return (CoordinatesXYZ)null;

            CoordinatesXYZ res = new CoordinatesXYZ();

            res.X = polarCoordinates.Rho * Math.Cos(polarCoordinates.Elevation) *
                Math.Sin(polarCoordinates.Theta);
            res.Y = polarCoordinates.Rho * Math.Cos(polarCoordinates.Elevation) *
                Math.Cos(polarCoordinates.Theta);
            res.Z = polarCoordinates.Rho * Math.Sin(polarCoordinates.Elevation);

            return res;
        }
        static public CoordinatesPolar change_radar_cartesian2radar_spherical(CoordinatesXYZ cartesianCoordinates)
        {
            if (cartesianCoordinates == null) return (CoordinatesPolar)null;

            CoordinatesPolar res = new CoordinatesPolar();

            res.Rho = Math.Sqrt(cartesianCoordinates.X * cartesianCoordinates.X +
                cartesianCoordinates.Y * cartesianCoordinates.Y +
                cartesianCoordinates.Z * cartesianCoordinates.Z);
            res.Theta = MapsFunctions.CalculateAzimuth(cartesianCoordinates.X, cartesianCoordinates.Y);
            res.Elevation = Math.Asin(cartesianCoordinates.Z / res.Rho);
            return res;
        }
        public CoordinatesXYZ change_radar_cartesian2geocentric(CoordinatesWGS84 radarCoordinates, CoordinatesXYZ cartesianCoordinates)
        {
            GeneralMatrix translationMatrix = ObtainTranslationMatrix(radarCoordinates);
            GeneralMatrix rotationMatrix = ObtainRotationMatrix(radarCoordinates);

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = cartesianCoordinates.X;
            coefInput[1][0] = cartesianCoordinates.Y;
            coefInput[2][0] = cartesianCoordinates.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            GeneralMatrix R1 = rotationMatrix.Transpose();
            GeneralMatrix R2 = R1.Multiply(inputMatrix);
            R2.AddEquals(translationMatrix);

            CoordinatesXYZ res = new CoordinatesXYZ(R2.GetElement(0, 0),
                                    R2.GetElement(1, 0),
                                    R2.GetElement(2, 0));
            return res;

        }
        public CoordinatesXYZ change_geocentric2radar_cartesian(CoordinatesWGS84 radarCoordinates, CoordinatesXYZ geocentricCoordinates)
        {
            GeneralMatrix translationMatrix = ObtainTranslationMatrix(radarCoordinates);
            GeneralMatrix rotationMatrix = ObtainRotationMatrix(radarCoordinates);

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = geocentricCoordinates.X;
            coefInput[1][0] = geocentricCoordinates.Y;
            coefInput[2][0] = geocentricCoordinates.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            inputMatrix.SubtractEquals(translationMatrix);
            GeneralMatrix R1 = rotationMatrix.Multiply(inputMatrix);

            CoordinatesXYZ res = new CoordinatesXYZ(R1.GetElement(0, 0),
                                    R1.GetElement(1, 0),
                                    R1.GetElement(2, 0));
            return res;

        }
        public CoordinatesXYZ change_radar_cartesian2system_cartesian(CoordinatesWGS84 radarCoordinates, CoordinatesXYZ cartesianCoordinates)
        {
            GeneralMatrix positionRadarMatrix = ObtainPositionRadarMatrix(radarCoordinates);
            GeneralMatrix rotationRadarMatrix = ObtainRotationRadarMatrix(radarCoordinates);

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = cartesianCoordinates.X;
            coefInput[1][0] = cartesianCoordinates.Y;
            coefInput[2][0] = cartesianCoordinates.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            inputMatrix.SubtractEquals(positionRadarMatrix);
            GeneralMatrix R1 = rotationRadarMatrix.Multiply(inputMatrix);

            CoordinatesXYZ res = new CoordinatesXYZ(R1.GetElement(0, 0),
                                                    R1.GetElement(1, 0),
                                                    R1.GetElement(2, 0));
            return res;
        }
        public CoordinatesXYZ change_system_cartesian2radar_cartesian(CoordinatesWGS84 radarCoordinates, CoordinatesXYZ cartesianCoordinates)
        {
            GeneralMatrix positionRadarMatrix = ObtainPositionRadarMatrix(radarCoordinates);
            GeneralMatrix rotationRadarMatrix = ObtainRotationRadarMatrix(radarCoordinates);

            double[][] coefInput = { new double[1], new double[1], new double[1] };
            coefInput[0][0] = cartesianCoordinates.X;
            coefInput[1][0] = cartesianCoordinates.Y;
            coefInput[2][0] = cartesianCoordinates.Z;
            GeneralMatrix inputMatrix = new GeneralMatrix(coefInput, 3, 1);

            GeneralMatrix R1 = rotationRadarMatrix.Multiply(inputMatrix);
            R1.AddEquals(positionRadarMatrix);

            CoordinatesXYZ res = new CoordinatesXYZ(R1.GetElement(0, 0),
                                                    R1.GetElement(1, 0),
                                                    R1.GetElement(2, 0));
            return res;
        }
        private GeneralMatrix ObtainRotationMatrix(CoordinatesWGS84 radarCoordinates)
        {
            GeneralMatrix rotationMatrix = null;
            if (this.rotationMatrixHT == null)
                this.rotationMatrixHT = new Dictionary<CoordinatesWGS84, GeneralMatrix>(16);
            if (this.rotationMatrixHT.ContainsKey(radarCoordinates))
            {
                rotationMatrix = this.rotationMatrixHT[radarCoordinates];
            }
            else
            {
                rotationMatrix = MapsFunctions.CalculateRotationMatrix(radarCoordinates.Lat, radarCoordinates.Lon);
                this.rotationMatrixHT.Add(radarCoordinates, rotationMatrix);
            }
            return rotationMatrix;
        }
        private GeneralMatrix ObtainTranslationMatrix(CoordinatesWGS84 radarCoordinates)
        {
            GeneralMatrix translationMatrix = null;
            if (this.translationMatrixHT == null)
                this.translationMatrixHT = new Dictionary<CoordinatesWGS84, GeneralMatrix>(16);
            if (this.translationMatrixHT.ContainsKey(radarCoordinates))
            {
                translationMatrix = this.translationMatrixHT[radarCoordinates];
            }
            else
            {
                translationMatrix = MapsFunctions.CalculateTranslationMatrix(radarCoordinates, this.A, this.E2);
                this.translationMatrixHT.Add(radarCoordinates, translationMatrix);
            }
            return translationMatrix;
        }
        private GeneralMatrix ObtainPositionRadarMatrix(CoordinatesWGS84 radarCoordinates)
        {
            GeneralMatrix p = null;
            lock (PositionRadarMatrixLock)
            {
                if (this.positionRadarMatrixHT == null)
                    this.positionRadarMatrixHT = new Dictionary<CoordinatesWGS84, GeneralMatrix>(16);
                if (this.positionRadarMatrixHT.ContainsKey(radarCoordinates))
                {
                    p = this.positionRadarMatrixHT[radarCoordinates];
                }
                else
                {
                    p = MapsFunctions.CalculatePositionRadarMatrix(this.T1,
                        ObtainTranslationMatrix(radarCoordinates),
                        ObtainRotationMatrix(radarCoordinates));
                    this.positionRadarMatrixHT.Add(radarCoordinates, p);
                }
            }
            return p;
        }
        private GeneralMatrix ObtainRotationRadarMatrix(CoordinatesWGS84 radarCoordinates)
        {
            GeneralMatrix p = null;
            lock (RotationRadarMatrixLock)
            {
                if (this.rotationRadarMatrixHT == null)
                    this.rotationRadarMatrixHT = new Dictionary<CoordinatesWGS84, GeneralMatrix>(16);
                if (this.rotationRadarMatrixHT.ContainsKey(radarCoordinates))
                {
                    p = this.rotationRadarMatrixHT[radarCoordinates];
                }
                else
                {
                    p = MapsFunctions.CalculateRotationRadarMatrix(this.R1,
                        ObtainRotationMatrix(radarCoordinates));
                    this.rotationRadarMatrixHT.Add(radarCoordinates, p);
                }
            }
            return p;
        }
    }
    public class Coordinates
    {
    }
    public class CoordinatesPolar : Coordinates
    {
        private double rho; private double theta;
        private double elevation;
        public double Rho { get { return rho; } set { rho = value; } }
        public double Theta { get { return theta; } set { theta = value; } }
        public double Elevation { get { return elevation; } set { elevation = value; } }
        public CoordinatesPolar() { }
        public CoordinatesPolar(double rho, double theta, double elevation) { this.Rho = rho; this.Theta = theta; this.Elevation = elevation; }
        public static string ToString(CoordinatesPolar c)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.AppendFormat(" R: {0:f4}m T: {1:f4}rad E: {2:f4}rad", c.Rho, c.Theta, c.Elevation);
            return s.ToString();
        }
        public static string ToStringStandard(CoordinatesPolar c)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.AppendFormat(" R: {0:f4}NM T: {1:f4}º E: {2:f4}º", c.Rho * MapsFunctions.METERS2NM, c.Theta * MapsFunctions.RADS2DEGS, c.Elevation * MapsFunctions.RADS2DEGS);
            return s.ToString();
        }
    }
    public class CoordinatesXYZ : Coordinates
    {
        private double x; private double y;
        private double z;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Z { get { return z; } set { z = value; } }
        public CoordinatesXYZ() { }
        public CoordinatesXYZ(double x, double y, double z) { this.X = x; this.Y = y; this.Z = z; }
        public static string ToString(CoordinatesXYZ c)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.AppendFormat(" X: {0:f4}m Y: {1:f4}m Z: {2:f4}m", c.X, c.Y, c.Z);
            return s.ToString();
        }
        public override string ToString()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.AppendFormat(" X: {0:f4}m Y: {1:f4}m Z: {2:f4}m", this.X, this.Y, this.Z);
            return s.ToString();
        }
    }
    public class CoordinatesUVH : Coordinates
    {
        private double u; private double v;
        private double h;
        public double U { get { return u; } set { u = value; } }
        public double V { get { return v; } set { v = value; } }
        public double Height { get { return h; } set { h = value; } }
    }
    public class CoordinatesXYH : Coordinates
    {
        private double x; private double y;
        private double height;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Height { get { return height; } set { height = value; } }
    }
    public class CoordinatesWGS84 : Coordinates
    {
        private double lat; private double lon; private double height;
        public double Height { get { return height; } set { height = value; } }
        public double Lat { get { return lat; } set { lat = value; } }
        public double Lon { get { return lon; } set { lon = value; } }
        public CoordinatesWGS84() { this.lat = 0; this.lon = 0; this.height = 0; }
        public CoordinatesWGS84(double lat, double lon) { this.lat = lat; this.lon = lon; this.height = 0; }

        public CoordinatesWGS84(string lat, string lon, double h)
        {
            this.lat = Convert.ToDouble(lat) * MapsFunctions.DEGS2RADS;
            this.lon = Convert.ToDouble(lon) * MapsFunctions.DEGS2RADS;
            this.height = h;
        }

        public CoordinatesWGS84(double lat, double lon, double height) { this.lat = lat; this.lon = lon; this.height = height; }
        public override string ToString()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            double d1, d2, d3; int n;
            MapsFunctions.Radians2LatLon(lat, out d1, out d2, out d3, out n);
            s.AppendFormat("{0:d2}:{1:d2}:{2:f4}" + (n == 0 ? 'N' : 'S') + " ", (int)d1, (int)d2, d3);
            MapsFunctions.Radians2LatLon(lon, out d1, out d2, out d3, out n);
            s.AppendFormat("{0:d3}:{1:d2}:{2:f4}" + (n == 0 ? 'E' : 'W') + " ", (int)d1, (int)d2, d3);
            s.AppendFormat("{0:f4}m", height);
            s.Append(Environment.NewLine);
            s.AppendFormat("lat:{0:f9} lon:{1:f9}", this.Lat* MapsFunctions.RADS2DEGS, this.Lon* MapsFunctions.RADS2DEGS);
            return s.ToString(); 
        }
    }
}
