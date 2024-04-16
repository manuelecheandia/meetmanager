//Student: Manuel Echeandia
//Class: WMAD Sr.A
// Desc: MeetManager Assignment
// Course: N-Tier 


using MeetManager.Model;
using MeetManager.Service;
using MeetManager.Types;
using System.ComponentModel;

namespace WinFrontEnd
{
    public partial class Form1 : Form
    {
        #region Fields

        private Meet meet = new();

        private readonly MeetService meetService = new();
        private BindingList<EventListDTO> allEvents = new();
        private BindingList<MeetEventsVM> meetEventsDisplay = new();


        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Events
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetMeets();
                GetVenues();
                LoadMeetTypes();
                Setup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void cmbMeet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int meetId = (int)cmbMeet.SelectedValue;

                ClearMeetEvents();

                ToggleDetailsForm(true);
                btnDelete.Enabled = true;

                meet = meetService.GetMeet(meetId);

                PopulateFormFields(meet);

                GetAllEvents();

                SetUpMeetEventsGridView();

                SyncEventLists();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ValidateChildren(ValidationConstraints.Enabled))
                //{
                bool isAdd = false;
                PopulateMeetObject();

                //update start time of the meet events with the start time that was entered in gridview
                foreach (MeetEvent meetEvent in meet.Events)
                {
                    meetEvent.StartTime = meetEventsDisplay.FirstOrDefault(
                        ev => ev.EventId == meetEvent.EventId)?.StartTime;


                }

                if (cmbMeet.SelectedIndex != -1)
                {
                    // update
                    meetService.UpdateMeet(meet);
                }
                else
                {
                    // insert
                    meetService.AddMeet(meet);
                    isAdd = true;
                }

                string msg = "";
                if (meet.Errors.Count > 0)
                {
                    foreach (ValidationError err in meet.Errors)
                    {
                        msg += err.Message + Environment.NewLine;
                    }
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show($"Meet was saved successfully!{(isAdd ? $"The new meet id is {meet.Id}" : "")}");
                    Setup();
                    GetAllEvents();
                    dgvMeetEvents.DataSource = null;
                    dgvAllEvents.DataSource = null;
                    ClearMeetEvents();
                }
                //}
                //else
                //{
                //MessageBox.Show("Please fix all errors.");
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetailsForm();
                ClearMeetEvents();

