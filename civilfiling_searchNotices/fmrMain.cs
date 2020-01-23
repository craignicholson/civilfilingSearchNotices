using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        string _readFromFileFolder;
        string _readFromFileProcessedFolder;
        int _daysBack;

        /// <summary>
        /// _mode holds 'Test' or 'Production' and assigns correct configs to the current configs
        /// </summary>
        string _mode;

        public fmrMain()
        {
            InitializeComponent();
            InitializeConfigurations();

            if (_readFrom.ToUpper() == "AUTO")
            {
                this.WindowState = FormWindowState.Minimized;
                // this.ShowInTaskbar = false;

                var noticeDate = DateTime.Now.AddDays(-_daysBack);
                Run(_firmId, noticeDate.ToString("yyyyMMdd"));
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
            _readFromFileFolder = ConfigurationManager.AppSettings["readFromFileFolder"].ToString();
            if (string.IsNullOrEmpty(_readFromFileFolder))
            {
                _logger.Error("'readFromFileFolder' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'readFromFileFolder' is not configured in Config file");
            }
            _readFromFileProcessedFolder = ConfigurationManager.AppSettings["readFromFileProcessedFolder"].ToString();
            if (string.IsNullOrEmpty(_readFromFileProcessedFolder))
            {
                _logger.Error("'readFromFileProcessedFolder' is not configured in Config file");
                rtbSearchNotices.AppendText(Environment.NewLine + "'readFromFileProcessedFolder' is not configured in Config file");
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

            // lblMode.Text = _mode;
            if (_mode == "Test")
            {
                _CurrentEndPoint = _testEndPoint;
                _CurrentUsername = _testUsername;
                _CurrentPwd = _testPwd;
                rtbSearchNotices.AppendText(Environment.NewLine + "Mode is set to 'Test'");
                _logger.Info("Mode is set to 'Test");
            }
            else if (_mode == "Production")
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
        /// SearchNoticeParameters Class used to support processing a csv file, see method ProcessFile.
        /// </summary>
        class SearchNoticeParameters
        {
            public string firmId;
            public string noticeDate;

            public static SearchNoticeParameters FromCsv(string csvLine)
            {
                string[] values = csvLine.Split(',');
                SearchNoticeParameters data = new SearchNoticeParameters
                {
                    firmId = values[0],
                    noticeDate = values[1]
                };
                return data;
            }
        }

        /// <summary>
        /// ProcessFile reads a csv file with the firmId and noticeDate.
        /// </summary>
        /// <returns>List(SearchNoticeParameters) containing firmId, noticeDate</returns>
        private List<SearchNoticeParameters> ProcessFile()
        {
            // 
            string ts = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".csv";
            
            // get the file on the folder
            List<SearchNoticeParameters> values = File.ReadAllLines(_readFromFileFolder)
                                          .Skip(1)
                                          .Select(v => SearchNoticeParameters.FromCsv(v))
                                          .ToList();

            // Archive the file
            File.Move(_readFromFileFolder, _readFromFileProcessedFolder + ts); // Try to move

            return values;
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
            if (_readFrom.ToUpper() == "AUTO")
            {
                this.Hide();
            }

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
                        _logger.Info("Run: AUTO Run Environment.Exit(0)");
                        Environment.Exit(0);
                    }

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

                // Scrub reponse file to make the import file
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

                if (newLines.Count() == 0)
                {
                    _logger.Info("No records found. No final import file will be written to disk.");
                    rtbSearchNotices.AppendText(Environment.NewLine + "No records found. No final import file will be written to disk.");
                    if (_readFrom.ToUpper() == "AUTO")
                    {
                        _logger.Info("Run: AUTO Run Environment.Exit(0)");
                        Environment.Exit(0);
                    }

                    rtbSearchNotices.SelectionStart = rtbSearchNotices.Text.Length;
                    rtbSearchNotices.ScrollToCaret();
                    return;
                }

                // Write the final output file which will be imported
                System.IO.File.WriteAllLines(responseImportFileName, newLines);
                _logger.Info("Write to Import File Complete: " + newLines.Count() + " Lines, " + responseImportFileName);
                rtbSearchNotices.AppendText(Environment.NewLine + "Write to Import File Complete: " + newLines.Count() + " Lines, " + responseImportFileName);

                // Finish process with a notification
                _logger.Info("End Search Notices Request. Thanks!");
                rtbSearchNotices.AppendText(Environment.NewLine + "End Search Notices Request. Thanks!");

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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_ClickAsync(object sender, EventArgs e)
        {
            _readFrom = _readFrom.ToUpper();

            if (_readFrom == "FILE")
            {
                List<SearchNoticeParameters> values = ProcessFile();
                foreach (var item in values)
                {
                    _logger.Info(item.firmId + ", " + item.noticeDate);
                    Run(item.firmId, item.noticeDate);
                }
            }
            else if(_readFrom == "FORM")
            {
                // if it was a button click get date from calendar
                string noticeDate = dtSearchNotices.Value.ToString("yyyyMMdd");
                Run(_firmId, noticeDate);
            }
        }
    }
}