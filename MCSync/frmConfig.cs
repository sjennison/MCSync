﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MesaSuite.Common;
using MesaSuite.Common.Extensions;
using WK.Libraries.BetterFolderBrowserNS;
using static MCSync.Syncer;

namespace MCSync
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void cmdBrowseMods_Click(object sender, EventArgs e)
        {
            BetterFolderBrowser browser = new BetterFolderBrowser();
            browser.Title = "Select Mods folder";
            browser.Multiselect = false;
            browser.RootFolder = txtModsDirectory.Text;
            if (browser.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtModsDirectory.Text = browser.SelectedPath;
        }

        private void cmdBrowseResourcePacks_Click(object sender, EventArgs e)
        {
            BetterFolderBrowser browser = new BetterFolderBrowser();
            browser.Title = "Select Resource Packs folder";
            browser.Multiselect = false;
            browser.RootFolder = txtResourcePacksDirectory.Text;
            if (browser.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtResourcePacksDirectory.Text = browser.SelectedPath;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            UserPreferences userPreferences = UserPreferences.Get();
            Dictionary<string, object> configValues = userPreferences.Sections.GetOrSetDefault("mcsync", new Dictionary<string, object>());
            configValues["modsDirectory"] = txtModsDirectory.Text;
            configValues["resourcePackDirectory"] = txtResourcePacksDirectory.Text;
            configValues["configFilesDirectory"] = txtConfigDirectory.Text;

            if (rbClient.Checked)
            {
                configValues["mode"] = SyncMode.Client.ToString();
            }
            else if (rbServer.Checked)
            {
                configValues["mode"] = SyncMode.Server.ToString();
            }

            userPreferences.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> configValues = UserPreferences.Get().Sections.GetOrSetDefault("mcsync", () => new Dictionary<string, object>());
            txtModsDirectory.Text = configValues.GetOrSetDefault("modsDirectory", string.Empty).Cast<string>();
            txtResourcePacksDirectory.Text = configValues.GetOrSetDefault("resourcePackDirectory", "").Cast<string>();
            txtConfigDirectory.Text = configValues.GetOrSetDefault("configFilesDirectory", "").Cast<string>();
            Enum.TryParse(configValues.GetOrSetDefault("mode", SyncMode.Client.ToString()).Cast<string>(), true, out SyncMode syncMode);

            rbClient.Checked = syncMode == SyncMode.Client;
            rbServer.Checked = syncMode == SyncMode.Server;
        }

        private void cmdBrowseConfig_Click(object sender, EventArgs e)
        {
            BetterFolderBrowser browser = new BetterFolderBrowser();
            browser.Title = "Select Config folder";
            browser.Multiselect = false;
            browser.RootFolder = txtConfigDirectory.Text;
            if (browser.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtConfigDirectory.Text = browser.SelectedPath;
        }

        private void cmdModsWhitelist_Click(object sender, EventArgs e)
        {
            frmWhitelist whitelist = new frmWhitelist();
            whitelist.Text = "Mods Whitelist";
            whitelist.WhitelistName = "mods_whitelist";
            whitelist.lblIntro.Text = "Edit your Mods whitelist.";
            whitelist.ShowDialog();
        }

        private void cmdResourcePacksWhitelist_Click(object sender, EventArgs e)
        {
            frmWhitelist whitelist = new frmWhitelist();
            whitelist.Text = "Resource Packs Whitelist";
            whitelist.WhitelistName = "resourcepacks_whitelist";
            whitelist.lblIntro.Text = "Edit your Resource Packs whitelist.";
            whitelist.ShowDialog();
        }
    }
}