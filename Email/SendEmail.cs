using Email.Models;
using Email.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;

namespace Email
{
    public class SendEmail : ISendEmail
    {
        public ResponseModel Gmail(EmailModel email)
        {
            try
            {
                var gmailAuth = new GmailAuth().AuthAsync();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    client.Authenticate(new SaslMechanismOAuth2(gmailAuth.Result.Email, gmailAuth.Result.Token));

                    #region MESSAGE
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("GO VAGAS", "govagas@gmail.com"));
                    message.To.Add(new MailboxAddress(email.AddresseeName, email.AddresseeEmail));

                    message.Subject = email.Subject;
                    message.Body = new TextPart("plain")
                    {
                        Text = email.Message
                    };
                    #endregion

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = ex.ToString()
                };
            }

            return new ResponseModel
            {
                Success = true,
                Message = "E-mail enviado com sucesso."
            };

        }

        public ResponseModel Outlook(EmailModel email)
        {
            return null;

            //try
            //{
            //    NetworkCredential credential = new NetworkCredential("govagas@outlook.com", Settings.outlookPassword);

            //    SmtpClient client = new SmtpClient("smtp-mail.outlook.com")
            //    {
            //        Port = 587,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        EnableSsl = true,
            //        Credentials = credential
            //    };

            //    client.Send(email);
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseModel
            //    {
            //        Success = false,
            //        Message = ex.ToString()
            //    };
            //}

            //return new ResponseModel
            //{
            //    Success = true,
            //    Message = "Email enviado com sucesso."
            //};
        }
    }
}
