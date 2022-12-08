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
using System.Windows.Forms;
using System.Windows.Input;
using System.Net;


using GMap.NET;
using System.Reflection;
using GMap.NET.MapProviders;
using Cursors = System.Windows.Input.Cursors;
using Image = System.Drawing.Image;
using GMap.NET.WindowsPresentation;
using MessageBox = System.Windows.MessageBox;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace AsterixDecoder
{
    public partial class AsterixDecoder : Form
    {
        DecodeFiles decodeFiles = new DecodeFiles();

        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();
        List<CATALL> listCATALL = new List<CATALL>();

        DataTable dataTableCAT10 = new DataTable();
        DataTable dataTableCAT21 = new DataTable();

        List<markerWithInfo> markers = new List<markerWithInfo>();
        int time = 0;

        public AsterixDecoder()
        {
            InitializeComponent();
        }

        
        private void AsterixDecoder_Load(object sender, EventArgs e)
        {
            process_label.Text = "Select a file to decode";
            clickInfo_label.Visible = false;
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
            info_label.Visible = false;
            process_label.Visible = true;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;
        }

        private void gridCAT10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            if (row >= 0 && column >= 0)
            {
                CAT10 cat10 = this.listCAT10[row];
                string columnName = gridCAT10.Columns[column].Name;

                string info = "";

                if (columnName == "Target Report")
                {
                    info = cat10.TYP + "\n" + cat10.DCR + "\n" + cat10.CHN +
                    "\n" + cat10.GBS + "\n" + cat10.CRT + "\n" + cat10.SIM + "\n" + cat10.TST + "\n"
                    + cat10.RAB + "\n" + cat10.LOP + "\n" + cat10.TOT + "\n" + cat10.SPI;
                }
                else if (columnName == "Track Status")
                {
                    info = cat10.CNF + "\n" + cat10.TRE + "\n" + cat10.CST + "\n"
                    + cat10.MAH + "\n" + cat10.TCC + "\n" + cat10.STH + "\n" + cat10.TOM + "\n" + cat10.DOU
                    + "\n" + cat10.MRS + "\n" + cat10.GHO;
                }
                else if (columnName == "Target Size and Orientation")
                {
                    info = cat10.targetSizeOrientation;
                }
                else if (columnName == "Flight Level")
                {
                    info = cat10.FlightLevelInfo;
                }
                else if (columnName == "System Status")
                {
                    info = cat10.NOGO + "\n" + cat10.OVL + "\n" + cat10.TSV + "\n"
                    + cat10.DIV + "\n" + cat10.TIF;
                }
                else if (columnName == "Mode S MB Data")
                {
                    //?info = cat10.modeSrep.ToString();
                }
                else if (columnName == "Standard Deviation of Position")
                {
                    info = cat10.StandardDevPos;
                }
                else if (columnName == "Presence")
                {
                    //?info = cat10.REPPresence.ToString();
                }

                string[] infoSplit = info.Split("\n");
                info = "";

                foreach (string i in infoSplit)
                {
                    if (i != "") info = info + i + "\n";
                }

                clickInfo_label.Text = info;
            }

        }

        private void gridCAT21_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            if (row >= 0 && column >= 0)
            {
                CAT21 cat21 = this.listCAT21[row];
                string columnName = gridCAT21.Columns[column].Name;

                string info = "";

                if (columnName == "Target Report")
                {
                    info = cat21.ATP + "\n" + cat21.ARC + "\n" + cat21.RC + "\n" +
                    cat21.RAB + "\n" + cat21.DCR + "\n" + cat21.GBS + "\n" + cat21.SIM + "\n" + cat21.TST
                    + "\n" + cat21.SAA + "\n" + cat21.CL + "\n" + cat21.IPC + "\n" + cat21.NOGO + "\n" +
                    cat21.LDPJ + "\n" + cat21.RCF;
                }
                else if (columnName == "Quality Indicators")
                {
                    info = cat21.NUCr_NACv + "\n" + cat21.NUCp_NIC + "\n" + cat21.NICbaro
                    + "\n" + cat21.SILsupp + "\n" + cat21.NACp + "\n" + cat21.SDA + "\n" + cat21.GVA + "\n" +
                    cat21.PICsupp + "\n" + cat21.ICB + "\n" + cat21.NUCp + "\n" + cat21.NIC;
                }
                else if (columnName == "Target Status")
                {
                    info = cat21.ICF + "\n" + cat21.LNAV + "\n" + cat21.PS + "\n" + cat21.SS;
                }
                else if (columnName == "Met Information")
                {
                    info = cat21.WindSpeed + "\n" + cat21.WindDirection + "\n" +
                    cat21.Temperature + "\n" + cat21.Turbulence;
                }
                else if (columnName == "Selected Altitude")
                {
                    info = cat21.SAS + "\n" + cat21.Source + "\n" + cat21.SA;
                }
                else if (columnName == "Final State Selected Altitude")
                {
                    info = cat21.MV + "\n" + cat21.AH + "\n" + cat21.AM + "\n" + cat21.finalStateSelectedAltitude;
                }
                else if (columnName == "Trajectory Intent")
                {
                    info = cat21.NAV + "\n" + cat21.NVB;
                }
                else if (columnName == "Aircraft Operational Status")
                {
                    info = cat21.RA + "\n" + cat21.TC + "\n" + cat21.TS + "\n" + cat21.CDTIA + "\n" +
                    cat21.NotTCAS + "\n" + cat21.SA;
                }
                else if (columnName == "Surface Capabilities and Characteristics")
                {
                    info = cat21.POA + "\n" + cat21.CDTIS + "\n" + cat21.B2Low + "\n" + cat21.RAS + "\n" +
                    cat21.IDENT + "\n" + cat21.Length_Width;
                }
                else if (columnName == "Mode S MB Data")
                {
                    //?info = cat21.REP_SMB.ToString();
                }
                else if (columnName == "ACAS Resolution Advisory Report")
                {
                    info = cat21.TYP + "\n" + cat21.STYP + "\n" + cat21.ARA + "\n" + cat21.RAC + "\n" + cat21.RAT
                    + "\n" + cat21.MTE + "\n" + cat21.TTI + "\n" + cat21.TID;
                }
                else if (columnName == "Data Ages")
                {
                    info = cat21.AOS + "\n" + cat21.TRD + "\n" + cat21.M3A + "\n" + cat21.QI + "\n" + cat21.TI +
                    "\n" + cat21.MAM + "\n" + cat21.GH + "\n" + cat21.FL + "\n" + cat21.ISA + "\n" + cat21.FSA + "\n" + cat21.AS + "\n"
                    + cat21.TAS + "\n" + cat21.MH + "\n" + cat21.BVR + "\n" + cat21.GVR + "\n" + cat21.GV + "\n" + cat21.TAR + "\n" +
                    cat21.TIDataAge + "\n" + cat21.TSDataAge + "\n" + cat21.MET + "\n" + cat21.ROA + "\n" + cat21.ARADataAge + "\n" + cat21.SCC;
                }

                string[] infoSplit = info.Split("\n");
                info = "";

                foreach (string i in infoSplit)
                {
                    if (i != "") info = info + i + "\n";
                }

                clickInfo_label.Text = info;
            }
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt) | *.txt* | ast files (*.ast) | *.ast*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();

            if (openFileDialog.SafeFileName != null)
            {
                decodeFiles.ClearStoredData();

                string[] fileNames = openFileDialog.FileNames;

                if (fileNames.Length == 1) process_label.Text = "Selected file: \n                        " + fileNames[0];
                else
                {
                    string process = "Selected files: \n                        ";
                    foreach (string p in fileNames) { process = process + p + "\n                        "; }

                    process_label.Text = process;
                }

                foreach (string path in fileNames)
                {
                    int result = decodeFiles.Read(path);

                    if (result == 1)
                    {
                        decodeFiles.numFiles++;
                        decodeFiles.nameFiles.Add(path);
                    }
                    else MessageBox.Show(path + " file is nor readable or an error ocurred while decoding");
                }

                msg_label.Text = "There are a total of " + decodeFiles.numMsgs.ToString() + " messages\n" +
                                 "CAT10: " + decodeFiles.numCAT10Msgs.ToString() + " messages\n" +
                                 "      SMR: " + decodeFiles.numCAT10SMRMsgs + " messages\n" +
                                 "      MLAT: " + decodeFiles.numCAT10MLATMsgs + " messages\n" +
                                 "CAT21: " + decodeFiles.numCAT21Msgs.ToString() + " messages";
            }

            this.listCAT10 = decodeFiles.GetListCAT10();
            this.listCAT21 = decodeFiles.GetListCAT21();
            this.listCATALL = decodeFiles.GetListCATALL();

            this.dataTableCAT10 = decodeFiles.GetTableCAT10();
            this.dataTableCAT21 = decodeFiles.GetTableCAT21();
        }

        private void CAT10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = true;
            gridCAT10.Visible = true;
            gridCAT21.Visible = false;
            info_label.Visible = true;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;
            if (listCAT10.Count != 0)
            {

                gridCAT10.Location = new Point(10, 40);
                clickInfo_label.Location = new Point(1075, 80);
                info_label.Location = new Point(1100, 40);

                gridCAT10.Size = new Size(1050, 600);
                clickInfo_label.Size = new Size(210, 250);

                this.Controls.Add(gridCAT10);

                gridCAT10.DataSource = dataTableCAT10;

                gridCAT10.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                info_label.Visible = true;
                clickInfo_label.Visible = true;
                gridCAT10.Visible = true;
            }
            else { MessageBox.Show("No data for CAT10, please upload a file with CAT10 messages"); }
        }

        private void CAT21ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = true;
            gridCAT10.Visible = false;
            gridCAT21.Visible = true;
            info_label.Visible = true;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;
            if (listCAT21.Count != 0)
            {

                gridCAT21.Location = new Point(10, 40);
                clickInfo_label.Location = new Point(1075, 80);
                info_label.Location = new Point(1100, 40);

                gridCAT21.Size = new Size(1050, 600);
                clickInfo_label.Size = new Size(210, 250);

                this.Controls.Add(gridCAT21);

                gridCAT21.DataSource = dataTableCAT21;

                gridCAT21.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                info_label.Visible = true;
                clickInfo_label.Visible = true;
                gridCAT21.Visible = true;
            }
            else { MessageBox.Show("No data for CAT21, please upload a file with CAT21 messages"); }
        }

        private void mapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = false;
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
            info_label.Visible = false;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = true;
            timeLabel.Visible = true;
            startButton.Visible = true;
            resetButton.Visible = true;
            speedButton.Visible = true;
            timer1.Stop();
            timer1.Interval = 1000;
            time = 86400;
            foreach (CATALL message in listCATALL) 
            { 
                if (message.timeOfDay < time) 
                {
                    time = message.timeOfDay;
                }
            }
            timeLabel.Text = Convert.ToString((time / (60 * 60)) % 24) + " : " + Convert.ToString((time / 60) % 60) + " : " + Convert.ToString((time % 60));


        }

        private void exportCSV_CAT10_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv*)|*.csv*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != null)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string path0 = saveFileDialog.FileName;
                string path = path0 + ".csv";
                StringBuilder sb = new StringBuilder();
                if (File.Exists(path)) { File.Delete(path); }

                StringBuilder ColumnsNames = new StringBuilder();
                foreach (DataColumn col in dataTableCAT10.Columns)
                {
                    if (col.ColumnName != "CAT number")
                    {
                        string Name = col.ColumnName.Replace('\n', ' ');
                        ColumnsNames.Append(Name + ",");
                    }
                }

                string ColNames = ColumnsNames.ToString();
                ColNames = ColNames.TrimEnd(',');
                sb.AppendLine(ColNames);
                foreach (DataRow row in dataTableCAT10.Rows) //cat10
                {
                    string nl = "; ";
                    StringBuilder RowData = new StringBuilder();
                    int number = Convert.ToInt32(row[1].ToString());
                    CAT10 message = listCAT10[number];
                    foreach (DataColumn column in dataTableCAT10.Columns)
                    {
                        string Value = "";
                        if (column.ColumnName != "CAT number")
                        {
                            string data = row[column].ToString();
                            if (data == "Click to expand")
                            {
                                if (column.ColumnName == "Target\nReport\nDescriptor")
                                {
                                    Value = "TYP: " + message.TYP + nl + message.DCR + nl + message.CHN + nl + message.GBS + nl + message.CRT;
                                    if (message.SIM != null) { Value = Value + nl + message.SIM + nl + message.TST + nl + message.RAB + nl + message.LOP + nl + message.TOT; }
                                    if (message.SPI != null) { Value = Value + nl + message.SPI; }
                                }
                                if (column.ColumnName == "Track Status")
                                {
                                    Value = Value + message.CNF + nl + message.TRE + nl + message.CST + nl + message.MAH + nl + message.TCC + nl + message.STH;
                                    if (message.TOM != null) { Value = Value + nl + message.TOM + nl + message.DOU + nl + message.MRS; }
                                    if (message.GHO != null) { Value = Value + nl + message.GHO; }
                                }
                                if (column.ColumnName == "System\nStatus")
                                {
                                    Value = message.NOGO + nl + message.OVL + nl + message.TSV + nl + message.DIV + nl + message.TIF;
                                }

                                if (column.ColumnName == "Mode-3A\nCode")
                                {
                                    Value = message.VMode3A + nl + message.GMode3A + nl + message.LMode3A + nl + message.Mode3A;
                                }
                                if (column.ColumnName == "Mode S MB\nData")
                                {
                                    Value = Value + "Repetitions: " + message.modeSrep;
                                    for (int s = 0; s < message.modeSrep; s++)
                                    {
                                        Value = Value + nl + "Repetition: " + Convert.ToString(s) + nl + "Mode S Comm B message data: " + message.MBData[s] + nl + "Comm B Data Buffer Store 1 Address: " + message.BDS1[s] + nl + "Comm B Data Buffer Store 2 Address: " + message.BDS2[s];
                                    }
                                }

                                if (column.ColumnName == "Standard\nDeviation\nof Position")
                                {
                                    Value = message.DeviationX + nl + message.DeviationY + nl + message.CovarianceXY;
                                }
                                if (column.ColumnName == "Presence")
                                {
                                    Value = Value + "Repetitions: " + Convert.ToString(message.REPPresence);
                                    for (int s = 0; s < message.REPPresence; s++)
                                    {
                                        Value = Value + nl + "Difference between the radial distance of the plot centre and that of the presence: " + message.DRHO[s] + nl + "Difference between the azimuth of the plot centre and that of the presence: " + message.DTHETA[s];
                                    }
                                }
                                data = Value;

                            }
                            data = data.Replace(",", ".");
                            RowData.Append(data);
                            RowData.Append(",");
                        }
                    }
                    string RowDat = RowData.ToString();
                    RowDat = RowDat.TrimEnd(',');
                    sb.AppendLine(RowDat);
                }

                File.WriteAllText(path, sb.ToString());
                Mouse.OverrideCursor = null;
            }
        }

        private void exportCSV_CAT21_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv*)|*.csv*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != null)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string path0 = saveFileDialog.FileName;
                string path = path0 + ".csv";
                StringBuilder sb = new StringBuilder();
                if (File.Exists(path)) { File.Delete(path); }

                StringBuilder ColumnsNames = new StringBuilder();
                foreach (DataColumn col in dataTableCAT21.Columns)
                {
                    if (col.ColumnName != "CAT number")
                    {
                        string Name = col.ColumnName.Replace('\n', ' ');
                        ColumnsNames.Append(Name + ",");
                    }
                }
                string ColNames = ColumnsNames.ToString();
                ColNames = ColNames.TrimEnd(',');
                sb.AppendLine(ColNames);
                foreach (DataRow row in dataTableCAT21.Rows) //cat10
                {
                    string nl = "; ";
                    string Value = "";
                    StringBuilder RowData = new StringBuilder();
                    int number = Convert.ToInt32(row[1].ToString());
                    CAT21 message = listCAT21[number];
                    foreach (DataColumn column in dataTableCAT21.Columns)
                    {
                        if (column.ColumnName != "CAT number")
                        {
                            string data = row[column].ToString();
                            Value = "";
                            if (data == "Click to expand")
                            {
                                if (column.ColumnName == "Target\nReport\nDescriptor")
                                {
                                    Value = " Address Type: " + message.ATP + nl + "Altitude Reporting Capability: " + message.ARC + nl + "Range Check: " + message.RC + nl + "Report Type: " + message.RAB;
                                    if (message.DCR != null)
                                    {
                                        Value = Value + nl + "Differential Correction: " + message.DCR + nl + "Ground Bit Setting: " + message.GBS + nl + "Simulated Target: " + message.SIM + nl + "Test Target: " + message.TST + nl + "Selected Altitude Available: " + message.SAA + nl + "Confidence Level: " + message.CL;
                                        if (message.IPC != null)
                                        {
                                            Value = Value + nl + "Independent Position Check: " + message.IPC + nl + "No-go Bit Status: " + message.NOGO + nl + "Compact Posiotion Reporting: " + message.CPR + nl + "Local Decoding Position Jump: " + message.LDPJ + nl + "Range Check: " + message.RCF;
                                        }
                                    }
                                }
                                if (column.ColumnName == "Quality\nIndicators")
                                {
                                    Value = "NUCr or NACv: " + message.NUCr_NACv + nl + "NUCp or NIC: " + message.NUCp_NIC;
                                    if (message.NICbaro != null)
                                    {
                                        Value = Value + nl + "Navigation Integrity Category for Barometric Altitude: " + message.NICbaro + nl + "Surveillance or Source  Integrity Level: " + message.SIL + nl + "Navigation Accuracy Category for Position: " + message.NACp;
                                        if (message.SILsupp != null)
                                        {
                                            Value = Value + nl + "SIL-Supplement: " + message.SILsupp + nl + "Horizontal Position System Design Assurance Level: " + message.SDA + nl + "Geometric Altitude Accuracy: " + message.GVA;
                                            if (message.ICB != null) { Value = Value + nl + "Position Integrity Category:" + nl + "Integrity Containment Bound" + message.ICB + nl + "NUCp: " + message.NUCp + nl + "NIC: " + message.NIC; }
                                        }
                                    }
                                }
                                if (column.ColumnName == "Target\nStatus")
                                {
                                    Value = "Intent Change Flag: " + message.ICF + nl + "LNAV Mode: " + message.LNAV + nl + "Priority Status: " + message.PS + nl + "Surveillance Status: " + message.SS;
                                }
                                if (column.ColumnName == "Met Information")
                                {
                                    if (message.WindSpeed != null) { Value = Value + "Wind Speed: " + message.WindSpeed; }
                                    if (message.WindDirection != null) { Value = Value + nl + "Wind Direction: " + message.WindDirection; }
                                    if (message.Temperature != null) { Value = Value + nl + "Temperature: " + message.Temperature; }
                                    if (message.Turbulence != null) { Value = Value + nl + "Turbulence: " + message.Turbulence; }
                                }
                                if (column.ColumnName == "Final\nState\nSelected\nAltitude")
                                {
                                    Value = Value + "Manage Vertical Mode: " + message.MV + nl + "Altitude Hold Mode: " + message.AH + "Approach Mode: " + message.AM + nl + "Altitude: " + message.finalStateSelectedAltitude;
                                }
                                if (column.ColumnName == "Trajectory\nIntent")
                                {
                                    if (message.NAV != null)
                                    {
                                        Value = Value + message.NAV + nl + message.NVB;
                                    }
                                    if (message.REP_TI != 0)
                                    {
                                        Value = Value + nl + "Repetitions: " + Convert.ToString(message.REP_TI);
                                        for (int s = 0; s < message.REP_TI; s++)
                                        {
                                            Value = Value + nl + "Repetition: " + Convert.ToString(s) + nl + message.TCA[s] + nl + message.NC[s] + nl + "Trajectory Change Point number: " + message.TCP[s] + nl + "Altitude: " + message.Altitude[s] + nl + "Latitude: " + message.Latitude[s] + nl + "Longitude: " + message.Longitude[s] + nl + "Point Type: " + message.PointType[s] + nl;
                                            Value = Value + nl + "TD: " + message.TD[s] + nl + "Turn Radius Availabilty" + message.TRA[s] + nl + message.TOA[s] + nl + "Time Over Point: " + message.TOV[s] + nl + "TCP Turn radius: " + message.TTR[s];
                                        }
                                    }
                                }
                                if (column.ColumnName == "Aircraft\nOperational\nStatus")
                                {
                                    Value = Value + message.RA + nl + "Target Trajectory Change Report Capability: " + message.TC + nl + "Target State Report Capability: " + message.TS + nl + "Air-Referenced Velocity Report Capability: " + message.ARV + nl + "Cockpit Display of Traffic Information airborne: " + message.CDTIA + nl + "TCAS System Status: " + message.NotTCAS + nl + message.SA;
                                }
                                if (column.ColumnName == "Surface\nCapabilities\nand\nCharacteristics")
                                {
                                    Value = Value + message.POA + nl + message.CDTIS + nl + message.B2Low + nl + message.RAS + nl + message.IDENT;
                                    if (message.Length_Width != null) { Value = Value + nl + message.Length_Width; }
                                }
                                if (column.ColumnName == "Mode S MB Data")
                                {
                                    Value = Value + "Repetitions: " + message.REP_SMB;
                                    for (int s = 0; s < message.REP_SMB; s++)
                                    {
                                        Value = Value + nl + "Repetition: " + Convert.ToString(s) + nl + "Mode S Comm B message data: " + message.MBData[s] + nl + "Comm B Data Buffer Store 1 Address: " + message.BDS1[s] + nl + "Comm B Data Buffer Store 2 Address: " + message.BDS2[s];
                                    }
                                }
                                if (column.ColumnName == "ACAS\nResolution\nAdvisory\nReport")
                                {
                                    Value = Value + "Message Type: " + message.TYP + nl + "Message Sub-type: " + message.STYP + nl + "Active Resolution Advisories: " + message.ARA + nl + "RAC(RA Complement) Record: " + message.RAC + nl + "RA Terminated: " + message.RAT + nl + "Multiple Threat Encounter: " + message.MTE + nl + "Threat Type Indicator: " + message.TTI + nl + "Threat Identity Data: " + message.TID;
                                }
                                if (column.ColumnName == "Data Ages")
                                {
                                    if (message.AOS != null) { Value = Value + "Age of the latest received information transmitted in item I021 / 008: " + message.AOS; }
                                    if (message.TRD != null) { Value = Value + nl + "Age of the last update of the Target Report Descriptor: " + message.TRD; }
                                    if (message.M3A != null) { Value = Value + nl + "Age of the last update of the Mode 3 / A Code: " + message.M3A; }
                                    if (message.QI != null) { Value = Value + nl + "Age of the latest information received to update the Quality Indicators: " + message.QI; }
                                    if (message.TI != null) { Value = Value + nl + "Age of the last update of the Trajectory Intent: " + message.TI; }
                                    if (message.MAM != null) { Value = Value + nl + "Age of the latest measurement of the message amplitude: " + message.MAM; }
                                    if (message.GH != null) { Value = Value + nl + "Age of the information contained in item 021 / 140: " + message.GH; }
                                    if (message.FL != null) { Value = Value + nl + "Age of the Flight Level information: " + message.FL; }
                                    if (message.ISA != null) { Value = Value + nl + "Age of the Intermediate State Selected Altitude: " + message.ISA; }
                                    if (message.FSA != null) { Value = Value + nl + "Age of the Final State Selected Altitude: " + message.FSA; }
                                    if (message.AS != null) { Value = Value + nl + "Age of the Air Speed: " + message.AS; }
                                    if (message.TAS != null) { Value = Value + nl + "Age of the value for the True Air Speed: " + message.TAS; }
                                    if (message.MH != null) { Value = Value + nl + "Age of the value for the Magnetic Heading: " + message.MH; }
                                    if (message.BVR != null) { Value = Value + nl + "Age of the Barometric Vertical Rate: " + message.BVR; }
                                    if (message.GVR != null) { Value = Value + nl + "Age of the Geometric Vertical Rate: " + message.GVR; }
                                    if (message.GV != null) { Value = Value + nl + "Age of the Ground Vector: " + message.GV; }
                                    if (message.TAR != null) { Value = Value + nl + "Age of item I021/165 Track Angle Rate: " + message.TAR; }
                                    if (message.TIDataAge != null) { Value = Value + nl + "Age of the Target Identification: " + message.TIDataAge; }
                                    if (message.TSDataAge != null) { Value = Value + nl + "Age of the Target Status: " + message.TSDataAge; }
                                    if (message.MET != null) { Value = Value + nl + "Age of the Meteorological: " + message.MET; }
                                    if (message.ROA != null) { Value = Value + nl + "Age of the Roll Angle value: " + message.ROA; }
                                    if (message.ARADataAge != null) { Value = Value + nl + "Age of the latest update of an active ACAS Resolution Advisory: " + message.ARADataAge; }
                                    if (message.SCC != null) { Value = Value + nl + "Age of the latest information received on the surface capabilities and characteristics of the respective target: " + message.SCC; }
                                }
                                data = Value;
                            }
                            data = data.Replace(",", ".");
                            RowData.Append(data);
                            RowData.Append(",");
                        }
                    }
                    string RowDat = RowData.ToString();
                    RowDat = RowDat.TrimEnd(',');
                    sb.AppendLine(RowDat);
                }

                File.WriteAllText(path, sb.ToString());
                Mouse.OverrideCursor = null;
            }
        }

        private void exportKMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        int zoom = 12;
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl1.RoutesEnabled = true;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.MarkersEnabled = true;
            gMapControl1.NegativeMode = false;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.AllowDrop = true;
            gMapControl1.IgnoreMarkerOnMouseWheel = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.DisableFocusOnMouseEnter = true;
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = zoom;
            gMapControl1.Position = new PointLatLng(41.295855, 2.08442);
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start")
            {
                timer1.Start();
                startButton.Text = "Pause";
            }
            else
            {
                timer1.Stop();
                startButton.Text = "Start";
            }
        }

        private void AddActualMarker(double X, double Y, string Callsign, int time, int num, string emmiter, string TargetAdd, string detectionmode, string CAT, string SIC, string SAC, string Flight_level, string Track_number, int direction, int refreshratio)
        {
            PointLatLng coordinates = new PointLatLng(X, Y);
            markerWithInfo marker = new markerWithInfo(coordinates, Callsign, time, num, emmiter, TargetAdd, detectionmode, CAT, SIC, SAC, Flight_level, Track_number, direction, refreshratio);
            markers.Add(marker);
           // SetMarkerShape(marker);
        }
        //private void SetMarkerShape(markerWithInfo marker)
        //{
        //    Bitmap bitmaptxt = MarkersDrawings.InsertText(marker);
        //    int heig = 50; //35
        //    int wid = 50; //35
        //    marker.Shape = new System.Windows.Controls.Image
        //    {

        //        Width = heig,
        //        Height = wid,
        //        Source = MarkersDrawings.ToBitmapImage(bitmaptxt)
        //    };
        //    marker.Offset = new System.Windows.Point((-wid / 2), (-heig / 2) - 5);
        //    bitmaptxt.Dispose();
        //    bitmaptxt = null;
        //    marker.Shape.MouseLeftButtonUp += markerclick;
        //}

        public GMapOverlay OverlayMarkers = new GMapOverlay("Markers");

        private void ShowMarkers()
        {
            OverlayMarkers.Markers.Clear();
            foreach (markerWithInfo marker in markers)
            {
                if (smrCheck.Checked)
                {
                    if (marker.DetectionMode == "SMR")
                    {
                        marker.IsVisible = true;
                        GMap.NET.WindowsForms.GMapMarker mk = new GMarkerGoogle(marker.p, GMarkerGoogleType.blue_dot);
                        OverlayMarkers.Markers.Add(mk);
                    }
                }
                if (mlatCheck.Checked)
                {
                    if (marker.DetectionMode == "MLAT")
                    {
                        marker.IsVisible = true;
                        GMap.NET.WindowsForms.GMapMarker mk = new GMarkerGoogle(marker.p, GMarkerGoogleType.pink_dot);
                        OverlayMarkers.Markers.Add(mk);
                    }
                }
                if (adsbCheck.Checked)
                {
                    if (marker.DetectionMode == "ADSB")
                    {
                        marker.IsVisible = true;
                        GMap.NET.WindowsForms.GMapMarker mk = new GMarkerGoogle(marker.p, GMarkerGoogleType.green_dot);
                        OverlayMarkers.Markers.Add(mk);
                    }
                }
            }
            gMapControl1.Overlays.Add(OverlayMarkers);
        }
        private void timer1_Tick(object sender, EventArgs e)
        { 
            time++;
            timeLabel.Text = Convert.ToString((time / (60 * 60))%24) + " : " + Convert.ToString((time / 60)%60) + " : " + Convert.ToString((time%60));
            timeIncrease();
        }

        private void timeIncrease()
        {
            bool first_found = false;
            int s = 0;

            for (int i = 0; first_found == false; i++) { if (listCATALL[i].timeOfDay == time) { first_found = true; s = i; }; }

            while (listCATALL[s].timeOfDay == time)
            {
                CATALL message = listCATALL[s];
                if (message.latitudeInWGS84 != -200 && message.longitudeInWGS84 != -200)
                {
                    bool DuplicatedTarget = false;
                    bool DuplicatedTrackNumber = false;
                    if (message.targetAddress != null) { DuplicatedTarget = markers.Any(x => x.TargetAddress == message.targetAddress && x.TargetAddress != null && x.Time == message.timeOfDay && message.detectionMode == x.DetectionMode); }
                    else { DuplicatedTrackNumber = markers.Any(x => x.Track_number == message.trackNumber && x.Track_number != null && x.Time == message.timeOfDay && message.detectionMode == x.DetectionMode); }

                    if (DuplicatedTarget == false && DuplicatedTrackNumber == false)
                    {
                        markers.RemoveAll(item => (((item.TargetAddress == message.targetAddress && item.TargetAddress != null) || (item.Track_number == message.trackNumber && item.Track_number != null) || (item.Callsign == message.targetIdentification && item.Callsign != null)) && item.DetectionMode == message.detectionMode));
                        AddActualMarker(Convert.ToDouble(message.latitudeInWGS84), Convert.ToDouble(message.longitudeInWGS84), message.targetIdentification, time, message.msgNum, message.type, message.targetAddress, message.detectionMode, message.CAT, message.SIC, message.SAC, message.flightLevel, message.trackNumber, message.direction, message.refreshratio);
                    }
                }
                s++;
            }
            ShowMarkers();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            time = 86400;
            foreach (CATALL message in listCATALL)
            {
                if (message.timeOfDay < time)
                {
                    time = message.timeOfDay;
                }
            }
            markers.Clear();
            OverlayMarkers.Markers.Clear();
            timeLabel.Text = Convert.ToString((time / (60 * 60)) % 24) + " : " + Convert.ToString((time / 60) % 60) + " : " + Convert.ToString((time % 60));
            startButton.Text = "Start";
            
        }

        private void speedButton_Click(object sender, EventArgs e)
        {
            if (speedButton.Text=="Speed (x1)")
            {
                timer1.Interval = 100;
                speedButton.Text = "Speed (x10)";
            }
            else if (speedButton.Text == "Speed (x10)")
            {
                timer1.Interval = 20;
                speedButton.Text = "Speed (x50)";
            }
            else if (speedButton.Text == "Speed (x50)")
            {
                timer1.Interval = 1000;
                speedButton.Text = "Speed (x1)";
            }
        }
    }
}
