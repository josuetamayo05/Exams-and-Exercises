
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using filesystem;
using Microsoft.VisualBasic;

public class Exam
{
    public static IFileSystem CreateFileSystem()
    {
        return new FileSystem();
    }

    // Borre esta excepción y ponga su nombre como string, e.j.
    // Nombre => "Fulano Pérez Pérez";
    public static string Nombre => "Josue";

    // Borre esta excepción y ponga su grupo como string, e.j.
    // Grupo => "C2XX";
    public static string Grupo => "C121";
}


public class FileSystem : IFileSystem
{
    public Folder root;

    public FileSystem()
    {
        root = new Folder("/");
    }
    public IFolder GetFolder(string path)
    {
        return NavegateFolder(path);
    }

    public IFile GetFile(string path)
    {
        var (parent,name) = NavegateToParent(path);
        if()
    }
    public IFileSystem GetRoot(string path)
    {
        var folder = (Folder)GetFolder(path);
        return new FileSystem(folder);
    }

    public void Copy(string origin, string destination)
    {
        
    }
    public void Move(string origin, string destination)
    {
        
    }
    public void Delete(string path)
    {
        
    }
    
    public IEnumerable<IFile> Find(FileFilter filter)
    {
        throw new Exception();
    }

    private FileSystem(Folder newRoot)
    {
        root = newRoot;
    }

    private IFile NavegateToParent(string path)
    {
        string[] parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        IFile newF = new File(null, 0);
        foreach (string part in parts)
        {
            //if (!root.ContainsFile(part)) throw new Exception("Archivo no existente");
            newF = root.GetFile(path);
        }
        return newF;
    }
    private IFolder NavegateFolder(string path)
    {
        if (path == "/") return root;
        string[] parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        IFolder current = root;
        foreach (string part in parts)
        {
            if (current is Folder c)
            {
                if (!c.ContainsFolder(path)) throw new Exception("Carpeta no existente");
                current = c.GetFolder(path);
            }
        }
        return current;
    }

}

public class File : IFile
{
    public string Name { get; }
    public int Size { get; set; }
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

}

public class Folder : IFolder
{
    public string Name { get; }
    private Dictionary<string, IFile> files = new();
    private Dictionary<string, IFolder> folders = new();

    public Folder(string name)
    {
        Name = name;
    }
    public IFolder CreateFolder(string name)
    {
        if (folders.ContainsKey(name)) throw new Exception("Carpeta ya existente");
        var folder = new Folder(name);
        folders[name] = folder;
        return folder;
    }
    public IFile CreateFile(string name, int size)
    {
        if (files.ContainsKey(name)) throw new Exception("Archivo ya existente");
        File newFile = new File(name, size);
        files[name] = newFile;
        return newFile;

    }
    public IEnumerable<IFolder> GetFolders() => folders.Values.OrderBy(x => x.Name);
    public IEnumerable<IFile> GetFiles() => files.Values.OrderBy(f => f.Name);
    public int TotalSize()
    {
        int size = files.Values.Sum(x => x.Size);
        size += folders.Values.Sum(x => x.TotalSize());
        return size;
    }

    public bool ContainsFile(string path) => files.ContainsKey(path);
    public bool ContainsFolder(string path) => folders.ContainsKey(path);
    public IFile GetFile(string path) => files[path];
    public IFolder GetFolder(string path) => folders[path];
    
}