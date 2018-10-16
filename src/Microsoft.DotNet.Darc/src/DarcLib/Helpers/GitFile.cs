// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Microsoft.DotNet.DarcLib
{
    public class GitFile
    {
        public GitFile(string filePath, XmlDocument xmlDocument) : this(filePath, GetIndentedXmlBody(xmlDocument))
        {
        }

        public GitFile(string filePath, JObject jsonObject) : this(filePath, jsonObject.ToString(Formatting.Indented))
        {
        }

        public GitFile(string filePath, string content) : this(filePath, content, "utf-8")
        {
        }

        public GitFile(string filePath, string content, string contentEncoding)
        {
            FilePath = filePath;
            Content = content.Replace(Environment.NewLine, "\n");
            ContentEncoding = contentEncoding;
        }

        public string FilePath { get; }

        public string Content { get; set; }

        public string ContentEncoding { get; set; }

        public string Mode { get; set; } = "100644";

        private static string GetIndentedXmlBody(XmlDocument xmlDocument)
        {
            XDocument doc = XDocument.Parse(xmlDocument.OuterXml);
            return $"{doc.Declaration}\n{doc}";
        }
    }
}
