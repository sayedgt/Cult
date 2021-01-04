using System;
using System.Data;
using System.Data.Common;
using System.Linq;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class DbConnectionExtensions
    {
        public static bool StateIsWithin(this IDbConnection connection, params ConnectionState[] states)
        {
            return connection != null && states != null && states.Length > 0 &&
                   states.Any(x => (connection.State & x) == x);
        }
        public static bool IsInState(this IDbConnection connection, ConnectionState state)
        {
            return connection != null &&
                   (connection.State & state) == state;
        }
        public static void OpenIfNot(this IDbConnection connection)
        {
            if (!connection.IsInState(ConnectionState.Open))
                connection.Open();
        }
        public static void SafeClose(this DbConnection toClose, bool dispose)
        {
            if (toClose == null)
            {
                return;
            }
            if (toClose.State != ConnectionState.Closed)
            {
                toClose.Close();
            }
            if (dispose)
            {
                toClose.Dispose();
            }
        }

        public static bool IsServerAvailable(this IDbConnection connection)
        {
            bool status;
            try
            {
                connection.Open();
                status = true;
                connection.Close();
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public static void EnsureOpen(this IDbConnection @this)
        {
            if (@this.State == ConnectionState.Closed)
            {
                @this.Open();
            }
        }

        public static bool IsOpen(this DbConnection @this)
        {
            return @this.State == ConnectionState.Open;
        }
    }
}
