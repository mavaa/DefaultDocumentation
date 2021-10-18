﻿using System;
using System.Collections.Generic;
using System.Linq;
using DefaultDocumentation.Model.Type;

namespace DefaultDocumentation.Writer.Section
{
    internal sealed class DerivedWriter : SectionWriter
    {
        public DerivedWriter()
            : base("derived")
        { }

        public override void Write(PageWriter writer)
        {
            if (writer.CurrentItem is TypeDocItem typeItem)
            {
                List<TypeDocItem> derived = writer.Context.Items.OfType<TypeDocItem>().Where(i => i.Type.DirectBaseTypes.Select(t => t.GetDefinition() ?? t).Contains(typeItem.Type)).OrderBy(i => i.FullName).ToList();
                if (derived.Count > 0)
                {
                    writer
                        .EnsureLineStart()
                        .AppendLine()
                        .AppendLine("Derived");

                    foreach (TypeDocItem t in derived)
                    {
                        writer
                            .Append("&#8627; ")
                            .AppendLink(t)
                            .AppendLine();
                    }
                }
            }
        }
    }
}
