//
// AggregateReport.cs
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DmarcRua
{
    /// <summary>
    /// A DMARC aggregate report 
    /// </summary>
    public class AggregateReport
    {
        /// <summary>
        /// Embedded schema use for validation of supplied reports.
        /// </summary>
        private const string SchemaName = "DmarcRua.rua.xsd";

        /// <summary>
        /// Indicates if the serialized report has an XML validation warnings or errors.
        /// </summary>
        public bool HasWarningsOrErrors;

        /// <summary>
        /// Indicates if the serialized report is valid against <see cref="rua.xsd"/>
        /// </summary>
        public bool ValidReport = true;

        /// <summary>
        /// Gets the serialized <see cref="Feedback"/> report.
        /// </summary>
        public Feedback Feedback { get; internal set; }

        /// <summary>
        /// XmlReader settings.
        /// </summary>
        private XmlReaderSettings _xmlReaderSettings;

        /// <summary>
        /// List of validation events raised during serialization.
        /// </summary>
        public IList<ValidationEventArgs> ValidationEvents { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AggregateReport()
        {
            ConfigureValidation();
            ValidationEvents = new List<ValidationEventArgs>();
        }

        /// <summary>
        /// Constructor with report stream.
        /// </summary>
        /// <param name="ruaStream">A stream containing the RUA report to serialize.</param>
        public AggregateReport(Stream ruaStream)
        {
            ConfigureValidation();
            ValidationEvents = new List<ValidationEventArgs>();
            ReadAggregateReport(ruaStream);
        }

        /// <summary>
        /// Configures report XML schema validation.
        /// </summary>
        private void ConfigureValidation()
        {
            // Settings per https://msdn.microsoft.com/en-us/magazine/ee335713.aspx
            _xmlReaderSettings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings,
                XmlResolver = null,
                DtdProcessing = DtdProcessing.Prohibit
            };

            using (var schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SchemaName))
            {
                using (var schemaReader = XmlReader.Create(schemaStream))
                {
                    _xmlReaderSettings.Schemas.Add(null, schemaReader);
                }
            }
            _xmlReaderSettings.ValidationEventHandler += ValidationEventCallback; 
        }

        /// <summary>
        /// Handler for validation events.
        /// </summary>
        /// <param name="sender">Validator sending the events.</param>
        /// <param name="args">Event arguments.</param>
        private void ValidationEventCallback(object sender, ValidationEventArgs args)
        {
            ValidationEvents.Add(args);
            HasWarningsOrErrors = true;

            if (args.Severity == XmlSeverityType.Error)
                ValidReport = false;
        }

        /// <summary>
        /// Indicates if this report has any warnings from validation.
        /// </summary>
        public bool HasWarnings => ValidationEvents.Any(x => x.Severity == XmlSeverityType.Warning);

        /// <summary>
        /// Indicates if this report has an erros from validation.
        /// </summary>
        public bool HasErrors => ValidationEvents.Any(x => x.Severity == XmlSeverityType.Error);

        /// <summary>
        /// Gets a validation error enumerable.
        /// </summary>
        public IEnumerable<ValidationEventArgs> Errors =>
            ValidationEvents.Where(x => x.Severity == XmlSeverityType.Error);

        /// <summary>
        /// Gets a validation warning enumerable.
        /// </summary>
        public IEnumerable<ValidationEventArgs> Warnings =>
            ValidationEvents.Where(x => x.Severity == XmlSeverityType.Warning);

        /// <summary>
        /// Reads a report from a stream, sets the results to <see cref="Feedback"/>
        /// </summary>
        /// <param name="ruaStream">A stream containing the RUA report to serialize.</param>
        public void ReadAggregateReport(Stream ruaStream)
        {
            using (XmlReader baseReader = XmlReader.Create(ruaStream, _xmlReaderSettings))
            {
                using (XmlReader reader = new NamespaceIgnorantXmlReader(baseReader))
                {
                    var serializer = new XmlSerializer(typeof(Feedback));
                    Feedback = (Feedback)serializer.Deserialize(reader);
                }
            }
        }
    }
}
