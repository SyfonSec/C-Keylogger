# Keylogger Collection

Discord Exfiltration Keylogger
-------------------
The Discord Exfiltration Keylogger will create a log file in the "C:\Windows\System32" folder and will continuously log keystrokes while the program is running. Every 30 minutes, it will send the keystrokes to the Discord webhook as a message. Make sure to replace "YOUR-WEBHOOK-URL-HERE" with the actual webhook URL you want to use. 


Dropbox Exfiltration Keylogger
----------------
The Dropbox Exfiltrartion Keylogger will create a log file under the  "C:\Windows\System32" directory, then it will upload the keystrokes to dropbox. Remember to replace "DROPBOX-API-TOKEN-HERE" with your actual Dropbox API token.

Note that this code requires the Dropbox API v2 and the Dropbox API v2 SDK for C# 

Email Exfiltration Keylogger
------------
The Email Exfiltration Keylogger will capture keystrokes using the robotgo package and send them to the specified email every 30 minutes using the gomail package. You will need to replace the YOUR_EMAIL, YOUR_PASSWORD, and RECEIVING_EMAIL placeholders with your own email addresses and password. You'll also need to authenticate with Gmail to send emails.


Run on Boot Keyloggers
----------------------
All of the "RunonBoot" keyloggers will create a new scheduled task using the Windows Task Scheduler API. The scheduled task is created to run the keylogger program on user login (or whenever you boot up your windows computer). 
