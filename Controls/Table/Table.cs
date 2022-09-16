using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Controls
{
    public class Table : WebControl
    {
        public Table()
            : base(HtmlTextWriterTag.Table)
        {

        }

        public List<TableRow> Rows { get; private set; }
        public Color BorderColor { get; set; }


        public void AddRow(TableRow row)
        {
            if (Rows == null)
                Rows = new List<TableRow>();
            Rows.Add(row);
            base.AddControl(row);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

                Color borderColor = BorderColor;
                if (!borderColor.IsEmpty)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Bordercolor, ColorTranslator.ToHtml(borderColor));
                }
            
        }
    }
}
