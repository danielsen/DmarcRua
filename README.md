# DmarcRua

`DmarcRua` is a simple .NET serializer for DMARC aggregate reports. Given 
aggregate report XML, `DmarcRua` serializes the report into an object and
provides some convenience functions for identifying and exploring DMARC
failures. `DmarcRua` supports both v1 and v2 aggregate reports.

### Features

- Serialize DMARC aggregate report XML into .NET types and objects.
- Discover reported DMARC failures.
- Summarize or itemize DMARC failures by IP address.
- Summarize or itemize DMARC failures by From header.

### Packages

Current Version: `1.1.5`

Target Framework: `netstandard2.0`

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
from https://tools.ietf.org/html/rfc7489#appendix-C the specifications
for version 2 come from https://datatracker.ietf.org/doc/draft-ietf-dmarc-aggregate-reporting/.

### Reporting Issues

Issues can be reported on the [Issues page of the GitHub repo](https://github.com/danielsen/DmarcRua/issues).
If reporting an issue related to a report that won't parse, please attach an
anonymized copy of the report.

### Anonymizing Reports

Remove any references to your domain(s). These will appear in `<domain></domain>`
and `<header_from></header_from>` elements throughout the report. They can be replaced with
any dummy domain, e.g. phony-domain.com.

Remove any references to your IP(s). These will appear in the `<source_ip></source_ip>` elements
of the report. They should be replaced with a parseable IPv4 or IPv6 address, e.g. 127.0.0.1. Do not
use `localhost`.

### Contributing

Contributions are always welcome.

1. Begin by forking and cloning the repository.
    - Go to the [DmarcRua](https://github.com/danielsen/DmarcRua/) repository and click the "Fork" button.
    - Clone the repository `git clone git@github.com:USERNAME/DmarcRua.git DmarcRua`
2. Checkout the develop branch 
    ```
    git checkout develop
    ```
3. Update the develop branch with the latest upstream changes.
    ```
    git pull upstream develop
    ```
4. Create a new branch
    ```
    git checkout -b fix-parse-for-xyz
    ```
5. Make your changes
    - If your changes involve parsing of a report, put an anonymized sample report in the 
   /dev/testing/resources/dmarc/aggregate/ directory and make sure to run the ResourceTests in the DmarcRua.Tests
   project. Refer to the Anonymizing Reports section above for anonymization details.
6. Add and commit your changes
    ```
    git add .
    git commit -m "Fixed enum value observed in report from xyz.com"
    ```
7. Push your local changes to the remote branch.
    ```
    git push origin fix-parse-for-xyz
    ```
8. Go to the [repo](https://github.com/danielsen/DmarcRua/) and open a pull request. Please make sure to write a 
detailed explanation of your changes.

### Running Tests

Project tests are in `DmarcRua.Tests`. When running the `ResourceTests` make sure to set the environment variable
`AGGREGATE_REPORTS` to the path of the sample reports.
    ```
    $ export AGGREGATE_REPORTS=/home/alice/projects/DmarcRua/dev/testing/resources/dmarc/aggregate/
    $ dotnet test src/DmarcRua.Tests/DmarcRua.Tests.csproj
    ```
