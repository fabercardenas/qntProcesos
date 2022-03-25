using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
    public class NCorreo
    {
        public string Envio_Correo(string mailto , string mailCopy , string body , string subject , string responderA  = "", string adjunto  = "", string adjuntoNombre  = "", string adjuntoDos  = "", string adjuntoNombreDos  = "")
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            try 
	        {
            #region MAIL
            
            mail.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationSettings.AppSettings["mailAddress"].ToString(), System.Configuration.ConfigurationSettings.AppSettings["mailDisplayName"], System.Text.Encoding.UTF8);
            mail.To.Add(mailto);
            
            if (mailCopy != "")
                mail.CC.Add(mailCopy);
            mail.Bcc.Add(System.Configuration.ConfigurationSettings.AppSettings["mailAddress"].ToString());

            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = "<html xmlns='http://www.w3.org/1999/xhtml' ><body style='background-color:White;padding:30px;'>";
            mail.Body += "<br />" + body + "<br /><br />";
        
            if (responderA == "")
                mail.Body += "<div style='font-weight:bold;'>" + System.Configuration.ConfigurationSettings.AppSettings["mailPieNoResponder"] + "</div>";
        
                mail.Body += "<br /><br /></body></html>";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
    
            if (responderA != "")
                mail.ReplyTo = new System.Net.Mail.MailAddress(responderA, "", System.Text.Encoding.UTF8);

            mail.Priority = System.Net.Mail.MailPriority.Normal;

            if (adjunto != "")
            {
            
                mail.Attachments.Add(new System.Net.Mail.Attachment(adjunto));
                mail.Attachments[0].Name = adjuntoNombre;
                if (adjuntoDos != ""){
                    mail.Attachments.Add(new System.Net.Mail.Attachment(adjuntoDos));
                    mail.Attachments[0].Name = adjuntoNombreDos;
                }
            }

            #endregion

            #region SMTP
            
            smtp.Host = System.Configuration.ConfigurationSettings.AppSettings["mailHost"];
            smtp.Port = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["mailPort"]);
            smtp.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["mailEnableSSL"]);

            smtp.UseDefaultCredentials = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["mailDefaultCredentials"]);
            smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["mailUserName"], System.Configuration.ConfigurationSettings.AppSettings["mailPass"]);
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            
            #endregion
           	        
		        smtp.Send(mail);
                return NMessaging.Success("El correo se envió satisfactoriamente");
	        }
	        catch (Exception ex)
	        {
                Negocio.NUtilidades.registraFalla("Envio Correo", ex.Message.Replace("'", "") + "->" + ((ex.InnerException!=null)?ex.InnerException.ToString().Replace("'", ""):""));
                return NMessaging.Error("No fue posible enviar el correo. Una copia con la descripción de la falla ha sido almacenada. Contacte al administrador del sistema.");
            }

        }

    }
}
