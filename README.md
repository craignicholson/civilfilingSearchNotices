# Search Notices Client

## Hours worked

Review Documentation - searchNotices
- Requires application to pass in BranchId (from file) and a Date (YYYYMMDD)
- Post the request to the webservice

Data Returned for each notice
- firmId
- date
- noticeDesc
- noticeType
- dockerNumber -> docketSeqNum
- dockerNumber -> docketCourtYear 
- dockerNumber -> docketDocketTypeCode
- dockerNumber -> docketVenue

Proposed output
- Write the data to a text file with the filename as SearchDate-Timestamp.txt
- Write the output of the webservice as comma separated values

Proposed Form
- Add new form to accept the user entering a date to collect the notices

Testing
- Re-test all functionality
- Fix anything we broke  when replacing the existing web service with the new web service.


Time Breakout
Development - 16 hours
Testing - 4 to 8 hours

Approximate Costs - 10, 16,  to 24 hours (min, avg, max)  if we have any issues or changes.

150 * 24 = 3600

-- 38 hours -

2020-01-23 - 4 hrs - Working on AUTO Feature and review of code and adding logging features for users
2020-01-22 - 2 hrs

-- 32 hours

2019-12-18 - 4
2019-12-17 - 4
2019-12-16 - 4

-- 22 hours

2019-12-13 - 2
2019-12-10 - 2
2019-12-09 - 2

2019-12-05 - 8
2019-12-04 - 8


Application makes a request the the New Jeresy Courts to perform a search of notices using a firmId and notice date.

## Features

Make a request from the form by choosing the search date from the drop down box.  Click the 'Send'

- Application reads from the request.xml
- Replaces the defaults in request.xml with the settings from civilfiling_searchNotices.exe.config
 -- username
 -- password
 -- firmId
 -- noticeDate
- Request is sent to the endpoint
- Soap Response is written to a file
- Reponse is parsed into the import file format.

### Example Form results in textbox

Mode is set to 'Test'
https://dptng.njcourts.gov:2045/civilFilingWS_t
F00000495
20191217
Write Raw Response to File...
Response File Complete C:\civilfiling_searchNotices\response.xml
messages><code>ECCV230</code><description>No Records found</description>
Attempt to Write Import File...
No records where found.
End Search Notices Request. Thanks!


Mode is set to 'Test'
https://dptng.njcourts.gov:2045/civilFilingWS_t
F00000495
20191202
Write Raw Response to File...
Response File Complete C:\civilfiling_searchNotices\response.xml
messages><code>ECCV600</code><description>Success Message</description>
Attempt to Write Import File...
Write to Import File Complete: C:\civilfiling_searchNotices\responses\F00000495_20191202.txt
End Search Notices Request. Thanks!


## Setup

The following configs in civilfiling_searchNotices.exe.config need to be set:

- request.txt should not be changed.  edits to this file will change the soap request.
- responseFileLocationFileName needs to be added or the file path made sure it is a valid path to the file. The application will try and write this file and always read from this file.
- importFileLocation is the folder where the final import files will be written.  Files will have a file name with  

```xml
  <appSettings>
    <add key="productionEndPoint" value="https://dpprod.njcourts.gov:2045/civilFilingWS_p"/>
    <add key="productionUsername" value="productionUsername"/>
    <add key="productionPwd" value="productionPassword"/>
    <add key="testEndPoint" value="https://dptng.njcourts.gov:2045/civilFilingWS_t"/>
    <add key="testUsername" value="888888029"/>
    <add key="testPwd" value="P@ssword"/>
    <add key="firmId" value="F00000495"/>
    <!-- requestFileLocationFileName stores the raw soap request we are making 'searchNotices' -->
    <add key="requestFileLocationFileName" value="C:\civilfiling_searchNotices\request.txt"/>
    <!-- responseFileLocationFileName is a temporary file overwritten on each request containing the raw soap response. Response errors can be found in this file.-->
    <add key="responseFileLocationFileName" value="C:\civilfiling_searchNotices\response.xml"/>
    <!-- importFileLocation is the location-->
    <add key="importFileLocation" value="C:\civilfiling_searchNotices\responses\"/>
    <!-- ReadFrom values {FILE, AUTO, FORM} is a csv file containing FirmId, noticeDate (YYYYMMDD) on each line-->
    <add key="readFrom" value="AUTO"/>
    <add key="readFromFileFolder" value="C:\searchDates.csv"/>
    <add key="readFromFileProcessedFolder" value="C:\processed\"/>
    <!-- mode is 'Test' -> testEndPoint and mode is 'Production' -> productionEndpoint-->
    <add key="mode" value="Test"/>
    <add key="daysBack" value="1"/>
  </appSettings>
```



## Soap Example

Headers for web request (POST)
Content-Type: text/xml;

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
    <s:Header>
        <Security xmlns="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
            <wsse:UsernameToken Id="feinsuch" xmlns:wsse="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
                <wsse:Username>888888029</wsse:Username>
                <wsse:Password Type="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText">P@ssword</wsse:Password>
            </wsse:UsernameToken>
        </Security>
    </s:Header>
    <s:Body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <searchNotices xmlns="http://webservice.civilfiling.ecourts.ito.aoc.nj/">
            <arg0 xmlns=""><firmId>F00000495</firmId><noticeDate>20191107</noticeDate></arg0>
        </searchNotices>
    </s:Body>
