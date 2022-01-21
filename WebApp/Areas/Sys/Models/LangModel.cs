using System.Data;

namespace WebApp.Areas.Sys.Models
{
    public class LangModel
    {
        public static DataTable LookupData()
        {
            string sql = "select distinct code as value, name as text from sys_lang order by code";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
    }
}
