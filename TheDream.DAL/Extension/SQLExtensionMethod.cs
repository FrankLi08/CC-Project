using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.DAL.Extension
{
    public static class SQLExtensionMethod
    {

        /// <summary>
        /// Return the value in <paramref name="columnName"/> or default value (default(T)) if the column is NULL in the database.
        /// </summary>
        /// <typeparam name="T">The datatype of the value in <paramref name="columnName"/>.</typeparam>
        /// <param name="reader">The <seealso cref="SqlDataReader"/> that is the target of this extension method.</param>
        /// <param name="columnName">The name of the column in the <paramref name="reader"/> to get the data for.</param>
        /// <returns>The strongly-type data, or default(<typeparamref name="T"/>).</returns>
        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            var columnIndex = reader.GetOrdinal(columnName);
            return reader.GetValue<T>(columnIndex);
        }
        /// <summary>
        /// Return the value in the column at  <paramref name="index"/> or default value (default(T)) if the column is NULL in the database.
        /// </summary>
        /// <typeparam name="T">The datatype of the value in <paramref name="columnName"/>.</typeparam>
        /// <param name="reader">The <seealso cref="SqlDataReader"/> that is the target of this extension method.</param>
        /// <param name="index">The zero based column ordinal of the column in the <paramref name="reader"/> to get the data for.</param>
        /// <returns>The strongly-type data, or default(<typeparamref name="T"/>).</returns>
        public static T GetValue<T>(this SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? default(T) : (T)reader.GetValue(index);
        }

        /// <summary>
        /// Adds a parameter to a <seealso cref="SqlParameterCollection"/> for a return code.
        /// </summary>
        /// <param name="parameters">The parameter collection that is the target of this extension method.</param>
        /// <param name="paramName">The name of return parameter.DEFAULT=@ReturnCode.</param>
        /// <returns>The <seealso cref="SqlParameter"/> that represents the return code.</returns>
        public static SqlParameter AddReturnCodeParameter(this SqlParameterCollection parameters, string paramName = "@ReturnCode")
        {
            // TODO: validate paramName

            var returnCodeParameter = new SqlParameter();
            returnCodeParameter.ParameterName = paramName;
            returnCodeParameter.Direction = ParameterDirection.ReturnValue;
            returnCodeParameter.SqlDbType = SqlDbType.Int;
            parameters.Add(returnCodeParameter);

            return returnCodeParameter;
        }
        /// <summary>
        /// Adds a table valued parameter to a <seealso cref="SqlParameterCollection"/>.
        /// </summary>
        /// <param name="parameters">The parameter collection that is the target of this extension method.</param>
        /// <param name="paramName">The name of table valued parameter.</param>
        /// <param name="paramType">The <seealso cref="SqlDbType"/> of the parameter (the table valued type).</param>
        /// <param name="values">The <seealso cref="DataTable"/> that represent the table valued data type.</param>
        /// <returns>The <seealso cref="SqlParameter"/> that represents the table valued parameter.</returns>
        /// <param name="paramType"></param>
        /// <returns></returns>
        public static SqlParameter AddTVPParameter(this SqlParameterCollection parameters, string paramName, string paramType, DataTable values)
        {
            var tvp = new SqlParameter(paramName, values);
            tvp.SqlDbType = SqlDbType.Structured;
            tvp.TypeName = paramType;
            parameters.Add(tvp);

            return tvp;
        }
    }
}