                if (btnAddNew.Text == "ADD NEW")
                {
                    pnlSearch.Enabled = false;
                    ToggleDetailsForm(true);
                    btnDelete.Enabled = false;
                    btnAddNew.Text = "CANCEL";
                    ClearDetailsForm();
                    cmbMeet.SelectedIndex = -1;

                    GetAllEvents();

                    SetUpMeetEventsGridView();
                }
                else
                {
                    pnlSearch.Enabled = true;
                    ToggleDetailsForm(false);
                    ClearDetailsForm();
                    ClearDateTimePickers();
                    btnAddNew.Text = "ADD NEW";
                    dgvAllEvents.DataSource = null;
                    dgvMeetEvents.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this meet?",
                "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (meetService.DeleteMeet(Convert.ToInt32(cmbMeet.SelectedValue)))
                    {
                        MessageBox.Show("Meet was deleted successfully!");
                        Setup();
                    }
                    else
                    {
                        MessageBox.Show("A problem occurred deleting the meet.  Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetailsForm();
                ClearMeetEvents();
                GetAllEvents();
                SyncEventLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        #region Validation

        private void txtMeetName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtMeetName.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMeetName, "Please enter the meet name.");
            }
            else if (txtMeetName.Text.Trim().Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMeetName, "Meet name cannot exceed 100 characters.");
            }
        }

        private void txtMeetName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtMeetName, "");
        }

        private void cmbVenue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbVenue.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbVenue, "Please select a venue.");
            }
        }

        private void cmbVenue_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbVenue, "");
        }

        private void dtpEndDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtpEndDate.Value <= dtpStartDate.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpEndDate, "End date must be later than start date.");
            }
        }

        private void dtpEndDate_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(dtpEndDate, "");
        }

        private void dtpRegDeadline_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((dtpStartDate.Value - dtpRegDeadline.Value).TotalHours < 24)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpRegDeadline, "Registration deadline must be at least 24 hours before Start Date");
            }
        }

        private void dtpRegDeadline_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(dtpRegDeadline, "");
        }

        private void txtEntryFee_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            decimal entryFee;

            if (!decimal.TryParse(txtEntryFee.Text, out entryFee))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEntryFee, "Entry fee must be a valid numeric amount.");
            }
            else if (entryFee <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEntryFee, "Entry fee must greater than zero.");
            }
        }

        private void txtEntryFee_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtEntryFee, "");
        }

        private void txtFeePerEvent_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            decimal feePerEvent;
            if (!decimal.TryParse(txtFeePerEvent.Text, out feePerEvent))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFeePerEvent, "Fee per event must be a valid numeric amount.");
            }
            else if (feePerEvent < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFeePerEvent, "Fee per event cannot be less than zero.");
            }
        }

        private void txtFeePerEvent_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtFeePerEvent, "");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #endregion

        #endregion

        #region Private Methods

        private void GetMeets()
        {
            //List<MeetListDTO> meets = meetService.GetMeetList();
            cmbMeet.DataSource = meetService.GetMeetList();
            cmbMeet.DisplayMember = "Name";
            cmbMeet.ValueMember = "Id";
            cmbMeet.SelectedIndex = -1;
        }

        private void GetVenues()
        {
            cmbVenue.DataSource = new ListsService().GetVenueList();
            cmbVenue.DisplayMember = "Name";
            cmbVenue.ValueMember = "Id";
            cmbVenue.SelectedIndex = -1;
        }

        private void GetAllEvents()
        {
            List<EventListDTO> events = new ListsService().GetEventList();
            allEvents = new BindingList<EventListDTO>(events);
            dgvAllEvents.DataSource = allEvents;
        }

        private void LoadMeetTypes()
        {
            cmbMeetType.DataSource = Enum.GetValues(typeof(MeetType));
            cmbMeetType.SelectedIndex = -1;
        }

        private void PopulateFormFields(Meet m)
        {
            txtMeetName.Text = m.Name;
            dtpStartDate.Value = (DateTime)m.StartDate!;
            dtpEndDate.Value = (DateTime)m.EndDate!;
            dtpRegDeadline.Value = (DateTime)m.RegistrationDeadline!;
            txtEntryFee.Text = m.EntryFee.ToString("N2");
            txtFeePerEvent.Text = m.FeePerEvent.ToString("N2");
            txtInformation.Text = m.Information;
            cmbVenue.SelectedValue = m.VenueId;
            cmbMeetType.SelectedItem = m.MeetType;
        }

        private void ClearMeetEvents()
        {
            meet.Events.Clear();
            meetEventsDisplay.Clear();
        }

        private void SyncEventLists()
        {
            // get all the events from allEvents that are part of the meet
            List<EventListDTO> matchingItems = allEvents
            .Where(eventItem => meet.Events.Any(meetEvent => meetEvent.EventId == eventItem.EventId))
            .ToList();

            // add them to meetEventDisplay
            foreach (var item in matchingItems)
            {
                meetEventsDisplay.Add(new MeetEventsVM
                {
                    EventId = item.EventId,
                    Name = item.Name,
                    Discipline = item.Discipline,
                    Division = item.Division,
                    Gender = item.Gender,
                    StartTime = meet.Events.FirstOrDefault(meetEvent => meetEvent.EventId == item.EventId)?.StartTime
                });

                allEvents.Remove(item);
            }

        }

        private void SetUpMeetEventsGridView()
        {
            dgvMeetEvents.DataSource = meetEventsDisplay;

            foreach (DataGridViewColumn column in dgvMeetEvents.Columns)
            {
                if (column.Name != "StartTime")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void PopulateMeetObject()
        {
            meet.Id = Convert.ToInt32(cmbMeet.SelectedValue);
            meet.Name = txtMeetName.Text.Trim();
            meet.StartDate = dtpStartDate.Value;
            meet.EndDate = dtpEndDate.Value;
            meet.RegistrationDeadline = dtpRegDeadline.Value;
            meet.EntryFee = Convert.ToDecimal(txtEntryFee.Text);
            meet.FeePerEvent = Convert.ToDecimal(txtFeePerEvent.Text);
            meet.Information = txtInformation.Text.Trim();
            meet.VenueId = Convert.ToInt32(cmbVenue.SelectedValue);
            meet.MeetType = (MeetType?)cmbMeetType.SelectedValue;
        }


        private void Setup()
        {
            pnlSearch.Enabled = true;
            pnlDetails.Enabled = false;
            pnlEvents.Enabled = false;
            btnDelete.Enabled = false;
            ClearDetailsForm();
            ClearDateTimePickers();
            ToggleDetailsForm(false);
            btnAddNew.Text = "ADD NEW";

            dgvAllEvents.DataSource = null;
            dgvMeetEvents.DataSource = null;
            cmbMeet.SelectedIndex = -1;

        }

        private void ToggleDetailsForm(bool state)
        {
            pnlDetails.Enabled = state;
            pnlEvents.Enabled = state;
            btnSave.Enabled = state;
            btnClear.Enabled = state;
            if (state) FormatDateTimePickers();
        }

        /// <summary>
        /// Empties contents of the DateTime Pickers
        /// This allows us to have DateTimePickers that do not show anything
        /// </summary>
        private void ClearDateTimePickers()
        {
            foreach (DateTimePicker dtp in pnlDetails.Controls.OfType<DateTimePicker>())
            {
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }

        /// <summary>
        /// Formats the DateTimePickers to use the Long Date format
        /// This method needs to be called to reset what ClearDateTimePickers does
        /// so that we can show a date in the DateTimePickers
        /// </summary>
        private void FormatDateTimePickers()
        {
            foreach (DateTimePicker dtp in pnlDetails.Controls.OfType<DateTimePicker>())
            {
                dtp.CustomFormat = "MM/dd/yyyy HH:mm";
            }
        }

        /// <summary>
        /// Clears all meet detail form controls, and sets DateTimePickers to 
        /// current date.
        /// </summary>
        private void ClearDetailsForm()
        {
            foreach (Control ctrl in pnlDetails.Controls)
            {
                if (ctrl is TextBox)
                    ctrl.Text = "";

                if (ctrl is ComboBox)
                    ((ComboBox)ctrl).SelectedIndex = -1;

                if (ctrl is DateTimePicker)
                    ((DateTimePicker)ctrl).Value = DateTime.Now;

                if (errorProvider1.GetError(ctrl).Length > 0)
                    errorProvider1.SetError(ctrl, "");
            }

            errorProvider1.Clear();
        }

        #endregion


        private void btnAddEvents_Click(object sender, EventArgs e)
        {
            if (dgvAllEvents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least 1 event to add.");
                return;
            }

            foreach (DataGridViewRow row in dgvAllEvents.SelectedRows)
            {
                EventListDTO selectedEvent = (EventListDTO)row.DataBoundItem;

                meet.Events.Add(new MeetEvent
                {
                    EventId = selectedEvent.EventId,
                    MeetId = cmbMeet.SelectedIndex != -1 ? (int)cmbMeet.SelectedValue : 0,
                    StartTime = null
                });
            }

            SyncEventLists();


        }

        private void cmbMeetType_Validating(object sender, CancelEventArgs e)
        {
            if (cmbMeetType.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbMeetType, "Please select a meet type.");
            }
        }

        private void cmbMeetType_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbMeetType, "");
        }
    }
}