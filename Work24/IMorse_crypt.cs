namespace Work24
{
    //Define interface IMorse_crypt wicth two methods  
    interface IMorse_crypt
    {
        //crypt - to crypt the string
        string Crypt(string text);
        //decrypt - to decrypt array of strings
        string Decrypt(string[] MorseChar);
    }
}
