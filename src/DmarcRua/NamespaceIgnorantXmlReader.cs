//
// NamespaceIgnorantXmlReader.cs
//
// Author: Dan Nielsen (dnielsen@fastmail.fm)
// Copyright (c) Dan Nielsen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Xml;

namespace DmarcRua;

public class NamespaceIgnorantXmlReader(XmlReader reader) : XmlReader
{
    public override bool Read() => reader.Read();
    public override int AttributeCount => reader.AttributeCount;
    public override string BaseURI => reader.BaseURI;
    public override int Depth => reader.Depth;
    public override bool EOF => reader.EOF;
    public override bool HasValue => reader.HasValue;
    public override bool IsEmptyElement => reader.IsEmptyElement;
    public override string LocalName => reader.LocalName;

    // Always return an empty namespace.
    public override string NamespaceURI => string.Empty;

    public override string Name => reader.Name;
    public override string Value => reader.Value;
    public override XmlNameTable NameTable => reader.NameTable;
    public override XmlNodeType NodeType => reader.NodeType;
    public override ReadState ReadState => reader.ReadState;
    public override bool CanResolveEntity => reader.CanResolveEntity;

    public override string Prefix => reader.Prefix;

    public override void Close() => reader.Close();

    public override string GetAttribute(string name) => reader
        .GetAttribute(name);

    public override string GetAttribute(string name, string namespaceURI) =>
        reader.GetAttribute(name, namespaceURI);

    public override string GetAttribute(int i) => reader
        .GetAttribute(i);

    public override bool MoveToAttribute(string name) => reader
        .MoveToAttribute(name);

    public override bool MoveToAttribute(string name, string ns) => reader
        .MoveToAttribute(name, ns);

    public override bool MoveToElement() => reader
        .MoveToElement();

    public override bool MoveToFirstAttribute() => reader
        .MoveToFirstAttribute();

    public override bool MoveToNextAttribute() => reader
        .MoveToNextAttribute();

    public override bool ReadAttributeValue() => reader
        .ReadAttributeValue();

    public override void Skip() => reader.Skip();

    public override void ResolveEntity() => reader
        .ResolveEntity();

    public override string LookupNamespace(string prefix)
    {
        return reader.LookupNamespace(prefix);
    }
}