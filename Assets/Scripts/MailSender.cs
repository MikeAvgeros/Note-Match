using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.EventSystems;

public class MailSender : MonoBehaviour, IPointerUpHandler
{
    public TextMeshProUGUI emailTextData;
    public TextMeshProUGUI emailResultData;

    const string kSenderEmailAddress = "mizointeractive@gmail.com";
    const string kReceiverEmailAddress = "mizointeractive@gmail.com";

    const string url = "https://mizointeractive.000webhostapp.com/emailer.php";

    public void OnPointerUp(PointerEventData eventData)
    {
        SendServerRequestForEmail(emailTextData.text);
    }

    private void SendServerRequestForEmail(string message)
    {
        StartCoroutine(SendMailRequestToServer(message));
    }

    private IEnumerator SendMailRequestToServer(string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Note Match");
        form.AddField("fromEmail", kSenderEmailAddress);
        form.AddField("message", message);
        form.AddField("toEmail", kReceiverEmailAddress);
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();
        while (!uwr.isDone)
        {
            yield return null;
        }
        if (uwr.error == null)
        {
            emailResultData.text = "Email sent successfully";
        }
        else
        {
            emailResultData.text = "Error sending the email";
        }
    }
}
