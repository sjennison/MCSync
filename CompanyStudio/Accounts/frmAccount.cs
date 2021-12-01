﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Utility;

namespace CompanyStudio.Accounts
{
    public partial class frmAccount : BaseCompanyStudioContent, ISaveable
    {
        public Account Account { get; set; }
        public event EventHandler OnSave;

        public frmAccount()
        {
            InitializeComponent();
            PermissionsManager.OnPermissionChange += PermissionsManager_OnPermissionChange;
        }

        public async void Save()
        {
            loader.BringToFront();
            loader.Visible = true;

            DropDownItem<Category> selectedCategory = null;
            if (cboCategory.SelectedItem != null)
            {
                selectedCategory = (DropDownItem<Category>)cboCategory.SelectedItem;
            }

            if (Account != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "Account/Patch", PatchData.PatchMethods.Replace, Account.AccountID, new Dictionary<string, object>()
                {
                    { "Description", txtDescription.Text },
                    { "CategoryID", selectedCategory?.Object?.CategoryID }
                });
                patch.Headers.Add("CompanyID", Company.CompanyID.ToString());
                await patch.Execute();

                if (patch.RequestSuccessful)
                {
                    IsDirty = false;
                    OnSave?.Invoke(this, new EventArgs());
                    Account.Description = txtDescription.Text;
                    Account.CategoryID = selectedCategory?.Object?.CategoryID;
                    PopulateForm();
                }
                else
                {
                    loader.Visible = false;
                }
            }
            else
            {
                Account account = new Account();
                account.CompanyID = Company.CompanyID;
                account.Description = txtDescription.Text;
                account.CategoryID = selectedCategory?.Object?.CategoryID;

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Account/Post", account);
                post.Headers.Add("CompanyID", Company.CompanyID.ToString());
                Account = await post.Execute<Account>();

                if (post.RequestSuccessful)
                {
                    IsDirty = false;
                    OnSave?.Invoke(this, new EventArgs());
                    PopulateForm();
                }
                else
                {
                    loader.Visible = false;
                }
            }
        }

        private void PermissionsManager_OnPermissionChange(object sender, PermissionsManager.PermissionChangeEventArgs e)
        {
            if (e.CompanyID == Company.CompanyID && e.Permission == PermissionsManager.Permissions.ManageAccounts && !e.Value)
            {
                IsDirty = false;
                Close();
            }
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            PopulateForm();
        }

