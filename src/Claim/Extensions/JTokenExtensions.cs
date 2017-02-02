using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Claim.Extensions
{
    public static class JTokenExtensions
    {
        public static object CastToSystemType(this JToken token, Type targetType, bool allowImplicitTypeConversions = false)
        {
            if(!allowImplicitTypeConversions)
                AssertTypeMatch(token, targetType);

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

        private static void AssertTypeMatch(JToken token, Type targetType)
        {
            var ret = false;

            switch (token.Type)
            {
                case JTokenType.Integer:
                    ret = new[] { typeof(byte), typeof(short), typeof(int), typeof(long) }.Contains(targetType);
                    break;            
                case JTokenType.Float:
                    ret = new[] { typeof(float), typeof(double) }.Contains(targetType);
                    break;
                case JTokenType.String:
                    ret = new[] { typeof(string), typeof(char) }.Contains(targetType);
                    break;
                case JTokenType.Boolean:
                    ret = typeof(bool) == targetType;
                    break;
                case JTokenType.Date:
                    ret = typeof(DateTime) == targetType;
                    break;
                case JTokenType.Bytes:
                    ret = typeof(byte[]) == targetType;
                    break;
                case JTokenType.Guid:
                    ret = typeof(Guid) == targetType;
                    break;
                case JTokenType.Uri:
                    ret = typeof(Uri) == targetType;
                    break;
                case JTokenType.TimeSpan:
                    ret = typeof(TimeSpan) == targetType;
                    break;
            }

            if (ret)
                return;

            throw new InvalidCastException($"Cannot convert the type '{token.Type}' to '{targetType.FullName}'.");
        }
    }
}
