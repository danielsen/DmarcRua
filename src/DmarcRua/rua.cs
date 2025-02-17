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
    [XmlType(AnonymousType = true)]
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
    [XmlType]
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
    [XmlType]
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
    [XmlType]
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
    [XmlType]
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
        [XmlEnum("")] Default = None,
        [XmlEnum("unknown")] Unknown = TempError
    }

    [Serializable]
    [XmlType]
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
    [XmlType]
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
        [XmlEnum("invalid")] Invalid = TempError,
        [XmlEnum("unknown")] Unknown = TempError,
        [XmlEnum("")] Default = None
    }

    [Serializable]
    [XmlType]
    public class AuthResultType
    {
        [XmlElement("dkim", Form = XmlSchemaForm.Unqualified)]
        public DKIMAuthResultType[] Dkim { get; set; }

        [XmlElement("spf", Form = XmlSchemaForm.Unqualified)]
        public SpfAuthResultType[] Spf { get; set; }
    }

    [Serializable]
    [XmlType]
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
    [XmlType]
    public class PolicyOverrideReason
    {
        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        public PolicyOverrideType Type { get; set; }

        [XmlElement("comment", Form = XmlSchemaForm.Unqualified)]
        public string Comment { get; set; }
    }

    [Serializable]
    [XmlType]
    public enum PolicyOverrideType
    {
        [XmlEnum("forwarded")] Forwarded,
        [XmlEnum("ampled_out")] SampledOut,
        [XmlEnum("trusted_forwarder")] TrustedForwarder,
        [XmlEnum("mailing_list")] MailingList,
        [XmlEnum("local_policy")] LocalPolicy,
        [XmlEnum("other")] Other,
        [XmlEnum("")] None
    }

    [Serializable]
    [XmlType]
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
    [XmlType]
    public enum DispositionType
    {
        [XmlEnum("none")] None,
        [XmlEnum("quarantine")] Quarantine,
        [XmlEnum("reject")] Reject,
        [XmlEnum("nil")] Nil = None,
        [XmlEnum("")] Default = None
    }

    [Serializable]
    [XmlType]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum DMARCResultType
    {
        [XmlEnum("pass")] Pass,
        [XmlEnum("fail")] Fail,
        [XmlEnum("softfail")] Softfail = Fail,
        [XmlEnum("none")] None = Pass
    }

    [Serializable]
    [XmlType]
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
    [XmlType]
    public class RecordType
    {
        [XmlElement("row", Form = XmlSchemaForm.Unqualified)]
        public RowType Row { get; set; }

        // Added to accomodate otherwise valid reports that use
        // "identities" instead of "identifiers".
        [XmlChoiceIdentifier("IdentifierChoiceType")]
        [XmlElement("identifiers", Form = XmlSchemaForm.Unqualified)]
        [XmlElement("identities", Form = XmlSchemaForm.Unqualified)]
        public IdentifierType Identifiers { get; set; }

        [XmlElement("auth_results", Form = XmlSchemaForm.Unqualified)]
        public AuthResultType AuthResults { get; set; }

        [XmlAnyElement] public XmlElement[] Extensions { get; set; }
        
        [XmlIgnore]
        public IdentifierChoiceType IdentifierChoiceType { get; set; }
    }

    public enum IdentifierChoiceType
    {
        [XmlEnum("identifiers")] Identifiers,
        [XmlEnum("identities")] Identities
    }

    [Serializable]
    [XmlType]
    public class PolicyPublishedType
    {
        [XmlElement("domain", Form = XmlSchemaForm.Unqualified)]
        public string Domain { get; set; }

        [XmlElement("adkim", Form = XmlSchemaForm.Unqualified)]
        public string AdkimRaw { get; set; }

        public AlignmentType? Adkim => AdkimRaw.AlignmentTypeFromString();

        [XmlElement("aspf", Form = XmlSchemaForm.Unqualified)]
        public string AspfRaw { get; set; }

        public AlignmentType? Aspf => AspfRaw.AlignmentTypeFromString();


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
    [XmlType]
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


    internal static class ValidationFunctions
    {
        internal static AlignmentType? AlignmentTypeFromString(this string alignmentType)
        {
            switch (alignmentType.CleanOutStringSpecials())
            {
                case "r":
                    return AlignmentType.Relaxed;
                case "s":
                    return AlignmentType.Strict;
                default:
                    return null;
            }
        }

        internal static string CleanOutStringSpecials(this string input)
        {
            input = input?.Trim().ToLower();
            // Clean out any special characters from the string. 
            return System.Text.RegularExpressions.Regex.Replace(input, "[^a-z0-9]", "");

        }
    }
}
