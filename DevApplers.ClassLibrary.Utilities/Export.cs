using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevApplers.ClassLibrary.Utilities
{
    public class Export
    {
        public IEnumerable DataSource { get; set; }
        public string FileName { get; set; }

        public void ExportToExcel()
        {
            var gridView = new GridView
            {
                DataSource = DataSource
            };
            gridView.DataBind();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = Encoding.UTF8.EncodingName;
            HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gridView.RenderControl(objHtmlTextWriter);
            //following line formats all cells as text:
            //HttpContext.Current.Response.Output.Write("<style> TD { mso-number-format:\\@; } </style>");
            HttpContext.Current.Response.Output.Write(objStringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public void ExportToJson()
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Output.Write(DataSource);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
