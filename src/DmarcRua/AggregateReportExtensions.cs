//
// AggregateReportExtensions.cs
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

using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DmarcRua;

/// <summary>
/// Summarizes DMARC failures by IP address.
/// </summary>
public class SourceIpSummary(IPAddress ipAddress, int count)
{
    public IPAddress IpAddress { get; } = ipAddress;
    public int Count { get; } = count;
}

/// <summary>
/// Summarizes DMARC failures by From header.
/// </summary>
public class FromHeaderSummary(string headerFrom, int count)
{
    public string HeaderFrom { get; } = headerFrom;
    public int Count { get; } = count;
}

/// <summary>
/// Collection of convenience functions for aggregate reports.
/// </summary>
public static class AggregateReportExtensions
{
    /// <param name="aggregateReport"></param>
    extension(AggregateReport aggregateReport)
    {
        /// <summary>
        /// Gets the aggregate report records which did not pass DMARC.
        /// </summary>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public IEnumerable<RecordType> GetFailureRecords()
        {
            return (aggregateReport.Feedback?.Record
                    ?? Enumerable.Empty<RecordType>())
                .Where(x =>
                    x.Row?.PolicyEvaluated is { } policyEvaluated &&
                    policyEvaluated.Dkim == DMARCResultType.Fail &&
                    policyEvaluated.Spf == DMARCResultType.Fail);
        }

        /// <summary>
        /// Gets the total number of failures in this report.
        /// </summary>
        /// <returns>Count of failures.</returns>
        public int GetFailureCount()
        {
            return aggregateReport
                .GetFailureRecords()
                .Sum(r => r.Row.Count);
        }

        /// <summary>
        /// Gets DMARC failures summarized by IpAddress
        /// </summary>
        /// <returns>IEnumerable of <see cref="SourceIpSummary"/></returns>
        public IEnumerable<SourceIpSummary> SummarizeFailuresByIpAddress()
        {
            var failedRecords = aggregateReport.GetFailureRecords();

            return failedRecords.GroupBy(
                r => r.Row.SourceIp,
                r => r.Row.Count,
                (s, c) => new SourceIpSummary(IPAddress.Parse(s), c.Sum()));
        }

        /// <summary>
        /// Gets DMARC failures summarized by message From header.
        /// </summary>
        /// <returns>IEnumerable of <see cref="FromHeaderSummary"/></returns>
        public IEnumerable<FromHeaderSummary>
            SummarizeFailuresByHeaderFrom()
        {
            var failedRecords = aggregateReport.GetFailureRecords();

            return failedRecords.GroupBy(
                r => r.Identifiers?.HeaderFrom,
                r => r.Row.Count,
                (h, c) => new FromHeaderSummary(h, c.Sum()));
        }

        /// <summary>
        /// Gets DMARC failures by the source IP address
        /// </summary>
        /// <param name="address">The IP address to target</param>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public IEnumerable<RecordType> GetFailedRecordsByIpAddress(
            IPAddress address)
        {
            return (aggregateReport.Feedback?.Record
                    ?? Enumerable.Empty<RecordType>())
                .Where(x => x.Row?.SourceIp == address.ToString());
        }

        /// <summary>
        /// Gets DMARC failures by the FROM header domain.
        /// </summary>
        /// <param name="fromHeader">The FROM header domain to target.</param>
        /// <returns>IEnumerable of <see cref="RecordType"/></returns>
        public IEnumerable<RecordType> GetFailedRecordsByFromHeader(
            string fromHeader)
        {
            return (aggregateReport.Feedback?.Record
                    ?? Enumerable.Empty<RecordType>())
                .Where(x => x.Identifiers?.HeaderFrom == fromHeader);
        }

        public RequestedReportingPolicy GetRequestedReportingPolicy()
        {
            var policy = RequestedReportingPolicy.All;

            var fo = aggregateReport.Feedback?.PolicyPublished?.Fo;
            if (fo == null)
                return policy;

            if (fo.Contains("1"))
                policy |= RequestedReportingPolicy.Any;

            if (fo.Contains("d"))
                policy |= RequestedReportingPolicy.Dkim;

            if (fo.Contains("s"))
                policy |= RequestedReportingPolicy.Spf;

            return policy;
        }
    }
}