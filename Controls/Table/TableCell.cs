using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Controls
{
    public class TableCell : WebControl
    {
        public TableCell()
           : base(HtmlTextWriterTag.Td)
        {
        }

        public int ColumnSpan { get; set; }

        public int RowSpan { get; set; }

        public string Text { get; set; }

        private bool _textSetByAddParsedSubObject;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            int columnSpan = ColumnSpan;
            if (columnSpan > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, columnSpan.ToString(NumberFormatInfo.InvariantInfo));
            }

            columnSpan = RowSpan;
            if (columnSpan > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, columnSpan.ToString(NumberFormatInfo.InvariantInfo));
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (HasControls())
            {
                base.AddParsedSubObject(obj);
                return;
            }

            if (obj is string res)
            {
                if (_textSetByAddParsedSubObject)
                {
                    Text += res;
                }
                else
                {
                    Text = res;
                }

                _textSetByAddParsedSubObject = true;
                return;
            }

            string text = Text;
            if (text.Length != 0)
            {
                Text = string.Empty;
                base.AddParsedSubObject(text);
            }

            base.AddParsedSubObject(obj);
        }


        public override void RenderContents(TextWriter writer)
        {
            writer.Write(Text);


        }
    }
}
