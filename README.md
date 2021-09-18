
# Search Notices Client (20190206)

Version 1.1, C#

Application integrates with the New Jersey eCourts system [https://www.njcourts.gov/attorneys/ecourts.html] to Search the results for a filed complaint.

Application - civilfiling_searchNotices downloads the Notices provided by Jefis (New Jersey Courts)

> SearchNotices:  This will be used to search notices. It accepts firmId and NoticeDate as inputs. On successful submission, Array of Notices will be the output. Any application errors or validation failures will be captured in the Response objects Message List.

Inside Scoop: After many years of posting these files to a site for downloading eCourts put the ownership on users of eCourts
to download the notices themselves. This was a quick weekend project for us.

Registration for eCourts: [https://portal.njcourts.gov/webe19/ecourtsweb/pages/home/home.faces]

See Also [https://github.com/craignicholson/civilfiling]

Hope this helps someone!

## FYI

This individual webservice does not work well with C#. I could not find a 'free' way to decode xop. I had to hack my way around this java feature to get the app shipped. [https://docs.oracle.com/cd/E12839_01/web.1111/e13734/mtom.htm#WSADV130]

```xml
<bytes>
  <xop:Include href="cid:1b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476@apache.org" xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
</bytes>
```

## Automation

If you want to use the app manually and automated you will have to use two seperate instances of the application. Or you can manually change the config value each time you want to use manually or automated. Changing the config value from readFrom to FORM or AUTO if you do run automation you will likely forget to change it back to the previous setting (IMHO).

```xml
<!-- ReadFrom values {AUTO, FORM} AUTO uses current date and daysBack, FORM is manually done using the app.-->
<add key="readFrom" value="FORM"/>
```

### Edit civilFiling_searchNotices.exe.config

- Open civilFiling_searchNotices.exe.config in a text editor like Notepad.
- Change "mode" value="AUTO".
- Save file and close or exit the text editor (Notepad)
- Leave daysBack value set to 1.

### Windows Task Scheduler

Setting up Windows Task Scheduler to run the application saves you a daily step. However, automation requires those involved to also check in weekly on if the files are being created without error. Monitoring of the task is required to be successful.

#### Step 1

- Action > Create Basic Task
  - Name: SearhNoticesTask
  - Description: Run the application Tue through Sat to get files for Mon through Fri.  Application located at C:\Jefis\Tools\SearchNotices\.  Data will be written to F:\Jefis\DownloadedFiles\
- Click Next
  - Select the Weekly radio button
  - Select your Start Date and Time (Tommorrow, and the time you want the application to run, I suggest later in the day, but see above notes about asking JEFIS when the total data is available)
  - Recur every 1 weeks on Tue, Wed, Thu, Fri, Sat
- ACTION
  - Start at program
  - Program script: C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe
- Next
- Finish

#### Step 2

Edit the Task and change the Security Options to:

- Run whether the user is logged on or not (You will need to have the user enter their pwd)

### Batch File Example

You can create a batch file to run multiple days at once or copy and paste values into the window Command Prompt.

Maybe the taskscheduler was off or disabled or the F:\ was down for a week so you need to run multiple days at once and want to avoid using the user interface in the application to get this done quickly.

Command Prompt

```bash

C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201201

```

Batch File (searchNotices_202012.bat)

```bat

C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201201
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201202
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201203
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201204

```

Powershell (searchNotices_202012.ps1)

```ps1

C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201201
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201202
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201203
C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe F00000000 20201204

```

## Testing Setup and Data

### Credentials

Make sure the testUsername and testPwd are setup with these credentials.

```xml
<add key="testUsername" value="888888029"/>
<add key="testPwd" value="P@ssword"/>
```

### Test Data

| FirmID      | Date        |
| ----------- | ----------- |
| F00000147   | 20191106    |
| F00000342   | 20191106    |
| F00000495   | 20191106    |

To test each one of these you will need to update the App.Config firmId and then run the app and pick the correct date.

```xml
<add key="firmId" value="F00000147"/>
```

## Application Setup - Production

The following configs in civilfiling_searchNotices.exe.config need to be set:

1. Download Setup.msi and put on network at Morgan
2. Put Setup.msi on Muriel computer
3. Double Click Setup.msi
4. Accept all warnings
5. All should install at C:\Jefis\Tools\SearchNotices\
6. Edit the civilfiling_searchNotices.exe.config
7. Change "importFileLocation" to value="F:\Jefis\DownloadedFiles\" (not sure if this is correct)
8. Make sure readFrom is set to FORM
9. Double click civilfiling_searchNotices.exe to Run the application
10. Choose a date, you can only send a previous date to Jefis, current date and future dates will not return data.
11. Click send
12. If you receive a Success Message review the F: drive for the file.
13. Any errors send me the log file from C:\Jefis\Tools\SearchNotices\logs
14. You can also copy the text in the App's window as well. All text in the window is written to the log.

I'm unsure when the data from Jefis is avaiable. If you run the application in the early AM or the late PM if more data is avaialble.  Only Jefis can answer when the complete dataset is available for the day.  

FYI - If you run the app before all the data is available and don't run the app again to collect the full dataset you may miss data  

Someone needs to ask JEFIS when the full dataset is available.

- request.txt should not be changed.  edits to this file will change the soap request.
- responseFileLocationFileName needs to be added or the file path made sure it is a valid path to the file. The application will try and write this file and always read from this file.
- importFileLocation is the folder where the final import files (Post Card Specification) will be written with extension of '.txt'.

Additional Items
You can right click the .exe and make a shortcut for the desktop

You can setup Task Scheduler to run the following days to produce a file with daysBack set to 1:

- Setting the readFrom to AUTO and setup in Task Scheduler
- Ok ... I'm tired of documenting this weekend project

### Launch Windows Task Scheduler

Action > Create Basic Task
    Name: SearhNoticesTask
    Description: Run the application Tue through Sat to get files for Mon through Fri.  Application located at C:\Jefis\Tools\SearchNotices\.  Data will be written to F:\Jefis\DownloadedFiles\

Click Next
Select the Weekly radio button
Select your Start Date and Time (Tommorrow, and the time you want the application to run, I suggest later in the day, but see above notes about asking JEFIS when the total data is available)
Recur every 1 weeks on
    Tue, Wed, Thu, Fri, Sat
ACTION
    Start at program
    program script: C:\Jefis\Tools\SearchNotices\civilfiling_searchNotices.exe
Next
Finish

Edit the Task and change the Security Options to:
    Run whether the user is logged on or not (You will need to have the user enter their pwd)
Multiple Dates

## Data Availability

Jefis can answer when the complete dataset is available for the day and if their web service returns partial data.  Currently it is expected all notices for a day are available for the previous day at once instead of a slow adding of data during the day.  Example, 10 notices are available at 6AM and then 16 notices available by 3PM.

## How does it work?

### Step 1 - Request

App sends a raw HTTP POST of this request body to the web service. All of the @variables are replaced with value from the .config file.

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
    <s:Header>
        <Security xmlns="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
            <wsse:UsernameToken Id="feinsuch" xmlns:wsse="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
                <wsse:Username>@username</wsse:Username>
                <wsse:Password Type="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText">@password</wsse:Password>
            </wsse:UsernameToken>
        </Security>
    </s:Header>
    <s:Body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <searchNotices xmlns="http://webservice.civilfiling.ecourts.ito.aoc.nj/">
            <arg0 xmlns=""><firmId>@firmId</firmId><noticeDate>@noticeDate</noticeDate></arg0>
        </searchNotices>
    </s:Body>
</s:Envelope>
```

### Step 2 - Response

The response is then written to a folder as reponse.xml and then converted to the Post Card File Import specificaion.

Data Returned for each notice

- firmId
- date
- noticeDesc
- noticeType
- dockerNumber -> docketSeqNum
- dockerNumber -> docketCourtYear
- dockerNumber -> docketDocketTypeCode
- dockerNumber -> docketVenue
- some weird formatted text left over from the Post Card format

Example (Truncated for readability) TODO: CHANGE THE FACTS THIS IS A REAL NOTICE OR USE THE EXAMPLES FROM TESTING

```html
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

```

As you can tell the data is married up by the content id (cid): 1b7a92ab08d1da79ec545140049c6e387d2fc9b83f332476. I just did not solve the parsing in C# for MTOM/XOP fast enough to get this shipped. :-(  All the tooling failed me.

### Step 3 - Post Card File Format Transformation

Application parses the response.xml file to create a similar file used by other applications. The Post Card format was the previous standard used by other applications before eCourts changed up the delivery of the data.

### Step 4 - Totally Different Software Application

Users import this file into their own system. Out of this apps scope.

## Example Form results in textbox

### No Records found

```cmd
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
```

### Success Message

```cmd
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
```

## App.Config

The following configs in civilfiling_searchNotices.exe.config need to be set:

- request.txt should not be changed.  Edits to this file will change the soap request.
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
    <!-- responseFileLocationFileName is a temporary file overwritten on each request containing the raw soap response. Response errors can be found in this file!-->
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

## Logs

Information for each request to Jefis is logged and we have one log file per day.  The log files are saved for 365 days before they are removed. Logs are saved sequentially with a number appended to the end. You can check the 'Date Modified' windows to see which date the log file was updated which corresponds to the day the application was used or ran with the Windows Task Scheduler.

Example from log file: C:\Jefis\Tools\SearchNotices\logs\log.txt

```txt

Info | 2020-02-05 17:14:41.5643 | *************************** New Application Instance *************************** |
Info | 2020-02-05 17:14:41.6604 | Mode is set to 'Production |
Info | 2020-02-05 17:14:41.6704 | Command Line Args:C:\Users\craig\source\repos\civilfiling_searchnotices\civilfiling_searchNotices\bin\Debug\civilfiling_searchNotices.exe |
Info | 2020-02-05 17:14:46.6660 | *************************** Run Web Request *************************** |
Info | 2020-02-05 17:14:46.6710 | Start Search Notices |
Info | 2020-02-05 17:14:46.6710 | CurrentEndPoint: https://dpprod.njcourts.gov:2045/civilFilingWS_p |
Info | 2020-02-05 17:14:46.6710 | CurrentUsername: F00012033 |
Info | 2020-02-05 17:14:46.6860 | firmID: F00012033 |
Info | 2020-02-05 17:14:46.6860 | noticeDate: 20200203 |
Info | 2020-02-05 17:14:46.6860 | Running in: FORM |
Info | 2020-02-05 17:14:46.7470 | Sending web request... |
Info | 2020-02-05 17:14:47.2991 | Writing Raw Response File... |
Info | 2020-02-05 17:14:47.3111 | Response File Written to disk: C:\Jefis\Tools\SearchNotices\response.xml |
Info | 2020-02-05 17:14:47.3111 | Message Code found. |
Info | 2020-02-05 17:14:47.3111 | Message Code: <code>ECCV600</code><description>Success Message</description> |
Info | 2020-02-05 17:14:47.3301 | Review response.xml for more details: C:\Jefis\Tools\SearchNotices\response.xml |
Info | 2020-02-05 17:14:47.3301 | Attempt to Write Import to File |
Info | 2020-02-05 17:14:47.3511 | Write to Import File Complete: 436 Lines, C:\Jefis\Tools\SearchNotices\responses\F00012033_20200203.txt |
Info | 2020-02-05 17:14:47.3631 | End Search Notices Request. Thanks! |
Info | 2020-02-05 17:14:47.3631 | Run -> ElapsedMilliseconds: 697 |
Info | 2020-02-05 17:15:42.3017 | *************************** New Application Instance *************************** |
Info | 2020-02-05 17:15:42.3657 | Mode is set to 'Production |
Info | 2020-02-05 17:15:42.3657 | Command Line Args:civilfiling_searchNotices.exe |
Info | 2020-02-05 17:15:42.3657 | Command Line Args:F00012033, |
Info | 2020-02-05 17:15:42.3867 | Command Line Args:20190915 |
Info | 2020-02-05 17:15:42.3867 | Command Line Run |
Info | 2020-02-05 17:15:42.4037 | *************************** Run Web Request *************************** |
Info | 2020-02-05 17:15:42.4037 | Start Search Notices |
Info | 2020-02-05 17:15:42.4157 | CurrentEndPoint: https://dpprod.njcourts.gov:2045/civilFilingWS_p |
Info | 2020-02-05 17:15:42.4157 | CurrentUsername: F00012033 |
Info | 2020-02-05 17:15:42.4157 | firmID: F00012033, |
Info | 2020-02-05 17:15:42.4337 | noticeDate: 20190915 |
Info | 2020-02-05 17:15:42.4337 | Running in: AUTO |
Info | 2020-02-05 17:15:42.4867 | Sending web request... |
Info | 2020-02-05 17:15:42.7768 | Writing Raw Response File...
Info | 2020-02-05 17:15:42.7898 | Response File Written to disk: C:\Jefis\Tools\SearchNotices\response.xml 
Info | 2020-02-05 17:15:42.7898 | Message Code found. 
Info | 2020-02-05 17:15:42.8038 | Message Code: <code>ECCV220</code><description>Error Occurred, Pelase contact HelpDesk</description> 
Info | 2020-02-05 17:15:42.8038 | Review response.xml for more details: C:\Jefis\Tools\SearchNotices\response.xml 
Info | 2020-02-05 17:15:42.8198 | Attempt to Write Import to File |
Info | 2020-02-05 17:15:42.8198 | No records found. No final import file will be written to disk. |
Info | 2020-02-05 17:15:42.8198 | Run -> ElapsedMilliseconds: 424 |
Info | 2020-02-05 17:15:42.8408 | Run: AUTO Run Environment.Exit(0) |

```

## Error Messages

Messages encountered while testing.  Note if you are getting errors check the response.xml file it will containt the error message.

```html
<code>ECCV230</code>
<description>No Records found</description>

<code>ECCV600</code>
<description>Success Message</description>

<code>ECCV700</code>
<description>Search Notices feature is not available.</description>
```

ECCV120	Specific validation message stating what is missing
ECCV130	Specific validation message stating what is incorrect
ECCV220	Error Occurred, Pelase contact HelpDesk
ECCV230	No Records found
ECCV700	Search Notices feature is not available
ECCV600	Success Message

If you receive the "Rejected by policy" and did not request a password reset please contact Jefis to have the password reset and then update the "productionPassword" value in civilFiling_searchNotices.exe.config.

Service Not Availble - Contact Jefis, this means their web service is down and not working.

## Soap Test Examples (Working)

You can load this up in your favorite web tool like PostMan and send the request to [https://dptng.njcourts.gov:2045/civilFilingWS_t]

## Request

```html
Headers for web request (POST)
Content-Type: text/xml;
```

```xml

<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
    <s:Header>
        <Security xmlns="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
            <wsse:UsernameToken Id="testId" xmlns:wsse="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
                <wsse:Username>888888029</wsse:Username>
                <wsse:Password Type="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText">P@ssword</wsse:Password>
            </wsse:UsernameToken>
        </Security>
    </s:Header>
    <s:Body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <searchNotices xmlns="http://webservice.civilfiling.ecourts.ito.aoc.nj/">
            <arg0 xmlns=""><firmId>F00000342</firmId><noticeDate>20191106</noticeDate></arg0>
        </searchNotices>
    </s:Body>
</s:Envelope>

```

### Response

```html

--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250
Content-Type: text/xml; charset=UTF-8
Content-Transfer-Encoding: binary
Content-ID:<0.22ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org>

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
                            <xop:Include href="cid:52ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org" 
                                xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>12900</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000342</firmId>
                        <noticeDate>20191106</noticeDate>
                        <noticeDesc>JUDGMENT NOTICE</noticeDesc>
                        <noticeType>CV0147</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:42ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org" 
                                xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>12900</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId>F00000342</firmId>
                        <noticeDate>20191106</noticeDate>
                        <noticeDesc>WRIT NOTICE</noticeDesc>
                        <noticeType>CV0148</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:72ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org" 
                                xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>25437</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId></firmId>
                        <noticeDate>20191106</noticeDate>
                        <noticeDesc>PRE TR CNF PROCEEDING</noticeDesc>
                        <noticeType>CV0155</noticeType>
                    </notices>
                    <notices>
                        <bytes>
                            <xop:Include href="cid:62ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org" 
                                xmlns:xop="http://www.w3.org/2004/08/xop/include"/>
                        </bytes>
                        <docketNumber>
                            <docketCenturyYear>0</docketCenturyYear>
                            <docketCourtYear>18</docketCourtYear>
                            <docketSeqNum>25437</docketSeqNum>
                            <docketTypeCode>F </docketTypeCode>
                            <docketVenue>SWC</docketVenue>
                        </docketNumber>
                        <firmId></firmId>
                        <noticeDate>20191106</noticeDate>
                        <noticeDesc>TRIAL TRIAL SCHD</noticeDesc>
                        <noticeType>CV0175</noticeType>
                    </notices>
                </return>
            </ns2:searchNoticesResponse>
        </soapenv:Body>
    </soapenv:Envelope>
--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: <52ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/06/19                                                        
                                                                                                                        
 CV0147                                     DOCKET: SWC - F -012900-18                                                  
                                            TD BANK, N.A., VS LEVY WALTER M                                             
                                                                                                                        
                                                                                                                        
 A JUDGMENT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE JUDGMENT IS                             
 ALSO AVAILABLE IN THE ECOURTS CASE JACKET.                                                                             
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  RYAN A GOWER                                          
                                                                  DUANE MORRIS LLP                                      
                                                                  30 SOUTH 17TH STREET                                  
                                                                  PHILADELPHIA PA 19103                                 
                                                                                                                        

--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: <42ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org>


                                                                                                                        
 SUPERIOR COURT CLERK'S OFFICE                                                                                          
 FORECLOSURE PROCESSING SERVICES                                                                                        
 PO BOX 971                                                                                                             
 TRENTON          NJ 08625-0971                                                                                         
                                                                                                                        
 TELEPHONE: (609) 421-6100                  DATE FILED: 11/06/19                                                        
                                                                                                                        
 CV0148                                     DOCKET: SWC - F -012900-18                                                  
                                            TD BANK, N.A., VS LEVY WALTER M                                             
                                                                                                                        
                                                                                                                        
 A WRIT HAS BEEN ENTERED FOR THE ABOVE-LISTED DOCKET NUMBER.  A COPY OF THE WRIT IS ALSO                                
 AVAILABLE IN THE ECOURTS CASE JACKET.                                                                                  
                                                                                                                        
 IF YOU HAVE ANY DIFFICULTY ACCESSING THESE DOCUMENTS, PLEASE CALL THE JUDICIARY HELP DESK                              
 AT (609) 421-6100.                                                                                                     
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  RYAN A GOWER                                          
                                                                  DUANE MORRIS LLP                                      
                                                                  30 SOUTH 17TH STREET                                  
                                                                  PHILADELPHIA PA 19103                                 
                                                                                                                        

--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: <72ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org>


                                                                                                                        
                                                                                                                        
 UNION SUPERIOR COURT                                                                                                   
 UNION COUNTY SUPERIOR COURT                                                                                            
 NEW ANNEX BLDG-5TH FLOOR                                                                                               
 ELIZABETH NJ  07207                                                                                                    
                                                                                                                        
 TELEPHONE: (908) 787-1650                                   NOVEMBER 06, 2019                                          
            8:30 AM - 4:30 PM                                                                                           
 CV0155                        DOCKET: SWC - F -025437-18                                                               
                               HSBC BANK USA, NATIO VS KOURAKOS JANE                                                    
                                                                                                                        
      A PRE TRIAL CONFERENCE IS SCHEDULED FOR THIS CASE                                                                 
      ON JANUARY 08, 2020 AT 11:00AM  BEFORE JUDGE JOSEPH P PERFILIO.                                                   
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
  PLEASE REPORT TO:  COURT ROOM 5NEW                                                                                    
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                  STUART I SEIDEN                                       
                                                                   DUANE MORRIS, LLP                                    
                                                                  30 SOUTH 17TH STREET                                  
                                                                  PHILADELPHIA PA 19103-4196                            

--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250
Content-Type: application/octet-stream
Content-Transfer-Encoding: binary
Content-ID: <62ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250@apache.org>


                                                                                                                        
                                                                                                                        
 UNION SUPERIOR COURT                                                                                                   
 UNION COUNTY SUPERIOR COURT                                                                                            
 NEW ANNEX BLDG-5TH FLOOR                                                                                               
 ELIZABETH NJ  07207                                                                                                    
                                                                                                                        
 TELEPHONE: (908) 787-1650                                   NOVEMBER 06, 2019                                          
            8:30 AM - 4:30 PM                                                                                           
 CV0175                        DOCKET: SWC - F -025437-18                                                               
                               HSBC BANK USA, NATIO VS KOURAKOS JANE                                                    
                                                                                                                        
   A TRIAL IS SCHEDULED FOR THIS CASE ON JANUARY 22, 2020 AT 10:30AM                                                    
   BEFORE JUDGE JOSEPH P PERFILIO.                                                                                      
                                                                                                                        
   COURT ORDERED MEDIATION MUST BE COMPLETED BY THE DISCOVERY END DATE.  ON-GOING                                       
   MEDIATION DOES NOT PROVIDE EXCEPTIONAL CIRCUMSTANCES FOR A REQUEST FOR AN                                            
   ADJOURNMENT OF TRIAL.                                                                                                
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
                                                                                                                        
    PLEASE REPORT TO:  COURT ROOM 5NEW                                                                                  
                                                                                                                        
                                                                  STUART I SEIDEN                                       
                                                                   DUANE MORRIS, LLP                                    
                                                                  30 SOUTH 17TH STREET                                  
                                                                  PHILADELPHIA PA 19103-4196                            

--MIMEBoundary_32ba64091fe41d92190c4bdc81037d20f0e7eedbfddf1250--

```
