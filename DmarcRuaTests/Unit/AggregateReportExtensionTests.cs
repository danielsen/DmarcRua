//
// AggregateReportExtensionTests.cs
// 
// Author: Dan Nielsen (dnielsen@fastmail.fm)
// Copyright (c) 2023 Dan Nielsen
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

using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Reflection;

namespace DmarcRua.Tests.Unit
{
    [TestFixture]
    public class AggregateReportExtensionTests
    {
        private AggregateReport _ruaViaSampleReportXml;

        [SetUp]
        public void Setup()
        {
            var assmebly = Assembly.GetExecutingAssembly();
            var reportStream = assmebly.GetManifestResourceStream("DmarcRua.Tests.Unit.SampleReport.xml");

            _ruaViaSampleReportXml = new AggregateReport(reportStream);
        }

        [Test]
        public void should_get_enumeration_of_failures()
        {
            Assert.AreEqual(1, _ruaViaSampleReportXml.GetFailureRecords().Count());
        }

        [Test]
        public void should_get_sum_of_all_failures()
        {
            Assert.AreEqual(1, _ruaViaSampleReportXml.GetFailureCount());
        }

        [Test]
        public void should_summarize_failures_by_ip_address()
        {
            var failureSummary = _ruaViaSampleReportXml.SummarizeFailuresByIpAddress();

            Assert.AreEqual(1, failureSummary.Count());
            Assert.AreEqual("62.149.157.24", failureSummary.First().IpAddress.ToString());
            Assert.AreEqual(1, failureSummary.First().Count);
        }

        [Test]
        public void should_summarize_failures_by_header_from()
        {
            var failureSummary = _ruaViaSampleReportXml.SummarizeFailuresByHeaderFrom();

            Assert.AreEqual(1, failureSummary.Count());
            Assert.AreEqual("mail6.acme-company.net", failureSummary.First().HeaderFrom);
            Assert.AreEqual(1, failureSummary.First().Count);
        }

        [Test]
        public void should_target_failures_by_ip_address()
        {
            var ip = IPAddress.Parse("62.149.157.24");

            var failureSummary = _ruaViaSampleReportXml.GetFailedRecordsByIpAddress(ip);

            Assert.AreEqual(1, failureSummary.Count());
        }

        [Test]
        public void should_target_failures_by_from_header()
        {
            var failureSummary = _ruaViaSampleReportXml.GetFailedRecordsByFromHeader("mail6.acme-company.net");

            Assert.AreEqual(1, failureSummary.Count());
        }
    }
}
