# Blazor5Email
A Blazor Wasm App with the following:
- Email sending
- Razor Templates for email

The goal of this is to learn email techniques.  I recognize this is all server side and really has nothing to do with Blazor, I just wanted to put it together in the context of a blazor app for practice purposes.

### SMTP Settings
Use secrets to store these without commiting
- dotnet user-secrets set "SmtpSettings:Server" "smtp.mailtrap.io"
- dotnet user-secrets set "SmtpSettings:Port" "2525"
- dotnet user-secrets set "SmtpSettings:Username" ""
- dotnet user-secrets set "SmtpSettings:Password" ""

### References
https://scottsauber.com/2018/07/07/walkthrough-creating-an-html-email-template-with-razor-and-razor-class-libraries-and-rendering-it-from-a-net-standard-class-library/
