using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class DocumentModel
    {
        // Existing properties
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }

        // Additional properties
        public string? Category { get; set; }       // Nullable if a document may not have a category
        public List<string>? Tags { get; set; }     // Nullable if a document may not have tags
        public string? Author { get; set; }         // Nullable if the author may not be specified
        public string? Client { get; set; }         // Nullable if the client may not be specified
        public string? CaseNumber { get; set; }     // Nullable if the case number may not be specified

        public DocumentModel(
            string fileName,
            string filePath,
            string content,
            string? category = null,
            List<string>? tags = null,
            string? author = null,
            string? client = null,
            string? caseNumber = null)
        {
            FileName = fileName;
            FilePath = filePath;
            Content = content;
            Category = category;
            Tags = tags;
            Author = author;
            Client = client;
            CaseNumber = caseNumber;
        }
    }


}
