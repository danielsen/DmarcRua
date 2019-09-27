//
// AggregateReportExtensions.cs
// 
// Author: Dan Nielsen (dnielsen@fastmail.fm)
// Copyright (c) 2018 Dan Nielsen
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

using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DmarcRua
{
    /// <summary>
    /// Abstract class suitable for summarizing DMARC failures by IP address.
    /// </summary>
    public class SourceIpSummary
    {
        public IPAddress IpAddress { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Abstract class suitable for summarizing DMARC failures by From header.
    /// </summary>
    public class FromHeaderSummary
    {
        public string HeaderFrom { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Collection of convenience functions for aggregate reports.
    /// </summary>
    public static class AggregateReportExtensions
    {
        /// <summary>
        /// Gets the aggregate report records which did not pass DMARC.
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public static IEnumerable<RecordType> GetFailureRecords(this AggregateReport aggregateReport)
        {
            return aggregateReport.Feedback.Record.Where(x =>
                x.Row.PolicyEvaluated.Dkim == DMARCResultType.fail &&
                x.Row.PolicyEvaluated.Spf == DMARCResultType.fail);
        }

        /// <summary>
        /// Gets the total number of failures in this report.
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <returns>Count of failures.</returns>
        public static int GetFailureCount(this AggregateReport aggregateReport)
        {
            return aggregateReport.GetFailureRecords().Sum(r => r.Row.Count);
        }

        /// <summary>
        /// Gets DMARC failures summarized by IpAddress
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <returns>IEnumerable of <see cref="SourceIpSummary"/></returns>
        public static IEnumerable<SourceIpSummary> SummarizeFailuresByIpAddress(this AggregateReport aggregateReport)
        {
            var failedRecords = aggregateReport.GetFailureRecords();

            return failedRecords.GroupBy(r => r.Row.SourceIp, r => r.Row.Count,
                (s, c) => new SourceIpSummary {IpAddress = IPAddress.Parse(s), Count = c.Sum()});
        }

        /// <summary>
        /// Gets DMARC failures summarized by message From header.
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <returns>IEnumerable of <see cref="FromHeaderSummary"/></returns>
        public static IEnumerable<FromHeaderSummary> SummarizeFailuresByHeaderFrom(this AggregateReport aggregateReport)
        {
            var failedRecords = aggregateReport.GetFailureRecords();

            return failedRecords.GroupBy(r => r.Identifiers.HeaderFrom, r => r.Row.Count,
                (h, c) => new FromHeaderSummary {HeaderFrom = h, Count = c.Sum()});
        }

        /// <summary>
        /// Gets DMARC failures by the source IP address
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <param name="address">The IP address to target</param>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public static IEnumerable<RecordType> GetFailedRecordsByIpAddress(this AggregateReport aggregateReport, IPAddress address)
        {
            return aggregateReport.Feedback.Record.Where(x => x.Row.SourceIp == address.ToString());
        }

        /// <summary>
        /// Gets DMARC failures by the From header domain.
        /// </summary>
        /// <param name="aggregateReport"></param>
        /// <param name="fromHeader">The From header domain to target.</param>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public static IEnumerable<RecordType> GetFailedRecordsByFromHeader(this AggregateReport aggregateReport,
            string fromHeader)
        {
            return aggregateReport.Feedback.Record.Where(x => x.Identifiers.HeaderFrom == fromHeader);
        }

        public static RequestedReportingPolicy GetRequestedReportingPolicy(this AggregateReport aggregateReport)
        {
            var policy = RequestedReportingPolicy.All;

            if (aggregateReport.Feedback.PolicyPublished.Fo.Contains("1"))
                policy |= RequestedReportingPolicy.Any;

            if (aggregateReport.Feedback.PolicyPublished.Fo.Contains("d"))
                policy |= RequestedReportingPolicy.Dkim;

            if (aggregateReport.Feedback.PolicyPublished.Fo.Contains("s"))
                policy |= RequestedReportingPolicy.Spf;

            return policy;
        }
    }
}
