using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;

namespace wep
{
    internal static class Ftp
    {
        static string host = "ftp.masri.uk";
        static string username = "u519277213.dania";
        static string password = "eX5/9BpZ;FR*N6GQ";
        static string uploadPath = "/uploaded";
        static public string successMessage = "File uploaded. URL copied to clipboard.";
        static public string failMessage = "An error occoured while uploading image.";

        public static bool UploadFile(string file)
        {
            string filename = System.IO.Path.GetFileName(file);
            string upload = $"{uploadPath}/{filename}";
            using (var ftp = new FtpClient(host, username, password))
            {
                ftp.Connect();
                FtpStatus status = ftp.UploadFile(@file, upload, FtpRemoteExists.Overwrite, createRemoteDir: false);
                switch (status)
                {
                    case FtpStatus.Success:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public static bool CreateDirectory()
        {
            bool res;
            using (var conn = new FtpClient(host, username, password))
            {
                conn.Connect();
                res = conn.CreateDirectory(uploadPath);
            }
            return res;
        }

        public static List<string> GetListing()
        {

            using (var conn = new FtpClient(host, username, password))
            {
                var res = new List<string>();
                conn.Connect();

                // get a recursive listing of the files & folders in a specific folder
                foreach (var item in conn.GetListing("/", FtpListOption.Auto))
                {
                    string str = "";
                    switch (item.Type)
                    {

                        case FtpObjectType.Directory:

                            str = "Dir:  " + item.FullName + "Modified date:  " + conn.GetModifiedTime(item.FullName);

                            break;

                        case FtpObjectType.File:
                            str = "File: " + item.FullName
                                + "Modified date:  " + conn.GetModifiedTime(item.FullName)
                                + "File size:  " + conn.GetFileSize(item.FullName)
                                + "Chmod:  " + conn.GetChmod(item.FullName);

                            break;

                        case FtpObjectType.Link:
                            str = "Link: " + item.FullName;
                            break;
                    }
                    res.Add(str);
                }
                return res;
            }
        }


    }
}