</s:Envelope>
```

```xml

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: text/xml; charset=UTF-8
Content-Transfer-Encoding: binary
Content-ID: 
<0.0b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>
    <?xml version="1.0" encoding="UTF-8"?>
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        <soapenv:Body>
            <ns2:searchNoticesResponse xmlns:ns2="http://webservice.civilfiling.ecourts.ito.aoc.nj/">
                <return>
                    <messages>
                        <code>ECCV600</code>
                        <description>Success Message</description>
                    </messages>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:1b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>11550</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>OCN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0211</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:2b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>10223</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0211</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:3b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>10182</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>OCN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0211</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>5630</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0211</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>8683</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>16270</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12798</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:887a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12794</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:987a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12789</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12783</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12519</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:487a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>16</docketCourtYear>
                            <docketSeqNum>7037</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:587a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>3950</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>SOM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:687a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>13900</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MID</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0225</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:787a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>16</docketCourtYear>
                            <docketSeqNum>7037</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:087a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12783</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:187a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12789</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:287a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>12798</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:387a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>16270</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>8711</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BUR</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0240</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>5260</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0255</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>4512</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MID</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0255</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>8403</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>OCN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0255</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:897a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>6</docketCourtYear>
                            <docketSeqNum>6314</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0255</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:997a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>15393</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0275</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>15394</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>9719</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:497a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>590</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CPM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:597a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>9718</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:697a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>9717</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:797a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>9716</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:097a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>15762</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:197a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>15730</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:297a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>5981</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:397a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>5874</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ATL</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>2786</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>OCN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>3379</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>SSX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>10564</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>10540</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:867a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>3463</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:967a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>7395</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>2963</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0285</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>20328</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:467a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>1691</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ATL</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:567a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>887</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CPM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:667a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>3503</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CUM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:767a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>16053</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:067a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>0</docketCourtYear>
                            <docketSeqNum>15745</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:167a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>11581</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:267a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>7719</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:367a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>13</docketCourtYear>
                            <docketSeqNum>7118</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>11</docketCourtYear>
                            <docketSeqNum>5622</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>4673</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CPM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>4062</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CPM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>106</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CPM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:877a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>12644</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:977a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>6</docketCourtYear>
                            <docketSeqNum>7019</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BUR</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>7241</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>12920</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BUR</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:477a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>6</docketCourtYear>
                            <docketSeqNum>4285</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:577a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>1832</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>CAM</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:677a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>4828</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BUR</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:777a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>13474</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:077a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>13021</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>BER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:177a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>6</docketCourtYear>
                            <docketSeqNum>5488</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:277a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>21140</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:377a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>12874</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>UNN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>1741</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>SSX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>24372</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>18120</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>5</docketCourtYear>
                            <docketSeqNum>15503</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:847a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>10423</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:947a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>13488</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>10295</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>13</docketCourtYear>
                            <docketSeqNum>9877</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:447a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>11027</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>OCN</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:547a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>2959</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>PAS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:647a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>3254</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:747a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>15708</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:047a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>10741</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:147a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>25224</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:247a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>11</docketCourtYear>
                            <docketSeqNum>2666</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:347a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>1</docketCourtYear>
                            <docketSeqNum>9748</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>11</docketCourtYear>
                            <docketSeqNum>6040</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MON</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>7674</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>MER</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>5</docketCourtYear>
                            <docketSeqNum>21422</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>20935</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:857a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>9</docketCourtYear>
                            <docketSeqNum>17503</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:957a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>11</docketCourtYear>
                            <docketSeqNum>15719</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>2</docketCourtYear>
                            <docketSeqNum>15444</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>12822</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:457a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>12</docketCourtYear>
                            <docketSeqNum>9571</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:557a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>6998</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:657a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>17031</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:757a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>6991</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:057a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>13</docketCourtYear>
                            <docketSeqNum>3327</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:157a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>10</docketCourtYear>
                            <docketSeqNum>2211</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:257a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>2148</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>HUD</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:357a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>5413</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>GLO</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>3773</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>GLO</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>3</docketCourtYear>
                            <docketSeqNum>30235</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>5</docketCourtYear>
                            <docketSeqNum>27155</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>25886</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:827a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>4</docketCourtYear>
                            <docketSeqNum>25567</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:927a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>20550</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>7</docketCourtYear>
                            <docketSeqNum>17829</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>11</docketCourtYear>
                            <docketSeqNum>16329</docketSeqNum>
                            <docketTypeCode>DC</docketTypeCode>
                            <docketVenue>ESX</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0288</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:427a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>18218</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>DKT # ASGN</noticeDesc>
                        <noticeType>CV0105</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:527a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>20029</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:627a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>19970</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:727a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>2326</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:027a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>19790</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:127a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>5330</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:227a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>5330</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:327a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>13</docketCourtYear>
                            <docketSeqNum>33783</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:c37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>14</docketCourtYear>
                            <docketSeqNum>32777</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:d37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>17</docketCourtYear>
                            <docketSeqNum>25401</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:e37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>25096</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:f37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>20029</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:837a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>19970</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:937a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>19790</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:a37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>2326</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000495</firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:b37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>8</docketCourtYear>
                            <docketSeqNum>48862</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc>PLENRY HRG PROCEEDING</noticeDesc>
                        <noticeType>CV0155</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:437a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>2318</docketSeqNum>
                            <docketTypeCode>LT</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0220</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:537a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>19</docketCourtYear>
                            <docketSeqNum>2317</docketSeqNum>
                            <docketTypeCode>LT</docketTypeCode>
                            <docketVenue>MRS</docketVenue>
                        </docketNumber>
                        <firmId>         </firmId>
                        <noticeDate>20191107</noticeDate>
                        <noticeDesc/>
                        <noticeType>CV0220</noticeType>
                    </notices>
                </return>
            </ns2:searchNoticesResponse>
        </soapenv:Body>
    </soapenv:Envelope>
