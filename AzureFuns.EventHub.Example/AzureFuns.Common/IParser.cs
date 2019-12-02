namespace AzureFuns.Common
{
    using System.IO;

    public interface IParser<out T>
    {
        T ReadAndParse(Stream stream);

        T Parse(string input);
    }
}