        private async void PopulateForm(bool retrieveNewAccount = false)
        {
            loader.BringToFront();
            loader.Visible = true;

            if (retrieveNewAccount)
            {
                GetData getAccount = new GetData(DataAccess.APIs.CompanyStudio, $"Account/Get");
                getAccount.Headers.Add("CompanyID", Company.CompanyID.ToString());
                getAccount.QueryString.Add("id", Account.AccountID.ToString());
                Account = await getAccount.GetObject<Account>();
            }

            cboCategory.Items.Clear();
            cboCategory.Items.Add(DropDownItem.Create<Category>(null, string.Empty));
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Category/GetForCompany");
            get.Headers.Add("CompanyID", Company.CompanyID.ToString());
            List<Category> categories = await get.GetObject<List<Category>>();

            if (categories != null)
            {
                foreach(Category category in categories)
                {
                    cboCategory.Items.Add(DropDownItem.Create(category, category.Name));
                }
            }

            if (Account != null)
            {
                cmdClose.Enabled = true;
                cmdTransfer.Enabled = true;
                txtAccountNumber.Text = Account.AccountNumber;
                txtDescription.Text = Account.Description;
                txtBalance.Text = Account.Balance.ToString("N2");
                cboCategory.SelectedItem = cboCategory.Items.Cast<DropDownItem<Category>>().FirstOrDefault(c => c.Object?.CategoryID == Account.CategoryID);

                Text = Account.Description + " - " + Company.Name;
            }
            else
            {
                cmdClose.Enabled = false;
                cmdTransfer.Enabled = false;
                Text = "New Account - " + Company.Name;
            }

            txtDescription.Focus();

            IsDirty = false;

            tabControl1_SelectedIndexChanged(this, new EventArgs());

            loader.Visible = false;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void MarkFormDirty(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void frmAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnPermissionChange -= PermissionsManager_OnPermissionChange;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            frmCloseAccount closeAccount = new frmCloseAccount();
            closeAccount.AccountToClose = Account;
            closeAccount.CompanyID = Company.CompanyID;
            closeAccount.Theme = Theme;
            DialogResult result = closeAccount.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            IsDirty = false;
            OnSave?.Invoke(this, new EventArgs());
            Close();
        }

        private int transactionPage = 1;
        private int totalTransactions = 0;
        private async void LoadTransactions()
        {
            if (Account == null)
            {
                return;
            }

            loaderTransactions.BringToFront();
            loaderTransactions.Visible = true;
            dgvTransactions.Rows.Clear();

            GetData getTransactions = new GetData(DataAccess.APIs.CompanyStudio, "Transaction/GetForAccount");
            getTransactions.Headers.Add("CompanyID", Company.CompanyID.ToString());
            getTransactions.QueryString.Add("accountID", Account.AccountID.ToString());
            getTransactions.QueryString.Add("skip", ((transactionPage - 1) * 50).ToString());
            getTransactions.QueryString.Add("take", "50");
            var responseObject = new
            {
                hasNext = false,
                hasPrevious = false,
                transactions = new List<Transaction>(),
                transactionCount = 0
            };

            responseObject = await getTransactions.GetAnonymousObject(responseObject);
            if (!getTransactions.RequestSuccessful)
            {
                loaderTransactions.Visible = false;
            }

            cmdTransFirst.Enabled = responseObject.hasPrevious;
            cmdTransPrev.Enabled = responseObject.hasPrevious;
            cmdTransNext.Enabled = responseObject.hasNext;
            cmdTransLast.Enabled = responseObject.hasNext;

            foreach(Transaction transaction in responseObject.transactions)
            {
                int row = dgvTransactions.Rows.Add();
                dgvTransactions[0, row].Value = TimeZoneInfo.ConvertTime(transaction.TransactionTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"), TimeZoneInfo.Local).ToString("MM/dd/yyyy HH:mm");
                dgvTransactions[1, row].Value = transaction.Amount.ToString("N2");
                dgvTransactions[1, row].Style.ForeColor = transaction.Amount >= 0 ? Color.DarkGreen : Color.Red;
                dgvTransactions[2, row].Value = transaction.Description;
            }

            totalTransactions = responseObject.transactionCount;

            loaderTransactions.Visible = false;
        }

        private async void LoadFiscalQuarters()
        {
            if (Account == null)
            {
                return;
            }

            loaderFQ.BringToFront();
            loaderFQ.Visible = true;

            dgvFiscalQuarters.Rows.Clear();

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "FiscalQuarter/GetForAccount");
            get.Headers.Add("CompanyID", Company.CompanyID.ToString());
            get.QueryString.Add("AccountID", Account.AccountID.ToString());

            List<FiscalQuarter> fiscalQuarters = await get.GetObject<List<FiscalQuarter>>();
            if (!get.RequestSuccessful)
            {
                loaderFQ.Visible = false;
                return;
            }

            foreach(FiscalQuarter fiscalQuarter in fiscalQuarters)
            {
                int row = dgvFiscalQuarters.Rows.Add();
                dgvFiscalQuarters[0, row].Value = fiscalQuarter.Year;
                dgvFiscalQuarters[1, row].Value = fiscalQuarter.Quarter;
                dgvFiscalQuarters[2, row].Value = fiscalQuarter.StartDate.ToString("MM/dd/yyyy");
                dgvFiscalQuarters[3, row].Value = fiscalQuarter.EndDate.ToString("MM/dd/yyyy");
                dgvFiscalQuarters[4, row].Value = fiscalQuarter.StartingBalance.ToString("N2");
                dgvFiscalQuarters[5, row].Value = fiscalQuarter.EndingBalance?.ToString("N2");

                decimal? netChange = fiscalQuarter.EndingBalance - fiscalQuarter.StartingBalance;
                decimal? netPercent = fiscalQuarter.StartingBalance == 0 ? netChange : netChange / fiscalQuarter.StartingBalance;

                dgvFiscalQuarters[6, row].Value = netChange?.ToString("N2");
                if (netChange != null)
                {
                    dgvFiscalQuarters[6, row].Style.ForeColor = netChange > 0 ? Color.DarkGreen : Color.Red;
                }

                dgvFiscalQuarters[7, row].Value = netPercent?.ToString("P");
                if (netPercent != null)
                {
                    dgvFiscalQuarters[7, row].Style.ForeColor = netPercent > 0 ? Color.DarkGreen : Color.Red;
                }
            }

            loaderFQ.Visible = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            transactionPage = 1;
            totalTransactions = 0;
            if (tctrlInfo.SelectedTab == tabTransactions)
            {
                LoadTransactions();
            }
            else if (tctrlInfo.SelectedTab == tabFiscalQuarters)
            {
                LoadFiscalQuarters();
            }
        }

        private void cmdTransNext_Click(object sender, EventArgs e)
        {
            transactionPage++;
            LoadTransactions();
        }

        private void cmdTransPrev_Click(object sender, EventArgs e)
        {
            transactionPage--;
            LoadTransactions();
        }

        private void cmdTransFirst_Click(object sender, EventArgs e)
        {
            transactionPage = 1;
            LoadTransactions();
        }

        private void cmdTransLast_Click(object sender, EventArgs e)
        {
            transactionPage = totalTransactions / 50 + 1;
            LoadTransactions();
        }

        private void cmdTransfer_Click(object sender, EventArgs e)
        {
            frmTransferFunds transfer = new frmTransferFunds();
            transfer.CompanyID = Company.CompanyID;
            transfer.Theme = Theme;
            transfer.AccountIDFrom = Account.AccountID;
            if (transfer.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            IsDirty = false;
            OnSave?.Invoke(this, new EventArgs());
            PopulateForm(true);
        }

        private void frmAccount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                PopulateForm(true);
            }
        }
    }
}
