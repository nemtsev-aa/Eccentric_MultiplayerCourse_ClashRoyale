public static class PasswordGenerator {
    private static string pass = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    public static string Generate(int length) {
        var Rdpass = new System.Random();
        string password = string.Empty;
        for (int i = 0; i < length; i++) {
            password += pass[Rdpass.Next(0, pass.Length)];
        }
        return password;
    }
}
