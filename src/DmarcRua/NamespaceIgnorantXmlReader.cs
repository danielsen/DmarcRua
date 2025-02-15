using System.Xml;

public class NamespaceIgnorantXmlReader : XmlReader
{
    private readonly XmlReader _reader;

    public NamespaceIgnorantXmlReader(XmlReader reader)
    {
        _reader = reader;
    }

    public override bool Read() => _reader.Read();
    public override int AttributeCount => _reader.AttributeCount;
    public override string BaseURI => _reader.BaseURI;
    public override int Depth => _reader.Depth;
    public override bool EOF => _reader.EOF;
    public override bool HasValue => _reader.HasValue;
    public override bool IsEmptyElement => _reader.IsEmptyElement;
    public override string LocalName => _reader.LocalName;

    // Always return an empty namespace.
    public override string NamespaceURI => string.Empty;

    public override string Name => _reader.Name;
    public override string Value => _reader.Value;
    public override XmlNameTable NameTable => _reader.NameTable;
    public override XmlNodeType NodeType => _reader.NodeType;
    public override ReadState ReadState => _reader.ReadState;
    public override bool CanResolveEntity => _reader.CanResolveEntity;

    public override string Prefix => _reader.Prefix;

    public override void Close() => _reader.Close();

    public override string GetAttribute(string name) => _reader.GetAttribute(name);
    public override string GetAttribute(string name, string namespaceURI) => _reader.GetAttribute(name, namespaceURI);
    public override string GetAttribute(int i) => _reader.GetAttribute(i);

    public override bool MoveToAttribute(string name) => _reader.MoveToAttribute(name);
    public override bool MoveToAttribute(string name, string ns) => _reader.MoveToAttribute(name, ns);
    public override bool MoveToElement() => _reader.MoveToElement();
    public override bool MoveToFirstAttribute() => _reader.MoveToFirstAttribute();
    public override bool MoveToNextAttribute() => _reader.MoveToNextAttribute();
    public override bool ReadAttributeValue() => _reader.ReadAttributeValue();

    public override void Skip() => _reader.Skip();
    public override void ResolveEntity() => _reader.ResolveEntity();

    public override string LookupNamespace(string prefix)
    {
        return _reader.LookupNamespace(prefix);
    }
}