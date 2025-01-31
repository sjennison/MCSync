﻿using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Validation;
using OAuth.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebModels.auth;

namespace OAuth.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            Client client = DataObjectFactory.Create<Client>();

            if (Request.QueryString.AllKeys.Contains("clientIdentifier"))
            {
                string clientIdentifierString = Request.QueryString["clientIdentifier"];
                if (Guid.TryParse(clientIdentifierString, out Guid clientIdentifier))
                {
                    client.ClientIdentifier = clientIdentifier;
                }
            }

            if (Request.QueryString.AllKeys.Contains("redirectionUri"))
            {
                client.RedirectionURI = Request.QueryString["redirectionUri"];
            }

            return View(client);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string[] keys = Request.Form.AllKeys;

            if (!keys.Contains("user"))
            {
                ModelState.AddModelError("user", "Username is a required field");
            }

            if (!keys.Contains("password"))
            {
                ModelState.AddModelError("password", "Password is a required field");
            }

            if (keys.Contains("user") && keys.Contains("password"))
            {
                string user = Request.Form["user"];
                string password = Request.Form["password"];

                if (!LDAPLogin.TryLDAPLogin(user, password))
                {
                    ModelState.AddModelError("user", "Invalid username/password");
                }

                ModelState.SetModelValue("user", new ValueProviderResult(user, user, CultureInfo.CurrentCulture));
            }

            Guid clientIdentifier = new Guid();
            if (keys.Contains("clientIdentifier") && !Guid.TryParse(form["clientIdentifier"], out clientIdentifier))
            {
                ModelState.AddModelError("ClientIdentifier", "Client Identifier is not in the correct format");
            }

            ITransaction transaction = null;
            try
            {
                transaction = SQLProviderFactory.GenerateTransaction();
                Client client = DataObjectFactory.Create<Client>();
                client.ClientIdentifier = clientIdentifier.Equals(new Guid()) ? null : clientIdentifier as Guid?;
                client.RedirectionURI = form.Get("RedirectionURI");
                
                if (!client.Save(transaction))
                {
                    foreach (Error error in client.Errors)
                    {
                        ModelState.AddModelError(error.FieldName, error.Message);
                    }
                }

                if (!ModelState.IsValid)
                {
                    transaction.Rollback();
                    return View(client);
                }

                transaction.Commit();
                return View("Success");
            }
            finally
            {
                if (transaction != null && transaction.IsActive)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}