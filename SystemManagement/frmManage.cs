﻿using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemManagement.Models;

namespace SystemManagement
{
    public partial class frmManage : Form
    {
        List<Models.Program> programs = new List<Models.Program>();
        List<User> users = new List<User>();
        List<Government> governments = new List<Government>();
        List<Company> companies = new List<Company>();
        List<Domain> domains = new List<Domain>();
        
        public frmManage()
        {
            InitializeComponent();
        }

        private async void frmManage_Load(object sender, EventArgs e)
        {
            imlSmall.Images.Add("user", Properties.Resources.user);
            imlSmall.Images.Add("company", Properties.Resources.company);
            imlSmall.Images.Add("government", Properties.Resources.government);
            imlSmall.Images.Add("domain", Properties.Resources.domain_email_small);
            imlLarge.Images.Add("user", Properties.Resources.user_large);
            imlLarge.Images.Add("company", Properties.Resources.company_large);
            imlLarge.Images.Add("government", Properties.Resources.government_large);
            imlLarge.Images.Add("domain", Properties.Resources.domain_email_large);
            await LoadData();
            txtSearch.Focus();
        }

        private async Task LoadData()
        {
            Enabled = false;
            lstSecurities.Items.Clear();

            GetData getData = new GetData(DataAccess.APIs.SystemManagement, "User/GetAllUsers");
            users = await getData.GetObject<List<User>>() ?? new List<User>();

            foreach (User user in users)
            {
                AddUser(user);
            }

            getData = new GetData(DataAccess.APIs.SystemManagement, "Government/GetAll");
            governments = await getData.GetObject<List<Government>>() ?? new List<Government>();

            foreach(Government government in governments)
            {
                AddGovernment(government);
            }

            getData = new GetData(DataAccess.APIs.SystemManagement, "Company/GetAll");
            companies = await getData.GetObject<List<Company>>();

            foreach(Company company in companies)
            {
                AddCompany(company);
            }

            getData = new GetData(DataAccess.APIs.SystemManagement, "Domain/GetAll");
            domains = await getData.GetObject<List<Domain>>();

            foreach(Domain domain in domains)
            {
                AddDomain(domain);
            }

            Enabled = true;
            BringToFront();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lstSecurities.Items.Clear();

            foreach (User user in users.Where(p => p.Username.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)))
            {
                AddUser(user);
            }

            foreach(Government government in governments.Where(g => g.Name.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)))
            {
                AddGovernment(government);
            }

