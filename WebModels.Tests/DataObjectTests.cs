﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebModels.security;

namespace WebModels.Tests
{
    [TestClass]
    public class DataObjectTests
    {
        User user = null;
        [TestMethod]
        public void AllDataObjectsHaveProtectedConstructor()
        {
            StringBuilder errors = new StringBuilder();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in assembly.GetTypes().Where(t => typeof(DataObject) != t && typeof(DataObject).IsAssignableFrom(t)))
                    {
                        if (type.GetConstructors().Length > 0)
                        {
                            errors.AppendLine($"{type.FullName}: At least one public constructor was detected - this is not allowed.");
                        }

                        ConstructorInfo[] constructorInfos = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                        ConstructorInfo constructorInfo = constructorInfos.FirstOrDefault(ci => ci.GetParameters().Length == 0);
                        if (constructorInfo == null)
                        {
                            errors.AppendLine($"{type.FullName}: No parameterless constructor detected.  A single, parameterless protected constructor is required on all Data Object type derivatives.");
                        }

                        if (constructorInfos.Length > 1)
                        {
                            errors.AppendLine($"{type.FullName}: More than one constructor detected.  A single, parameterless protected constructor is required on all Data Object type derivatives.");
                        }

                        if (constructorInfo != null && !constructorInfo.IsFamily)
                        {
                            errors.AppendLine($"{type.FullName}: Detected constructor was not protected.  The constructor must be protected.");
                        }
                    }
                }
                catch { }
            }

            Assert.AreEqual(0, errors.Length, errors.ToString());
        }

        [TestMethod]
        public void AllRelationshipsHaveReverseRelationships()
        {
            StringBuilder errors = new StringBuilder();
            foreach(SchemaObject schemaObject in Schema.GetAllSchemaObjects())
            {
                foreach(Relationship relationship in schemaObject.GetRelationships())
                {
                    SchemaObject relatedSchemaObject = relationship.RelatedSchemaObject;
                    if (!relatedSchemaObject.GetRelationshipLists().Any(rl => rl.ForeignKeyName == relationship.ForeignKeyField.FieldName))
                    {
                        errors.AppendLine($"{schemaObject.DataObjectType.FullName}: Relationship {relationship.RelationshipName} is missing a Reverse Relationship on {relatedSchemaObject.DataObjectType.FullName}");
                    }
                }
            }

            Assert.AreEqual(0, errors.Length, errors.ToString());
        }
    }
}
