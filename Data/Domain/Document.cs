using System;

namespace Data.Domain
{
    public class Document
    {
#pragma warning disable 649
        private Guid _id;
#pragma warning restore 649

        public virtual Guid Id
        {
            get { return _id; }
        }

        public virtual string Name { get; set; }
    }
}