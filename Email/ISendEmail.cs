using Email.Models;
using System.Net.Mail;

namespace Email
{
    public interface ISendEmail
    {
        public ResponseModel Gmail(EmailModel email);

        public ResponseModel Outlook(EmailModel email);
    }
}