            foreach(Company company in companies.Where(c => c.Name.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)))
            {
                AddCompany(company);
            }

            foreach(Domain domain in domains.Where(d => d.DomainName.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)))
            {
                AddDomain(domain);
            }
        }

        private void AddUser(User user)
        {
            ListViewItem item = new ListViewItem();
            item.Text = user.Username;
            item.Tag = user.UserID;
            item.Group = lstSecurities.Groups["grpUsers"];
            item.ImageKey = "user";
            item.SubItems.Add("User");

            lstSecurities.Items.Add(item);
        }

        private void AddGovernment(Government government)
        {
            ListViewItem item = new ListViewItem();
            item.Text = government.Name;
            item.Tag = government.GovernmentID;
            item.Group = lstSecurities.Groups["grpGovernments"];
            item.ImageKey = "government";
            item.SubItems.Add("Government");

            lstSecurities.Items.Add(item);
        }

        private void AddCompany(Company company)
        {
            ListViewItem item = new ListViewItem();
            item.Text = company.Name;
            item.Tag = company.CompanyID;
            item.Group = lstSecurities.Groups["grpCompanies"];
            item.ImageKey = "company";
            item.SubItems.Add("Company");

            lstSecurities.Items.Add(item);
        }

        private void AddDomain(Domain domain)
        {
            ListViewItem item = new ListViewItem();
            item.Text = domain.DomainName;
            item.Tag = domain.DomainID;
            item.Group = lstSecurities.Groups["grpDomains"];
            item.ImageKey = "domain";
            item.SubItems.Add("Domain");

            lstSecurities.Items.Add(item);
        }

        private void lstSecurities_DoubleClick(object sender, EventArgs e)
        {
            if (lstSecurities.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach(ListViewItem item in lstSecurities.SelectedItems)
            {
                if (item.Group.Name == "grpUsers")
                {
                    OpenEditUserForm(item.Tag as long?);
                }

                if (item.Group.Name == "grpGovernments")
                {
                    frmEditGovernment editGovernment = new frmEditGovernment();
                    editGovernment.GovernmentID = (long)item.Tag;
                    editGovernment.FormClosed += EditGovernment_FormClosed;
                    editGovernment.Show();
                }

                if (item.Group.Name == "grpCompanies")
                {
                    frmEditCompany editCompany = new frmEditCompany();
                    editCompany.CompanyID = (long)item.Tag;
                    editCompany.FormClosed += EditCompany_FormClosed;
                    editCompany.Show();
                }

                if (item.Group.Name == "grpDomains")
                {
                    frmEditDomain editDomain = new frmEditDomain();
                    editDomain.DomainID = (int)item.Tag;
                    editDomain.FormClosed += EditDomain_FormClosed;
                    editDomain.Show();
                }
            }
        }

        private void ListViewTypeUpdate(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            foreach(ToolStripMenuItem viewItem in mnuView.DropDownItems)
            {
                viewItem.Checked = false;
            }

            item.Checked = true;
            lstSecurities.ShowGroups = true;

            switch(item.Name)
            {
                case "mnuLargeIcon":
                    lstSecurities.View = View.LargeIcon;
                    break;
                case "mnuSmallIcon":
                    lstSecurities.View = View.SmallIcon;
                    break;
                case "mnuList":
                    lstSecurities.View = View.List;
                    break;
                case "mnuTile":
                    lstSecurities.View = View.Tile;
                    lstSecurities.ShowGroups = false;
                    break;
                case "mnuDetails":
                    lstSecurities.View = View.Details;
                    lstSecurities.ShowGroups = false;
                    break;
            }
        }

        private async void UserFormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            form.FormClosed -= UserFormClosed;

            await LoadData();
        }

        private async void EditGovernment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            form.FormClosed -= EditGovernment_FormClosed;

            await LoadData();
        }

        private async void EditCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            form.FormClosed -= EditGovernment_FormClosed;

            await LoadData();
        }

        private async void EditDomain_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmEditDomain editDomain = (frmEditDomain)sender;
            editDomain.FormClosed -= EditDomain_FormClosed;

            await LoadData();
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected items?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            foreach(ListViewItem item in lstSecurities.SelectedItems.Cast<ListViewItem>().Where(lsv => lsv.Group.Name == "grpUsers"))
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.SystemManagement, "User/DeleteUser");
                delete.QueryString.Add("id", ((long?)item.Tag).ToString());
                await delete.Execute();
            }

            foreach (ListViewItem item in lstSecurities.SelectedItems.Cast<ListViewItem>().Where(lsv => lsv.Group.Name == "grpGovernments"))
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.SystemManagement, "Government/Delete");
                delete.QueryString.Add("id", ((long?)item.Tag).ToString());
                await delete.Execute();
            }

            foreach (ListViewItem item in lstSecurities.SelectedItems.Cast<ListViewItem>().Where(lsv => lsv.Group.Name == "grpCompanies"))
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.SystemManagement, "Company/Delete");
                delete.QueryString.Add("id", ((long?)item.Tag).ToString());
                await delete.Execute();
            }

            foreach(ListViewItem item in lstSecurities.SelectedItems.Cast<ListViewItem>().Where(lsv => lsv.Group.Name == "grpDomains"))
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.SystemManagement, "Domain/Delete");
                delete.QueryString.Add("id", ((int?)item.Tag).ToString());
                await delete.Execute();
            }

            await LoadData();
        }

        internal void OpenEditUserForm(long? userID)
        {
            frmEditUser user = new frmEditUser();
            user.UserID = userID;
            user.Show();

            user.FormClosed += new FormClosedEventHandler(UserFormClosed);
        }

        private void lstSecurities_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                mnuDelete.PerformClick();
            }
        }

        private void mnuNewUser_Click(object sender, EventArgs e)
        {
            frmNewUser newUser = new frmNewUser();
            newUser.ManageForm = this;
            newUser.Show();

            newUser.FormClosed += new FormClosedEventHandler(UserFormClosed);
        }

        private void mnuNewGovernment_Click(object sender, EventArgs e)
        {
            frmNewGovernment newGovernment = new frmNewGovernment();
            newGovernment.Show();

            newGovernment.FormClosed += NewGovernment_FormClosed;
        }

        private async void NewGovernment_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((frmNewGovernment)sender).FormClosed -= NewGovernment_FormClosed;

            await LoadData();
        }

        private void mnuNewCompany_Click(object sender, EventArgs e)
        {
            frmNewCompany newCompany = new frmNewCompany();
            newCompany.FormClosed += NewCompany_FormClosed;
            newCompany.Show();
        }

        private async void NewCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmNewCompany newCompany = (frmNewCompany)sender;
            newCompany.FormClosed -= NewCompany_FormClosed;

            await LoadData();
        }

        private void mnuNewDomain_Click(object sender, EventArgs e)
        {
            frmNewDomain newDomain = new frmNewDomain();
            newDomain.FormClosed += NewDomain_FormClosed;
            newDomain.Show();
        }

        private async void NewDomain_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmNewDomain newDomain = (frmNewDomain)sender;
            newDomain.FormClosed -= NewDomain_FormClosed;

            await LoadData();
        }
    }
}
