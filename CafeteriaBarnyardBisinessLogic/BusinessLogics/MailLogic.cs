﻿using System;
using System.Text;

namespace CafeteriaBarnyardBisinessLogic.BusinessLogics
{
    public static class MailLogic
    {
        private static string smtpClientHost;

        private static int smtpClientPort;

        private static string mailLogin;

        private static string mailPassword;

        public static void MailConfig(MailConfig config)
        {
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
        }

        public static async void MailSendAsync(MailSendInfo info)
        {
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            using (var objMailMessage = new MailMessage())
            {
                using (var objSmtpClient = new SmtpClient(smtpClientHost, smtpClientPort))
                {
                    try
                    {
                        objMailMessage.From = new MailAddress(mailLogin);
                        objMailMessage.To.Add(new MailAddress(info.MailAddress));
                        objMailMessage.Subject = info.Subject;
                        objMailMessage.Body = info.Text;
                        objMailMessage.SubjectEncoding = Encoding.UTF8;
                        objMailMessage.BodyEncoding = Encoding.UTF8;

                        objSmtpClient.UseDefaultCredentials = false;
                        objSmtpClient.EnableSsl = true;
                        objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        objSmtpClient.Credentials = new NetworkCredential(mailLogin, mailPassword);
                        await Task.Run(() => objSmtpClient.Send(objMailMessage));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
