using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ZamodsPacker
{
    public class Manifesto
    {
        const string MANIFESTHEADER = "<DAZInstallManifest VERSION=\"0.1\">";
        const string MANIFESTFOOTER = "</DAZInstallManifest>";

        public async Task FinalizeAndWriteToFile(string path, string globalID = "REPLACE THIS WITH ID")
        {
            try
            {
                var fileDataModels = await ReadAndProcessData(path);
                string GLOBALID = $"<GlobalID VALUE=\"{globalID}\"/>";
                string finalText = MANIFESTHEADER;
                finalText += $"\n{GLOBALID}";

                foreach (var fileDataModel in fileDataModels)
                {
                    finalText += $"\n{fileDataModel}";
                }

                finalText += $"\n{MANIFESTFOOTER}";
                string finalFilePath = $"{path}\\Manifest.dsx";

                await File.WriteAllTextAsync(finalFilePath, finalText);
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to write file data models to manifest.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }
            Console.WriteLine("Done!");
            await Task.CompletedTask;
        }

        private async Task<List<FileDataModel>> ReadAndProcessData(string path)
        {
            var finalizedFileDataModelList = new List<FileDataModel>();
            try
            {
                List<string> directoriesPaths = new List<string>();
                List<string> filePaths = new List<string>();

                // Load all directories paths recursively
                await LoadDirectories(path, ref directoriesPaths);
                await LoadFiles(directoriesPaths, ref filePaths);

                List<string> convertedContentFilePaths = await ConvertFilePaths(filePaths);
                finalizedFileDataModelList = await ConvertToFileDataModels(convertedContentFilePaths);
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to load file data models to write.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            return await Task.FromResult<List<FileDataModel>>(finalizedFileDataModelList);
        }

        private async Task<List<FileDataModel>> ConvertToFileDataModels(List<string> contentFilesPath)
        {
            var outputFilesDataModels = new List<FileDataModel>();
            try
            {
                for (int i = 0; i < contentFilesPath.Count; i++)
                {
                    var fileDataModel = new FileDataModel(value: contentFilesPath[i]);
                    outputFilesDataModels.Add(fileDataModel);
                }
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to convert content file paths to data models.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            return await Task.FromResult<List<FileDataModel>>(outputFilesDataModels);
        }

        private async Task<List<string>> ConvertFilePaths(List<string> contentFilesPath)
        {
            var outputContentFilesPaths = new List<string>();
            try
            {
                foreach (var rawFilePath in contentFilesPath)
                {
                    var splittedRawFilePath = rawFilePath.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                    var isLastContent = false;
                    var processedFilePath = "";

                    foreach (var folder in splittedRawFilePath)
                    {
                        if (isLastContent)
                        {
                            processedFilePath += $"/{folder}";
                        }

                        if (folder == "Content")
                        {
                            processedFilePath += folder;
                            isLastContent = true;
                        }
                    }
                    outputContentFilesPaths.Add(processedFilePath);
                }

            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to convert content file paths.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            return await Task.FromResult<List<string>>(outputContentFilesPaths);
        }

        private Task LoadFiles(List<string> directoryPaths, ref List<string> filePaths)
        {
            foreach (var directory in directoryPaths)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    filePaths.Add(file);
                }
            }

            return Task.CompletedTask;
        }

        private Task LoadDirectories(string path, ref List<string> directoriesPaths)
        {
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                directoriesPaths.Add(directory);
                LoadDirectories(directory, ref directoriesPaths);
            }

            return Task.CompletedTask;
        }
    }
}
