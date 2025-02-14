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
        public ReportMetadataType ReportMetadata { get; set; }

        [XmlElement("policy_published", Form = XmlSchemaForm.Unqualified)]
        public PolicyPublishedType PolicyPublished { get; set; }

        [XmlElement("record", Form = XmlSchemaForm.Unqualified)]
        public RecordType[] Record { get; set; }

        [XmlElement("version", Form = XmlSchemaForm.Unqualified)]
        public string Version { get; set; }
        
        [XmlElement("extension", Form = XmlSchemaForm.Unqualified)]
        public ExtensionType Extension { get; set; }
    }

    [Serializable]
    public enum DiscoveryType
    {
        [XmlEnum("psl")] Psl,
        [XmlEnum("treewalk")] Treewalk
    }

    [Serializable]
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
        public string OrgName { get; set; }

        [XmlElement("email", Form = XmlSchemaForm.Unqualified)]
        public string Email { get; set; }

        [XmlElement("extra_contact_info", Form = XmlSchemaForm.Unqualified)]
        public string ExtraContactInfo { get; set; }

        [XmlElement("report_id", Form = XmlSchemaForm.Unqualified)]
        public string ReportId { get; set; }

        [XmlElement("date_range", Form = XmlSchemaForm.Unqualified)]
        public DateRangeType DateRange { get; set; }

        [XmlElement("error", Form = XmlSchemaForm.Unqualified)]
        public string[] Error { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class DateRangeType
    {
        [XmlElement("begin", Form = XmlSchemaForm.Unqualified)]
        public long Begin { get; set; }

        [XmlElement("end", Form = XmlSchemaForm.Unqualified)]
        public long End { get; set; }
    }

       [Serializable]
   public enum SpfDomainScope
   {
       [XmlEnum("mfrom")] MFrom,
       [XmlEnum("helo")] Helo
   }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class SpfAuthResultType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        public string Domain { get; set; }

        [XmlElement("result", Form = XmlSchemaForm.Unqualified)]
        public SpfResultType Result { get; set; }

        [XmlElement("scope", Form = XmlSchemaForm.Unqualified)]
        public SpfDomainScope? Scope { get; set; }

        [XmlElement("human_result", Form = XmlSchemaForm.Unqualified)]
        public string HumanResult { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
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
    // ReSharper disable once InconsistentNaming
    public class DKIMAuthResultType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        public string Domain { get; set; }

        [XmlElement("result", Form = XmlSchemaForm.Unqualified)]
        public DKIMResultType Result { get; set; }

        [XmlElement("human_result", Form = XmlSchemaForm.Unqualified)]
        public string HumanResult { get; set; }

        [XmlElement("selector", Form = XmlSchemaForm.Unqualified)]
        public string Selector { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    // ReSharper disable once InconsistentNaming
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
        public DKIMAuthResultType[] Dkim { get; set; }

        [XmlElement("spf", Form = XmlSchemaForm.Unqualified)]
        public SpfAuthResultType[] Spf { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class IdentifierType
    {
        [XmlElement("envelope_to", Form = XmlSchemaForm.Unqualified)]
        public string EnvelopeTo { get; set; }

        [XmlElement("header_from", Form = XmlSchemaForm.Unqualified)]
        public string HeaderFrom { get; set; }

        [XmlElement("envelope_from", Form = XmlSchemaForm.Unqualified)]
        public string EnvelopeFrom { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class PolicyOverrideReason
    {
        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        public PolicyOverrideType Type { get; set; }

        [XmlElement("comment", Form = XmlSchemaForm.Unqualified)]
        public string Comment { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
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
        public DispositionType Disposition { get; set; }

        [XmlElement("dkim", Form = XmlSchemaForm.Unqualified)]
        public DMARCResultType Dkim { get; set; }

        [XmlElement("spf", Form = XmlSchemaForm.Unqualified)]
        public DMARCResultType Spf { get; set; }

        [XmlElement("reason", Form = XmlSchemaForm.Unqualified)]
        public PolicyOverrideReason[] Reason { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
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
        public string SourceIp { get; set; }

        [XmlElement("count", Form = XmlSchemaForm.Unqualified)]
        public int Count { get; set; }

        [XmlElement("policy_evaluated", Form = XmlSchemaForm.Unqualified)]
        public PolicyEvaluatedType PolicyEvaluated { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class RecordType
    {
        [XmlElement("row", Form = XmlSchemaForm.Unqualified)]
        public RowType Row { get; set; }

        [XmlElement("identifiers", Form = XmlSchemaForm.Unqualified)]
        public IdentifierType Identifiers { get; set; }

        [XmlElement("auth_results", Form = XmlSchemaForm.Unqualified)]
        public AuthResultType AuthResults { get; set; }

        [XmlAnyElement] public XmlElement[] Extensions { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public class PolicyPublishedType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        public string Domain { get; set; }

        [XmlElement("adkim", Form = XmlSchemaForm.Unqualified)]
        public AlignmentType? Adkim { get; set; }

        [XmlElement("aspf", Form = XmlSchemaForm.Unqualified)]
        public AlignmentType? Aspf { get; set; }

        [XmlElement("p", Form = XmlSchemaForm.Unqualified)]
        public DispositionType P { get; set; }

        [XmlElement("np", Form = XmlSchemaForm.Unqualified)]
        public DispositionType? Np { get; set; }

        [XmlElement("sp", Form = XmlSchemaForm.Unqualified)]
        public DispositionType Sp { get; set; }

        [XmlElement("pct", Form = XmlSchemaForm.Unqualified)]
        public string Percent { get; set; }

        [XmlElement("fo", Form = XmlSchemaForm.Unqualified)]
        public string Fo { get; set; }

        [XmlElement("ri", Form = XmlSchemaForm.Unqualified)]
        public int Ri { get; set; }

        [XmlElement("rua", Form = XmlSchemaForm.Unqualified)]
        public string Rua { get; set; }

        [XmlElement("ruf", Form = XmlSchemaForm.Unqualified)]
        public string Ruf { get; set; }

        [XmlElement("rf", Form = XmlSchemaForm.Unqualified)]
        public string Rf { get; set; }

        [XmlElement("v", Form = XmlSchemaForm.Unqualified)]
        public string V { get; set; }

        [XmlElement("testing", Form = XmlSchemaForm.Unqualified)]
        public TestingType? Testing { get; set; }

        [XmlElement("discovery_method", Form = XmlSchemaForm.Unqualified)]
        public DiscoveryType? DiscoveryMethod { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://dmarc.org/dmarc-xml/0.1")]
    public enum AlignmentType
    {
        [XmlEnum("r")] Relaxed,
        [XmlEnum("s")] Strict
    }

    [Serializable]
    public class ExtensionType
    {
        [XmlAnyElement] public XmlElement[] Any { get; set; }
    }
}
