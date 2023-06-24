using System.IO;

namespace CSharpScriptOperations;

public static partial class OperationUtils
{
    public static class IO
    {
        static string _workingDirBase = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Change the working directory base throughout the application.
        /// Be default, this is the directory of the executable.
        /// </summary>
        /// <param name="path">Absolute path</param>
        public static void SetWorkingDirBase(params string[] path)
        {
            _workingDirBase = Path.Combine(path);
        }

        /// <summary>
        /// Prepare the directory tree for a path and return whether the file or directory exists.
        /// </summary>
        /// <param name="pathIsDir"></param>
        /// <param name="makeMissingDir"></param>
        /// <returns>True when the path exists as a file or directory based on pathIsDir</returns>
        public static bool EnsureValidPath(string path, bool pathIsDir, bool makeMissingDir = true) 
        {
            string dir = pathIsDir ? path : Path.GetDirectoryName(path);
            if (makeMissingDir && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Check if the file already exists
            return pathIsDir ? Directory.Exists(path) : File.Exists(path);
        }

        /// <summary>
        /// Returns a path relative to the working directory.
        /// Working directory can be changed with SetWorkingDirBase() on startup.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static string GetWorkingPath(params string[] parts)
        {
            string[] finalParts = new string[parts.Length + 1];
            finalParts[0] = _workingDirBase;
            for (int i = 0; i < parts.Length; i++)
                finalParts[i + 1] = parts[i];
            return Path.Combine(finalParts);
        }
    }
    
}
