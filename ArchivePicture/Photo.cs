using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ArchivePicture
{
    using static F;
    public static class Photo
    {
        private static readonly int exifDateTaken = 0x0132;
        private static readonly int exifDateTimeOriginal = 0x9003;

        [Pure]
        private static Maybe<DateTime> tryParseDate(string s)
        {
            var res = DateTime.TryParseExact(
                s,
                "yyyy:MM:dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dt);

            return res ? Just(dt) : Nothing;
        }

        [Pure]
        public static Maybe<DateTime> extractDateTaken(FileInfo fi)
        {
            Maybe<string> extractExif(Image image, int exif)
            {
                if (image.PropertyIdList.ToList().Contains(exif))
                {
                    var pi = image.GetPropertyItem(exif);
                    return Just(Encoding.ASCII.GetString(pi.Value, 0, pi.Len - 1));
                }
                return Nothing;
            }

            try
            {
                using var photo = Image.FromFile(fi.FullName);

                return new List<int>() { exifDateTimeOriginal, exifDateTaken }
                .Map(exif => extractExif(photo, exif)).ToList()
                .FindAll(o => o.IsJust())
                .Map(e => e.GetOrElse(""))
                .Head()
                .Bind(tryParseDate)
                ;
            } catch(OutOfMemoryException _)
            {
                return Nothing;
            } catch(FileNotFoundException _)
            {
                return Nothing;
            }
        }
    }
}
