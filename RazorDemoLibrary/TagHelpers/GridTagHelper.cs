using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorDemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controls;

namespace RazorDemoLibrary.TagHelpers
{
    [HtmlTargetElement("custom-grid", Attributes = "total-rows, total-columns")]
    public class GridTagHelper : TagHelper
    {
        public int TotalRows { get; set; }
        public int TotalColumns { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Table table = new Table();

            for (int i = 0; i < this.TotalRows; i++)
            {
                TableRow row = new TableRow();

                for (int c = 0; c < this.TotalColumns; c++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = c.ToString();

                    row.AddCell(cell);
                }

                table.AddRow(row);
            }
            TextWriter writer = new StringWriter();

            table.RenderControl(writer);


            HtmlString htmlString = new HtmlString(writer.ToString());

            output.Content.SetHtmlContent(htmlString);
            output.Attributes.Clear();
        }


        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
