namespace StoreApp.UI.DataAccess
{
    public interface IFileHandler
    {
        string ReadAll(string path);
        void Writeall(string path, string contents);
    }

    public class FileHandler : IFileHandler
    {
        public string ReadAll(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public void Writeall(string path, string contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }
    }
}
