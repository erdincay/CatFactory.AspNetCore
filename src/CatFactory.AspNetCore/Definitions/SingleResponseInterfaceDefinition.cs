﻿using System.Collections.Generic;
using CatFactory.DotNetCore;
using CatFactory.OOP;

namespace CatFactory.AspNetCore.Definitions
{
    public static class SingleResponseInterfaceDefinition
    {
        public static CSharpInterfaceDefinition GetSingleResponseInterfaceDefinition(this AspNetCoreProject project)
        {
            var definition = new CSharpInterfaceDefinition();

            definition.Namespace = project.GetResponsesNamespace();
            definition.Name = "ISingleResponse";

            definition.GenericTypes = new List<GenericTypeDefinition>
            {
                new GenericTypeDefinition { Name = "TModel", Constraint = "TModel : class" }
            };

            definition.Implements.Add("IResponse");
            definition.Properties.Add(new PropertyDefinition("TModel", "Model"));

            return definition;
        }
    }
}
