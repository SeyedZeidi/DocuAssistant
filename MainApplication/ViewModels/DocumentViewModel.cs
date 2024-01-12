using Azure;
using Azure.AI.OpenAI;
using MainApplication.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.CustomProperties;
using System.Linq;

namespace MainApplication.ViewModels
{
    public class DocumentViewModel : ObservableObject
    {
        private List<DocumentModel>? _documents;
        private FolderSelectionViewModel _folderSelectionViewModel;
        private CategoryViewModel _categories;

        public List<DocumentModel> Documents
        {
            get { return _documents; }
            set { SetProperty(ref _documents, value); }
        }

        public ICommand LoadDocumentsCommand { get; }

        public DocumentViewModel(FolderSelectionViewModel folderSelectionViewModel, CategoryViewModel categoryViewModel)
        {
            _folderSelectionViewModel = folderSelectionViewModel;
            _categories = categoryViewModel;
        }

        private async Task<string> ReadWordFileContentAsync(string filePath)
        {
            try
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false))
                {
                    var body = wordDocument.MainDocumentPart.Document.Body;
                    var paragraphs = body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>();

                    StringBuilder contentBuilder = new StringBuilder();

                    foreach (var paragraph in paragraphs)
                    {
                        foreach (var run in paragraph.Elements<DocumentFormat.OpenXml.Wordprocessing.Run>())
                        {
                            foreach (var text in run.Elements<DocumentFormat.OpenXml.Wordprocessing.Text>())
                            {
                                contentBuilder.Append(text.Text);
                            }
                        }
                        contentBuilder.AppendLine();
                    }

                    return contentBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading Word file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        public async Task LoadDocumentsAsync(string OutputFolder)
        {
            try
            {
                // Get all files with .pdf and .docx extensions from the folder
                string[] filePaths = Directory.GetFiles(OutputFolder, "*.*", SearchOption.AllDirectories);

                // Initialize the list of documents
                List<DocumentModel> documents = new List<DocumentModel>();

                // Process each file
                foreach (string filePath in filePaths)
                {
                    // Read content from the Word file
                    string content = await ReadWordFileContentAsync(filePath);

                    // Extract metadata from the file
                    DocumentModel document = ExtractMetadata(filePath, content);

                    // Process the content through AI model to extract the category
                    document.Content = await ProcessContentThroughAIAsync(content);

                    // Add the document to the list
                    documents.Add(document);
                }

                // Set the list of documents in the view model
                Documents = documents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading documents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task<string> ReadFileContentAsync(string filePath)
        {
            try
            {
                return await File.ReadAllTextAsync(filePath);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        private DocumentModel ExtractMetadata(string filePath, string content)
        {
            // Initialize a new DocumentModel
            DocumentModel document = new DocumentModel(
                fileName: string.Empty,  
                filePath: string.Empty,  
                content: content);

            try
            {
                // Load the Word document using OpenXml
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false))
                {
                    // Extract custom properties if any
                    var customProperties = wordDocument.ExtendedFilePropertiesPart.Properties.Elements<CustomDocumentProperty>();
                    foreach (var customProperty in customProperties)
                    {
                        // Extract additional metadata based on your custom properties
                        if (customProperty.Name == "Category")
                        {
                            document.Category = customProperty.VTLPWSTR != null ? customProperty.VTLPWSTR.Text : string.Empty;
                        }
                        // Add more conditions for other custom properties
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to opening or processing the Word document
                Console.WriteLine($"Error extracting metadata from Word document: {ex.Message}");
            }

            // Set the remaining properties
            FileInfo fileInfo = new FileInfo(filePath);
            document.FileName = fileInfo.Name;
            document.FilePath = filePath;

            // Hardcoded values for testing
            document.Tags = new List<string> { "Tag1", "Tag2" };
            document.Client = "Bol.com";
            document.CaseNumber = "12345";

            return document;
        }

        private async Task<string> ProcessContentThroughAIAsync(string content)
        {
            try
            {
                var categoryNames = new List<string>();

                foreach (var category in _categories.Categories)
                {
                    categoryNames.Add(category.CategoryName);
                }

                var categoriesText = string.Join(", ", categoryNames); //List of categories


                // Create an instance of the Azure OpenAI client
                OpenAIClient client = new OpenAIClient(
                    new Uri(""),//these two are secret
                    new AzureKeyCredential(""));

                // Set up the chat message for the AI model
                var chatMessage = new ChatMessage(ChatRole.System, $"There is a text file. There are categories: {categoriesText} and I want you to categorize this file. I only want you to return the name of the category.");
                var userMessage = new ChatMessage(ChatRole.User, content);

                // Call the Azure OpenAI API for chat completions
                Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
                    "GPT35",
                    new ChatCompletionsOptions()
                    {
                        Messages = { chatMessage, userMessage },
                        Temperature = (float)0.7,
                        MaxTokens = 3800,
                        NucleusSamplingFactor = (float)0.95,
                        FrequencyPenalty = 0,
                        PresencePenalty = 0,
                    });

                // Get the response from the Azure OpenAI API
                ChatCompletions response = responseWithoutStream.Value;

                // Extract and return the generated content from the AI model
                var generatedContent = response.Choices.FirstOrDefault()?.Message?.Content;
                return generatedContent ?? string.Empty;
            }
            catch (Exception ex)
            {
                // Handle exceptions related to the Azure OpenAI API
                Console.WriteLine($"Error processing content through AI: {ex.Message}");
                return content; // Fallback to original content in case of an error
            }
        }

    }
}
