using System;

namespace MvcTemplate.Objects
{
    public abstract class BaseCodeView : BaseView, ICodeable
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}