--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID:
    <1b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 OCEAN SPECIAL CIVIL PART       :
 OCEAN COUNTY COURTHOUSE        :
 118 WASHINGTON STREET          :
 TOMS RIVER NJ 08754            :
 (732) 504-0700                 :
 CASE NUMBER:
   OCN DC-011550-19             :
 VANZ LLC                       :
   VS                   CV0211  : NOVEMBER 07, 2019
 HOPKINS KATELIN                :
                                :
                                :
 A SUMMONS WAS MAILED TO        : PHILIP A KAHN
 KATELIN HOPKINS                : FEIN SUCH KAHN & SHEPARD P
 ON 11-12-19 FOR                : 7 CENTURY DR STE 201
 CASE DC-011550-19.             :
 UNLESS OTHERWISE NOTIFIED,     : PARSIPPANY NJ
 THIS CASE WILL DEFAULT         :
 ON 12-20-2019.                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
        <2b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH STREET             :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
 CASE NUMBER:
   CAM DC-010223-19             :
 PLAZA SERVICES, LLC            :
   VS                   CV0211  : NOVEMBER 07, 2019
 SCARBOROUGH TANYA              :
                                :
                                :
 A SUMMONS WAS MAILED TO        : PHILIP A KAHN
 ADONTI CRONE                   : FEIN SUCH KAHN & SHEPARD P
 ON 11-12-19 FOR                : 7 CENTURY DR STE 201
 CASE DC-010223-19.             :
 UNLESS OTHERWISE NOTIFIED,     : PARSIPPANY NJ
 THIS CASE WILL DEFAULT         :
 ON 12-20-2019.                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
            <3b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 OCEAN SPECIAL CIVIL PART       :
 OCEAN COUNTY COURTHOUSE        :
 118 WASHINGTON STREET          :
 TOMS RIVER NJ 08754            :
 (732) 504-0700                 :
 CASE NUMBER:
   OCN DC-010182-19             :
 PLAZA SERVICES, LLC            :
   VS                   CV0211  : NOVEMBER 07, 2019
 QUIROS-ALVARADO CRISTIAN       :
                                :
                                :
 A SUMMONS WAS MAILED TO        : PHILIP A KAHN
 NICOLE QUIROS                  : FEIN SUCH KAHN & SHEPARD P
 ON 11-12-19 FOR                : 7 CENTURY DR STE 201
 CASE DC-010182-19.             :
 UNLESS OTHERWISE NOTIFIED,     : PARSIPPANY NJ
 THIS CASE WILL DEFAULT         :
 ON 12-20-2019.                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                <c87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
 CASE NUMBER:
   MRS DC-005630-17             :
 THE MUSIC DEN                  :
   VS                   CV0211  : NOVEMBER 07, 2019
 COTUGNO JENNIFER               :
                                :
                                :
 A SUMMONS WAS MAILED TO        : PHILIP A KAHN
 JENNIFER  COTUGNO              : FEIN SUCH KAHN & SHEPARD P
 ON 11-12-19 FOR                : 7 CENTURY DR STE 201
 CASE DC-005630-17.             :
 UNLESS OTHERWISE NOTIFIED,     : PARSIPPANY NJ
 THIS CASE WILL DEFAULT         :
 ON 12-20-2019.                 :               07054-4609
                                :
 DEFAULT DEPENDANT ON EFFECTIVE :
 SERVICE-PLEASE USE DOCKET/CASE :
 NUMBER FOR ALL INQUIRIES       :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                    <d87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
 CASE NUMBER:
   MRS DC-008683-19             :
 MORRISTOWN MEDICAL C           :
   VS                   CV0225  : NOVEMBER 07, 2019
 AKARSUMAQSUDI HANDAN           :
                                :
 THE SUMMONS ISSUED TO:         :
 HANDAN AKARSUMAQSUDI           : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 OTHER - NOT SERVED             : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
 PLEASE CONTACT COURT FOR       :
 RESERVICE INFORMATION AND      :
 INSTRUCTIONS                   :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                        <e87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
 CASE NUMBER:
   ESX DC-016270-19             :
 FAIRLEIGH DICKINSON            :
   VS                   CV0225  : NOVEMBER 07, 2019
 PASCUAL OLGA                   :
                                :
 THE SUMMONS ISSUED TO:         :
 OLGA PASCUAL                   : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 INSUFFICIENT ADDRESS           : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
 SEND CLERK CORRECT ADDRESS     :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                            <f87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012798-19             :
 OVERLOOK HOSPITAL              :
   VS                   CV0225  : NOVEMBER 07, 2019
 BALDWIN NORMAN W               :
                                :
 THE SUMMONS ISSUED TO:         :
 NORMAN W BALDWIN               : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 MOVED-NO FORWARDING ORDER      : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                <887a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012794-19             :
 PRASHANT PHATAK AND            :
   VS                   CV0225  : NOVEMBER 07, 2019
 NOVAK DAVID                    :
                                :
 THE SUMMONS ISSUED TO:         :
 DAVID NOVAK                    : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 MOVED-NO FORWARDING ORDER      : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                    <987a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012789-19             :
 OVERLOOK HOSPITAL              :
   VS                   CV0225  : NOVEMBER 07, 2019
 CAJUSTE FRANTZ                 :
                                :
 THE SUMMONS ISSUED TO:         :
 FRANTZ CAJUSTE                 : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 ADDRESSEE UNKNOWN              : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                        <a87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012783-19             :
 NEWTON MEDICAL CENTE           :
   VS                   CV0225  : NOVEMBER 07, 2019
 BALDWIN NORMAN W               :
                                :
 THE SUMMONS ISSUED TO:         :
 NORMAN W BALDWIN               : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 MOVED-NO FORWARDING ORDER      : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                            <b87a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012519-19             :
 OVERLOOK HOSPITAL              :
   VS                   CV0225  : NOVEMBER 07, 2019
 SCHEIDEGGER CHRISTOPH          :
                                :
 THE SUMMONS ISSUED TO:         :
 CHRISTOPH SCHEIDEGGER          : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 OTHER - NOT SERVED             : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                <487a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-007037-16             :
 MOTORS FINANCIAL ACC           :
   VS                   CV0225  : NOVEMBER 07, 2019
 WRIGHT ROBIN                   :
                                :
 THE SUMMONS ISSUED TO:         :
 ROBIN M WRIGHT                 : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 INSUFFICIENT ADDRESS           : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                    <587a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 SOMERSET SPECIAL CIVIL PART    :
 40 NORTH BRIDGE STREET         :
 1ST FLR PO BOX 3000            :
 SOMERVILLE NJ 08876-1262       :
 (908) 332-7700                 :
 CASE NUMBER:
   SOM DC-003950-19             :
 IRA M. KLEMONS, DDS,           :
   VS                   CV0225  : NOVEMBER 07, 2019
 SANGIOVANNI DAWN               :
                                :
 THE SUMMONS ISSUED TO:         :
 DAWN SANGIOVANNI               : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 CERTIFIED & REGULAR MAIL RETUR : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                        <687a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MIDDLESEX SPECIAL CIVIL PART   :
 56 PATERSON STREET             :
 PO BOX 1146                    :
 NEW BRUNSWICK NJ 08903         :
 (732) 645-4300                 :
 CASE NUMBER:
   MID DC-013900-19             :
 MOTORS FINANCIAL ACC           :
   VS                   CV0225  : NOVEMBER 07, 2019
 DISLA FABIO                    :
                                :
 THE SUMMONS ISSUED TO:         :
 FABIO DISLA                    : PHILIP A KAHN
 WAS UNSERVED BECAUSE:          : FEIN SUCH KAHN & SHEPARD P
 ADDRESSEE UNKNOWN              : 7 CENTURY DR STE 201
                                :
 PER R1:13-7D, THIS CASE WILL   : PARSIPPANY NJ
 BE DISMISSED IN 60 DAYS.       :
 IF SERVED, THIS CASE WILL BE   :               07054-4609
 AUTOMATICALLY REINSTATED.      :
 ? (732) 645-4300 X 88382       :
 SI NECESITA UN INTERPRETE      :
 LLAMAR (732) 645-4300 X 2      :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                            <787a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-007037-16             :
 MOTORS FINANCIAL ACC           :
   VS                   CV0240  : NOVEMBER 07, 2019
 WRIGHT ROBIN                   :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-06-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                <087a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012783-19             :
 NEWTON MEDICAL CENTE           :
   VS                   CV0240  : NOVEMBER 07, 2019
 BALDWIN NORMAN W               :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-04-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                    <187a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012789-19             :
 OVERLOOK HOSPITAL              :
   VS                   CV0240  : NOVEMBER 07, 2019
 CAJUSTE FRANTZ                 :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-01-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                        <287a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
 CASE NUMBER:
   UNN DC-012798-19             :
 OVERLOOK HOSPITAL              :
   VS                   CV0240  : NOVEMBER 07, 2019
 BALDWIN NORMAN W               :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-04-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                            <387a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
 CASE NUMBER:
   ESX DC-016270-19             :
 FAIRLEIGH DICKINSON            :
   VS                   CV0240  : NOVEMBER 07, 2019
 PASCUAL OLGA                   :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-04-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                <c97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
 CASE NUMBER:
   BUR DC-008711-19             :
 RUTGERS THE STATE U            :
   VS                   CV0240  : NOVEMBER 07, 2019
 GRANT DYAMOND T                :
                                :
                                :
 PER R1:13-7D,                  : PHILIP A KAHN
 THIS CASE WAS MARKED           : FEIN SUCH KAHN & SHEPARD P
 DISMISSED ON 11-06-2019        : 7 CENTURY DR STE 201
 SUBJECT TO AUTOMATIC           :
 RESTORATION UPON SERVICE OF    : PARSIPPANY NJ
 THE SUMMONS WITHIN ONE YEAR.   :
                                :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                    <d97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
 CASE NUMBER:
   ESX DC-005260-17             :
 171 NORTH 16 LLC               :
   VS                   CV0255  : NOVEMBER 07, 2019
 DUPREE SHAMEKA L               :
                                :
 A WAGE EXECUTION HEARING       :
 IS SCHEDULED FOR               : PHILIP A KAHN
 CASE DC-005260-17              : FEIN SUCH KAHN & SHEPARD P
 ON 12-04-2019 AT 09:00AM       : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE BEACHAM                  :
 COURT ROOM 412OC               :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                        <e97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MIDDLESEX SPECIAL CIVIL PART   :
 56 PATERSON STREET             :
 PO BOX 1146                    :
 NEW BRUNSWICK NJ 08903         :
 (732) 645-4300                 :
 CASE NUMBER:
   MID DC-004512-12             :
 NEW CENTURY FINANCIA           :
   VS                   CV0255  : NOVEMBER 07, 2019
 BOOTHE CHRISTINE               :
                                :
 A WAGE EXECUTION HEARING       :
 IS SCHEDULED FOR               : PHILIP A KAHN
 CASE DC-004512-12              : FEIN SUCH KAHN & SHEPARD P
 ON 12-05-2019 AT 09:00AM       : 7 CENTURY DR STE 201
 ? CALL (732) 645-4300 X 88383  :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE CORMAN                   :
 COURT ROOM 205                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                            <f97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 OCEAN SPECIAL CIVIL PART       :
 OCEAN COUNTY COURTHOUSE        :
 118 WASHINGTON STREET          :
 TOMS RIVER NJ 08754            :
 (732) 504-0700                 :
 CASE NUMBER:
   OCN DC-008403-19             :
 ADVANCED ORTHOPEDICS           :
   VS                   CV0255  : NOVEMBER 07, 2019
 BENEDICT JUDITH                :
                                :
 A SETTLEMENT CONFERENCE        :
 IS SCHEDULED FOR               : PHILIP A KAHN
 CASE DC-008403-19              : FEIN SUCH KAHN & SHEPARD P
 ON 12-03-2019 AT 09:00AM       : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE PALMER JR                :
 COURT ROOM 008                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                <897a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MERCER SPECIAL CIVIL PART      :
 175 SOUTH BROAD STREET  1ST FL :
 PO BOX 8068                    :
 TRENTON NJ 08650-0068          :
 (609) 571-4200                 :
 CASE NUMBER:
   MER DC-006314-06             :
 PALISADES COLLECTION           :
   VS                   CV0255  : NOVEMBER 07, 2019
 KSEPKA MICHAEL                 :
                                :
 A MOTION HEARING               :
 IS SCHEDULED FOR               : PHILIP A KAHN
 CASE DC-006314-06              : FEIN SUCH KAHN & SHEPARD P
 ON 11-21-2019 AT 09:00AM       : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE ANKLOWITZ                :
 COURT ROOM 1B                  :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                    <997a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
 CASE NUMBER:
   BER DC-015393-19             :
 UNIVERSITY PHYSICIAN           :
   VS                   CV0275  : NOVEMBER 07, 2019
 HAKOU AGHNATIOU                :
                                :
 A NON-JURY TRIAL HAS BEEN      :
 SCHEDULED FOR THIS CASE        : PHILIP A KAHN
 ON 12-10-2019 AT 08:45AM       : FEIN SUCH KAHN & SHEPARD P
                                : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE MONAGHAN                 :
 COURT ROOM 332                 :               07054-4609
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                        <a97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   BER DC-015394-19             :
 CREDITOR(S): MONTCLAIR RADIOLO :
 DEBTORS(S):  LATTE G           : PHILIP A KAHN
 VJ NUMBER:   011804-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 1216.52      :
 COST:               57.00      : PARSIPPANY NJ
 ATTORNEY FEE:       39.33      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 1312.85      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                            <b97a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   CAM DC-009719-19             :
 CREDITOR(S): FAIRLEIGH DICKINS :
 DEBTORS(S):  MCCLEERY F        : PHILIP A KAHN
 VJ NUMBER:   008217-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 8049.14      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      224.28      :
 OTHER COST:       2414.74      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $10770.16      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                <497a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAPE MAY SPECIAL CIVIL PART    :
 DN-203 CASE MANAGEMENT OFFICE  :
 9 NORTH MAIN STREET            :
 CAPE MAY CRT HSE NJ 08210-3096 :
 (609) 402-0100                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   CPM DC-000590-19             :
 CREDITOR(S): VANZ LLC          :
 DEBTORS(S):  DONAHUE J         : PHILIP A KAHN
 VJ NUMBER:   001383-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/05/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 3266.29      :
 COST:               89.00      : PARSIPPANY NJ
 ATTORNEY FEE:       80.33      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 3435.62      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                    <597a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   CAM DC-009718-19             :
 CREDITOR(S): UNITED AUTO CREDI :
 DEBTORS(S):  MANGUM B          : PHILIP A KAHN
 VJ NUMBER:   008216-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 6936.13      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      153.72      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 7171.85      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                        <697a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   CAM DC-009717-19             :
 CREDITOR(S): UNITED AUTO CREDI :
 DEBTORS(S):  LORM S            : PHILIP A KAHN
 VJ NUMBER:   008215-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 6452.32      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      144.05      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 6678.37      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                            <797a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   CAM DC-009716-19             :
 CREDITOR(S): UNITED AUTO CREDI :
 DEBTORS(S):  WILLIAMS M        : PHILIP A KAHN
 VJ NUMBER:   008214-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 4627.00      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      107.54      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 4816.54      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                <097a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   BER DC-015762-19             :
 CREDITOR(S): FAIRLEIGH DICKINS :
 DEBTORS(S):  THORNE A          : PHILIP A KAHN
 VJ NUMBER:   011813-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 7100.00      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      157.00      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 7339.00      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                    <197a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   BER DC-015730-19             :
 CREDITOR(S): SAME DAY PROCEDUR :
 DEBTORS(S):  PARISI E          : PHILIP A KAHN
 VJ NUMBER:   011808-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 1558.56      :
 COST:               57.00      : PARSIPPANY NJ
 ATTORNEY FEE:       46.17      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 1661.73      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                        <297a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   BER DC-005981-18             :
 CREDITOR(S): PARAMUS SURGICAL  :
 DEBTORS(S):  DELL AQUILA L     : PHILIP A KAHN
 VJ NUMBER:   011802-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/04/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 5100.00      :
 COST:                0.00      : PARSIPPANY NJ
 ATTORNEY FEE:        0.00      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 5100.00      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                            <397a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ATLANTIC SPECIAL CIVIL PART    :
 ATLANTIC COUNTY COURTHOUSE     :
 1201 BACHARACH BLVD            :
 ATLANTIC CITY NJ 08401         :
 (609) 402-0100                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   ATL DC-005874-19             :
 CREDITOR(S): UNITED AUTO CREDI :
 DEBTORS(S):  THOMAS D          : PHILIP A KAHN
              DITZEL JR A       : FEIN SUCH KAHN & SHEPARD P
 VJ NUMBER:   004683-19         : 7 CENTURY DR STE 201
 EFFECTIVE DATE: 11/06/2019     :
 AMOUNT:         $ 3197.07      : PARSIPPANY NJ
 COST:               94.00      :
 ATTORNEY FEE:       78.94      :                07054-4609
 OTHER COST:          0.00      :
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 3370.01      :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                <c67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 OCEAN SPECIAL CIVIL PART       :
 OCEAN COUNTY COURTHOUSE        :
 118 WASHINGTON STREET          :
 TOMS RIVER NJ 08754            :
 (732) 504-0700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   OCN DC-002786-18             :
 CREDITOR(S): TOMS RIVER MEDICA :
 DEBTORS(S):  WILSON L          : PHILIP A KAHN
 VJ NUMBER:   008481-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/07/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $  295.00      :
 COST:                0.00      : PARSIPPANY NJ
 ATTORNEY FEE:        0.00      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $  295.00      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                    <d67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 SUSSEX SPECIAL CIVIL PART      :
 SUSSEX COUNTY JUDICIAL CENTER  :
 43-47 HIGH ST PLAZA LEVEL      :
 NEWTON NJ 07860-1738           :
 (862) 397-5700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   SSX DC-003379-19             :
 CREDITOR(S): NEW CITY FUNDING  :
 DEBTORS(S):  BEATTY J          : PHILIP A KAHN
 VJ NUMBER:   002372-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/07/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 4359.71      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      102.19      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 4543.90      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                        <e67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   PAS DC-010564-19             :
 CREDITOR(S): SHOMAF NAKHJO DO  :
 DEBTORS(S):  PERRY C           : PHILIP A KAHN
 VJ NUMBER:   008286-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $12455.80      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      264.12      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $12801.92      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                            <f67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   PAS DC-010540-19             :
 CREDITOR(S): SAME DAY PROCEDUR :
 DEBTORS(S):  REYNALDO C        : PHILIP A KAHN
 VJ NUMBER:   008285-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 1794.67      :
 COST:               57.00      : PARSIPPANY NJ
 ATTORNEY FEE:       50.89      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 1902.56      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                <867a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   PAS DC-003463-18             :
 CREDITOR(S): HACKENSACK NJ MED :
 DEBTORS(S):  BURBANO J         : PHILIP A KAHN
 VJ NUMBER:   008288-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/07/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 2578.99      :
 COST:               57.00      : PARSIPPANY NJ
 ATTORNEY FEE:       66.58      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 2702.57      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                    <967a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   MRS DC-007395-19             :
 CREDITOR(S): FAIRLEIGH DICKINS :
 DEBTORS(S):  JONIAUX R         : PHILIP A KAHN
 VJ NUMBER:   004744-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 5094.51      :
 COST:               82.00      : PARSIPPANY NJ
 ATTORNEY FEE:      116.89      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 5293.40      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                        <a67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
                        CV0285  : NOVEMBER 07, 2019
 CASE NUMBER:
   MRS DC-002963-19             :
 CREDITOR(S): ADVANCED CAR CARE :
 DEBTORS(S):  HARTLE A          : PHILIP A KAHN
 VJ NUMBER:   004741-19         : FEIN SUCH KAHN & SHEPARD P
 EFFECTIVE DATE: 11/06/2019     : 7 CENTURY DR STE 201
 AMOUNT:         $ 3623.90      :
 COST:              107.00      : PARSIPPANY NJ
 ATTORNEY FEE:        0.00      :
 OTHER COST:          0.00      :                07054-4609
 CREDITS:             0.00      :
 JUDGMENT TOTAL: $ 3730.90      :
                                :
                                :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                            <b67a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-020328-08  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   OUSLEY J          : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1175.19        : PARSIPPANY NJ
 COSTS & MILE:     45.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     200.75        :
 CRT OFR COMM:    142.09        :
 GRAND TOTAL:  $ 1563.03        :
 CRT OFR: GLAB D                :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 857-0085  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                <467a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>



 ATLANTIC SPECIAL CIVIL PART    :
 ATLANTIC COUNTY COURTHOUSE     :
 1201 BACHARACH BLVD            :
 ATLANTIC CITY NJ 08401         :
 (609) 402-0100                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ATL DC-001691-09  :
 CREDITOR(S): BAXTER FINANCIAL  :
 DEBTOR(S):   COLON H           : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 7549.96        :
 COSTS & MILE:     37.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     589.89        :                07054-4609
 CRT OFR COMM:    817.69        :
 GRAND TOTAL:  $ 8994.54        :
 CRT OFR: BOSCO F               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 204-0575  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                    <567a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAPE MAY SPECIAL CIVIL PART    :
 DN-203 CASE MANAGEMENT OFFICE  :
 9 NORTH MAIN STREET            :
 CAPE MAY CRT HSE NJ 08210-3096 :
 (609) 402-0100                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CPM DC-000887-18  :
 CREDITOR(S): BAXTER FINANCIAL, :
 DEBTOR(S):   GRIFFIN R         : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 4650.47        : PARSIPPANY NJ
 COSTS & MILE:     43.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     110.31        :
 CRT OFR COMM:    480.38        :
 GRAND TOTAL:  $ 5284.16        :
 CRT OFR: WELSH R               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 365-8793  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                        <667a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CUMBERLAND SPECIAL CIVIL PART  :
 60 W BROAD ST                  :
 BRIDGETON NJ 08302-0010        :
                                :
 (856) 878-5050                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CUM DC-003503-10  :
 CREDITOR(S): BAXTER FINANCIAL, :
 DEBTOR(S):   SAINT-JEAN P      : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 1163.67        :
 COSTS & MILE:     38.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:      68.34        :                07054-4609
 CRT OFR COMM:    127.00        :
 GRAND TOTAL:  $ 1397.01        :
 CRT OFR: BERGEN     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 646-2245  :
                                :
                                :
 PLEASE ALLOW 30 DAYS BEFORE    :
 COURT OFFICER STATUS REQUEST   :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                            <767a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-016053-12  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   WHEELER J         : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $  815.70        :
 COSTS & MILE:     37.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:      36.16        :                07054-4609
 CRT OFR COMM:     88.89        :
 GRAND TOTAL:  $  977.75        :
 CRT OFR: LANZO M               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-7122  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                <067a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-015745-00  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   LUCIANO G         : PHILIP A KAHN
 WRIT NUMBER: 006               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2119.29        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     935.56        :
 CRT OFR COMM:    309.19        :
 GRAND TOTAL:  $ 3401.04        :
 CRT OFR: ESPOSITO A            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-5560  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                    <167a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-011581-10  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   BOLIVAR G         : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1136.32        : PARSIPPANY NJ
 COSTS & MILE:     44.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      94.89        :
 CRT OFR COMM:    127.52        :
 GRAND TOTAL:  $ 1402.73        :
 CRT OFR: SWEETWOOD J           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 227-1080  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                        <267a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-007719-19  :
 CREDITOR(S): PARABOLIC PERFORM :
 DEBTOR(S):   BOOKER M          : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 7818.40        : PARSIPPANY NJ
 COSTS & MILE:     43.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      43.05        :
 CRT OFR COMM:    790.45        :
 GRAND TOTAL:  $ 8694.90        :
 CRT OFR: CARROLL D             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 227-2848  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                            <367a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-007118-13  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   GORHAM T          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1014.66        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      76.63        :
 CRT OFR COMM:    112.83        :
 GRAND TOTAL:  $ 1241.12        :
 CRT OFR: CARROLL D             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 227-2848  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                <c77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-005622-11  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   MAZAGWU E         : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: ALIAS WAGE       : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $  979.78        :
 COSTS & MILE:     37.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:      75.98        :                07054-4609
 CRT OFR COMM:    109.28        :
 GRAND TOTAL:  $ 1202.04        :
 CRT OFR: UNION      COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                    <d77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAPE MAY SPECIAL CIVIL PART    :
 DN-203 CASE MANAGEMENT OFFICE  :
 9 NORTH MAIN STREET            :
 CAPE MAY CRT HSE NJ 08210-3096 :
 (609) 402-0100                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CPM DC-004673-10  :
 CREDITOR(S): BAXTER FINANCIAL, :
 DEBTOR(S):   KING D            : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 1622.28        :
 COSTS & MILE:     40.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     104.35        :                07054-4609
 CRT OFR COMM:    176.66        :
 GRAND TOTAL:  $ 1943.29        :
 CRT OFR: WELSH R               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 365-8793  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                        <e77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAPE MAY SPECIAL CIVIL PART    :
 DN-203 CASE MANAGEMENT OFFICE  :
 9 NORTH MAIN STREET            :
 CAPE MAY CRT HSE NJ 08210-3096 :
 (609) 402-0100                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CPM DC-004062-09  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   TOZER B           : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2826.95        : PARSIPPANY NJ
 COSTS & MILE:     43.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     237.95        :
 CRT OFR COMM:    310.79        :
 GRAND TOTAL:  $ 3418.69        :
 CRT OFR: WELSH R               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 365-8793  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                            <f77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAPE MAY SPECIAL CIVIL PART    :
 DN-203 CASE MANAGEMENT OFFICE  :
 9 NORTH MAIN STREET            :
 CAPE MAY CRT HSE NJ 08210-3096 :
 (609) 402-0100                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CPM DC-000106-08  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   DADURA W          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1193.21        : PARSIPPANY NJ
 COSTS & MILE:     38.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     133.64        :
 CRT OFR COMM:    136.49        :
 GRAND TOTAL:  $ 1501.34        :
 CRT OFR: OCEAN      COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                <877a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CAM DC-012644-08  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   HEATH A           : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2694.98        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     306.67        :
 CRT OFR COMM:    303.77        :
 GRAND TOTAL:  $ 3341.42        :
 CRT OFR: FRANKLIN J            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 566-9600  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                    <977a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: BUR DC-007019-06  :
 CREDITOR(S): JACKSON CAPITAL I :
 DEBTOR(S):   ROCKEMORE T       : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $10472.73        :
 COSTS & MILE:     36.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:    2110.51        :                07054-4609
 CRT OFR COMM:   1261.92        :
 GRAND TOTAL:  $13881.16        :
 CRT OFR: CAMDEN     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                        <a77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CAM DC-007241-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   WEBB S            : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1326.48        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     372.24        :
 CRT OFR COMM:    173.47        :
 GRAND TOTAL:  $ 1908.19        :
 CRT OFR: TODORO F              :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 707-5187  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                            <b77a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: BUR DC-012920-10  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   DIAZ E            : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 4347.79        : PARSIPPANY NJ
 COSTS & MILE:     38.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     253.36        :
 CRT OFR COMM:    463.92        :
 GRAND TOTAL:  $ 5103.07        :
 CRT OFR: GADDY L               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 234-3504  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                <477a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CAM DC-004285-06  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   STEIN H           : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1198.86        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     253.41        :
 CRT OFR COMM:    148.83        :
 GRAND TOTAL:  $ 1637.10        :
 CRT OFR: LACEY D               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 851-4126  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                    <577a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 CAMDEN SPECIAL CIVIL PART      :
 HALL OF JUSTICE  STE 150       :
 101 S FIFTH ST                 :
 CAMDEN NJ 08103-4001           :
 (856) 379-2200                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: CAM DC-001832-09  :
 CREDITOR(S): BAXTER FINANCIAL  :
 DEBTOR(S):   RODRIGUEZ F       : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 4572.13        :
 COSTS & MILE:     35.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     425.62        :                07054-4609
 CRT OFR COMM:    503.28        :
 GRAND TOTAL:  $ 5536.03        :
 CRT OFR: CANADY SR C           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 296-4812  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                        <677a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: BUR DC-004828-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   SHINN R           : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $  847.73        : PARSIPPANY NJ
 COSTS & MILE:     38.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     217.40        :
 CRT OFR COMM:    110.31        :
 GRAND TOTAL:  $ 1213.44        :
 CRT OFR: MCKERNAN J            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 222-0277  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                            <777a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: BER DC-013474-12  :
 CREDITOR(S): CACH OF NJ, LLC   :
 DEBTOR(S):   CARO D            : PHILIP A KAHN
              NJ LOFT LLC       : FEIN SUCH KAHN & SHEPARD P
 WRIT NUMBER: 002               : 7 CENTURY DR STE 201
 TYPE OF WRIT: WRIT             :
 EXPIRATION: 11/07/21           : PARSIPPANY NJ
 WRIT AMOUNT:  $ 7031.52        :
 COSTS & MILE:     36.00        :                07054-4609
 CREDITS:           0.00        :
 ADD'L COSTS:     262.31        :
 CRT OFR COMM:    732.98        :
 GRAND TOTAL:  $ 8062.81        :
 CRT OFR: SCONZO R              :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 426-0779  :
 CONTACT COURT OFFICER 30 DAYS  :
 FROM DATE OF THIS POSTCARD     :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                <077a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BERGEN SPECIAL CIVIL PART      :
 BERGEN COUNTY COURTHOUSE       :
 BERGEN COUNTY JUSTICE CENTER   :
 HACKENSACK NJ 07601-7680       :
 (201) 221-0700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: BER DC-013021-12  :
 CREDITOR(S): CACH OF NJ, LLC   :
 DEBTOR(S):   TORCHIA G         : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 3856.82        : PARSIPPANY NJ
 COSTS & MILE:     46.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     183.00        :
 CRT OFR COMM:    408.58        :
 GRAND TOTAL:  $ 4494.40        :
 CRT OFR: GUERRA M              :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 343-2112  :
                                :
 CONTACT COURT OFFICER 30 DAYS  :
 FROM DATE OF THIS POSTCARD     :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                    <177a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-005488-06  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   DEMIRBAS K        : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2088.58        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     476.61        :
 CRT OFR COMM:    260.12        :
 GRAND TOTAL:  $ 2861.31        :
 CRT OFR: COVE N                :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 854-6146  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                        <277a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: UNN DC-021140-08  :
 CREDITOR(S): DCFS TRUST        :
 DEBTOR(S):   JOHNSON C         : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 8020.68        :
 COSTS & MILE:     39.00        : PARSIPPANY NJ
 CREDITS:        2707.22        :
 ADD'L COSTS:     350.52        :                07054-4609
 CRT OFR COMM:    570.30        :
 GRAND TOTAL:  $ 6273.28        :
 CRT OFR: GENABITH R            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (908) 756-6265  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                            <377a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 UNION SPECIAL CIVIL PART       :
 SPECIAL CIVIL PART             :
 2 BROAD STREET                 :
 ELIZABETH NJ 07207             :
 (908) 787-1650                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: UNN DC-012874-17  :
 CREDITOR(S): FAIRLEIGH DICKINS :
 DEBTOR(S):   ANTHONY M         : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 8199.80        :
 COSTS & MILE:     36.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     182.38        :                07054-4609
 CRT OFR COMM:    841.82        :
 GRAND TOTAL:  $ 9260.00        :
 CRT OFR: HUDSON     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                <c47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 SUSSEX SPECIAL CIVIL PART      :
 SUSSEX COUNTY JUDICIAL CENTER  :
 43-47 HIGH ST PLAZA LEVEL      :
 NEWTON NJ 07860-1738           :
 (862) 397-5700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: SSX DC-001741-09  :
 CREDITOR(S): FIRST RESOLUTION  :
 DEBTOR(S):   GERALD J          : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 5015.26        : PARSIPPANY NJ
 COSTS & MILE:     45.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     419.59        :
 CRT OFR COMM:    547.99        :
 GRAND TOTAL:  $ 6027.84        :
 CRT OFR: SCRIVANI M            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 948-3169  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                    <d47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-024372-08  :
 CREDITOR(S): BAXTER FINANCIAL  :
 DEBTOR(S):   PEREYRA A         : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 1037.84        :
 COSTS & MILE:     41.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     129.44        :                07054-4609
 CRT OFR COMM:    120.83        :
 GRAND TOTAL:  $ 1329.11        :
 CRT OFR: BERGEN     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                        <e47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-018120-08  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   BURNETT L         : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1721.75        : PARSIPPANY NJ
 COSTS & MILE:     39.00        :
 CREDITS:         114.61        :                07054-4609
 ADD'L COSTS:      80.95        :
 CRT OFR COMM:    172.71        :
 GRAND TOTAL:  $ 1899.80        :
 CRT OFR: HUDSON     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                            <f47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-015503-05  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   SABATO R          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1366.99        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     301.45        :
 CRT OFR COMM:    170.44        :
 GRAND TOTAL:  $ 1874.88        :
 CRT OFR: VELAZQUEZ J           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 696-0609  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                <847a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-010423-09  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   DUYGU M           : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1081.88        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      87.18        :
 CRT OFR COMM:    120.51        :
 GRAND TOTAL:  $ 1325.57        :
 CRT OFR: VELAZQUEZ J           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 696-0609  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                    <947a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-013488-09  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   SOLANO R          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1182.14        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      91.38        :
 CRT OFR COMM:    130.95        :
 GRAND TOTAL:  $ 1440.47        :
 CRT OFR: COVE N                :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 854-6146  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                        <a47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-010295-09  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   JADALHACK T       : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 7337.36        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     606.11        :
 CRT OFR COMM:    797.95        :
 GRAND TOTAL:  $ 8777.42        :
 CRT OFR: COVE N                :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 854-6146  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                            <b47a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-009877-13  :
 CREDITOR(S): PRECISION PAIN MA :
 DEBTOR(S):   RIOS M            : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 4309.22        :
 COSTS & MILE:     40.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     170.85        :                07054-4609
 CRT OFR COMM:    452.01        :
 GRAND TOTAL:  $ 4972.08        :
 CRT OFR: HUDSON     COUNTY     :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (000) 000-0000  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                <447a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: OCN DC-011027-10  :
 CREDITOR(S): ARROW FINANCIAL S :
 DEBTOR(S):   FRANCES M         :  PHILIP KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 2379.15        :
 COSTS & MILE:      9.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     205.70        :                07054-4609
 CRT OFR COMM:    259.39        :
 GRAND TOTAL:  $ 2853.24        :
 CRT OFR: MCKERNAN J            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 222-0277  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                    <547a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 PASSAIC SPECIAL CIVIL PART     :
 77 HAMILTON STREET             :
 PATERSON NJ 07505              :
                                :
 (973) 653-2910                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: PAS DC-002959-12  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   ROBINSON M        : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $  662.05        : PARSIPPANY NJ
 COSTS & MILE:     43.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      24.02        :
 CRT OFR COMM:     72.91        :
 GRAND TOTAL:  $  801.98        :
 CRT OFR: TRIONFO R             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 838-5031  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                        <647a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MRS DC-003254-04  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   ENTIAN M          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 8960.33        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:    2108.17        :
 CRT OFR COMM:   1110.45        :
 GRAND TOTAL:  $12214.95        :
 CRT OFR: ROMANO Z              :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 460-4548  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                            <747a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-015708-12  :
 CREDITOR(S): SECURITY CREDIT S :
 DEBTOR(S):   FILS P            : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1320.84        : PARSIPPANY NJ
 COSTS & MILE:     45.00        :
 CREDITS:        1133.25        :                07054-4609
 ADD'L COSTS:     139.48        :
 CRT OFR COMM:     37.21        :
 GRAND TOTAL:  $  409.28        :
 CRT OFR: HERBERT G             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 449-8391  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                <047a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-010741-04  :
 CREDITOR(S): CACV OF COLORADO  :
 DEBTOR(S):   HOPKINS C         : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 4021.08        : PARSIPPANY NJ
 COSTS & MILE:     38.00        :
 CREDITS:           9.00        :                07054-4609
 ADD'L COSTS:     894.14        :
 CRT OFR COMM:    494.42        :
 GRAND TOTAL:  $ 5438.64        :
 CRT OFR: MICHALS W             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 922-3323  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                    <147a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-025224-09  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   RIVERA A          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $  864.02        : PARSIPPANY NJ
 COSTS & MILE:     40.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      78.01        :
 CRT OFR COMM:     98.20        :
 GRAND TOTAL:  $ 1080.23        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                        <247a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-002666-11  :
 CREDITOR(S): BAXTER FINANCIAL, :
 DEBTOR(S):   ROMMELL M         : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: ALIAS WAGE       : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $  640.46        :
 COSTS & MILE:     40.00        : PARSIPPANY NJ
 CREDITS:          47.73        :
 ADD'L COSTS:     169.51        :                07054-4609
 CRT OFR COMM:     80.22        :
 GRAND TOTAL:  $  882.46        :
 CRT OFR: BAKER R               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 531-2744  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                            <347a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-009748-01  :
 CREDITOR(S): SEARS ROEBUCK C   :
 DEBTOR(S):   GUNTER J          : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1326.20        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:         149.63        :                07054-4609
 ADD'L COSTS:     485.19        :
 CRT OFR COMM:    169.78        :
 GRAND TOTAL:  $ 1867.54        :
 CRT OFR: MICHALS W             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 922-3323  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                <c57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MONMOUTH SPECIAL CIVIL PART    :
 71 MONUMENT PARK               :
 PO BOX 1270                    :
 FREEHOLD NJ 07728              :
 (732) 358-8700                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MON DC-006040-11  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   ISAAC H           : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2046.72        : PARSIPPANY NJ
 COSTS & MILE:     46.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     108.83        :
 CRT OFR COMM:    220.16        :
 GRAND TOTAL:  $ 2421.71        :
 CRT OFR: HERBERT G             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 449-8391  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                    <d57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MERCER SPECIAL CIVIL PART      :
 175 SOUTH BROAD STREET  1ST FL :
 PO BOX 8068                    :
 TRENTON NJ 08650-0068          :
 (609) 571-4200                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: MER DC-007674-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   SANDHU D          : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 8715.67        : PARSIPPANY NJ
 COSTS & MILE:     39.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:    2175.13        :
 CRT OFR COMM:   1092.98        :
 GRAND TOTAL:  $12022.78        :
 CRT OFR: GAFFIGAN J            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (732) 450-8871  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                        <e57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-021422-05  :
 CREDITOR(S): CREDIGY RECEIVABL :
 DEBTOR(S):   LOPEZ D           : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2079.52        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     436.94        :
 CRT OFR COMM:    255.25        :
 GRAND TOTAL:  $ 2807.71        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                            <f57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-020935-10  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   VELAZQUEZ R       : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 3969.87        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     183.34        :
 CRT OFR COMM:    418.92        :
 GRAND TOTAL:  $ 4608.13        :
 CRT OFR: CASTELLUCCI D         :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 339-4273  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                <857a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-017503-09  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   ABU-ATIE M        : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1090.04        : PARSIPPANY NJ
 COSTS & MILE:     40.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     124.37        :
 CRT OFR COMM:    125.44        :
 GRAND TOTAL:  $ 1379.85        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                    <957a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-015719-11  :
 CREDITOR(S): LVNV FUNDING, LLC :
 DEBTOR(S):   KONG D            : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2435.70        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      77.52        :
 CRT OFR COMM:    254.92        :
 GRAND TOTAL:  $ 2804.14        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                        <a57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-015444-02  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   KELLY K           : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $10336.50        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:    2712.06        :
 CRT OFR COMM:   1308.46        :
 GRAND TOTAL:  $14393.02        :
 CRT OFR: CASTELLUCCI D         :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 339-4273  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                            <b57a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-012822-12  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   HORAN T           : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $  811.17        : PARSIPPANY NJ
 COSTS & MILE:     40.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      43.05        :
 CRT OFR COMM:     89.42        :
 GRAND TOTAL:  $  983.64        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                <457a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-009571-12  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   SAROFIEM E        : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 8242.95        : PARSIPPANY NJ
 COSTS & MILE:     41.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     292.74        :
 CRT OFR COMM:    857.67        :
 GRAND TOTAL:  $ 9434.36        :
 CRT OFR: SOTO RIOS L           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 271-9503  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                    <557a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-006998-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   ROBINSON P        : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2609.18        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     695.22        :
 CRT OFR COMM:    334.04        :
 GRAND TOTAL:  $ 3674.44        :
 CRT OFR: JOHNSON T             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 303-0531  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                        <657a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-017031-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   OKOYE O           : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 3150.55        : PARSIPPANY NJ
 COSTS & MILE:     44.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     805.87        :
 CRT OFR COMM:    400.04        :
 GRAND TOTAL:  $ 4400.46        :
 CRT OFR: ESPOSITO A            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-5560  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                            <757a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-006991-04  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   DALAIKA S         : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2402.38        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     560.46        :
 CRT OFR COMM:    299.88        :
 GRAND TOTAL:  $ 3298.72        :
 CRT OFR: JOHNSON T             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 303-0531  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                                <057a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-003327-13  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   CAMEJO H          : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WRIT     : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 5531.81        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     289.92        :
 CRT OFR COMM:    585.77        :
 GRAND TOTAL:  $ 6443.50        :
 CRT OFR: CASTELLUCCI D         :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 339-4273  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                                    <157a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-002211-10  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   QUIRUMBAY J       : PHILIP A KAHN
 WRIT NUMBER: 004               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $  972.73        : PARSIPPANY NJ
 COSTS & MILE:     36.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     142.87        :
 CRT OFR COMM:    115.16        :
 GRAND TOTAL:  $ 1266.76        :
 CRT OFR: AJAMIAN E             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 437-5549  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                                        <257a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 HUDSON SPECIAL CIVIL PART      :
 595 NEWARK AVE                 :
 JERSEY CITY NJ 07306           :
                                :
 (201) 748-4400                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: HUD DC-002148-18  :
 CREDITOR(S): RUTGERS, THE STAT :
 DEBTOR(S):   CABRERA D         : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 4825.10        : PARSIPPANY NJ
 COSTS & MILE:      0.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      27.17        :
 CRT OFR COMM:    485.23        :
 GRAND TOTAL:  $ 5337.50        :
 CRT OFR: CASTELLUCCI D         :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (201) 339-4273  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                                            <357a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 BURLINGTON SPECIAL CIVIL PART  :
 49 RANCOCAS ROAD               :
 COUNTY OFFICE BUILDING         :
 MT HOLLY NJ 08060              :
 (609) 288-9500                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: GLO DC-005413-04  :
 CREDITOR(S): MERCHANTS COMMERC :
 DEBTOR(S):   SCARPINATO J      : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WAGE             : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 3511.32        :
 COSTS & MILE:      5.00        : PARSIPPANY NJ
 CREDITS:         600.00        :
 ADD'L COSTS:     731.26        :                07054-4609
 CRT OFR COMM:    364.76        :
 GRAND TOTAL:  $ 4012.34        :
 CRT OFR: JENKINS K             :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (609) 405-7657  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: 
                                                                                                                                                                                                                                                                                                                                                                                                                <c27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 GLOUCESTER SPECIAL CIVIL PART  :
 GLOUCESTER COUNTY SPECIAL CIVI :
 1 NORTH BROAD ST               :
 WOODBURY NJ 08096              :
 (856) 878-5050                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: GLO DC-003773-17  :
 CREDITOR(S): LVNV FUNDING LLC  :
 DEBTOR(S):   BABAJIDE O        : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 1115.48        : PARSIPPANY NJ
 COSTS & MILE:     39.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      21.29        :
 CRT OFR COMM:    117.58        :
 GRAND TOTAL:  $ 1293.35        :
 CRT OFR: MCILVAINE C           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (856) 875-7500  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<d27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-030235-03  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   PANCHAMSINGH V    : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 5613.31        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:    1411.30        :
 CRT OFR COMM:    706.16        :
 GRAND TOTAL:  $ 7767.77        :
 CRT OFR: ESPOSITO A            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-5560  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<e27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-027155-05  :
 CREDITOR(S): CREDIGY RECEIVABL :
 DEBTOR(S):   SAXTON C          : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2285.54        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     492.38        :
 CRT OFR COMM:    281.49        :
 GRAND TOTAL:  $ 3096.41        :
 CRT OFR: LANZO M               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-7122  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<f27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-025886-04  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   LOPEZ C           : PHILIP A KAHN
 WRIT NUMBER: 003               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 3520.04        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     801.48        :
 CRT OFR COMM:    435.85        :
 GRAND TOTAL:  $ 4794.37        :
 CRT OFR: SWEETWOOD J           :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 227-1080  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<827a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-025567-04  :
 CREDITOR(S): GREAT SENECA FINA :
 DEBTOR(S):   MARTINEZ J        : PHILIP A KAHN
 WRIT NUMBER: 002               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 2850.04        : PARSIPPANY NJ
 COSTS & MILE:     37.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:     639.37        :
 CRT OFR COMM:    352.64        :
 GRAND TOTAL:  $ 3879.05        :
 CRT OFR: LANZO M               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-7122  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<927a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-020550-17  :
 CREDITOR(S): RUTGERS, THE STAT :
 DEBTOR(S):   UDECHUKWU M       : PHILIP A KAHN
 WRIT NUMBER: 001               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: WRIT             : 7 CENTURY DR STE 201
 EXPIRATION: 11/07/21           :
 WRIT AMOUNT:  $ 8646.09        : PARSIPPANY NJ
 COSTS & MILE:      0.00        :
 CREDITS:           0.00        :                07054-4609
 ADD'L COSTS:      99.84        :
 CRT OFR COMM:    874.59        :
 GRAND TOTAL:  $ 9620.52        :
 CRT OFR: LANZO M               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-7122  :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<a27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-017829-07  :
 CREDITOR(S): PINNACLE CREDIT S :
 DEBTOR(S):   WHITE L           : PHILIP A KAHN
 WRIT NUMBER: 005               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: ALIAS WAGE       : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $ 1020.19        :
 COSTS & MILE:     37.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     219.41        :                07054-4609
 CRT OFR COMM:    127.66        :
 GRAND TOTAL:  $ 1404.26        :
 CRT OFR: LANZO M               :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-7122  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<b27a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 ESSEX SPECIAL CIVIL PART       :
 HALL OF RECORDS - ESSEX COUNTY :
 465 MARTIN LUTHER KING BLVD    :
 NEWARK NJ 07102                :
 (973) 776-9300                 :
                        CV0288  : NOVEMBER 07, 2019
 CASE NUMBER: ESX DC-016329-11  :
 CREDITOR(S): NEW CENTURY FINAN :
 DEBTOR(S):   EALEY K           : PHILIP A KAHN
 WRIT NUMBER: 006               : FEIN SUCH KAHN & SHEPARD P
 TYPE OF WRIT: PLURIES WAGE     : 7 CENTURY DR STE 201
 WRIT AMOUNT:  $  612.75        :
 COSTS & MILE:     44.00        : PARSIPPANY NJ
 CREDITS:           0.00        :
 ADD'L COSTS:     103.87        :                07054-4609
 CRT OFR COMM:     76.06        :
 GRAND TOTAL:  $  836.68        :
 CRT OFR: ESPOSITO A            :
 ASSIGN DATE: 11/07/2019        :
 CRT OFR PHONE: (973) 228-5560  :
                                :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<427a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
                                                                                                                        
 SUP CLERK SUPERIOR COURT                                                                                               
 PO BOX 971                                                                                                             
 TRENTON NJ  08625                                                                                                      
                                                                                                                        
                                                                                                                        
 TELEPHONE: (609) 292-0151                                   NOVEMBER 07, 2019                                          
            8:30 AM - 4:30 PM                                                                                           
 CV0105                        DOCKET: SWC - F -018218-19                                                               
                               VANGUARD FUNDING LLC VS COLETTI WENDY                                                    
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
      THE DOCKET NUMBER SHOWN ABOVE HAS BEEN ASSIGNED TO YOUR CASE, WHICH WAS FILED                                     
                                                                                                                        
      ON NOVEMBER 07, 2019.  PLEASE BE SURE TO INCLUDE IT ON ALL DOCUMENTS SUBMITTED TO THE COURT.                      
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  VINCENT,  J DI MAIOLO                                 
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054-4609                              

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<527a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -020029-18                                                  
                                            JPMORGAN CHASE BANK VS KENNEDY BENJAMIN D                                   
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  ROBERT E SMITHSON                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<627a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -019970-18                                                  
                                            FLAGSTAR BANK, FSB VS CELESTIN PIERRE                                       
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  DOLORES M DE ALMEIDA                                  
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<727a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -002326-19                                                  
                                            U.S. BANK TRUST, N.A VS DAVILA OLIVERA GREGORIO                             
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  NICHOLAS J CANOVA                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<027a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -019790-18                                                  
                                            PNC BANK, NATIONAL A VS MADJAROV NIKOLAY I                                  
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  ROBERT E SMITHSON                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<127a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -005330-19                                                  
                                            NEWREZ D/B/A SHELLPOINT VS TUPKIELEWICZ MICHAEL J                           
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  DOLORES M DE ALMEIDA                                  
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<227a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -005330-19                                                  
                                            NEWREZ D/B/A SHELLPOINT VS TUPKIELEWICZ MICHAEL J                           
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  DOLORES M DE ALMEIDA                                  
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<327a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -033783-13                                                  
                                            WELLS FARGO BANK  N VS LUTNER NANCY                                         
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  NICHOLAS CANOVA                                       
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<c37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -032777-14                                                  
                                            FEDERAL NATIONAL MOR VS OKAFOR REGINA                                       
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  MEHMET BASOGLU                                        
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<d37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -025401-17                                                  
                                            PNC BANK, NATIONAL A VS MURPHY MICHAEL N                                    
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  NICHOLAS CANOVA                                       
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<e37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -025096-18                                                  
                                            WILMINGTON SAVINGS FUND VS KEAR JACQUELINE                                  
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  SIMONE A SEBASTIAN                                    
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<f37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -020029-18                                                  
                                            JPMORGAN CHASE BANK VS KENNEDY BENJAMIN D                                   
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  ROBERT E SMITHSON                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<837a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -019970-18                                                  
                                            FLAGSTAR BANK, FSB VS CELESTIN PIERRE                                       
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  DOLORES M DE ALMEIDA                                  
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<937a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -019790-18                                                  
                                            PNC BANK, NATIONAL A VS MADJAROV NIKOLAY I                                  
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  ROBERT E SMITHSON                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<a37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/07/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -002326-19                                                  
                                            U.S. BANK TRUST, N.A VS DAVILA OLIVERA GREGORIO                             
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  NICHOLAS J CANOVA                                     
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054                                   
                                                                                                                        

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<b37a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


                                                                                                                        
                                                                                                                        
 CAPE MAY SUPERIOR COURT                                                                                                
 DN-209A                                                                                                                
 9 NORTH MAIN STREET                                                                                                    
 CAPE MAY CRT HSE NJ  08210-3096                                                                                        
                                                                                                                        
 TELEPHONE: (609) 402-0100                                   NOVEMBER 07, 2019                                          
            8:30 AM - 4:30 PM                                                                                           
 CV0155                        DOCKET: SWC - F -048862-08                                                               
                               U.S. BANK NA VS WARE SANDRA                                                              
                                                                                                                        
      A PLENARY HEARING IS SCHEDULED FOR THIS CASE                                                                      
      ON NOVEMBER 20, 2019 AT 10:00AM  BEFORE JUDGE MICHAEL J BLEE.                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
  PLEASE REPORT TO:  COURT ROOM A                                                                                       
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  NICHOLAS  J CANOVA                                    
                                                                  FEIN SUCH KAHN & SHEPARD PC                           
                                                                  7 CENTURY DR STE 201                                  
                                                                  PARSIPPANY NJ 07054-4609                              

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<437a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
 CASE NUMBER:
   MRS LT-002318-19             :
 SKELETOR LLC                   :
   VS                   CV0220  : NOVEMBER 07, 2019
 BROWN DAWN                     :
                                :
 A SUMMONS WAS ISSUED 11-07-19  :
 AND A NON-JURY TRIAL HAS BEEN  : MARIAN MIAWAD
 SCHEDULED FOR THIS CASE        : FEIN SUCH KAHN & SHEPARD P
 ON 12-06-2019  AT 08:45AM      : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE N-A                      :
                                :               07054-4609
 COURTHOUSE-WASHINGTON ST       :
 JURY ASSEMBLY ROOM             :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: undefined<537a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org>


 MORRIS SPECIAL CIVIL PART      :
 ADMINISTRATION RECORDS BLDG    :
 PO BOX 910                     :
 MORRISTOWN NJ 07963-0910       :
 (862) 397-5700                 :
 CASE NUMBER:
   MRS LT-002317-19             :
 SKELETOR LLC                   :
   VS                   CV0220  : NOVEMBER 07, 2019
 MARTINELLI JOSEPH              :
                                :
 A SUMMONS WAS ISSUED 11-07-19  :
 AND A NON-JURY TRIAL HAS BEEN  : MARIAN MIAWAD
 SCHEDULED FOR THIS CASE        : FEIN SUCH KAHN & SHEPARD P
 ON 12-06-2019  AT 08:45AM      : 7 CENTURY DR STE 201
                                :
 PLEASE REPORT TO:              : PARSIPPANY NJ
 JUDGE N-A                      :
                                :               07054-4609
 COURTHOUSE-WASHINGTON ST       :
 JURY ASSEMBLY ROOM             :
                                :
                                :
                                :

--MIMEBoundary_7b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476--

```