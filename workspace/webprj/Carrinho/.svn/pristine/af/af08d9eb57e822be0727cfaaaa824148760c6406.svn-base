using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Email
    {
        /// <summary>
        /// Enviar o email.
        /// </summary>
        public void Enviar(string corpoMontado, List<Entity.Usuario> emailDoDestinatario, string assunto)
        {
            MailMessage Mensagem = new MailMessage();
            SmtpClient smtpSender;

            MailAddress From = new MailAddress("sistemashcrp@hcrp.usp.br", "Carrinho de Emergência");

            Mensagem.From = From;
            Mensagem.To.Add("lsilva@hcrp.usp.br");
            //Mensagem.To.Add("lamarques@hcrp.usp.br");

            foreach (var usuario in emailDoDestinatario)
            {
                // Validação de e-mail
                if (this.EhUmEmailValido(usuario.Email))
                    Mensagem.To.Add(usuario.Email);
            }

            Mensagem.Subject = assunto;
            Mensagem.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            Mensagem.Priority = MailPriority.High;
            Mensagem.IsBodyHtml = true;
            Mensagem.Body = corpoMontado;
            Mensagem.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            smtpSender = new SmtpClient(Parametrizacao.Instancia().smtp);

            // quando estiver em ambiente de desenvolvimento, o email será gerado em um diretório
            // local da máquina que está fazendo o debug, caso contrário utilizar o servidor de SMTP na rede.
            if (Parametrizacao.Instancia().EstaEmAmbienteDeDesenvolvimento == false)
            {
                smtpSender.DeliveryMethod = SmtpDeliveryMethod.Network;
            }
            else
            {
                smtpSender.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpSender.PickupDirectoryLocation = Parametrizacao.Instancia().CaminhoGerarEmailQuandoEmDesenvolvimento;
            }

            smtpSender.Send(Mensagem);

            // libera tudo que estiver relacionado com a mensagem, principalmente
            // os anexos.
            Mensagem.Dispose();
        }

        /// <summary>
        /// Valida se o email informado é valido.
        /// </summary>        
        public bool EhUmEmailValido(string inputEmail)
        {

            //if (string.IsNullOrEmpty(inputEmail))
            //    return false;

            //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            //@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            //@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            //Regex re = new Regex(strRegex);

            //if (re.IsMatch(inputEmail))
            //    return (true);
            //else
            //    return (false);


            return true;
        }
    }
}
