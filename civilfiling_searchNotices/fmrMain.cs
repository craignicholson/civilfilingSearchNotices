using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace civilfiling_searchNotices
{
    public partial class fmrMain : Form
    {
        /// <summary>
        /// Initialize NLog logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        string _CurrentEndPoint;
        string _CurrentUsername;
        string _CurrentPwd;

        string _productionEndPoint;
        string _productionUsername;
        string _productionPwd;

        string _testEndPoint;
        string _testUsername;
        string _testPwd;

        string _firmId;
        string _requestFileLocationFileName;
        string _responseFileLocationFileName;
        string _importFileLocation;
        string _readFrom;
        int _daysBack;

        /// <summary>
        /// _mode holds 'Test' or 'Production' and assigns correct configs to the current configs
        /// </summary>
        string _mode;

        public fmrMain()
        {
            _logger.Info("*************************** New Application Instance ***************************");
            InitializeComponent();
            InitializeConfigurations();

            // Check for Command Line Parameters
            string[] args = Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                _logger.Info("Command Line Args:" + arg);
            }

            // Check to see if we are running in AUTO mode See .Config value 'readFrom'
            // which is used to process files daily setup on a Task Scheduler
            // Check for args.Length == 1, when app is run without parameters the application executable path
            // is returned as the first arg.  {C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe}
            // When the application is ran with parameters, expecting firmId and noticeDate
            // we get 3 parameters {civilfiling_searchNotices.exe, F0000495, 20191215}
            if (_readFrom.ToUpper() == "AUTO" && args.Length == 1)
            {
                _logger.Info("AUTO Run");
                this.WindowState = FormWindowState.Minimized;
                // this.ShowInTaskbar = false;

                var noticeDate = DateTime.Now.AddDays(-(_daysBack));
                Run(_firmId, noticeDate.ToString("yyyyMMdd"));
            }

            // Check to see if we have 3 args, and if we do hopefully the 2nd and 3rd are firmId and noticeDate
            // TODO: Make a .bat or .PS script to run this application
            if(args.Length ==3)
            {
                // Minimize the window instead of hiding 
                this.WindowState = FormWindowState.Minimized;

                // override the mode in case it is FORM so we can have the app hide and close
                _logger.Info("Reset readFrom to AUTO");
                _readFrom = "AUTO";
                _logger.Info("Command Line Run");
                Run(args[1].ToString(), args[2].ToString());
            }
        }

        /// <summary>
        /// InitializeConfigurations assigns the app.config settings to variables the application needs to run.
        /// </summary>
        private void InitializeConfigurations()
        {
            /// Production Endpoint, Username and Password
            _productionEndPoint = ConfigurationManager.AppSettings["productionEndPoint"].ToString();
            if (string.IsNullOrEmpty(_productionEndPoint))
            {
                _logger.Error("'productionEndPoint' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'productionEndPoint' is not configured in Config file");
            }
            _productionUsername = ConfigurationManager.AppSettings["productionUsername"].ToString();
            if (string.IsNullOrEmpty(_productionUsername))
            {
                _logger.Error("'productionUsername' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'productionUsername' is not configured in Config file");
            }
            _productionPwd = ConfigurationManager.AppSettings["productionPwd"].ToString();
            if (string.IsNullOrEmpty(_productionPwd))
            {
                _logger.Error("'productionPwd' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'productionPwd' is not configured in Config file");
            }

            /// Test Endpoint Username and password
            _testEndPoint = ConfigurationManager.AppSettings["testEndPoint"].ToString();
            if (string.IsNullOrEmpty(_testEndPoint))
            {
                _logger.Error("'testEndPoint' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'testEndPoint' is not configured in Config file");
            }

            _testUsername = ConfigurationManager.AppSettings["testUsername"].ToString();
            if (string.IsNullOrEmpty(_testUsername))
            {
                _logger.Error("'testUsername' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'testUsername' is not configured in Config file");
            }

            _testPwd = ConfigurationManager.AppSettings["testPwd"].ToString();
            if (string.IsNullOrEmpty(_testPwd))
            {
                _logger.Error("'testPwd' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'testPwd' is not configured in Config file");
            }

            _firmId = ConfigurationManager.AppSettings["firmId"].ToString();
            if (string.IsNullOrEmpty(_firmId))
            {
                _logger.Error("'firmId' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'firmId' is not configured in Config file");
            }
            _requestFileLocationFileName = ConfigurationManager.AppSettings["requestFileLocationFileName"].ToString();
            if (string.IsNullOrEmpty(_requestFileLocationFileName))
            {
                _logger.Error("'requestFileLocationFileName' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'requestFileLocationFileName' is not configured in Config file");
            }
            _responseFileLocationFileName = ConfigurationManager.AppSettings["responseFileLocationFileName"].ToString();
            if (string.IsNullOrEmpty(_responseFileLocationFileName))
            {
                _logger.Error("'responseFileLocationFileName' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'responseFileLocationFileName' is not configured in Config file");
            }
            _importFileLocation = ConfigurationManager.AppSettings["importFileLocation"].ToString();
            if (string.IsNullOrEmpty(_importFileLocation))
            {
                _logger.Error("'importFileLocation' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'importFileLocation' is not configured in Config file");
            }
            _readFrom = ConfigurationManager.AppSettings["readFrom"].ToString();
            if (string.IsNullOrEmpty(_readFrom))
            {
                _logger.Error("'readFrom' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'readFrom' is not configured in Config file");
            }

            // Mode is set in the app.config and correct values are Test and Production
            _mode = ConfigurationManager.AppSettings["mode"].ToString();
            if (string.IsNullOrEmpty(_mode))
            {
                _logger.Error("'mode' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'mode' is not configured in Config file");
            }

            // Mode is set in the app.config and correct values are Test and Production
            string daysBack = ConfigurationManager.AppSettings["daysBack"].ToString();
            if (string.IsNullOrEmpty(daysBack))
            {
                _logger.Error("'daysBack' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'daysBack' is not configured in Config file");
            }
            // Convert String to int
            if (!int.TryParse(daysBack, out _daysBack))
            {
                _logger.Error("'daysBack' failed to convert "+ daysBack+" to an interger");
                rtbSearchNotices.AppendText(Environment.NewLine + "'daysBack' failed to convert " + daysBack + " to an interger");
            }

            if (_mode.ToUpper() == "Test".ToUpper())
            {
                _CurrentEndPoint = _testEndPoint;
                _CurrentUsername = _testUsername;
                _CurrentPwd = _testPwd;
                rtbSearchNotices.AppendText(Environment.NewLine + "Mode is set to 'Test'");
                _logger.Info("Mode is set to 'Test");
            }
            else if (_mode.ToUpper() == "Production".ToUpper())
            {
                _CurrentEndPoint = _productionEndPoint;
                _CurrentUsername = _productionUsername;
                _CurrentPwd = _productionPwd;
                rtbSearchNotices.AppendText(Environment.NewLine + "Mode is set to 'Production");
                _logger.Info("Mode is set to 'Production");
            }
            else
            {
                rtbSearchNotices.AppendText(Environment.NewLine + "Mode should set to 'Test' or 'Production");
            }
        }

        /// <summary>
        /// Run initializes the process to execute a http request to retreive the notices. Writes the response 
        /// to response.xml and parses the response for valid file and writes the data out to a text file for importing.
        /// </summary>
        /// <param name="firmId">FirmId for the Law Firm</param>
        /// <param name="noticeDate">yyyyMMdd</param>
        private async void Run(string firmId, string noticeDate)
        {
            _logger.Info("*************************** Run Web Request ***************************");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                _logger.Info("Start Search Notices");
                _logger.Info("CurrentEndPoint: " + _CurrentEndPoint);
                _logger.Info("CurrentUsername: " + _CurrentUsername);
                _logger.Info("firmID: " + firmId);
                _logger.Info("noticeDate: " + noticeDate);
                _logger.Info("Running in: " + _readFrom);

                rtbSearchNotices.AppendText(Environment.NewLine + "CurrentEndPoint: " + _CurrentEndPoint);
                rtbSearchNotices.AppendText(Environment.NewLine + "CurrentUsername: " + _CurrentUsername);
                rtbSearchNotices.AppendText(Environment.NewLine + "firmID: " + firmId);
                rtbSearchNotices.AppendText(Environment.NewLine + "noticeDate: " + noticeDate);
                rtbSearchNotices.AppendText(Environment.NewLine + "Running in: " + _readFrom);

                // Create /responses directory
                System.IO.Directory.CreateDirectory(_importFileLocation);

                HttpClient client = new HttpClient();
                
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_CurrentEndPoint);

                // Set the Method property of the request to POST.
                request.Method = "POST";

                // Load the request body and replace the paramters with the app.config parameters
                string content = System.IO.File.ReadAllText(_requestFileLocationFileName);
                StringBuilder builder = new StringBuilder(content);
                builder.Replace("@username", _CurrentUsername);
                builder.Replace("@password", _CurrentPwd);
                builder.Replace("@firmId", firmId);
                builder.Replace("@noticeDate", noticeDate);

                // Prepare and send the request
                string requestBody = builder.ToString();

                _logger.Info("Sending web request...");
                rtbSearchNotices.AppendText(Environment.NewLine + "Sending web request...");

                var response = await client.PostAsync(_CurrentEndPoint, new StringContent(requestBody));

                // Prepare and read the reponse from the web service
                var responseFileName = _responseFileLocationFileName;
                var responseImportFileName = _importFileLocation + firmId + "_" + noticeDate + ".txt";
                var responseString = await response.Content.ReadAsStringAsync();

                // Write the response to disk
                _logger.Info("Writing Raw Response File...");
                rtbSearchNotices.AppendText(Environment.NewLine + "Write Raw Response to File...");
                System.IO.File.WriteAllText(responseFileName, responseString);

                _logger.Info("Response File Written to disk: " + _responseFileLocationFileName);
                rtbSearchNotices.AppendText(Environment.NewLine + "Response File Written to disk: " + _responseFileLocationFileName);

                // Check for an fault codes
                if (responseString.Contains("<faultstring>"))
                {
                    // Parse out the error code
                    int start = responseString.IndexOf("<faultstring>") + "<faultstring>".Length;
                    int end = responseString.IndexOf("</faultstring>", start);
                    string result = responseString.Substring(start, end - start);

                    _logger.Info("faultcode: " + result);
                    rtbSearchNotices.AppendText(Environment.NewLine + Environment.NewLine + result);

                    if (result == "Rejected by policy. (from client)")
                    {
                        _logger.Info("Issue: " + "username or password is incorrect");
                        rtbSearchNotices.AppendText(Environment.NewLine + "username or password is incorrect" + Environment.NewLine);
                    }

                    _logger.Info("Fault encoutered no final import file will be written to disk.");
                    rtbSearchNotices.AppendText(Environment.NewLine + "Fault encoutered no final import file will be written to disk.");

                    // stop processing
                    if (_readFrom.ToUpper() == "AUTO")
                    {
                        watch.Stop();
                        _logger.Info("Run -> ElapsedMilliseconds: " + watch.ElapsedMilliseconds.ToString());

                        _logger.Info("Run: AUTO Run Environment.Exit(0)");
                        Environment.Exit(0);
                    }

                    watch.Stop();
                    _logger.Info("Run -> ElapsedMilliseconds: " + watch.ElapsedMilliseconds.ToString());

                    rtbSearchNotices.SelectionStart = rtbSearchNotices.Text.Length;
                    rtbSearchNotices.ScrollToCaret();
                    return;
                }

                // Check for an message codes
                if (responseString.Contains("<messages><code>"))
                {
                    _logger.Info("Message Code found.");
                    rtbSearchNotices.AppendText(Environment.NewLine + "Message Code found.");

                    // Parse out the code
                    int start = responseString.IndexOf("<messages>") + "<messages>".Length;
                    int end = responseString.IndexOf("</messages>", start);
                    string result = responseString.Substring(start, end - start);

                    _logger.Info("Message Code: " + result);
                    rtbSearchNotices.AppendText(Environment.NewLine + Environment.NewLine + "Message Code: " + result);

                    _logger.Info("Review response.xml for more details: " + _responseFileLocationFileName);
                    rtbSearchNotices.AppendText(Environment.NewLine + Environment.NewLine + "Review response.xml for more details: " + _responseFileLocationFileName);
                }

                // Scrub response file to make the import file
                // Damn you xop!!!
                var oldLines = System.IO.File.ReadAllLines(responseFileName);
                // Remove: 'Content' headers
                var newLines = oldLines.Where(line => !line.Contains("Content"));
                // Remove: '--MIMEBoundary'
                newLines = newLines.Where(line => !line.Contains("MIMEBoundary"));
                // Remove: 'xml version="1.0" encoding="UTF-8"?>'
                newLines = newLines.Where(line => !line.Contains("xml version"));
                // Remove: 'soapenv:Envelope'
                newLines = newLines.Where(line => !line.Contains("soapenv:Envelope"));

                rtbSearchNotices.AppendText(Environment.NewLine + "Attempt to Write Import File...");
                _logger.Info("Attempt to Write Import to File");

                // Catch the No Records Found and inform users and stop processing.
                if (newLines.Count() == 0)
                {
                    _logger.Info("No records found. No final import file will be written to disk.");
                    rtbSearchNotices.AppendText(Environment.NewLine + "No records found. No final import file will be written to disk.");
                    if (_readFrom.ToUpper() == "AUTO")
                    {
                        watch.Stop();
                        _logger.Info("Run -> ElapsedMilliseconds: " + watch.ElapsedMilliseconds.ToString());

                        _logger.Info("Run: AUTO Run Environment.Exit(0)");
                        Environment.Exit(0);
                    }

                    watch.Stop();
                    _logger.Info("Run -> ElapsedMilliseconds: " + watch.ElapsedMilliseconds.ToString());

                    rtbSearchNotices.SelectionStart = rtbSearchNotices.Text.Length;
                    rtbSearchNotices.ScrollToCaret();
                    return;
                }

                // Write the final output file which will be imported
                System.IO.File.WriteAllLines(responseImportFileName, newLines);

                // Post Process - this is were the nasty code begins
                PostProcess(responseImportFileName);

                // Finish process with a notification
                _logger.Info("End Search Notices Request. Thanks!");
                rtbSearchNotices.AppendText(Environment.NewLine + "End Search Notices Request. Thanks!");

                watch.Stop();
                _logger.Info("Run -> ElapsedMilliseconds: " + watch.ElapsedMilliseconds.ToString());

                if (_readFrom.ToUpper() == "AUTO")
                {
                    _logger.Info("Run: AUTO Run Environment.Exit(0)");
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                _logger.Info("Method->Run Error: " + e.Message);
                rtbSearchNotices.AppendText(Environment.NewLine + "Method->Run Error:" + e.Message);
            }

            rtbSearchNotices.SelectionStart = rtbSearchNotices.Text.Length;
            rtbSearchNotices.ScrollToCaret();
        }

        /// <summary>
        /// Send Button for posting the notice request.
        /// https://github.com/rstropek/Samples/tree/master/WiXSamples
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                _readFrom = _readFrom.ToUpper();
                if (_readFrom.ToUpper() == "FORM")
                {
                    // if it was a button click get date from calendar
                    string noticeDate = dtSearchNotices.Value.ToString("yyyyMMdd");
                    Run(_firmId, noticeDate);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// PostProcess cleans up the file to match the current Post Card file specification.
        /// </summary>
        /// <param name="filePath"></param>
        private void PostProcess(string filePath)
        {
            // Remove all empty lines at the start only leaving one empty line between the start and the data.
            bool previousLineHadData = false;
            int counter = 0;
            string line;
            List<string> lines = new List<string>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@filePath);
            while ((line = file.ReadLine()) != null)
            {
                // At the top because found empty lines with one char
                line = line.TrimEnd(); 
                if (line.Length == 0)
                {
                    previousLineHadData = false;
                }
                else // we have data
                {
                    // File we are parsing has 5 CRLF's at the start, we only need one CRLF
                    if(previousLineHadData == false)
                    {
                        lines.Add("");
                    }
                    if(line.Length > 1)
                    {
                        // We have two spaces added to each line and only need one space
                        // remove the first char
                        line = line.Substring(1);
                    }
                    lines.Add(line);
                    previousLineHadData = true;
                }
                counter++;
            }

            file.Close();
            System.IO.File.WriteAllLines(filePath, lines.ToArray());
            _logger.Info("Write to Import File Complete: " + lines.Count() + " Lines, " + filePath);
            rtbSearchNotices.AppendText(Environment.NewLine + "Write to Import File Complete: " + lines.Count() + " Lines");
            rtbSearchNotices.AppendText(Environment.NewLine + "File: " + filePath);
        }
    }
}