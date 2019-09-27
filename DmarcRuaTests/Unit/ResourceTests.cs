using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DmarcRua.Tests.Unit
{
    [TestFixture, Ignore("")]
    public class ResourceTests
    {
        private class Error
        {
            public string FileName { get; set; }
            public List<string> ErrorList { get; set; }
        }

        [Test]
        public void should_verify_wll_known_resources()
        {
            var validationErrors = new List<Error>();
            var xmlPath = Path.Combine("../", "../", "../", "../", "xml/");
            var files = Directory.GetFiles(xmlPath);

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
                            ErrorList = new List<string>{ex.Message}
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