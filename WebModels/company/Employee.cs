﻿using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System.Collections;
using System.Collections.Generic;
using WebModels.security;

namespace WebModels.company
{
    [Table("6E0513C5-D9B0-4792-9D31-72553ED41676")]
    [Unique(new string[] { "UserID", "CompanyID" })]
    public class Employee : DataObject
    {
        protected Employee() : base() { }

        private long? _employeeID;
        [Field("074E6255-F2D9-4610-B7B1-E859F999D6BE")]
        public long? EmployeeID
        {
            get { CheckGet(); return _employeeID; }
            set { CheckSet(); _employeeID = value; }
        }

        private long? _companyID;
        [Field("AFCA879D-6C69-41EC-AB1F-339F3565249F")]
        [Required]
        public long? CompanyID
        {
            get { CheckGet(); return _companyID; }
            set { CheckSet(); _companyID = value; }
        }

        private Company _company = null;
        [Relationship("F4DF162A-E7E7-4E52-B9EE-1940CEC957AD")]
        public Company Company
        {
            get { CheckGet(); return _company; }
        }

        private long? _userID;
        [Field("D4EEFDA8-8BD3-49BF-9D04-D71689900E0A")]
        [Required]
        public long? UserID
        {
            get { CheckGet(); return _userID; }
            set { CheckSet(); _userID = value; }
        }

        private User _user = null;
        [Relationship("8D17A10B-8B0C-40E1-A266-100A6B555F14")]
        public User User
        {
            get { CheckGet(); return _user; }
        }

        private bool _manageEmails;
        [Field("37403CD7-6FF4-41DE-A6E4-DA40BFA108FF")]
        public bool ManageEmails
        {
            get { CheckGet(); return _manageEmails; }
            set { CheckSet(); _manageEmails = value; }
        }

        private bool _manageEmployees;
        [Field("31A9F3D3-8681-4BF9-8302-68181F10B1D5")]
        public bool ManageEmployees
        {
            get { CheckGet(); return _manageEmployees; }
            set { CheckSet(); _manageEmployees = value; }
        }

        private bool _manageAccounts;
        [Field("2C9CAC2E-7CEF-4425-B6D3-7169355EC056")]
        public bool ManageAccounts
        {
            get { CheckGet(); return _manageAccounts; }
            set { CheckSet(); _manageAccounts = value; }
        }

        private string _employeeName;
        [Field("E792C619-76D5-4A69-8264-BF7D067C25DF", HasOperation = true)]
        public string EmployeeName
        {
            get { CheckGet(); return _employeeName; }
        }

        public static OperationDelegate EmployeeNameOperation
        {
            get
            {
                return (myAlias) =>
                {
                    ISelectQuery selectQuery = SQLProviderFactory.GetSelectQuery();
                    selectQuery.SelectList = new List<Select>() { "Username" };
                    selectQuery.Table = new Table("security", "User", "U");
                    selectQuery.WhereCondition = new Condition()
                    {
                        Left = (Field)"U.UserID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (Field)$"{myAlias}.UserID"
                    };

                    return new SubQuery(selectQuery);
                };
            }
        }

        public static IEnumerable<string> GetPermissionFieldNames()
        {
            yield return nameof(ManageEmails);
            yield return nameof(ManageEmployees);
            yield return nameof(ManageAccounts);
        }
    }
}
