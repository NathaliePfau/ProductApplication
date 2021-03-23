using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace ProductApplication.Application.Import
{
    public class CategoryImport
    {
        public static IEnumerable<string> Import(IFormFile file)
        {
            var fileDatas = new List<string>();
            if (file.OpenReadStream() != null)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    reader.ReadLine();
                    while (reader.Peek() >= 0)
                    {
                        fileDatas.Add(reader.ReadLine());
                    }
                }
            }
            return fileDatas;
        }
    }
}
