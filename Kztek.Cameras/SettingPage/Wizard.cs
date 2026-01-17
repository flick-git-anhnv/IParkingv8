using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class Wizard : Form
    {
        private string title;
        private int currentPage;
        private Control currentControl = null;

        public Wizard()
        {
            InitializeComponent();
        }

        // Add page
        public void AddPage(IWizardPage page)
        {
            Control ctrl = (Control)page;

            workPanel.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;

            page.StateChanged += new EventHandler(page_StateChanged);
            page.Reset += new EventHandler(page_Reset);
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            // save original title
            title = this.Text;

            // set current page to the first
            SetCurrentPage(0);
        }

        // Update control buttons state
        private void UpdateControlButtons()
        {
            // "Back" button
            btnBack.Enabled = (currentPage != 0);
            // "Next" button
            btnNext.Enabled = ((currentPage < workPanel.Controls.Count - 1) && (currentControl != null) && (((IWizardPage)currentControl).Completed));
            // "Finish" button
            btnFinish.Enabled = true;
            foreach (IWizardPage page in workPanel.Controls)
            {
                if (!page.Completed)
                {
                    btnFinish.Enabled = false;
                    break;
                }
            }
        }

        // Set current page
        private void SetCurrentPage(int n)
        {
            OnPageChanging(n);

            // hide previous page
            if (currentControl != null)
            {
                currentControl.Hide();
            }

            //
            currentPage = n;

            // update dialog text
            this.Text = title + " - Page " + ((int)(n + 1)).ToString() + " of " + workPanel.Controls.Count.ToString();

            // show new page
            if (workPanel.Controls.Count > 0)
            {
                currentControl = workPanel.Controls[currentPage];
                IWizardPage page = (IWizardPage)currentControl;

                currentControl.Show();

                // description
                descriptionLabel.Text = page.PageDescription;

                // notify the page
                page.Display();

                // update conrol buttons
                UpdateControlButtons();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (((IWizardPage)currentControl).Apply() == true)
            {
                if (currentPage < workPanel.Controls.Count - 1)
                {
                    SetCurrentPage(currentPage + 1);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                SetCurrentPage(currentPage - 1);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (((IWizardPage)currentControl).Apply() == true)
            {
                OnFinish();

                // close the dialog
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // On page state changed
        private void page_StateChanged(object sender, System.EventArgs e)
        {
            // update conrol buttons
            UpdateControlButtons();
        }

        // Reset on page
        private void page_Reset(object sender, System.EventArgs e)
        {
            OnResetOnPage(workPanel.Controls.GetChildIndex((Control)sender));

            // update conrol buttons
            UpdateControlButtons();
        }

        // Before page changing
        protected virtual void OnPageChanging(int page)
        {
        }

        // Reset event ocuren on page
        protected virtual void OnResetOnPage(int page)
        {
        }

        // On dialog finish
        protected virtual void OnFinish()
        {
        }
    }
}