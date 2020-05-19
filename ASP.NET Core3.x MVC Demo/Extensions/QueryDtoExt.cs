using System.Reflection;
using System.Text;
using Demo.Dto.Dtos;

namespace ASP.NET_Core3.x_MVC_Demo.Extensions
{
    public static class QueryDtoExt
    {
        /// <summary>
        /// 转化QueryString
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string QueryStringFormat(this QueryBase query)
        {
            var queryStringBuilder = new StringBuilder();
            if (query == null) return null;

            foreach (var propInfo in query.GetType().GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = propInfo.GetValue(query, null);
                if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    continue;
                }
                queryStringBuilder.Append($"{propInfo.Name}={propertyValue}&");
            }
            return queryStringBuilder.ToString().TrimEnd('&');
        }
    }
}
