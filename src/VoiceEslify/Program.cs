using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace VoiceEslify
{
    public static class Program
    {
        public static SortedDictionary<string, VoiceLine> FormList = new SortedDictionary<string, VoiceLine>();
        public static Settings config;
        public static void Main(string[] args)
        {
            config = SettingsJson.GetConfig();
            try
            {
                if (File.Exists($"{config.xEditFolder}\\VoiceEslify\\xEditOutput\\_1PreEslify.csv"))
                {
                    Console.WriteLine($"_1PreEslify.csv found.");
                    using (var reader = new StreamReader($"{config.xEditFolder}\\VoiceEslify\\xEditOutput\\_1PreEslify.csv"))
                    {
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            string[] csvArr = reader.ReadLine().Split(';');
                            if (csvArr[2].Split(":").Length >= 2 && csvArr[2].Split(":")[1] != "" && csvArr[2].Split(":")[1] != null)
                            {
                                VoiceLine voiceLine = new VoiceLine(csvArr[0], csvArr[1], csvArr[2]);
                                FormList.Add(voiceLine.EDID, voiceLine);
                            }
                            
                        }
                    }

                    if (File.Exists($"{config.xEditFolder}\\VoiceEslify\\xEditOutput\\_2PostEslify.csv"))
                    {
                        Console.WriteLine("_2PostEslify.csv found.");
                        using (var reader = new StreamReader($"{config.xEditFolder}\\VoiceEslify\\xEditOutput\\_2PostEslify.csv"))
                        {
                            reader.ReadLine();
                            while (!reader.EndOfStream)
                            {
                                string[] csvArr = reader.ReadLine().Split(';');
                                if (csvArr[2].Split(":").Length >= 2 && csvArr[2].Split(":")[1] != "" && csvArr[2].Split(":")[1] != null)
                                {
                                    VoiceLine voiceLine = new VoiceLine(csvArr[0], false, csvArr[2], csvArr[1]);
                                    FormList.GetValueOrDefault(voiceLine.EDID).IsEsl = voiceLine.IsEsl;
                                    FormList.GetValueOrDefault(voiceLine.EDID).SetFormIDPost(voiceLine.FormIDPost);
                                }
                            }
                        }
                        Console.WriteLine("_2PostEslify.csv loaded.");
                        IDictionaryEnumerator myEnumerator = FormList.GetEnumerator();
                        
                        if (config.RemoveNotCopy)
                        {
                            while (myEnumerator.MoveNext())
                            {
                                string voicePuginPath = $"{config.SkyrimDataFolder}\\Sound\\Voice\\{((VoiceLine)(myEnumerator.Value)).PluginName}";
                                if (Directory.Exists(voicePuginPath))
                                {
                                    Console.WriteLine("Plugin voice files path found");
                                    Rename(voicePuginPath, (VoiceLine)(myEnumerator.Value));
                                }
                            }
                        }
                        else
                        {
                            while (myEnumerator.MoveNext())
                            {
                                string voicePuginPath = $"{config.SkyrimDataFolder}\\Sound\\Voice\\{((VoiceLine)(myEnumerator.Value)).PluginName}";
                                if (Directory.Exists(voicePuginPath))
                                {
                                    Console.WriteLine("Plugin voice files path found");
                                    Copy(voicePuginPath, (VoiceLine)(myEnumerator.Value));
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"_2PostEslifyVoice.csv not found.");
                        Console.WriteLine($"eslifing voice line quit.");
                    }
                }
                else
                {
                    Console.WriteLine($"_1PreEslifyVoice.csv not found.");
                    Console.WriteLine($"eslifing voice line quit.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"eslifing voice line quit on error.");
            }
            Console.WriteLine("VoiceEslify done.");
            Console.WriteLine("Press enter to close. >");
            Console.ReadLine();
        }

        public static void Rename(string targetDirectory, VoiceLine voiceLine)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (Path.GetFileName(fileName).Contains(voiceLine.FormIDPre, StringComparison.OrdinalIgnoreCase))
                {
                    RenameVoiceFile(fileName, voiceLine);
                }
            }

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                Rename(subdirectory, voiceLine);
            }
        }

        public static void RenameVoiceFile(string orgFilePath, VoiceLine voiceLine)
        {
            string eslFilePath = Regex.Replace(orgFilePath, voiceLine.FormIDPre, voiceLine.FormIDPost, RegexOptions.IgnoreCase);
            if (File.Exists(orgFilePath))
            {
                try
                {
                    Console.WriteLine("\"" + orgFilePath + "\" found.");
                    File.Move(orgFilePath, eslFilePath, true);
                    Console.WriteLine("\"" + eslFilePath + "\" replaced origonal.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            else Console.WriteLine(orgFilePath + "\" not found.");
        }
        //-----------------------------------
        public static void Copy(string targetDirectory, VoiceLine voiceLine)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (Path.GetFileName(fileName).Contains(voiceLine.FormIDPre, StringComparison.OrdinalIgnoreCase))
                {
                    CopyVoiceFile(fileName, voiceLine);
                }
            }

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                Copy(subdirectory, voiceLine);
            }
        }

        public static void CopyVoiceFile(string orgFilePath, VoiceLine voiceLine)
        {
            string eslFilePath = Regex.Replace(orgFilePath, voiceLine.FormIDPre, voiceLine.FormIDPost, RegexOptions.IgnoreCase);
            try
            {
                Console.WriteLine("\"" + orgFilePath + "\" found.");
                File.Copy(orgFilePath, eslFilePath, true);
                Console.WriteLine("\"" + eslFilePath + "\" origonal copied to this.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
            
        }

    }
}
