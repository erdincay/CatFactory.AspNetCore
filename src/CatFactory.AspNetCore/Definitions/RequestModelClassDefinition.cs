﻿using System.Collections.Generic;
using System.Linq;
using CatFactory.DotNetCore;
using CatFactory.EfCore;
using CatFactory.Mapping;
using CatFactory.OOP;

namespace CatFactory.AspNetCore.Definitions
{
    public static class RequestModelClassDefinition
    {
        public static CSharpClassDefinition GetResponsesExtensionsClassDefinition(this EntityFrameworkCoreProject project, ITable table, AspNetCoreProjectSettings settings)
        {
            var classDefinition = new CSharpClassDefinition
            {
                Namespaces = new List<string>
                {
                    "System",
                    "System.ComponentModel.DataAnnotations"
                },
                Namespace = settings.GetRequestModelsNamespace(),
                Name = table.GetRequestModelName()
            };

            foreach (var column in table.Columns.Where(item => project.Settings.ConcurrencyToken != item.Name).ToList())
            {
                var property = new PropertyDefinition(column.GetClrType(), column.GetPropertyName());

                if (table.PrimaryKey?.Key.Count > 0 && table.PrimaryKey?.Key.First() == column.Name)
                {
                    property.Attributes.Add(new MetadataAttribute("Key"));
                }

                if (!column.Nullable && table.PrimaryKey?.Key.Count > 0 && table.PrimaryKey?.Key.First() != column.Name)
                {
                    property.Attributes.Add(new MetadataAttribute("Required"));
                }

                if (column.IsString() && column.Length > 0)
                {
                    property.Attributes.Add(new MetadataAttribute("StringLength", column.Length.ToString()));
                }

                classDefinition.Properties.Add(property);
            }

            return classDefinition;
        }
    }
}
