using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Meteo.ExtensionMethods
{
    public static class StringExtensions
    {

        public static int SumAsciiValues(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String cannot be null or empty.", nameof(str));
            }

            int sum = 0;

            foreach (char c in str)
            {
                sum += (int)c; // Convert each character to its ASCII value and add to sum  
            }

            return sum;
        }
        public static string GetGitBranchName()
        {
            try
            {
                // اجرای دستور گیت برای گرفتن نام برنچ جاری
                var startInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "rev-parse --abbrev-ref HEAD", // دستور برای گرفتن نام برنچ جاری
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(startInfo))
                using (var reader = process.StandardOutput)
                {
                    string branchName = reader.ReadToEnd().Trim(); // خواندن خروجی و حذف فاصله‌های اضافی
                    return branchName;
                }
            }
            catch (Exception ex)
            {
                //Serilog.Log.Error(ex.Message);
                return string.Empty; // اگر خطا رخ دهد، مقدار خالی برمی‌گرداند
            }
        }
        public static (string Gender, string FirstName, string LastName) ParseFullName(this string fullName)
        {
            string gender = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (string.IsNullOrWhiteSpace(fullName))
                return (gender, firstName, lastName);

            fullName = fullName.NormalizePersianText();

            // شناسایی جنسیت و حذف عبارات مرتبط
            if (fullName.StartsWith("جناب آقای"))
            {
                gender = "Male";
                fullName = fullName.Replace("جناب آقای", "").Trim();
            }
            else if (fullName.StartsWith("سرکار خانم"))
            {
                gender = "Female";
                fullName = fullName.Replace("سرکار خانم", "").Trim();
            }

            // مدیریت پیشوندهای خاص مانند سید، سیده، میر
            string[] nameParts = fullName.Split(' ');
            string[] prefixes = { "سید", "سیده", "میر", "ابن", "بنت" };

            if (nameParts.Length > 1 && prefixes.Contains(nameParts[0]))
            {
                firstName = nameParts[0] + " " + nameParts[1];
                lastName = string.Join(" ", nameParts.Skip(2));
            }
            else if (nameParts.Length >= 2)
            {
                firstName = nameParts[0];
                lastName = string.Join(" ", nameParts.Skip(1));
            }
            else
            {
                firstName = fullName; // در صورتی که نام فقط یک بخش باشد
            }

            return (gender, firstName, lastName);
        }
        public static string NormalizePersianText(this string? input,bool AlsoForHamze=false)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            // Replace Arabic ی with Persian ی
            input = input.Replace("ي", "ی");

            // Replace Arabic ك with Persian ک
            input = input.Replace("ك", "ک");
            if (AlsoForHamze)
            {
                input = input.Replace("ئ", "ی");
            }

            // Replace Arabic "هٔ" with Persian "ه‌"
            input = input.Replace("ۀ", "ه‌");

            // Remove extra spaces and normalize spaces
            input = Regex.Replace(input, @"\s+", " ");

            // Optionally, remove other unwanted characters like Zero Width Space
            input = input.Replace("\u200B", " "); // Zero Width Space

            // Return the normalized string
            return input.Trim();
        }
        // متد برای رمزگذاری رشته به Base64  
        public static string ToBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            byte[] textBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(textBytes);
        }

        // متد برای رمزگشایی Base64 به رشته  
        public static string FromBase64(this string base64Encoded)
        {
            if (string.IsNullOrEmpty(base64Encoded))
                return null;

            byte[] textBytes = Convert.FromBase64String(base64Encoded);
            return Encoding.UTF8.GetString(textBytes);
        }
        //public static string GenerateIconBase64(this string path)
        //{

        //    try
        //    {
        //        using (var icon = Icon.ExtractAssociatedIcon(path))
        //        {
        //            if (icon != null)
        //            {
        //                using (var bitmap = icon.ToBitmap())
        //                {
        //                    // تغییر اندازه  
        //                    var resizedBitmap = new Bitmap(bitmap, new Size(200, 200));
        //                    using (var ms = new MemoryStream())
        //                    {
        //                        resizedBitmap.Save(ms, ImageFormat.Png);
        //                        return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            // اگر خطایی در پردازش آیکون بود، آیکون پیش‌فرض ایجاد می‌شود  
        //            using (var defaultBitmap = new Bitmap(10, 10))
        //            {
        //                defaultBitmap.MakeTransparent();
        //                using (var ms = new MemoryStream())
        //                {
        //                    defaultBitmap.Save(ms, ImageFormat.Png);
        //                    return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        //                }
        //            }
        //        }
        //        catch { }
        //    }

        //    return null;

        //}
        public static string ToSha256Hash(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                foreach (byte b in hashBytes)
                    builder.Append(b.ToString("x2")); // تبدیل به هگزادسیمال

                return builder.ToString();
            }
        }
    //    public static string ToBarcodeBase64(this string text,
    //           BarcodeFormat format = BarcodeFormat.CODE_128,
    //           int width = 300,
    //           int height = 300,
    //           bool includeDataUri = true)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(text))
    //                throw new ArgumentNullException(nameof(text));

    //            // === گزینه‌ها ===
    //            EncodingOptions options;
    //            if (format == BarcodeFormat.QR_CODE)
    //            {
    //                options = new QrCodeEncodingOptions
    //                {
    //                    Width = width,
    //                    Height = height,
    //                    Margin = 1,
    //                    CharacterSet = "UTF-8",
    //                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
    //                };
    //            }
    //            else
    //            {
    //                var enc = new EncodingOptions
    //                {
    //                    Width = width,
    //                    Height = height,
    //                    Margin = 0,
    //                    PureBarcode = true
    //                };

    //                // در نسخه‌هایی که Hints از پیش مقداردهی شده است، فقط اضافه می‌کنیم
    //                if (!enc.Hints.ContainsKey(EncodeHintType.CHARACTER_SET))
    //                    enc.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");

    //                options = enc;
    //            }

    //            // === استفاده از PixelData writer (پرتابل و بدون نیاز به renderer) ===
    //            var pixelWriter = new BarcodeWriterPixelData
    //            {
    //                Format = format,
    //                Options = options
    //            };

    //            var pixelData = pixelWriter.Write(text);

    //            // تبدیل PixelData به Bitmap (Format32bppArgb — بسته به PixelData)
    //            using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb);
    //            var rect = new Rectangle(0, 0, pixelData.Width, pixelData.Height);
    //            var bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
    //            try
    //            {
    //                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
    //            }
    //            finally
    //            {
    //                bitmap.UnlockBits(bmpData);
    //            }

    //            using var ms = new MemoryStream();
    //            bitmap.Save(ms, ImageFormat.Png);
    //            var base64 = Convert.ToBase64String(ms.ToArray());
    //            return includeDataUri ? $"data:image/png;base64,{base64}" : base64;
    //        }
    //        catch (Exception ex)
    //        {
    //            // طبق خواست شما: در صورت خطا رشته خالی برگردانده شود
    //            return "barcode error:"+ex.Message;
    //        }
    //    }
    }
}
