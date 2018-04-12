using System;
using System.IO;
using System.Linq;
using System.Text;

namespace K9Abp.Rename
{
    class Program
    {
        private static readonly string[] ExcludePaths = new string[] {  "\\bin", "\\obj", "\\K9Abp.Rename" };

        static void Main(string[] args)
        {
            var path = @"D:\Projects\201712_YkAbp\abp\test";
            var old = "YkAbp";
            var replaced = "K9Abp";
            ProcessPath(path, old, replaced);
        }

        static void ProcessPath(string path, string old, string replaced)
        {
            if (ExcludePaths.Any(x => path.Contains(x))) return;

            foreach (var subPath in Directory.GetDirectories(path))
            {
                ProcessPath(subPath, old, replaced);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                ProcessFile(file, old, replaced);
                RenameFile(file, GetNewName(file, old, replaced));
            }
            RenamePath(path, GetNewName(path, old, replaced));
        }

        private static string GetNewName(string path, string old, string replaced)
        {
            var dotIndex = path.LastIndexOf('\\');
            var name = path.Substring(dotIndex);
            var newName = name.Replace(old, replaced);
            return path.Substring(0, dotIndex) + newName;
        }

        private static void RenameFile(string file, string newFile)
        {
            if (file == newFile)
            {
                return;
            }
            File.Move(file, newFile);
        }

        private static void RenamePath(string path, string newPath)
        {
            if (path == newPath)
            {
                return;
            }
            Directory.Move(path, newPath);
        }

        private static void ProcessFile(string file, string old, string replaced)
        {
            var content = string.Empty;
            using (var reader = new StreamReader(file, Encoding.Default))
            {
                content = reader.ReadToEnd();
            }
            content = content.Replace(old, replaced);
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine(content);
            }
        }
    }
}
