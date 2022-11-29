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

namespace AsterixDecoder
{
    public partial class AsterixDecoder : Form
    {
        DecodeFiles decodeFiles = new DecodeFiles();

        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();
        
        DataTable dataTableCAT10 = new DataTable();
        DataTable dataTableCAT21 = new DataTable();

        public AsterixDecoder()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
            clickInfo_label.Visible = false;
            info_label.Visible = false;
            process_label.Text = "Select a file to decode";
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

            this.dataTableCAT10 = decodeFiles.GetTableCAT10();
            this.dataTableCAT21 = decodeFiles.GetTableCAT21();
        }

        private void CAT10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listCAT10.Count != 0)
            {
                gridCAT21.Visible = false;

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
            if (listCAT21.Count != 0)
            {
                gridCAT10.Visible = false;

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
    }
}
