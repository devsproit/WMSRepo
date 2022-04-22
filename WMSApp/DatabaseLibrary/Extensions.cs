using System.Data;
using System.Reflection;

namespace DatabaseLibrary;
public static class Extensions
{
    /// <summary>
    /// Extensions Method to List<T> for Left Outer Join Operation.
    /// </summary>        
    /// <returns>Left Outer Join Result<T></returns>
    public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey, Func<TLeft, TRight, TResult> result)
    {
        return left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
             .SelectMany(
                 o => o.r.DefaultIfEmpty(),
                 (l, r) => new { lft = l.l, rght = r })
             .Select(o => result.Invoke(o.lft, o.rght));
    }

    /// <summary>
    /// Converts datatable to list<T> dynamically
    /// </summary>
    /// <typeparam name="T">Class name</typeparam>
    /// <param name="dataTable">data table to convert</param>
    /// <returns>List<T></returns>
    public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    {
        var dataList = new List<T>();

        //Define what attributes to be read from the class
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        //Read Attribute Names and Types
        var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
            Select(item => new
            {
                Name = item.Name,
                Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
            }).ToList();

        //Read Datatable column names and types
        var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
            Select(item => new {
                Name = item.ColumnName,
                Type = item.DataType
            }).ToList();

        foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
        {
            var classObj = new T();

            foreach (var dtField in dtlFieldNames)
            {
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                var field = objFieldNames.Find(x => x.Name == dtField.Name);

                if (field != null)
                {

                    if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToString(dataRow[dtField.Name]), null);
                        }
                    }
                }
            }
            dataList.Add(classObj);
        }
        return dataList;
    }

    private static string ConvertToDateString(object date)
    {
        if (date == null)
            return string.Empty;

        return Convert.ToString(Convert.ToDateTime(date));
    }

    private static string ConvertToString(object value)
    {
        if (value == null)
            return string.Empty;
        return Convert.ToString(value);
    }

    private static int? ConvertToInt(object value)
    {
        if (value == null)
            return null;
        return Convert.ToInt32(value);
    }

    private static long? ConvertToLong(object value)
    {
        if (value == null)
            return null;
        return Convert.ToInt64(value);
    }

    private static decimal? ConvertToDecimal(object value)
    {
        if (value == null)
            return null;
        return Convert.ToDecimal(value);
    }

    private static DateTime? convertToDateTime(object date)
    {
        if (date == null)
            return null;
        return Convert.ToDateTime(date);
    }
}