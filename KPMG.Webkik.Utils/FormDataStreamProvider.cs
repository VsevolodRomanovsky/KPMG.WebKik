using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace KPMG.Webkik.Utils
{
    public class FormDataStreamProvider : MultipartMemoryStreamProvider
    {
        private readonly Collection<bool> isFormData;

        public NameValueCollection FormData { get; }

        public Dictionary<string, byte[]> Files { get; }

        public FormDataStreamProvider()
        {
            isFormData = new Collection<bool>();
            FormData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            Files = new Dictionary<string, byte[]>();
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            if (headers.ContentDisposition == null)
            {
                throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part.");
            }

            isFormData.Add(string.IsNullOrEmpty(headers.ContentDisposition.FileName));
            return base.GetStream(parent, headers);
        }

        public override async Task ExecutePostProcessingAsync()
        {
            for (var index = 0; index < Contents.Count; index++)
            {
                var formContent = Contents[index];
                if (isFormData[index])
                {
                    // Параметр
                    var formFieldName = UnquoteToken(formContent.Headers.ContentDisposition.Name) ?? string.Empty;
                    var formFieldValue = await formContent.ReadAsStringAsync();
                    FormData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    // Файл
                    var fileName = UnquoteToken(formContent.Headers.ContentDisposition.FileName);
                    var stream = await formContent.ReadAsStreamAsync();
                    Files.Add(fileName, ReadFully(stream));
                }
            }
        }

        private static string UnquoteToken(string token)
        {
            return string.IsNullOrWhiteSpace(token)
                ? token
                : (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) &&
                   token.Length > 1
                    ? token.Substring(1, token.Length - 2)
                    : token);
        }

        private static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
