namespace AsterixDecoder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableCAT10 = new System.Windows.Forms.DataGridView();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetIdentification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetReport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlightLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeofDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositioninWGS84Coordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositioninCartesianCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositioninPolarCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackVelocityinPolarCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackVelocityinCartesianCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetSizeandOrientation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SystemStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleFleetIdentification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreprogrammedMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasuredHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode3ACode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModeSMBData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandardDeviationofPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Presence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmplitudeofPrimaryPlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalculatedAcceleration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableCAT21 = new System.Windows.Forms.DataGridView();
            this.CategoryCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SACCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SICCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetIdentificationCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackNumberCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceIdentificationCAT21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tableCAT10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCAT21)).BeginInit();
            this.SuspendLayout();
            // 
            // tableCAT10
            // 
            this.tableCAT10.AllowUserToAddRows = false;
            this.tableCAT10.AllowUserToDeleteRows = false;
            this.tableCAT10.AllowUserToOrderColumns = true;
            this.tableCAT10.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tableCAT10.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.tableCAT10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableCAT10.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableCAT10.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tableCAT10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableCAT10.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.SAC,
            this.SIC,
            this.TargetIdentification,
            this.TrackNumber,
            this.TargetReport,
            this.MessageType,
            this.FlightLevel,
            this.TimeofDay,
            this.TrackStatus,
            this.PositioninWGS84Coordinates,
            this.PositioninCartesianCoordinates,
            this.PositioninPolarCoordinates,
            this.TrackVelocityinPolarCoordinates,
            this.TrackVelocityinCartesianCoordinates,
            this.TargetSizeandOrientation,
            this.TargetAddress,
            this.SystemStatus,
            this.VehicleFleetIdentification,
            this.PreprogrammedMessage,
            this.MeasuredHeight,
            this.Mode3ACode,
            this.ModeSMBData,
            this.StandardDeviationofPosition,
            this.Presence,
            this.AmplitudeofPrimaryPlot,
            this.CalculatedAcceleration});
            this.tableCAT10.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableCAT10.Location = new System.Drawing.Point(20, 9);
            this.tableCAT10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableCAT10.Name = "tableCAT10";
            this.tableCAT10.ReadOnly = true;
            this.tableCAT10.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.tableCAT10.RowTemplate.Height = 29;
            this.tableCAT10.Size = new System.Drawing.Size(800, 300);
            this.tableCAT10.TabIndex = 0;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Width = 80;
            // 
            // SAC
            // 
            this.SAC.HeaderText = "SAC";
            this.SAC.MinimumWidth = 6;
            this.SAC.Name = "SAC";
            this.SAC.ReadOnly = true;
            this.SAC.Width = 54;
            // 
            // SIC
            // 
            this.SIC.HeaderText = "SIC";
            this.SIC.MinimumWidth = 6;
            this.SIC.Name = "SIC";
            this.SIC.ReadOnly = true;
            this.SIC.Width = 49;
            // 
            // TargetIdentification
            // 
            this.TargetIdentification.HeaderText = "Target Identification";
            this.TargetIdentification.MinimumWidth = 6;
            this.TargetIdentification.Name = "TargetIdentification";
            this.TargetIdentification.ReadOnly = true;
            this.TargetIdentification.Width = 125;
            // 
            // TrackNumber
            // 
            this.TrackNumber.HeaderText = "Track Number";
            this.TrackNumber.MinimumWidth = 6;
            this.TrackNumber.Name = "TrackNumber";
            this.TrackNumber.ReadOnly = true;
            this.TrackNumber.Width = 97;
            // 
            // TargetReport
            // 
            this.TargetReport.HeaderText = "Target Report";
            this.TargetReport.MinimumWidth = 6;
            this.TargetReport.Name = "TargetReport";
            this.TargetReport.ReadOnly = true;
            this.TargetReport.Width = 94;
            // 
            // MessageType
            // 
            this.MessageType.HeaderText = "Message Type";
            this.MessageType.MinimumWidth = 6;
            this.MessageType.Name = "MessageType";
            this.MessageType.ReadOnly = true;
            this.MessageType.Width = 97;
            // 
            // FlightLevel
            // 
            this.FlightLevel.HeaderText = "Flight Level";
            this.FlightLevel.MinimumWidth = 6;
            this.FlightLevel.Name = "FlightLevel";
            this.FlightLevel.ReadOnly = true;
            this.FlightLevel.Width = 85;
            // 
            // TimeofDay
            // 
            this.TimeofDay.HeaderText = "Time of Day";
            this.TimeofDay.MinimumWidth = 6;
            this.TimeofDay.Name = "TimeofDay";
            this.TimeofDay.ReadOnly = true;
            this.TimeofDay.Width = 70;
            // 
            // TrackStatus
            // 
            this.TrackStatus.HeaderText = "Track Status";
            this.TrackStatus.MinimumWidth = 6;
            this.TrackStatus.Name = "TrackStatus";
            this.TrackStatus.ReadOnly = true;
            this.TrackStatus.Width = 87;
            // 
            // PositioninWGS84Coordinates
            // 
            this.PositioninWGS84Coordinates.HeaderText = "Position in WGS-84 Co-ordinates";
            this.PositioninWGS84Coordinates.MinimumWidth = 6;
            this.PositioninWGS84Coordinates.Name = "PositioninWGS84Coordinates";
            this.PositioninWGS84Coordinates.ReadOnly = true;
            this.PositioninWGS84Coordinates.Width = 187;
            // 
            // PositioninCartesianCoordinates
            // 
            this.PositioninCartesianCoordinates.HeaderText = "Position in Cartesian Co-ordinates";
            this.PositioninCartesianCoordinates.MinimumWidth = 6;
            this.PositioninCartesianCoordinates.Name = "PositioninCartesianCoordinates";
            this.PositioninCartesianCoordinates.ReadOnly = true;
            this.PositioninCartesianCoordinates.Width = 193;
            // 
            // PositioninPolarCoordinates
            // 
            this.PositioninPolarCoordinates.HeaderText = "Position in Polar Co-ordinates";
            this.PositioninPolarCoordinates.MinimumWidth = 6;
            this.PositioninPolarCoordinates.Name = "PositioninPolarCoordinates";
            this.PositioninPolarCoordinates.ReadOnly = true;
            this.PositioninPolarCoordinates.Width = 173;
            // 
            // TrackVelocityinPolarCoordinates
            // 
            this.TrackVelocityinPolarCoordinates.HeaderText = "Track Velocity in Polar Coordinates";
            this.TrackVelocityinPolarCoordinates.MinimumWidth = 6;
            this.TrackVelocityinPolarCoordinates.Name = "TrackVelocityinPolarCoordinates";
            this.TrackVelocityinPolarCoordinates.ReadOnly = true;
            this.TrackVelocityinPolarCoordinates.Width = 136;
            // 
            // TrackVelocityinCartesianCoordinates
            // 
            this.TrackVelocityinCartesianCoordinates.HeaderText = "Track Velocity in Cartesian Coordinates";
            this.TrackVelocityinCartesianCoordinates.MinimumWidth = 6;
            this.TrackVelocityinCartesianCoordinates.Name = "TrackVelocityinCartesianCoordinates";
            this.TrackVelocityinCartesianCoordinates.ReadOnly = true;
            this.TrackVelocityinCartesianCoordinates.Width = 156;
            // 
            // TargetSizeandOrientation
            // 
            this.TargetSizeandOrientation.HeaderText = "Target Size and Orientation";
            this.TargetSizeandOrientation.MinimumWidth = 6;
            this.TargetSizeandOrientation.Name = "TargetSizeandOrientation";
            this.TargetSizeandOrientation.ReadOnly = true;
            this.TargetSizeandOrientation.Width = 158;
            // 
            // TargetAddress
            // 
            this.TargetAddress.HeaderText = "Target Address";
            this.TargetAddress.MinimumWidth = 6;
            this.TargetAddress.Name = "TargetAddress";
            this.TargetAddress.ReadOnly = true;
            // 
            // SystemStatus
            // 
            this.SystemStatus.HeaderText = "System Status";
            this.SystemStatus.MinimumWidth = 6;
            this.SystemStatus.Name = "SystemStatus";
            this.SystemStatus.ReadOnly = true;
            this.SystemStatus.Width = 97;
            // 
            // VehicleFleetIdentification
            // 
            this.VehicleFleetIdentification.HeaderText = "Vehicle Fleet Identification";
            this.VehicleFleetIdentification.MinimumWidth = 6;
            this.VehicleFleetIdentification.Name = "VehicleFleetIdentification";
            this.VehicleFleetIdentification.ReadOnly = true;
            this.VehicleFleetIdentification.Width = 155;
            // 
            // PreprogrammedMessage
            // 
            this.PreprogrammedMessage.HeaderText = "Pre-programmed Message";
            this.PreprogrammedMessage.MinimumWidth = 6;
            this.PreprogrammedMessage.Name = "PreprogrammedMessage";
            this.PreprogrammedMessage.ReadOnly = true;
            this.PreprogrammedMessage.Width = 158;
            // 
            // MeasuredHeight
            // 
            this.MeasuredHeight.HeaderText = "Measured Height";
            this.MeasuredHeight.MinimumWidth = 6;
            this.MeasuredHeight.Name = "MeasuredHeight";
            this.MeasuredHeight.ReadOnly = true;
            this.MeasuredHeight.Width = 113;
            // 
            // Mode3ACode
            // 
            this.Mode3ACode.HeaderText = "Mode-3A Code";
            this.Mode3ACode.MinimumWidth = 6;
            this.Mode3ACode.Name = "Mode3ACode";
            this.Mode3ACode.ReadOnly = true;
            this.Mode3ACode.Width = 104;
            // 
            // ModeSMBData
            // 
            this.ModeSMBData.HeaderText = "Mode S MB Data";
            this.ModeSMBData.MinimumWidth = 6;
            this.ModeSMBData.Name = "ModeSMBData";
            this.ModeSMBData.ReadOnly = true;
            this.ModeSMBData.Width = 88;
            // 
            // StandardDeviationofPosition
            // 
            this.StandardDeviationofPosition.HeaderText = "Standard Deviation of Position";
            this.StandardDeviationofPosition.MinimumWidth = 6;
            this.StandardDeviationofPosition.Name = "StandardDeviationofPosition";
            this.StandardDeviationofPosition.ReadOnly = true;
            this.StandardDeviationofPosition.Width = 136;
            // 
            // Presence
            // 
            this.Presence.HeaderText = "Presence";
            this.Presence.MinimumWidth = 6;
            this.Presence.Name = "Presence";
            this.Presence.ReadOnly = true;
            this.Presence.Width = 79;
            // 
            // AmplitudeofPrimaryPlot
            // 
            this.AmplitudeofPrimaryPlot.HeaderText = "Amplitude of Primary Plot";
            this.AmplitudeofPrimaryPlot.MinimumWidth = 6;
            this.AmplitudeofPrimaryPlot.Name = "AmplitudeofPrimaryPlot";
            this.AmplitudeofPrimaryPlot.ReadOnly = true;
            this.AmplitudeofPrimaryPlot.Width = 136;
            // 
            // CalculatedAcceleration
            // 
            this.CalculatedAcceleration.HeaderText = "Calculated Acceleration";
            this.CalculatedAcceleration.MinimumWidth = 6;
            this.CalculatedAcceleration.Name = "CalculatedAcceleration";
            this.CalculatedAcceleration.ReadOnly = true;
            this.CalculatedAcceleration.Width = 143;
            // 
            // tableCAT21
            // 
            this.tableCAT21.AllowUserToAddRows = false;
            this.tableCAT21.AllowUserToDeleteRows = false;
            this.tableCAT21.AllowUserToOrderColumns = true;
            this.tableCAT21.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tableCAT21.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.tableCAT21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableCAT21.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableCAT21.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableCAT21.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableCAT21.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryCAT21,
            this.SACCAT21,
            this.SICCAT21,
            this.TargetIdentificationCAT21,
            this.TrackNumberCAT21,
            this.ServiceIdentificationCAT21});
            this.tableCAT21.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableCAT21.Location = new System.Drawing.Point(20, 352);
            this.tableCAT21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableCAT21.Name = "tableCAT21";
            this.tableCAT21.ReadOnly = true;
            this.tableCAT21.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.tableCAT21.RowTemplate.Height = 29;
            this.tableCAT21.Size = new System.Drawing.Size(800, 300);
            this.tableCAT21.TabIndex = 1;
            // 
            // CategoryCAT21
            // 
            this.CategoryCAT21.HeaderText = "Category";
            this.CategoryCAT21.Name = "CategoryCAT21";
            this.CategoryCAT21.ReadOnly = true;
            this.CategoryCAT21.Width = 80;
            // 
            // SACCAT21
            // 
            this.SACCAT21.HeaderText = "SAC";
            this.SACCAT21.MinimumWidth = 6;
            this.SACCAT21.Name = "SACCAT21";
            this.SACCAT21.ReadOnly = true;
            this.SACCAT21.Width = 54;
            // 
            // SICCAT21
            // 
            this.SICCAT21.HeaderText = "SIC";
            this.SICCAT21.MinimumWidth = 6;
            this.SICCAT21.Name = "SICCAT21";
            this.SICCAT21.ReadOnly = true;
            this.SICCAT21.Width = 49;
            // 
            // TargetIdentificationCAT21
            // 
            this.TargetIdentificationCAT21.HeaderText = "Target Identification";
            this.TargetIdentificationCAT21.MinimumWidth = 6;
            this.TargetIdentificationCAT21.Name = "TargetIdentificationCAT21";
            this.TargetIdentificationCAT21.ReadOnly = true;
            this.TargetIdentificationCAT21.Width = 125;
            // 
            // TrackNumberCAT21
            // 
            this.TrackNumberCAT21.HeaderText = "Track Number";
            this.TrackNumberCAT21.Name = "TrackNumberCAT21";
            this.TrackNumberCAT21.ReadOnly = true;
            this.TrackNumberCAT21.Width = 97;
            // 
            // ServiceIdentificationCAT21
            // 
            this.ServiceIdentificationCAT21.HeaderText = "Service Identification";
            this.ServiceIdentificationCAT21.Name = "ServiceIdentificationCAT21";
            this.ServiceIdentificationCAT21.ReadOnly = true;
            this.ServiceIdentificationCAT21.Width = 130;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 749);
            this.Controls.Add(this.tableCAT21);
            this.Controls.Add(this.tableCAT10);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableCAT10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCAT21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tableCAT10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetIdentification;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlightLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeofDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositioninWGS84Coordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositioninCartesianCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositioninPolarCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackVelocityinPolarCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackVelocityinCartesianCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetSizeandOrientation;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn SystemStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleFleetIdentification;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreprogrammedMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeasuredHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode3ACode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModeSMBData;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandardDeviationofPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Presence;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmplitudeofPrimaryPlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalculatedAcceleration;
        private System.Windows.Forms.DataGridView tableCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn SACCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn SICCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetIdentificationCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackNumberCAT21;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceIdentificationCAT21;
    }
}

