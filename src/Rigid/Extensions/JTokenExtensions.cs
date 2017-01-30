using System;
using Newtonsoft.Json.Linq;

namespace Rigid.Extensions
{
    public static class JTokenExtensions
    {
        public static object CastToSystemType(this JToken token, Type targetType)
        {
            if (targetType == typeof(bool))
                return (bool)token;
            if(targetType == typeof(DateTimeOffset))
                return (DateTimeOffset)token;
            if(targetType == typeof(bool?))
                return (bool?)token;
            if(targetType == typeof(long))
                return (long)token;
            if(targetType == typeof(DateTime?))
                return (DateTime?)token;
            if(targetType == typeof(DateTimeOffset?))
                return (DateTimeOffset?)token;
            if(targetType == typeof(decimal?))
                return (decimal?)token;
            if(targetType == typeof(double?))
                return (double?)token;
            if(targetType == typeof(char?))
                return (char?)token;
            if(targetType == typeof(int))
                return (int)token;
            if(targetType == typeof(short))
                return (short)token;
            if(targetType == typeof(ushort))
                return (ushort)token;
            if(targetType == typeof(char))
                return (char)token;
            if(targetType == typeof(byte))
                return (byte)token;
            if(targetType == typeof(sbyte))
                return (sbyte)token;
            if(targetType == typeof(int?))
                return (int?)token;
            if(targetType == typeof(short?))
                return (short?)token;
            if(targetType == typeof(ushort?))
                return (ushort?)token;
            if(targetType == typeof(byte?))
                return (byte?)token;
            if(targetType == typeof(sbyte?))
                return (sbyte?)token;
            if(targetType == typeof(DateTime))
                return (DateTime)token;
            if(targetType == typeof(long?))
                return (long?)token;
            if(targetType == typeof(float?))
                return (float?)token;
            if(targetType == typeof(decimal))
                return (Decimal)token;
            if(targetType == typeof(uint?))
                return (uint?)token;
            if(targetType == typeof(ulong?))
                return (ulong?)token;
            if(targetType == typeof(double))
                return (double)token;
            if(targetType == typeof(float))
                return (float)token;
            if(targetType == typeof(string))
                return (string)token;
            if(targetType == typeof(uint))
                return (uint)token;
            if(targetType == typeof(ulong))
                return (ulong)token;
            if(targetType == typeof(byte[]))
                return (byte[])token;
            if(targetType == typeof(Guid))
                return (Guid)token;
            if(targetType == typeof(Guid?))
                return (Guid?)token;
            if(targetType == typeof(TimeSpan))
                return (TimeSpan)token;
            if(targetType == typeof(TimeSpan?))
                return (TimeSpan?)token;
            if(targetType == typeof(Uri))
                return (Uri)token;

            throw new InvalidCastException($"Cannot convert the type '{token.Type}' to '{targetType.FullName}'.");
        }
    }
}
