﻿using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using Microsoft.CSharp;

namespace Biblioteca.Infrastructure
{
    public static class ExtensionMethods
    {
        public static string ToFriendlyName (this Type type){
            string typeName;
            using (var provider = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                typeName = provider.GetTypeOutput(typeRef);
            }
            return typeName;
        }
    }
}
