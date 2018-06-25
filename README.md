# DmarcRua

`DmarcRua` is a simple .NET serializer for DMARC aggregate reports. Given 
aggregate report XML, `DmarcRua` serializes the report into an object and
provides some convenience functions for identifying and exploring DMARC
failures.

### Features

- Serialize DMARC aggregate report XML into .NET types and objects.
- Discover reported DMARC failures.
- Summarize or itemize DMARC failures by IP address.
- Summarize or itemize DMARC failures by From header.

### Packages

Current Version: `1.0.0`

Target Framework: `.NET 4.5`, `.NET 4.6` and up, `.NET Core 2`

### Dependencies

- None.

### Development Dependencies

- [Microsoft.NET.Test.SDK](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/)
- [NUnit](https://www.nuget.org/packages/NUnit/)
- [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/)

### Usage

`AggregateReport` objects can be constructed with a stream of the report XML
or the report can be serialized later.

For example:

        using (var stream = File.OpenRead("\path\to\report.xml"))
        {
            var aggregateReport = new AggregateReport(stream);
        }

Or:

        var aggregateReport = new AggregateReport();
         
        using (var stream = File.OpenRead("\path\to\report.xml"))
        {
            aggregateReport.ReadAggregateReport(stream);
        }

### Features / Methods in Brief

- `AggregateReport.GetFailureRecords()`
    - An enumeration of all report records that failed DMARC.
- `AggregateReport.GetFailureCount()`
    - Count of all report records that failed DMARC
- `AggregateReport.SummarizeFailuresByIpAddress()`
    - All report records that failed DMARC summarized by IP address.
- `AggregateReport.SummarizeFailuresByHeaderFrom()`
    - All report records that failed DMARC summarized by From header.
- `AggregateReport.GetFailedRecordsByIpAddress(IPAddress ipAddress)`
    - Itemized DMARC fail report records for a single IP address.
- `AggregateReport.GetFailedRecordsByFromHeader(string fromHeader)`
    - Itemized DMARC fail report records for a single From header.

### References

Specifications on the DMARC aggregate report format were taken
from https://tools.ietf.org/html/rfc7489#appendix-C
