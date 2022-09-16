using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Controls
{
    public class TableRow : WebControl
    {
        public TableRow()
            : base(HtmlTextWriterTag.Tr)
        {
           
        }

        public virtual List<TableCell> Cells { get; private set; }

        public void AddCell(TableCell cell)
        {
            if (Cells == null)
                Cells = new List<TableCell>();

            Cells.Add(cell);

            base.Controls.Add(cell);
        }
    }
}
