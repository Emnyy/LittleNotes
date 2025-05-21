using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littlenotes
{
    public class Settings
    {
        public SystemSettings System { get; set; } = new SystemSettings();
        public ColorSettings Color { get; set; } = new ColorSettings();
        public ShortcutSettings Shortcuts { get; set; } = new ShortcutSettings();
    }


    public class SystemSettings
    {
        public string FolderLocation { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Notes");
        public string AutoStart { get; set; } = "false";
        public string AutoRestore { get; set; } = "false";

    }
    public class ColorSettings
    {
        public string Color { get; set; } = "#ffffd2";
        public string UseAccentColor { get; set; } = "true";
        public string UseRandomColor { get; set; } = "false";
    }
    public class ShortcutSettings
    {
        public string NoteShortcut { get; set; } = "Ctrl + Alt + N";
        public string SettingsShortcut { get; set; } = "Ctrl + Alt + C";
    }
}
