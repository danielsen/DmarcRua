using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DmarcRuaV2.Helpers
{
    public static class DmarcV2Converter
    {
        public static DmarcRua.Feedback ConvertV2ToV1(this DmarcRuaV2.Feedback v2Feedback)
        {
            return new DmarcRua.Feedback
            {
                Version = v2Feedback.Version?.ToString() ?? "1.0",
                ReportMetadata = ConvertReportMetadata(v2Feedback.ReportMetadata),
                PolicyPublished = ConvertPolicyPublished(v2Feedback.PolicyPublished),
                Record = v2Feedback.Records?.Select(ConvertRecord).ToArray()
            };
        }

        private static DmarcRua.ReportMetadataType ConvertReportMetadata(DmarcRuaV2.ReportMetadataType v2Metadata)
        {
            return new DmarcRua.ReportMetadataType
            {
                OrgName = v2Metadata.OrgName,
                Email = v2Metadata.Email,
                ExtraContactInfo = v2Metadata.ExtraContactInfo,
                ReportId = v2Metadata.ReportId,
                DateRange = new DmarcRua.DateRangeType
                {
                    Begin = v2Metadata.DateRange.Begin,
                    End = v2Metadata.DateRange.End
                },
                Error = v2Metadata.Error != null ? new[] { v2Metadata.Error } : null
            };
        }

        private static DmarcRua.PolicyPublishedType ConvertPolicyPublished(DmarcRuaV2.PolicyPublishedType v2Policy)
        {
            return new DmarcRua.PolicyPublishedType
            {
                Domain = v2Policy.Domain,
                Adkim = (DmarcRua.AlignmentType)v2Policy.Adkim,
                Aspf = (DmarcRua.AlignmentType)v2Policy.Aspf,
                P = (DmarcRua.DispositionType)v2Policy.P,
                Sp = (DmarcRua.DispositionType)v2Policy.Sp,
                Fo = v2Policy.Fo,
                DiscoveryMethod = (DmarcRua.DiscoveryType)v2Policy.DiscoveryMethod,
                Testing = (DmarcRua.TestingType)v2Policy.Testing
            };
        }

        private static DmarcRua.RecordType ConvertRecord(DmarcRuaV2.RecordType v2Record)
        {
            return new DmarcRua.RecordType
            {
                Row = ConvertRow(v2Record.Row),
                Identifiers = ConvertIdentifiers(v2Record.Identifiers),
                AuthResults = ConvertAuthResults(v2Record.AuthResults)
            };
        }

        private static DmarcRua.RowType ConvertRow(DmarcRuaV2.RowType v2Row)
        {
            return new DmarcRua.RowType
            {
                SourceIp = v2Row.SourceIp,
                Count = v2Row.Count,
                PolicyEvaluated = ConvertPolicyEvaluated(v2Row.PolicyEvaluated)
            };
        }

        private static DmarcRua.PolicyEvaluatedType ConvertPolicyEvaluated(DmarcRuaV2.PolicyEvaluatedType v2PolicyEvaluated)
        {
            return new DmarcRua.PolicyEvaluatedType
            {
                Disposition = (DmarcRua.DispositionType)v2PolicyEvaluated.Disposition,
                Dkim = (DmarcRua.DMARCResultType)v2PolicyEvaluated.Dkim,
                Spf = (DmarcRua.DMARCResultType)v2PolicyEvaluated.Spf,
                Reason = v2PolicyEvaluated.Reasons?.Select(ConvertPolicyOverrideReason).ToArray()
            };
        }

        private static DmarcRua.PolicyOverrideReason ConvertPolicyOverrideReason(DmarcRuaV2.PolicyOverrideReason v2Reason)
        {
            return new DmarcRua.PolicyOverrideReason
            {
                Type = (DmarcRua.PolicyOverrideType)v2Reason.Type,
                Comment = v2Reason.Comment
            };
        }

        private static DmarcRua.IdentifierType ConvertIdentifiers(DmarcRuaV2.IdentifierType v2Identifiers)
        {
            return new DmarcRua.IdentifierType
            {
                EnvelopeTo = v2Identifiers.EnvelopeTo,
                HeaderFrom = v2Identifiers.HeaderFrom,
                EnvelopeFrom = v2Identifiers.EnvelopeFrom
            };
        }

        private static DmarcRua.AuthResultType ConvertAuthResults(DmarcRuaV2.AuthResultType v2AuthResults)
        {
            return new DmarcRua.AuthResultType
            {
                Dkim = v2AuthResults.Dkim?.Select(ConvertDkimAuthResult).ToArray(),
                Spf = v2AuthResults.Spf != null ? new[] { ConvertSpfAuthResult(v2AuthResults.Spf) } : null
            };
        }

        private static DmarcRua.DKIMAuthResultType ConvertDkimAuthResult(DmarcRuaV2.DKIMAuthResultType v2DkimResult)
        {
            return new DmarcRua.DKIMAuthResultType
            {
                Domain = v2DkimResult.Domain,
                Result = (DmarcRua.DKIMResultType)v2DkimResult.Result,
                HumanResult = v2DkimResult.HumanResult,
                Selector = v2DkimResult.Selector
            };
        }

        private static DmarcRua.SpfAuthResultType ConvertSpfAuthResult(DmarcRuaV2.SPFAuthResultType v2SpfResult)
        {
            return new DmarcRua.SpfAuthResultType
            {
                Domain = v2SpfResult.Domain,
                Scope = v2SpfResult.Scope?.ToString().ToLower(),
                Result = (DmarcRua.SpfResultType)v2SpfResult.Result
            };
        }
    }
}
