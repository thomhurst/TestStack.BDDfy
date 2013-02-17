﻿// Copyright (C) 2011, Mehdi Khalili
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using TestStack.BDDfy.Core;

namespace TestStack.BDDfy.Processors.Diagnostics
{
    public class DiagnosticsReporter : IBatchProcessor
    {
        private readonly IDiagnosticsCalculator _calculator;
        private readonly ISerializer _serializer;
        private readonly IReportWriter _writer;

        public DiagnosticsReporter() : this(new DiagnosticsCalculator(),  new JsonSerializer(), new FileWriter()) { }

        public DiagnosticsReporter(IDiagnosticsCalculator calculator,  ISerializer serializer, IReportWriter writer)
        {
            _calculator = calculator;
            _serializer = serializer;
            _writer = writer;
        }

        public void Process(IEnumerable<Core.Story> stories)
        {
            const string error = "There was an error compiling the json report: ";
            var viewModel = new FileReportModel(stories);
            string report;

            try
            {
                var data = _calculator.GetDiagnosticData(viewModel);
                report = _serializer.Serialize(data);
            }
            catch (Exception ex)
            {
                report = error + ex.Message;
            }

            _writer.Create(report, "Diagnostics.json");
        }
    }
}
