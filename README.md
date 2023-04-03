# C-Sharp Keylogger
-------------------
The C# Keylogger will create a log file in the "My Documents" folder and will continuously log keystrokes while the program is running. Every 30 minutes, it will send the keystrokes to the Discord webhook as a message. Make sure to replace "INSERT YOUR DISCORD WEBHOOK URL HERE" with the actual webhook URL you want to use. 

The C# Keylogger Uses Discord Webhook Exfiltration

Python Keylogger
----------------
The Python Keylogger will create a log file called "keylogs.txt", then it will upload the keystrokes to pastebin. Remember to replace YOUR_PASTEBIN_API_KEY_HERE with your actual Pastebin API key.

The Python Keylogger Uses Pastebin Exfiltration

Go Keylogger
------------
This code will capture keystrokes using the robotgo package and send them to the specified email every 30 minutes using the gomail package. You will need to replace the YOUR_EMAIL, YOUR_PASSWORD, and RECEIVING_EMAIL placeholders with your own email addresses and password. You'll also need to authenticate with Gmail to send emails.

The Go Keylogger Uses Gmail Exfiltration
