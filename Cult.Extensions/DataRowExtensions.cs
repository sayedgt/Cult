using Cult.Extensions.ExtraObject;
using Cult.Extensions.ExtraString;
using Cult.Extensions.ExtraType;
using System;
using System.Data;
// ReSharper disable All 
namespace Cult.Extensions.ExtraDataRow
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string field)
        {
            return row.Get(field, default(T));
        }
        public static T Get<T>(this DataRow row, string field, T defaultValue)
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            return value.ConvertTo(defaultValue);
        }
        public static bool GetBoolean(this DataRow row, string field)
        {
            return row.GetBoolean(field, false);
        }
        public static bool GetBoolean(this DataRow row, string field, bool defaultValue)
        {
            var value = row[field];
            return value is bool b ? b : defaultValue;
        }
        public static byte[] GetBytes(this DataRow row, string field)
        {
            return row[field] as byte[];
        }
        public static DateTime GetDateTime(this DataRow row, string field)
        {
            return row.GetDateTime(field, DateTime.MinValue);
        }
        public static DateTime GetDateTime(this DataRow row, string field, DateTime defaultValue)
        {
            var value = row[field];
            return value is DateTime time ? time : defaultValue;
        }
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field)
        {
            return new DateTimeOffset(row.GetDateTime(field), TimeSpan.Zero);
        }
        public static DateTimeOffset GetDateTimeOffset(this DataRow row, string field, DateTimeOffset defaultValue)
        {
            var dt = row.GetDateTime(field);
            return dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue;
        }
        public static decimal GetDecimal(this DataRow row, string field)
        {
            return row.GetDecimal(field, 0);
        }
        public static decimal GetDecimal(this DataRow row, string field, long defaultValue)
        {
            var value = row[field];
            return value is decimal value1 ? value1 : defaultValue;
        }
        public static Guid GetGuid(this DataRow row, string field)
        {
            var value = row[field];
            return value is Guid guid ? guid : Guid.Empty;
        }
        public static int GetInt32(this DataRow row, string field)
        {
            return row.GetInt32(field, 0);
        }
        public static int GetInt32(this DataRow row, string field, int defaultValue)
        {
            var value = row[field];
            return value is int i ? i : defaultValue;
        }
        public static long GetInt64(this DataRow row, string field)
        {
            return row.GetInt64(field, 0);
        }
        public static long GetInt64(this DataRow row, string field, int defaultValue)
        {
            var value = row[field];
            return value is long l ? l : defaultValue;
        }
        public static string GetString(this DataRow row, string field)
        {
            return row.GetString(field, null);
        }
        public static string GetString(this DataRow row, string field, string defaultValue)
        {
            var value = row[field];
            return value is string s ? s : defaultValue;
        }
        public static Type GetType(this DataRow row, string field)
        {
            return row.GetType(field, null);
        }
        public static Type GetType(this DataRow row, string field, Type defaultValue)
        {
            var classType = row.GetString(field);
            if (classType.IsNotEmpty())
            {
                var type = Type.GetType(classType);
                if (type != null)
                    return type;
            }
            return defaultValue;
        }
        public static object GetTypeInstance(this DataRow row, string field)
        {
            return row.GetTypeInstance(field, null);
        }
        public static object GetTypeInstance(this DataRow row, string field, Type defaultValue)
        {
            var type = row.GetType(field, defaultValue);
            return type != null ? Activator.CreateInstance(type) : null;
        }
        public static T GetTypeInstance<T>(this DataRow row, string field) where T : class
        {
            return row.GetTypeInstance(field, null) as T;
        }
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field, Type type) where T : class
        {
            var instance = row.GetTypeInstance(field, null) as T;
            return instance ?? Activator.CreateInstance(type) as T;
        }
        public static T GetTypeInstanceSafe<T>(this DataRow row, string field) where T : class, new()
        {
            var instance = row.GetTypeInstance(field, null) as T;
            return instance ?? new T();
        }
        public static bool IsDbNull(this DataRow row, string field)
        {
            var value = row[field];
            return value == DBNull.Value;
        }
        public static TValue Value<TValue>(this DataRow row, string columnName)
        {
            var toReturn = default(TValue);
            if (
                !(row == null || string.IsNullOrEmpty(columnName) ||
                  !row.Table.Columns.Contains(columnName)))
            {
                var columnValue = row[columnName];
                if (columnValue != DBNull.Value)
                {
                    var destinationType = typeof(TValue);
                    if (typeof(TValue).IsNullableValueType())
                    {
                        destinationType = destinationType.GetGenericArguments()[0];
                    }
                    if (columnValue is TValue value)
                    {
                        toReturn = value;
                    }
                    else
                    {
                        toReturn = (TValue)Convert.ChangeType(columnValue, destinationType);
                    }
                }
            }
            return toReturn;
        }
    }
}
