using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Data.Domain;

namespace Data.Mapping
{
    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap()
        {
            Id(x => x.Id).Access.ReadOnlyPropertyThroughLowerCaseField(Prefix.Underscore);
            Map(x => x.Name);
        }
    }
}
