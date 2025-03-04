﻿//
// AggregateReportTests.cs
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

using System.Reflection;
using NUnit.Framework;

namespace DmarcRua.Tests.Unit;

[TestFixture]
public class AggregateReportTests
{
    [Test]
    public void should_validate_rua_report()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var reportStream = assembly.GetManifestResourceStream("DmarcRua.Tests.Unit.SampleReport.xml");

        var aggregate = new AggregateReport();
        aggregate.ReadAggregateReport(reportStream);

        Assert.AreEqual(true, aggregate.ValidReport);
        Assert.AreEqual(aggregate.Feedback.PolicyPublished.Domain, "acme-company.net");
        Assert.AreEqual(aggregate.Feedback.PolicyPublished.Adkim, AlignmentType.Relaxed);
    }

    [Test]
    public void should_catch_invalid_report()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var reportStream = assembly.GetManifestResourceStream("DmarcRua.Tests.Unit.InvalidReport.xml");

        var aggregate = new AggregateReport();
        aggregate.ReadAggregateReport(reportStream);

        Assert.AreEqual(false, aggregate.ValidReport);
    }

    [Test]
    public void should_handle_ipv6_addresses_correctly()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var reportStream = assembly.GetManifestResourceStream("DmarcRua.Tests.Unit.GoogleGenerated.xml");

        var aggregate = new AggregateReport();
        aggregate.ReadAggregateReport(reportStream);

        Assert.AreEqual(true, aggregate.ValidReport);
    }
}