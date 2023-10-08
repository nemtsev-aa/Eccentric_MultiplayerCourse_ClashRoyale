using UnityEngine;

public class URLLibrary : MonoBehaviour {
    private const string MAIN = "http://localhost/Projects/LessionDatabase/";
    private const string AUTHORIZATION = "Authorization/authorization.php";
    private const string REGISTRATION = "Authorization/registration.php";
    private const string DECKINFO = "Game/getDeckInfo.php";
    private const string SETSELECTDECK = "Game/setSelectDeck.php";
    private const string GETRATING = "Game/getRating.php";

    private const string FROMMAIL = "";
    private const string FROMPASS = "";
 
    public static string Authorization { get { return MAIN + AUTHORIZATION; } }
    public static string Registration { get { return MAIN + REGISTRATION; } }
    public static string FromMail { get { return FROMMAIL; } }
    public static string FromPass { get { return FROMPASS; } }
    public static string GetDeckInfo { get { return MAIN + DECKINFO; } }
    public static string SetSelectDeck { get { return MAIN + SETSELECTDECK; } }
    public static string GetRating { get { return MAIN + GETRATING; } }
}
