using System.Xml.Serialization;

namespace DmarcRuaV2
{
    [XmlRoot("feedback", Namespace = "urn:ietf:params:xml:ns:dmarc-2.0")]
    public class Feedback
    {
        [XmlElement("version")]
        public decimal? Version { get; set; }

        [XmlElement("report_metadata")]
        public ReportMetadataType ReportMetadata { get; set; }

        [XmlElement("policy_published")]
        public PolicyPublishedType PolicyPublished { get; set; }

        [XmlElement("extension")]
        public ExtensionType Extension { get; set; }

        [XmlElement("record")]
        public RecordType[] Records { get; set; }
    }

    public class DateRangeType
    {
        [XmlElement("begin")]
        public long Begin { get; set; }

        [XmlElement("end")]
        public long End { get; set; }
    }

    public class ReportMetadataType
    {
        [XmlElement("org_name")]
        public string OrgName { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("extra_contact_info")]
        public string ExtraContactInfo { get; set; }

        [XmlElement("report_id")]
        public string ReportId { get; set; }

        [XmlElement("date_range")]
        public DateRangeType DateRange { get; set; }

        [XmlElement("error")]
        public string Error { get; set; }
    }

    public enum AlignmentType
    {
        [XmlEnum("r")]
        Relaxed,
        [XmlEnum("s")]
        Strict
    }

    public enum DispositionType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("quarantine")]
        Quarantine,
        [XmlEnum("reject")]
        Reject
    }

    public enum ActionDispositionType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("quarantine")]
        Quarantine,
        [XmlEnum("reject")]
        Reject
    }

    public enum DiscoveryType
    {
        [XmlEnum("psl")]
        Psl,
        [XmlEnum("treewalk")]
        Treewalk
    }

    public class PolicyPublishedType
    {
        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("discovery_method")]
        public DiscoveryType? DiscoveryMethod { get; set; }

        [XmlElement("adkim")]
        public AlignmentType? Adkim { get; set; }

        [XmlElement("aspf")]
        public AlignmentType? Aspf { get; set; }

        [XmlElement("p")]
        public DispositionType P { get; set; }

        [XmlElement("sp")]
        public DispositionType Sp { get; set; }

        [XmlElement("testing")]
        public TestingType? Testing { get; set; }

        [XmlElement("fo")]
        public string Fo { get; set; }
    }

    public enum TestingType
    {
        [XmlEnum("n")]
        No,
        [XmlEnum("y")]
        Yes
    }

    public enum DMARCResultType
    {
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail
    }

    public enum PolicyOverrideType
    {
        [XmlEnum("forwarded")]
        Forwarded,
        [XmlEnum("sampled_out")]
        SampledOut,
        [XmlEnum("trusted_forwarder")]
        TrustedForwarder,
        [XmlEnum("mailing_list")]
        MailingList,
        [XmlEnum("local_policy")]
        LocalPolicy,
        [XmlEnum("other")]
        Other
    }

    public class PolicyOverrideReason
    {
        [XmlElement("type")]
        public PolicyOverrideType Type { get; set; }

        [XmlElement("comment")]
        public string Comment { get; set; }
    }

    public class PolicyEvaluatedType
    {
        [XmlElement("disposition")]
        public ActionDispositionType Disposition { get; set; }

        [XmlElement("dkim")]
        public DMARCResultType Dkim { get; set; }

        [XmlElement("spf")]
        public DMARCResultType Spf { get; set; }

        [XmlElement("reason")]
        public PolicyOverrideReason[] Reasons { get; set; }
    }

    public class RowType
    {
        [XmlElement("source_ip")]
        public string SourceIp { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("policy_evaluated")]
        public PolicyEvaluatedType PolicyEvaluated { get; set; }
    }

    public class IdentifierType
    {
        [XmlElement("envelope_to")]
        public string EnvelopeTo { get; set; }

        [XmlElement("envelope_from")]
        public string EnvelopeFrom { get; set; }

        [XmlElement("header_from")]
        public string HeaderFrom { get; set; }
    }

    public enum DKIMResultType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail,
        [XmlEnum("policy")]
        Policy,
        [XmlEnum("neutral")]
        Neutral,
        [XmlEnum("temperror")]
        TempError,
        [XmlEnum("permerror")]
        PermError
    }

    public class DKIMAuthResultType
    {
        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("selector")]
        public string Selector { get; set; }

        [XmlElement("result")]
        public DKIMResultType Result { get; set; }

        [XmlElement("human_result")]
        public string HumanResult { get; set; }
    }

    public enum SPFDomainScope
    {
        [XmlEnum("mfrom")]
        Mfrom
    }

    public enum SPFResultType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("neutral")]
        Neutral,
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail,
        [XmlEnum("softfail")]
        Softfail,
        [XmlEnum("temperror")]
        TempError,
        [XmlEnum("permerror")]
        PermError
    }

    public class SPFAuthResultType
    {
        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("scope")]
        public SPFDomainScope? Scope { get; set; }

        [XmlElement("result")]
        public SPFResultType Result { get; set; }

        [XmlElement("human_result")]
        public string HumanResult { get; set; }
    }

    public class AuthResultType
    {
        [XmlElement("dkim")]
        public DKIMAuthResultType[] Dkim { get; set; }

        [XmlElement("spf")]
        public SPFAuthResultType Spf { get; set; }
    }

    public class RecordType
    {
        [XmlElement("row")]
        public RowType Row { get; set; }

        [XmlElement("identifiers")]
        public IdentifierType Identifiers { get; set; }

        [XmlElement("auth_results")]
        public AuthResultType AuthResults { get; set; }

        [XmlAnyElement]
        public System.Xml.XmlElement[] Extensions { get; set; }
    }

    public class ExtensionType
    {
        [XmlAnyElement]
        public System.Xml.XmlElement[] Any { get; set; }
    }
}