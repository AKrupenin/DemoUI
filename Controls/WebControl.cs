using System.Globalization;
using System.Web.UI;

namespace Controls
{
    public abstract class WebControl
    {
        private string tagName;

        private HtmlTextWriterTag tagKey;

        private List<WebControl> _controls;

        protected virtual HtmlTextWriterTag TagKey => tagKey;

        protected virtual string TagName
        {
            get
            {
                if (tagName == null && TagKey != 0)
                {
                    tagName = Enum.Format(typeof(HtmlTextWriterTag), TagKey, "G").ToLower(CultureInfo.InvariantCulture);
                }

                return tagName;
            }
        }

        public virtual List<WebControl> Controls
        {
            get
            {
                if (_controls == null)
                {
                    _controls = new List<WebControl>();
                }

                return _controls;
            }
        }

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        private string _id;
        public virtual string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        protected WebControl()
            : this(HtmlTextWriterTag.Span)
        {
        }

        public WebControl(HtmlTextWriterTag tag)
        {
            tagKey = tag;
        }

        protected WebControl(string tag)
        {
            tagKey = HtmlTextWriterTag.Unknown;
            tagName = tag;
        }


        public virtual void AddControl(WebControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            this.Controls.Add(control);
        }

        protected virtual void AddParsedSubObject(object obj)
        {
            WebControl control = obj as WebControl;
            if (control != null)
            {
                Controls.Add(control);
            }
        }


        protected virtual void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (ID != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ID);
            }



            if (Attributes.Count > 0)
            {
                foreach (var attribute in Attributes)
                {
                    writer.AddAttribute(attribute.Key, attribute.Value);
                }
                    
                
            }
        }

        protected virtual void Render(HtmlTextWriter writer)
        {
            RenderBeginTag(writer);
            RenderContents(writer);
            RenderEndTag(writer);
        }

        public virtual void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }


        public virtual bool HasControls()
        {
            if (_controls != null)
            {
                return _controls.Count > 0;
            }

            return false;
        }

        public virtual void RenderBeginTag(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            HtmlTextWriterTag htmlTextWriterTag = TagKey;
            if (htmlTextWriterTag != 0)
            {
                writer.RenderBeginTag(htmlTextWriterTag);
            }
            else
            {
                writer.RenderBeginTag(TagName);
            }
        }

        public virtual void RenderContents(TextWriter writer)
        {
            this.RenderInternal(writer);
        }

        private void RenderInternal(TextWriter writer)
        {
            RenderChildren(writer);
        }

        protected internal virtual void RenderChildren(TextWriter writer)
        {
            List<WebControl> controls = _controls;
            RenderChildrenInternal(writer, controls);
        }

        internal void RenderChildrenInternal(TextWriter writer, List<WebControl> children)
        {

            if (children == null)
            {
                return;
            }

            foreach (WebControl child in children)
            {
                child.RenderControl(writer);
            }

        }


        public virtual void RenderControl(TextWriter writer)
        {
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer);
            RenderControlInternal(htmlTextWriter);

        }


        private void RenderControlInternal(HtmlTextWriter writer)
        {
            Render(writer);


        }
    }
}