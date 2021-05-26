﻿using MesaSuite.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MesaSuite
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public Dictionary<Panel, string> GetRequiredPermissionsForButton()
        {
            return new Dictionary<Panel, string>()
            {
                { pnlMCSync, "" },
                { pnlUserManagement, "User" }
            };
        }

        private void cmdLogIn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
                {
                    Authentication.LogOut();
                }
                else
                {
                    Authentication.GetAuthToken(true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            foreach(Panel panel in flow.Controls.OfType<Panel>())
            {
                panel.Visible = false;
            }

            pboxMCSyncLogo.Visible = false;
            pboxUser.Visible = false;
            lblVersion.Text = "Version " + Application.ProductVersion;
            Authentication.OnLoggedIn += Authentication_OnLoggedIn;
            Authentication.OnLoggedOut += Authentication_OnLoggedOut;
            Authentication.OnPermissionsUpdate += Authentication_OnPermissionsUpdate;

            if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLogOutBase;
                lblLogInStatus.Text = "Logged In";
            }
            else
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLoginBase;
                lblLogInStatus.Text = "Not Logged In";
            }

            Authentication_OnPermissionsUpdate(sender, e);
        }

        private void Authentication_OnPermissionsUpdate(object sender, EventArgs e)
        {
            Dictionary<Panel, string> permissionsForButton = GetRequiredPermissionsForButton();
            foreach(Panel panel in flow.Controls.OfType<Panel>())
            {
                if (!permissionsForButton.ContainsKey(panel))
                {
                    continue;
                }

                string requiredPermission = permissionsForButton[panel];

                Invoke(new MethodInvoker(() =>
                {
                    panel.Visible = string.IsNullOrEmpty(requiredPermission) || Authentication.GetPermissionsByModule(requiredPermission).Any();
                }));
            }
        }

        private void Authentication_OnLoggedOut(object sender, EventArgs e)
        {
            lblLogInStatus.Text = "Not Logged In";
        }

        private void Authentication_OnLoggedIn(object sender, EventArgs e)
        {
            lblLogInStatus.Text = "Logged In";
        }

        private void cmdMCSync_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(StartMCSync)).Start();
            Close();
        }

        private void StartMCSync()
        {
            MCSync.Program.Main(StartupArguments.GetArgsForApp("mcsync"));
        }

        private void StartProgram(Action method)
        {
            Thread thread = new Thread(new ThreadStart(method));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void pboxMCSync_MouseEnter(object sender, EventArgs e)
        {
            pboxMCSync.BackgroundImage = Properties.Resources.btnMCSync_Hover;
            pboxMCSyncLogo.Visible = true;
        }

        private void pboxMCSync_MouseLeave(object sender, EventArgs e)
        {
            pboxMCSync.BackgroundImage = Properties.Resources.btnMCSync;
            pboxMCSyncLogo.Visible = false;
        }

        private void pboxMCSync_Click(object sender, EventArgs e)
        {
            StartProgram(StartMCSync);
            Close();
        }

        private void pnlUserBtn_MouseEnter(object sender, EventArgs e)
        {
            if(Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedOut)
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLoginHover;
            }
            else if(Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLogOutHover;
            }
        }

        private void pnlUserBtn_MouseLeave(object sender, EventArgs e)
        {
            if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedOut)
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLoginBase;
            }
            else if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
            {
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLogOutBase;
            }
        }

        private void pnlUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
                {
                    Authentication.LogOut();
                }
                else
                {
                    Authentication.GetAuthToken(true);
                    pnlUserBtn.BackgroundImage = Properties.Resources.btnLogOutBase;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pnlUserBtn.BackgroundImage = Properties.Resources.btnLoginBase;
            }
        }

        private void pboxMCSync_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pboxMCSync, "Launch MCSync");
        }

        private void pnlUserBtn_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedOut)
            {
                tt.SetToolTip(this.pnlUserBtn, "Sign in to your Mesabrook account.");
            }
            else if (Authentication.AuthenticationStatus == Authentication.AuthenticationStatuses.LoggedIn)
            {
                tt.SetToolTip(this.pnlUserBtn, "Sign out of your Mesabrook account.");
            }
        }

        private void pboxUserManagement_Click(object sender, EventArgs e)
        {
            StartProgram(() => UserManagement.Program.Main(StartupArguments.GetArgsForApp("usermanagement")));
        }

        private void pboxUserManagement_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pboxMCSync, "User Management");
        }

        private void pboxUserManagement_MouseEnter(object sender, EventArgs e)
        {
            pboxUserManagement.BackgroundImage = Properties.Resources.btn_User_hover;
            pboxUser.Visible = true;
        }

        private void pboxUserManagement_MouseLeave(object sender, EventArgs e)
        {
            pboxUserManagement.BackgroundImage = Properties.Resources.Actions_im_user_icon;
            pboxUser.Visible = false;
        }
    }
}
