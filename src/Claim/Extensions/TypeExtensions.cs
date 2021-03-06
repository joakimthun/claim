﻿using System;
using System.Reflection;

namespace Claim.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterface<TInterface>(this Type type)
        {
            return typeof(TInterface).IsAssignableFrom(type);
        }
    }
}
