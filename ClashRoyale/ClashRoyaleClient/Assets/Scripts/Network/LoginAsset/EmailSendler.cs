using UnityEngine;

public class EmailSendler : MonoBehaviour {
    private const string Host = "smtp.mail.ru";
    private const int Port = 587;

    string subjectLine = "Подтверждение регистрации в приложении";

    private string _toMail;
    private string _verificationCode;
    
    public void SendMessageToMail(string toMail, string verificationCode) {
        _toMail = toMail;
        _verificationCode = verificationCode;
        SendMessage();
    }

    private void SendMessage() {
        string FromMail = URLLibrary.FromMail;
        string FromPass = URLLibrary.FromPass;

        using (var smtpClient = new System.Net.Mail.SmtpClient(Host, Port)) {
            smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromPass);
            smtpClient.EnableSsl = true;

            var message = new System.Net.Mail.MailMessage(FromMail, _toMail, subjectLine, $"Проверочный код: {_verificationCode}");
            smtpClient.Send(message);
            Debug.Log("Письмо оправлено!");
        }
    }
}
