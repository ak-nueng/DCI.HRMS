using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace DCIBizPro.Util.Data
{
    public class ZipUtil
    {
        public static void Compress(List<BaseFile> files, string zipFile, int zipLevel)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream zip = new ZipOutputStream(File.Create(zipFile));
            zip.SetLevel(CheckZipLevel(zipLevel));

            foreach (BaseFile file in files)
            {
                Compress(crc, zip, file.ToString(true));
            }

            zip.Finish();
            zip.Close();
        }
        public static void Compress(List<string> files, string zipFile, int zipLevel)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream zip = new ZipOutputStream(File.Create(zipFile));
            zip.SetLevel(CheckZipLevel(zipLevel));

            foreach (string file in files)
            {
                Compress(crc, zip, file);
            }

            zip.Finish();
            zip.Close();
        }

        private static int CheckZipLevel(int zipLevel)
        {
            if (zipLevel < 1)
                zipLevel = 1;
            if (zipLevel > 9)
                zipLevel = 9;
            return zipLevel;
        }

        private static void Compress(Crc32 crc, ZipOutputStream zip, string file)
        {
            string fileName = file;
            try
            {
                string[] s = file.Split('\\');
                if (s.Length > 0)
                {
                    fileName = s[s.Length - 1];
                }
            }
            catch { }
            FileStream fs = File.OpenRead(file);

            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            ZipEntry entry = new ZipEntry(fileName);

            entry.DateTime = DateTime.Now;

            entry.Size = fs.Length;
            fs.Close();

            crc.Reset();
            crc.Update(buffer);

            entry.Crc = crc.Value;

            zip.PutNextEntry(entry);
            zip.Write(buffer, 0, buffer.Length);
        }
        public static void Compress(string []files , string zipFile , int zipLevel)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream zip = new ZipOutputStream(File.Create(zipFile));
            zip.SetLevel(CheckZipLevel(zipLevel));

            foreach (string file in files)
            {
                Compress(crc, zip, file);
            }

            zip.Finish();
            zip.Close();
        }
    }
}
