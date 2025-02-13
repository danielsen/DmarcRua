//
// Rua.cs
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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace DmarcRua
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [XmlRoot("feedback", IsNullable = false)]
    public class Feedback
    {
        [XmlElement("report_metadata", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("report_metadata")]
        public ReportMetadataType ReportMetadata { get; set; }

        [XmlElement("policy_published", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("policy_published")]
        public PolicyPublishedType PolicyPublished { get; set; }

        [XmlElement("record", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("record")]
        public RecordType[] Record { get; set; }

        [XmlElement("version", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [XmlElement("extension", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("extension")]
        public ExtensionType Extension { get; set; }
    }

    [Serializable]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DiscoveryType
    {
        [XmlEnum("psl")] Psl,
        [XmlEnum("treewalk")] Treewalk
    }

    [Serializable]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TestingType
    {
        [XmlEnum("n")] No,
        [XmlEnum("y")] Yes
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class ReportMetadataType
    {
        [XmlElement("org_name", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("org_name")]
        public string OrgName { get; set; }

        [XmlElement("email", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [XmlElement("extra_contact_info", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("extra_contact_info")]
        public string ExtraContactInfo { get; set; }

        [XmlElement("report_id", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("report_id")]
        public string ReportId { get; set; }

        [XmlElement("date_range", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("date_range")]
        public DateRangeType DateRange { get; set; }

        [XmlElement("error", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("error")]
        public string[] Error { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class DateRangeType
    {
        [XmlElement("begin", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("begin")]
        public long Begin { get; set; }

        [XmlElement("end", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("end")]
        public long End { get; set; }
    }

    [Serializable]
    public enum SpfDomainScope
    {
        [XmlEnum("mfrom")] MFrom
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class SpfAuthResultType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [XmlElement("result", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("result")]
        public SpfResultType Result { get; set; }

        [XmlElement("scope", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("scope")]
        public SpfDomainScope? Scope { get; set; }

        [XmlElement("human_result", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("human_result")]
        public string HumanResult { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SpfResultType
    {
        [XmlEnum("none")] None,
        [XmlEnum("neutral")] Neutral,
        [XmlEnum("pass")] Pass,
        [XmlEnum("fail")] Fail,
        [XmlEnum("softfail")] SoftFail,
        [XmlEnum("temperror")] TempError,
        [XmlEnum("permerror")] PermError,

        // Non-standard values observed in the wild.
        [XmlEnum("hardfail")] HardFail = PermError,
        [XmlEnum("")] Default = None
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]    
    public class DKIMAuthResultType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [XmlElement("result", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("result")]
        public DKIMResultType Result { get; set; }

        [XmlElement("human_result", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("human_result")]
        public string HumanResult { get; set; }

        [XmlElement("selector", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("selector")]
        public string Selector { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DKIMResultType
    {
        [XmlEnum("none")] None,
        [XmlEnum("neutral")] Neutral,
        [XmlEnum("pass")] Pass,
        [XmlEnum("fail")] Fail,
        [XmlEnum("policy")] Policy,
        [XmlEnum("softfail")] SoftFail,
        [XmlEnum("temperror")] TempError,
        [XmlEnum("permerror")] PermError,

        // Non-standard values observed in the wild.
        [XmlEnum("hardfail")] HardFail = PermError,
        [XmlEnum("")] Default = None
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class AuthResultType
    {
        [XmlElement("dkim", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("dkim")]
        public DKIMAuthResultType[] Dkim { get; set; }

        [XmlElement("spf", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("spf")]
        public SpfAuthResultType[] Spf { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class IdentifierType
    {
        [XmlElement("envelope_to", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("envelope_to")]
        public string EnvelopeTo { get; set; }

        [XmlElement("header_from", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("header_from")]
        public string HeaderFrom { get; set; }

        [XmlElement("envelope_from", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("envelope_from")]
        public string EnvelopeFrom { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class PolicyOverrideReason
    {
        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("type")]
        public PolicyOverrideType Type { get; set; }

        [XmlElement("comment", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PolicyOverrideType
    {
        [XmlEnum("forwarded")] Forwarded,
        [XmlEnum("ampled_out")] SampledOut,
        [XmlEnum("trusted_forwarded")] TrustedForwarder,
        [XmlEnum("mailing_list")] MailingList,
        [XmlEnum("local_policy")] LocalPolicy,
        [XmlEnum("other")] Other,
        [XmlEnum("")] None
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class PolicyEvaluatedType
    {
        [XmlElement("disposition", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("disposition")]
        public DispositionType Disposition { get; set; }

        [XmlElement("dkim", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("dkim")]
        public DMARCResultType Dkim { get; set; }

        [XmlElement("spf", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("spf")]
        public DMARCResultType Spf { get; set; }

        [XmlElement("reason", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("reason")]
        public PolicyOverrideReason[] Reason { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DispositionType
    {
        [XmlEnum("none")] None,
        [XmlEnum("quarantine")] Quarantine,
        [XmlEnum("reject")] Reject,
        [XmlEnum("nil")] Nil = None,
        [XmlEnum("")] Default = None
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DMARCResultType
    {
        [XmlEnum("pass")] Pass,
        [XmlEnum("fail")] Fail
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class RowType
    {
        [XmlElement("source_ip", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("source_ip")]
        public string SourceIp { get; set; }

        [XmlElement("count", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [XmlElement("policy_evaluated", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("policy_evaluated")]
        public PolicyEvaluatedType PolicyEvaluated { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class RecordType
    {
        [XmlElement("row", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("row")]
        public RowType Row { get; set; }

        [XmlElement("identifiers", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("identifiers")]
        public IdentifierType Identifiers { get; set; }

        [XmlElement("auth_results", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("auth_results")]
        public AuthResultType AuthResults { get; set; }

        [XmlAnyElement]
        [JsonIgnore]
        public XmlElement[] Extensions { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class PolicyPublishedType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [XmlElement("adkim", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("adkim")]
        public AlignmentType? Adkim { get; set; }

        [XmlElement("aspf", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("aspf")]
        public AlignmentType? Aspf { get; set; }

        [XmlElement("p", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("p")]
        public DispositionType P { get; set; }

        [XmlElement("np", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("np")]
        public DispositionType? Np { get; set; }

        [XmlElement("sp", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("sp")]
        public DispositionType Sp { get; set; }

        [XmlElement("pct", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("pct")]
        public string Percent { get; set; }

        [XmlElement("fo", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("fo")]
        public string Fo { get; set; }

        [XmlElement("ri", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("ri")]
        public int Ri { get; set; }

        [XmlElement("rua", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("rua")]
        public string Rua { get; set; }

        [XmlElement("ruf", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("ruf")]
        public string Ruf { get; set; }

        [XmlElement("rf", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("rf")]
        public string Rf { get; set; }

        [XmlElement("v", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("v")]
        public string V { get; set; }

        [XmlElement("testing", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("testing")]
        public TestingType? Testing { get; set; }

        [XmlElement("discovery_method", Form = XmlSchemaForm.Unqualified)]
        [JsonPropertyName("discovery_method")]
        public DiscoveryType? DiscoveryMethod { get; set; }
    }

    [Serializable]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public enum AlignmentType
    {
        [XmlEnum("r")] Relaxed,
        [XmlEnum("s")] Strict
    }

    [Serializable]
    public class ExtensionType
    {
        [JsonIgnore]
        [XmlAnyElement] 
        public XmlElement[] Any { get; set; }

        [XmlIgnore]
        [JsonPropertyName("Any")]  
        public string[] JsonAny { get; set; }
    }
}
