//
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DmarcRua.Tests.Unit
{
    [TestFixture]
    public class ResourceTests
    {
        private class Error
        {
            public string FileName { get; set; }
            public List<string> ErrorList { get; set; }
        }

        [Test]
        public void should_verify_well_known_resources()
        {
            var validationErrors = new List<Error>();
            var xmlPath = Environment.GetEnvironmentVariable("AGGREGATE_REPORTS");
            var files = Directory.GetFiles(xmlPath!);

            foreach (var file in files)
            {
                using (var fileStream = File.OpenRead(file))
                {
                    var aggregateReport = new AggregateReport();
                    try
                    {
                        aggregateReport.ReadAggregateReport(fileStream);
                    }
                    catch (Exception ex)
                    {
                        var err = new Error
                        {
                            FileName = file,
                            ErrorList = new List<string> { ex.Message }
                        };
                        validationErrors.Add(err);
                    }

                    if (!aggregateReport.HasErrors) continue;
                    var error = new Error
                    {
                        FileName = file,
                        ErrorList = aggregateReport.Errors
                            .Select(e => e.Exception.Message).ToList()
                    };
                    validationErrors.Add(error);
                }
            }

            if (!validationErrors.Any()) return;
            foreach (var error in validationErrors)
            {
                Console.WriteLine($"Error in {error.FileName}");
                foreach (var errorMessage in error.ErrorList)
                {
                    Console.WriteLine($"\t{errorMessage}");
                }
            }

            Assert.AreEqual(0, validationErrors.Count);
        }
    }
}